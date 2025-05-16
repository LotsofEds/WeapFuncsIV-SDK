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
using System.Linq;

namespace WeapFuncs.ivsdk
{
    internal class ShotgunRel
    {
        private static int ClipAmmo = 0;
        private static int TotalAmmo = 0;
        private static bool FirstShot = false;
        private static bool ReloadStart = false;
        private static bool LoopStart = false;
        private static bool LastShot = false;
        private static bool FullAmmoFix = false;
        private static bool OneRound = false;
        private static float shotrel;
        private static int pAmmo = 0;
        private static int aAmmo = 0;
        private static int mAmmo = 0;
        private static string wAnim = "";
        private static int bulletsFired = 0;
        private static readonly List<eWeaponType> AllRoundReloads = new List<eWeaponType>();
        private static List<float> Loop1Start = new List<float>();
        private static List<float> Loop1End = new List<float>();
        private static List<float> Loop2Start = new List<float>();
        private static List<float> Loop2End = new List<float>();
        private static List<float> PumpDuration = new List<float>();
        private static int weapIndex = 0;

        private static void GetAmmo()
        {
            GET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out pAmmo);
            GET_AMMO_IN_CHAR_WEAPON(Main.PlayerHandle, Main.currWeap, out aAmmo);
            GET_MAX_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out mAmmo);
        }
        public static void Init(SettingsFile settings)
        {
            string IncludedWeaps = settings.GetValue("INCLUDED WEAPONS", "AllRoundReloads", "");
            AllRoundReloads.Clear();
            foreach (var weaponName in IncludedWeaps.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                AllRoundReloads.Add(weaponType);
            }
            Loop1Start.Clear();
            Loop1End.Clear();
            Loop2Start.Clear();
            Loop2End.Clear();
            PumpDuration.Clear();
            string LoopStart1 = settings.GetValue("INCLUDED WEAPONS", "ReloadLoop1Start", "");
            Loop1Start = LoopStart1.Split(',').Select(float.Parse).ToList();
            string LoopEnd1 = settings.GetValue("INCLUDED WEAPONS", "ReloadLoop1End", "");
            Loop1End = LoopEnd1.Split(',').Select(float.Parse).ToList();
            string LoopStart2 = settings.GetValue("INCLUDED WEAPONS", "ReloadLoop2Start", "");
            Loop2Start = LoopStart2.Split(',').Select(float.Parse).ToList();
            string LoopEnd2 = settings.GetValue("INCLUDED WEAPONS", "ReloadLoop2End", "");
            Loop2End = LoopEnd2.Split(',').Select(float.Parse).ToList();
            string PumpDur = settings.GetValue("INCLUDED WEAPONS", "PumpDuration", "");
            PumpDuration = PumpDur.Split(',').Select(float.Parse).ToList();
        }
        public static void Tick()
        {
            foreach (eWeaponType weaponType in AllRoundReloads)
            {
                if (Main.currWeap == (int)weaponType)
                {
                    weapIndex = AllRoundReloads.IndexOf(weaponType);
                    wAnim = Main.WeapAnim;
                    GetAmmo();
                    if (!IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch"))
                    {
                        ReloadStart = false;
                        LoopStart = false;
                        if (pAmmo != mAmmo)
                        {
                            ClipAmmo = pAmmo;
                            TotalAmmo = aAmmo;
                        }
                        if (bulletsFired < GET_INT_STAT(287))
                        {
                            LastShot = true;
                            bulletsFired = GET_INT_STAT(287);
                        }
                        else if (LastShot)
                            LastShot = false;

                        if (pAmmo == 0)
                            LastShot = false;
                    }

                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload"))
                    {
                        if (bulletsFired < GET_INT_STAT(287))
                        {
                            LastShot = true;
                            bulletsFired = GET_INT_STAT(287);
                        }
                        if (pAmmo > ClipAmmo)
                        {
                            if (LastShot)
                            {
                                SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 0);
                                SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo - 1);
                                FullAmmoFix = true;
                            }
                            else
                            {
                                SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, ClipAmmo);
                                SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                            }
                        }
                        if ((pAmmo == 0 || FirstShot || LastShot) && (((aAmmo - pAmmo) > 0 && aAmmo > mAmmo) || (aAmmo > 0 && aAmmo <= mAmmo)))
                        {
                            if (!LoopStart)
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                                if (shotrel < Loop1Start[weapIndex])
                                {
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", Loop1Start[weapIndex]);
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                                    FirstShot = true;
                                }
                                if (shotrel < Loop1End[weapIndex] && shotrel >= Loop1Start[weapIndex])
                                    return;

                                if (shotrel >= Loop1End[weapIndex] && shotrel < 0.92)
                                {
                                    if (aAmmo == 1)
                                    {
                                        SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 1);
                                        SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, 1);
                                        FullAmmoFix = false;
                                    }
                                    else if (FullAmmoFix)
                                    {
                                        SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 1);
                                        SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo - 1);
                                        FullAmmoFix = false;
                                    }
                                    else
                                    {
                                        SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 1);
                                        SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                                    }
                                    GetAmmo();
                                    ClipAmmo = pAmmo;
                                    TotalAmmo = aAmmo;
                                }
                            }

                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload") && shotrel >= (Loop1End[weapIndex] - PumpDuration[weapIndex]))
                            {
                                LoopStart = true;
                                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, wAnim, "reload", (Main.weapReload * -1));
                                return;
                            }
                            LoopStart = false;
                            FirstShot = false;
                            LastShot = false;
                            if (shotrel < 0.92 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(0, GameKey.Jump) || (IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft)))))
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", 0.92f);
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                            }
                            else
                                RelShot();
                        }
                        else if (!FirstShot)
                        {
                            LastShot = false;
                            RelShot();
                        }
                    }

                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch"))
                    {
                        if (bulletsFired < GET_INT_STAT(287))
                        {
                            LastShot = true;
                            bulletsFired = GET_INT_STAT(287);
                        }
                        if (pAmmo > ClipAmmo)
                        {
                            if (LastShot)
                            {
                                SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 0);
                                SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo - 1);
                                FullAmmoFix = true;
                            }
                            else
                            {
                                SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, ClipAmmo);
                                SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                            }
                        }
                        if ((pAmmo == 0 || FirstShot || LastShot) && (((aAmmo - pAmmo) > 0 && aAmmo > mAmmo) || (aAmmo > 0 && aAmmo <= mAmmo)))
                        {
                            if (!LoopStart)
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                                if (shotrel < Loop1Start[weapIndex])
                                {
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", Loop1Start[weapIndex]);
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                                    FirstShot = true;
                                }
                                if (shotrel < Loop1End[weapIndex] && shotrel >= Loop1Start[weapIndex])
                                    return;

                                if (shotrel >= Loop1End[weapIndex] && shotrel < 0.92)
                                {
                                    if (aAmmo == 1)
                                    {
                                        SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 1);
                                        SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, 1);
                                        FullAmmoFix = false;
                                    }
                                    else if (FullAmmoFix)
                                    {
                                        SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 1);
                                        SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo - 1);
                                        FullAmmoFix = false;
                                    }
                                    else
                                    {
                                        SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 1);
                                        SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                                    }
                                    GetAmmo();
                                    ClipAmmo = pAmmo;
                                    TotalAmmo = aAmmo;
                                }
                            }

                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch") && shotrel >= (Loop1End[weapIndex] - PumpDuration[weapIndex]))
                            {
                                LoopStart = true;
                                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, wAnim, "reload_crouch", (Main.weapReload * -1));
                                return;
                            }
                            LoopStart = false;
                            FirstShot = false;
                            LastShot = false;
                            if (shotrel < 0.92 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(0, GameKey.Jump) || (IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft)))))
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", 0.92f);
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                            }
                            else
                                RelShotC();
                        }
                        else if (!FirstShot)
                        {
                            LastShot = false;
                            RelShotC();
                        }
                    }
                }
            }
        }
        private static void RelShot()
        {
            if (!LastShot && pAmmo != 0 && pAmmo != mAmmo && (aAmmo - pAmmo) > 0)
            {
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                //IVGame.ShowSubtitleMessage(shotrel.ToString() + "   " + Loop2End[weapIndex]);
                if (((shotrel > Loop2Start[weapIndex] && shotrel < 0.92) || shotrel <= 0.10) && !ReloadStart && !LoopStart)
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", Loop2Start[weapIndex]);
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= Loop2End[weapIndex] && shotrel >= Loop2Start[weapIndex] && !LoopStart)
                    return;

                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload") && shotrel < 0.92 && shotrel >= Loop2End[weapIndex] && pAmmo != mAmmo)
                {
                    SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, ClipAmmo + 1);
                    SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                    GetAmmo();
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }

                if (shotrel < 0.92 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(0, GameKey.Jump) || (IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft)))))
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", 0.92f);
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                }

                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload") && shotrel >= Loop2Start[weapIndex] && shotrel < 0.92 && pAmmo != mAmmo)
                {
                    LoopStart = true;
                    SET_CHAR_ANIM_SPEED(Main.PlayerHandle, wAnim, "reload", (Main.weapReload * -1));
                    return;
                }
                LoopStart = false;
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
            }
            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
            if (!LastShot && shotrel < 0.92 && shotrel > (Loop2End[weapIndex] + 0.1))
            {
                ReloadStart = false;
                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", 0.92f);
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
            }
        }
        private static void RelShotC()
        {
            if (!LastShot && pAmmo != 0 && pAmmo != mAmmo && (aAmmo - pAmmo) > 0)
            {
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                if (((shotrel > Loop2Start[weapIndex] && shotrel < 0.92) || shotrel <= 0.10) && !ReloadStart && !LoopStart)
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", Loop2Start[weapIndex]);
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                    ReloadStart = true;
                }

                if (shotrel <= Loop2End[weapIndex] && shotrel >= Loop2Start[weapIndex] && !LoopStart)
                    return;

                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch") && shotrel < 0.92 && shotrel >= Loop2End[weapIndex] && pAmmo != mAmmo)
                {
                    SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, ClipAmmo + 1);
                    SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                    GetAmmo();
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }

                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch") && shotrel >= Loop2Start[weapIndex] && shotrel < 0.92 && pAmmo != mAmmo)
                {
                    LoopStart = true;
                    SET_CHAR_ANIM_SPEED(Main.PlayerHandle, wAnim, "reload_crouch", (Main.weapReload * -1));
                    return;
                }
                LoopStart = false;
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                if (shotrel < 0.92 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(0, GameKey.Jump) || (IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft)))))
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", 0.92f);
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                }
            }
            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
            if (!LastShot && shotrel <= 0.92 && shotrel > (Loop2End[weapIndex] + 0.1))
            {
                ReloadStart = false;
                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", 0.92f);
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
            }
        }
    }
}
