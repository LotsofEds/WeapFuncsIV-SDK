using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using System.Threading;
using System.Runtime;

namespace WeapFuncs.ivsdk
{
    internal class RateOfFire
    {
        // Dont try to make the reload work with this, dumbass me
        static string WeapAnim = "";
        static string BFAnim = "";
        static float FireRate;
        static float DbFireRate;
        static float BFFireRate;

        public static void LoadWeaponConfig(int weapon)
        {
            if (Main.wConfFile.DoesSectionExists(weapon.ToString()))
            {
                WeapAnim = Main.wConfFile.GetValue(weapon.ToString(), "Anim", "");
                FireRate = Main.wConfFile.GetFloat(weapon.ToString(), "NormalROF", 1);
                DbFireRate = Main.wConfFile.GetFloat(weapon.ToString(), "DrivebyROF", 1);
                BFFireRate = Main.wConfFile.GetFloat(weapon.ToString(), "InCoverROF", 1);
                switch (IVWeaponInfo.GetWeaponInfo((uint)weapon).WeaponSlot)
                {
                    case 2:
                        if (Main.TwoHanded(weapon))
                            BFAnim = "ak47_blindfire";
                        else
                            BFAnim = "pistol_blindfire";
                        break;
                    case 3:
                        BFAnim = "shotgun_blindfire";
                        break;
                    case 4:
                        if (Main.TwoHanded(weapon))
                            BFAnim = "ak47_blindfire";
                        else
                            BFAnim = "uzi_blindfire";
                        break;
                    case 5:
                        BFAnim = "ak47_blindfire";
                        break;
                    case 6:
                        BFAnim = "rifle_blindfire";
                        break;
                    case 7:
                        BFAnim = "rocket_blindfire";
                        break;
                }
            }
        }
        public static void Tick()
        {
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (IS_CHAR_INJURED(pedHandle))
                    continue;
                if (IS_CHAR_DEAD(pedHandle))
                    continue;
                if (!IS_CHAR_SHOOTING(pedHandle))
                    continue;

                GET_CURRENT_CHAR_WEAPON(pedHandle, out int currWeap);

                LoadWeaponConfig(currWeap);

                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_crouch", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_alt", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_crouch_alt", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_up", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_down", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "dbfire", (DbFireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "dbfire_l", (DbFireRate));
                if (pedHandle != Main.PlayerHandle)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", BFAnim, (BFFireRate));
                }
            }
        }
    }
}