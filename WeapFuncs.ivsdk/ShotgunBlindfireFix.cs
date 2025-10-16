using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class ShotgunBlindfireFix
    {
        private static List<int> pedList = new List<int>();
        private static readonly List<eWeaponType> NotPump = new List<eWeaponType>();
        public static void Init(SettingsFile settings)
        {
            pedList.Clear();
            string weaponsString = settings.GetValue("BLINDFIRING", "ShotgunBlindfire", "");
            NotPump.Clear();
            foreach (var weaponName in weaponsString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                NotPump.Add(weaponType);
            }
        }
        public static void Tick()
        {
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (!DOES_CHAR_EXIST(pedHandle)) continue;
                if (IS_CHAR_DEAD(pedHandle)) continue;
                if (pedList.Contains(pedHandle)) continue;
                if (!IS_PED_IN_COVER(pedHandle)) continue;

                foreach (eWeaponType weaponType in NotPump)
                {
                    GET_CURRENT_CHAR_WEAPON(pedHandle, out int currentWeapon);
                    GET_AMMO_IN_CLIP(pedHandle, currentWeapon, out int pAmmo);

                    if (currentWeapon == (int)weaponType)
                        pedList.Add(pedHandle);
                }
            }

            if (pedList.Count > 0)
            {
                for (int i = 0; i < pedList.Count; i++)
                {
                    if (!DOES_CHAR_EXIST(pedList[i]) || IS_CHAR_INJURED(pedList[i]) || IS_CHAR_DEAD(pedList[i]))
                        pedList.RemoveAt(i);
                }
            }

            foreach (var vped in pedList)
            {
                GET_CURRENT_CHAR_WEAPON(vped, out int thisWeapon);
                GET_AMMO_IN_CLIP(vped, thisWeapon, out int currAmmo);

                bool dontRemoveFromList = false;
                foreach (eWeaponType weaponType in NotPump)
                {
                    if (thisWeapon == (int)weaponType)
                    {
                        dontRemoveFromList = true;
                        break;
                    }
                }
                if (!dontRemoveFromList)
                    pedList.Remove(vped);

                else if (IS_CHAR_PLAYING_ANIM(vped, "cover_l_high_corner", "shotgun_blindfire"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_high_corner", "shotgun_blindfire", out float ShotgunBF);
                    if (ShotgunBF > 0.3428 && ShotgunBF < 0.44)
                        SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_high_corner", "shotgun_blindfire", 0.7f);
                }
                else if (IS_CHAR_PLAYING_ANIM(vped, "cover_r_high_corner", "shotgun_blindfire"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_high_corner", "shotgun_blindfire", out float ShotgunBF);
                    if (ShotgunBF > 0.3676 && ShotgunBF < 0.51)
                        SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_high_corner", "shotgun_blindfire", 0.7353f);
                }
                else if (IS_CHAR_PLAYING_ANIM(vped, "cover_l_low_centre", "shotgun_blindfire"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_low_centre", "shotgun_blindfire", out float ShotgunBF);
                    if (ShotgunBF > 0.3875 && ShotgunBF < 0.51)
                        SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_low_centre", "shotgun_blindfire", 0.7f);
                }
                else if (IS_CHAR_PLAYING_ANIM(vped, "cover_r_low_centre", "shotgun_blindfire"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_low_centre", "shotgun_blindfire", out float ShotgunBF);
                    if (ShotgunBF > 0.3239 && ShotgunBF < 0.51)
                        SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_low_centre", "shotgun_blindfire", 0.676f);
                }
                else if (IS_CHAR_PLAYING_ANIM(vped, "cover_l_low_corner", "shotgun_blindfire"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_low_corner", "shotgun_blindfire", out float ShotgunBF);
                    if (ShotgunBF > 0.3589 && ShotgunBF < 0.51)
                        SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_low_corner", "shotgun_blindfire", 0.6794f);
                }
                else if (IS_CHAR_PLAYING_ANIM(vped, "cover_r_low_corner", "shotgun_blindfire"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_low_corner", "shotgun_blindfire", out float ShotgunBF);
                    if (ShotgunBF > 0.3285 && ShotgunBF < 0.51)
                        SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_low_corner", "shotgun_blindfire", 0.6857f);
                }
            }
        }
    }
}