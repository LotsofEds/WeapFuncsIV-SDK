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
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class RateOfFire
    {
        // Dont try to make the reload work with this, dumbass me
        private static bool OverridePedROF;
        static string WeapAnim = "";
        static string BFAnim = "";
        static float FireRate;
        static float DbFireRate;
        static float BFFireRate;

        public static void Init(SettingsFile settings)
        {
            OverridePedROF = settings.GetBoolean("MAIN", "OverridePedROF", false);
        }

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
                if (!DOES_CHAR_EXIST(pedHandle)) continue;
                if (IS_CHAR_INJURED(pedHandle)) continue;
                if (IS_CHAR_DEAD(pedHandle)) continue;
                if (!IS_CHAR_SHOOTING(pedHandle)) continue;

                GET_CURRENT_CHAR_WEAPON(pedHandle, out int currWeap);

                LoadWeaponConfig(currWeap);
                if (OverridePedROF)
                {
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
                else
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_crouch", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_alt", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_crouch_alt", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_up", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_down", IVWeaponInfo.GetWeaponInfo((uint)currWeap).FireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "dbfire", (DbFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "dbfire_l", (DbFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", BFAnim, IVWeaponInfo.GetWeaponInfo((uint)currWeap).BlindFireRate);
                }
            }
        }
    }
}