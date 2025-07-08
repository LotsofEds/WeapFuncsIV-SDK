using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using CCL.GTAIV;
using IVSDKDotNet.Enums;

namespace WeapFuncs.ivsdk
{
    internal class NPCSemiAutoShot
    {
        private static bool CheckDateTime;
        private static DateTime currentDateTime;
        private static int pedIndex;
        private static List<int> pedList = new List<int>();
        private static List<int> ammoList = new List<int>();
        private static int pedHandle;
        private static int Boolet;
        private static readonly List<eWeaponType> NotPump = new List<eWeaponType>();
        public static void Init(SettingsFile settings)
        {
            pedList.Clear();
            ammoList.Clear();
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
            if (CheckDateTime == false)
            {
                currentDateTime = DateTime.Now;
                CheckDateTime = true;
            }
            if (DateTime.Now.Subtract(currentDateTime).TotalMilliseconds > 100.0)
            {
                CheckDateTime = false;

                foreach (var ped in PedHelper.PedHandles)
                {
                    int pedHandle = ped.Value;
                    if (IS_CHAR_DEAD(pedHandle)) continue;
                    if (pedHandle == Main.PlayerHandle) continue;
                    if (pedList.Contains(pedHandle)) continue;
                    if (!IS_PED_IN_COVER(pedHandle)) continue;

                    foreach (eWeaponType weaponType in NotPump)
                    {
                        GET_CURRENT_CHAR_WEAPON(pedHandle, out int currentWeapon);
                        GET_AMMO_IN_CLIP(pedHandle, currentWeapon, out int pAmmo);

                        if (currentWeapon == (int)weaponType)
                        {
                            pedList.Add(pedHandle);
                            ammoList.Add(pAmmo);
                        }
                    }
                }

                if (pedList.Count > 0)
                {
                    for (int i = 0; i < pedList.Count; i++)
                    {
                        if (!DOES_CHAR_EXIST(pedList[i]) || IS_CHAR_INJURED(pedList[i]) || IS_CHAR_DEAD(pedList[i]) || pedList[i] == Main.PlayerHandle)
                        {
                            pedList.Remove(pedList[i]);
                            ammoList.Remove(ammoList[i]);
                        }
                    }
                }

                foreach (var vped in pedList)
                {
                    GET_CURRENT_CHAR_WEAPON(vped, out int thisWeapon);
                    GET_AMMO_IN_CLIP(vped, thisWeapon, out int currAmmo);
                    if (IS_CHAR_PLAYING_ANIM(vped, "cover_l_high_corner", "shotgun_blindfire"))
                    {
                        if ((ammoList[pedList.IndexOf(vped)] - currAmmo) == 1)
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_high_corner", "shotgun_blindfire", 0.67f);
                            ammoList[pedList.IndexOf(vped)] = currAmmo;
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(vped, "cover_r_high_corner", "shotgun_blindfire"))
                    {
                        if ((ammoList[pedList.IndexOf(vped)] - currAmmo) == 1)
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_high_corner", "shotgun_blindfire", 0.72f);
                            ammoList[pedList.IndexOf(vped)] = currAmmo;
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(vped, "cover_l_low_centre", "shotgun_blindfire"))
                    {
                        if ((ammoList[pedList.IndexOf(vped)] - currAmmo) == 1)
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_low_centre", "shotgun_blindfire", 0.69f);
                            ammoList[pedList.IndexOf(vped)] = currAmmo;
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(vped, "cover_r_low_centre", "shotgun_blindfire"))
                    {
                        if ((ammoList[pedList.IndexOf(vped)] - currAmmo) == 1)
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_low_centre", "shotgun_blindfire", 0.7f);
                            ammoList[pedList.IndexOf(vped)] = currAmmo;
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(vped, "cover_l_low_corner", "shotgun_blindfire"))
                    {
                        if ((ammoList[pedList.IndexOf(vped)] - currAmmo) == 1)
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_l_low_corner", "shotgun_blindfire", 0.68f);
                            ammoList[pedList.IndexOf(vped)] = currAmmo;
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(vped, "cover_r_low_corner", "shotgun_blindfire"))
                    {
                        if ((ammoList[pedList.IndexOf(vped)] - currAmmo) == 1)
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(vped, "cover_r_low_corner", "shotgun_blindfire", 0.69f);
                            ammoList[pedList.IndexOf(vped)] = currAmmo;
                        }
                    }
                    else
                        ammoList[pedList.IndexOf(vped)] = currAmmo;
                }
            }
        }
    }
}