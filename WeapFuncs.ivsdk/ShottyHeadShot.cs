using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using CCL.GTAIV;
using System.Security.Policy;
using System.Numerics;

namespace WeapFuncs.ivsdk
{
    internal class ShottyHeadShot
    {
        IVPed ped;
        private static readonly List<eWeaponType> Shotties = new List<eWeaponType>();
        private static int ArmorThresh;
        public static void Init(SettingsFile settings)
        {
            string weaponsString = settings.GetValue("INCLUDED WEAPONS", "LethalShotgunHeadshots", "");
            Shotties.Clear();
            foreach (var weaponName in weaponsString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                Shotties.Add(weaponType);
            }
            ArmorThresh = settings.GetInteger("WEAPFUNCS", "ArmorThreshold", 50);
        }
        public static void Tick()
        {
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (pedHandle == Main.PlayerHandle)
                    continue;
                if (IS_CHAR_DEAD(pedHandle))
                    continue;
                foreach (eWeaponType weaponType in Shotties)
                {
                    if (!HAS_CHAR_BEEN_DAMAGED_BY_WEAPON(pedHandle, (int)weaponType))
                        continue;
                }
                if (NativeWorld.GetPedInstanceFromHandle(pedHandle).PedFlags.NoHeadshots)
                    continue;

                GET_CHAR_ARMOUR(pedHandle, out uint pArmor);
                if (pArmor > ArmorThresh)
                    continue;

                GET_CHAR_LAST_DAMAGE_BONE(pedHandle, out int pedBone);

                if (pedBone == (int)(eBone.BONE_HEAD))
                    SET_CHAR_HEALTH(pedHandle, 0);
            }
        }
    }
}
