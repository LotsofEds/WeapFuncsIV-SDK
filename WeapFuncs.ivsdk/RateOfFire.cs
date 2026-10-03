using CCL;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Threading;
using System.Windows.Forms;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class RateOfFire
    {
        // Dont try to make the reload work with this, dumbass me
        private static bool OverrideROF;

        public static void Init(SettingsFile settings)
        {
            OverrideROF = settings.GetBoolean("MAIN", "OverrideROF", false);
        }

        public static void Tick()
        {
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (!DOES_CHAR_EXIST(pedHandle)) continue;
                if (IS_CHAR_INJURED(pedHandle)) continue;
                if (IS_CHAR_DEAD(pedHandle)) continue;
                if (!IS_CHAR_SHOOTING(pedHandle)) continue;

                GET_CURRENT_CHAR_WEAPON(pedHandle, out int currWeap);

                int wIndex = Main.weaponData.FindIndex(w => w.ID == currWeap);

                if (wIndex >= 0)
                {
                    WeaponData weapData = Main.weaponData[wIndex];
                    //LoadWeaponConfig(currWeap);
                    if (OverrideROF)
                    {
                        IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate = weapData.FireRate;
                        IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate = weapData.BFFireRate;

                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire", (weapData.FireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_crouch", (weapData.FireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_alt", (weapData.FireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_crouch_alt", (weapData.FireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_up", (weapData.FireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_down", (weapData.FireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "dbfire", (weapData.DBFireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "dbfire_l", (weapData.DBFireRate));
                        if (pedHandle != Main.PlayerHandle)
                        {
                            SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", weapData.BFAnim, (weapData.BFFireRate));
                            SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", weapData.BFAnim, (weapData.BFFireRate));
                            SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", weapData.BFAnim, (weapData.BFFireRate));
                            SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", weapData.BFAnim, (weapData.BFFireRate));
                            SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", weapData.BFAnim, (weapData.BFFireRate));
                            SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", weapData.BFAnim, (weapData.BFFireRate));
                        }
                    }
                    else
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_crouch", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_alt", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_crouch_alt", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_up", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "fire_down", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "dbfire", (weapData.DBFireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, weapData.Anim, "dbfire_l", (weapData.DBFireRate));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", weapData.BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", weapData.BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", weapData.BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", weapData.BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", weapData.BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", weapData.BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                    }
                }
            }
        }
    }
}