using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class TapFireSpreadFix
    {
        private static bool enable;

        private static bool getAccuracy;
        private static bool isFiring;
        private static float defaultAccuracy;
        private static float defaultReticule;
        private static uint pWeapon;

        private static float CurrentSpread;
        private static float AppliedSpread;
        private static float AdditionalSpread;
        private static float SpreadDecayRate;
        private static float MaxSpread;

        private static List<eWeaponType> exceptionList = new List<eWeaponType>();
        public static void Init(SettingsFile settings)
        {
            exceptionList.Clear();
            enable = settings.GetBoolean("RECOIL & BULLETSPREAD", "TapFireBulletspreadFix", false);

            AdditionalSpread = settings.GetFloat("RECOIL & BULLETSPREAD", "TapFireAccuracyPenalty", 3.0f);
            SpreadDecayRate = settings.GetFloat("RECOIL & BULLETSPREAD", "AccuracyPenaltyDecayRate", 15.0f);
            MaxSpread = settings.GetFloat("RECOIL & BULLETSPREAD", "MaxPenaltyMultiplier", 17.5f);

            string weaponsString = settings.GetValue("RECOIL & BULLETSPREAD", "TapFireFixExceptions", "");
            foreach (var weaponName in weaponsString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                exceptionList.Add(weaponType);
            }
        }

        public static void Tick()
        {
            if (!enable)
                return;

            GET_FRAME_TIME(out float frameTime);

            bool tapFireException = false;
            foreach (eWeaponType weaponType in exceptionList)
            {
                if (Main.currWeap == (int)weaponType)
                    tapFireException = true;
            }
            if (pWeapon == Main.currWeap)
            {
                if (!getAccuracy)
                {
                    defaultAccuracy = IVWeaponInfo.GetWeaponInfo((uint)pWeapon).Accuracy;
                    defaultReticule = IVWeaponInfo.GetWeaponInfo((uint)pWeapon).ReticuleScale;
                    getAccuracy = true;
                }

                if (!tapFireException)
                {
                    if (NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                    {
                        IVWeaponInfo.GetWeaponInfo((uint)pWeapon).Accuracy = defaultAccuracy * AppliedSpread;
                        IVWeaponInfo.GetWeaponInfo((uint)pWeapon).ReticuleScale = defaultReticule / AppliedSpread;
                        if (!isFiring)
                        {
                            CurrentSpread = Math.Min(CurrentSpread + AdditionalSpread, MaxSpread);
                            isFiring = true;
                        }
                    }

                    else if (!NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                    {
                        AppliedSpread = CurrentSpread;
                        isFiring = false;
                    }
                }
            }
            else if (pWeapon != Main.currWeap)
            {
                if (getAccuracy)
                {
                    IVWeaponInfo.GetWeaponInfo((uint)pWeapon).Accuracy = defaultAccuracy;
                    defaultAccuracy = IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).Accuracy;

                    IVWeaponInfo.GetWeaponInfo((uint)pWeapon).ReticuleScale = defaultReticule;
                    defaultReticule = IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).ReticuleScale;
                    getAccuracy = false;
                }
                pWeapon = (uint)Main.currWeap;
            }

            AppliedSpread = Math.Max(AppliedSpread - SpreadDecayRate * frameTime, 1);
            CurrentSpread = Math.Max(CurrentSpread - SpreadDecayRate * frameTime, 1);
            //IVGame.ShowSubtitleMessage(Math.Truncate(AppliedSpread * 100).ToString() + "  " + Math.Truncate(CurrentSpread * 100).ToString() + "  " + Math.Truncate(SpreadDecayRate * frameTime * 100).ToString());
        }
    }
}
