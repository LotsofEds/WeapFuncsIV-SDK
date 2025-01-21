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
    internal class BlindFireFixes
    {
        private static float AnimPointer;
        private static string pWeapAnim = "";
        private static bool OneHandedIsOut(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow_conv", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_big", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop_1h"));
        private static bool TwoHandedIsOut(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop"));

        private static readonly List<eWeaponType> Automatics = new List<eWeaponType>();
        public static void Init(SettingsFile settings)
        {
            string weaponString = settings.GetValue("INCLUDED WEAPONS", "Full Auto Blindfire", "");
            Automatics.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                Automatics.Add(weaponType);
            }
        }
        public static void Tick()
        {
            if (!IS_CHAR_DEAD(Main.PlayerHandle) && !IS_PED_RAGDOLL(Main.PlayerHandle) && !IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                Main.CurrEp = GET_CURRENT_EPISODE();

                foreach (eWeaponType weaponType in Automatics)
                {
                    if (Main.currWeap == (int)weaponType)
                    {
                        if (Main.FullAutoShotgun)
                        {
                            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.255 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.2f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.255 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.77f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.305 && AnimPointer < 0.51)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.25f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.305 && AnimPointer < 0.51)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.78f);
                                }
                            }

                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.3 && AnimPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.73f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.3 && AnimPointer < 0.56)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.76f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.24 && AnimPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.725f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.24 && AnimPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.76f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.295 && AnimPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.25f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.295 && AnimPointer < 0.56)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.76f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.275 && AnimPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.75f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.275 && AnimPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.76f);
                                }
                            }
                        }

                        if (Main.FullAutoPistol)
                        {
                            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.74f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.8f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.76f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.8f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.7f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.8f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.725f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.8f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.18 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.12f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.18 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.8f);
                                }
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire"))
                            {
                                if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", 0.74f);
                                }
                                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", out AnimPointer);
                                    if (AnimPointer > 0.15 && AnimPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", 0.8f);
                                }
                            }
                        }
                    }

                    else if (Main.CurrEp == 2 && Main.ConsistentPistolBlindfireLoop && (Main.FullAutoPistol || Main.currWeap != (int)weaponType))
                    {
                        if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.08 && AnimPointer < 0.12)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.24 && AnimPointer < 0.28 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.66f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.11 && AnimPointer < 0.16)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.31 && AnimPointer < 0.36 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.67f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.19 && AnimPointer < 0.22)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.31 && AnimPointer < 0.34)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.69f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.17 && AnimPointer < 0.2)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.39 && AnimPointer < 0.43 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.72f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.12 && AnimPointer < 0.16)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.32 && AnimPointer < 0.35 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.68f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                    }

                    else if (Main.CurrEp < 2 && Main.ConsistentPistolBlindfireLoop && (!Main.FullAutoPistol || Main.currWeap != (int)weaponType))
                    {
                        if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.06 && AnimPointer < 0.11)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.23 && AnimPointer < 0.26 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.68f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.11 && AnimPointer < 0.15)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.26 && AnimPointer < 0.3 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.72f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.08 && AnimPointer < 0.12)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.18 && AnimPointer < 0.22)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.42f);
                                Main.Boolet = Main.pAmmo;
                            }
                            if (AnimPointer > 0.54 && AnimPointer < 0.58)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.32f);
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.06 && AnimPointer < 0.1)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.18 && AnimPointer < 0.22)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", 0.64f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.04 && AnimPointer < 0.09)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.2 && AnimPointer < 0.24 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.6f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack)) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out AnimPointer);
                            if (AnimPointer > 0.06 && AnimPointer < 0.1)
                                Main.Boolet = Main.pAmmo;

                            if (AnimPointer > 0.19 && AnimPointer < 0.23 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.62f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                    }
                }
            }
        }
    }
}
