using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using CCL.GTAIV;
using IVSDKDotNet.Enums;
using System.Runtime;

namespace WeapFuncs.ivsdk
{
    internal class ShotgunBlindfireFix
    {
        private static float ShotgunBF;
        private static readonly List<eWeaponType> NotPump = new List<eWeaponType>();
        public static void Init(SettingsFile settings)
        {
            string weaponsString = settings.GetValue("INCLUDED WEAPONS", "Shotgun Blindfire", "");
            NotPump.Clear();
            foreach (var weaponName in weaponsString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                NotPump.Add(weaponType);
            }
        }
        public static void Tick()
        {
            foreach (eWeaponType weaponType in NotPump)
            {
                if (Main.currWeap == (int)weaponType)
                {
                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.345 && ShotgunBF < 0.44)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.2f);
                        }
                        else if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.345 && ShotgunBF < 0.44)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.77f);
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.4 && ShotgunBF < 0.51)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.25f);
                        }
                        else if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.4 && ShotgunBF < 0.51)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.78f);
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF < 0.27)
                                Main.Boolet = Main.pAmmo;

                            else if (ShotgunBF > 0.33 && ShotgunBF < 0.54)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.71f);

                            else if (ShotgunBF > 0.73 && ShotgunBF < 0.76 && Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack) && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                Main.Boolet = Main.pAmmo;
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.23f);
                            }
                        }
                        else if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.33 && ShotgunBF < 0.56)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", "shotgun_blindfire", 0.76f);
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF < 0.21)
                                Main.Boolet = Main.pAmmo;

                            else if (ShotgunBF > 0.24 && ShotgunBF < 0.54)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.76f);

                            else if (ShotgunBF > 0.82 && ShotgunBF < 0.9 && Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack) && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                Main.Boolet = Main.pAmmo;
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.17f);
                            }
                        }
                        else if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.24 && ShotgunBF < 0.54)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", "shotgun_blindfire", 0.76f);
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.33 && ShotgunBF < 0.54)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.21f);
                        }
                        else if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.33 && ShotgunBF < 0.56)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.76f);
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF < 0.25)
                                Main.Boolet = Main.pAmmo;

                            else if (ShotgunBF > 0.3 && ShotgunBF < 0.54)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.69f);

                            else if (ShotgunBF > 0.76 && ShotgunBF < 0.9 && Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack) && (Main.Boolet - Main.pAmmo) == 1)
                            {
                                Main.Boolet = Main.pAmmo;
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.23f);
                            }
                        }
                        else if (Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack) || !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out ShotgunBF);
                            if (ShotgunBF > 0.3 && ShotgunBF < 0.54)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.76f);
                        }
                    }
                }
            }
        }
    }
}
