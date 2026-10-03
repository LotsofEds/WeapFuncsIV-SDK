using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime;
using System.Security.Policy;
using System.Windows.Forms;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class BlindFireFixes
    {
        // IniShit
        private static bool consistentPistolBFLoop;
        private static bool fullAutoPistol;
        private static bool fullAutoShotgun;
        private static bool heavyRifle;
        private static bool semiAutoShotgunBF;

        // OtherShit
        private static float animPointer;
        private static readonly List<eWeaponType> Automatics = new List<eWeaponType>();
        public static void Init(SettingsFile settings)
        {
            consistentPistolBFLoop = settings.GetBoolean("BLINDFIRING", "ConsistentPistolBlindfireLoop", false);
            fullAutoPistol = settings.GetBoolean("BLINDFIRING", "FullAutoPistolBlindfire", false);
            fullAutoShotgun = settings.GetBoolean("BLINDFIRING", "FullAutoShotgunBlindfire", false);
            heavyRifle = settings.GetBoolean("BLINDFIRING", "HeavyRiflesBlindfireFix", false);
            semiAutoShotgunBF = settings.GetBoolean("BLINDFIRING", "SemiAutoShotgunBlindfire", false);

            string weaponString = settings.GetValue("BLINDFIRING", "FullAutoBlindfire", "");
            Automatics.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                Automatics.Add(weaponType);
            }

            if (semiAutoShotgunBF)
                ShotgunBlindfireFix.Init(Main.wfConfig);
        }
        public static void Tick()
        {
            if (semiAutoShotgunBF)
                ShotgunBlindfireFix.Tick();

            if (!IS_CHAR_DEAD(Main.PlayerHandle) && !IS_PED_RAGDOLL(Main.PlayerHandle) && !IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                foreach (eWeaponType weaponType in Automatics)
                {
                    if (Main.currWeap == (int)weaponType)
                    {
                        if (fullAutoShotgun)
                        {
                            if (Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack)))
                            {
                                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.255 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.2f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.305 && animPointer < 0.51)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.25f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.3 && animPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.73f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.24 && animPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.725f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.295 && animPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.25f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.275 && animPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.75f);
                                }
                            }
                            else if (Main.pAmmo < 1 || (!NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack)))
                            {
                                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.255 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.77f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.305 && animPointer < 0.51)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.78f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.3 && animPointer < 0.56)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.76f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.24 && animPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.76f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.295 && animPointer < 0.56)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.76f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out animPointer);
                                    if (animPointer > 0.275 && animPointer < 0.54)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.76f);
                                }
                            }
                        }

                        if (fullAutoPistol)
                        {
                            if (Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack)))
                            {
                                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.74f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.75f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.7f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.725f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.18 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.12f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", 0.74f);
                                }
                            }
                            if (Main.pAmmo < 1 || (!NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack)) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                            {
                                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.8f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.8f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.8f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.8f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.18 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.8f);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", out animPointer);
                                    if (animPointer > 0.15 && animPointer < 0.44)
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", 0.8f);
                                }
                            }
                        }
                    }

                    else if (Main.CurrEp == 2 && consistentPistolBFLoop && (fullAutoPistol || Main.currWeap != (int)weaponType))
                    {
                        if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.08 && animPointer < 0.12)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.24 && animPointer < 0.28 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.66f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.11 && animPointer < 0.16)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.31 && animPointer < 0.36 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.67f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.19 && animPointer < 0.22)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.31 && animPointer < 0.34)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.69f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.17 && animPointer < 0.2)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.39 && animPointer < 0.43 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.72f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.12 && animPointer < 0.16)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.32 && animPointer < 0.35 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.68f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                    }

                    else if (Main.CurrEp < 2 && consistentPistolBFLoop && (!fullAutoPistol || Main.currWeap != (int)weaponType))
                    {
                        if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.06 && animPointer < 0.11)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.23 && animPointer < 0.26 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "pistol_blindfire", 0.68f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.11 && animPointer < 0.15)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.26 && animPointer < 0.3 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "pistol_blindfire", 0.72f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.08 && animPointer < 0.12)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.18 && animPointer < 0.22)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.42f);
                                Main.Boolet = Main.pAmmo;
                            }
                            if (animPointer > 0.54 && animPointer < 0.58)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "pistol_blindfire", 0.32f);
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.06 && animPointer < 0.1)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.18 && animPointer < 0.22)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "pistol_blindfire", 0.64f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.04 && animPointer < 0.09)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.2 && animPointer < 0.24 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "pistol_blindfire", 0.6f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                        else if ((Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))) && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", out animPointer);
                            if (animPointer > 0.06 && animPointer < 0.1)
                                Main.Boolet = Main.pAmmo;

                            if (animPointer > 0.19 && animPointer < 0.23 && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "pistol_blindfire", 0.62f);
                                Main.Boolet = Main.pAmmo;
                            }
                        }
                    }
                }

                if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                {
                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out animPointer);
                        if (animPointer > 0.67 && animPointer < 0.83)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.83f);
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out animPointer);
                        if (animPointer > 0.72 && animPointer < 0.82)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.82f);
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out animPointer);
                        if (animPointer > 0.68 && animPointer < 0.79)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.79f);
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out animPointer);
                        if (animPointer > 0.7 && animPointer < 0.79)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.79f);
                    }
                }

                if (IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponFlags.HeavyWeaponUsesRifleAnims && heavyRifle)
                {
                    if ((Main.pAmmo > 0 || !IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponFlags.AnimReload) && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack)))
                    {
                        if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "rocket_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "rocket_blindfire", out animPointer);
                            if (animPointer > 0.2807 && animPointer < 0.5)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "rocket_blindfire", 0.5965f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "rocket_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "rocket_blindfire", out animPointer);
                            if (animPointer > 0.3673 && animPointer < 0.5)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "rocket_blindfire", 0.65306f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "rocket_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "rocket_blindfire", out animPointer);
                            if (animPointer > 0.3859 && animPointer < 0.5)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "rocket_blindfire", 0.5789f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "rocket_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "rocket_blindfire", out animPointer);
                            if (animPointer > 0.3333 && animPointer < 0.5)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "rocket_blindfire", 0.6078f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "rocket_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "rocket_blindfire", out animPointer);
                            if (animPointer > 0.3478 && animPointer < 0.5)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "rocket_blindfire", 0.6739f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "rocket_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "rocket_blindfire", out animPointer);
                            if (animPointer > 0.3877 && animPointer < 0.5)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "rocket_blindfire", 0.6122f);
                        }
                    }
                }
            }
        }
    }
}
