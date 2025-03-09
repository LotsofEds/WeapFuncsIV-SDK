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
    internal class ShotgunRel
    {
        private static int ClipAmmo = 0;
        private static int TotalAmmo = 0;
        private static bool SpacePressed = false;
        private static bool FirstShot = false;
        private static bool ReloadStart = false;
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
        /*private static readonly List<eWeaponType> ReloadWithPump = new List<eWeaponType>();
        private static readonly List<eWeaponType> ReloadNoPump = new List<eWeaponType>();*/

        private static void GetAmmo()
        {
            GET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out pAmmo);
            GET_AMMO_IN_CHAR_WEAPON(Main.PlayerHandle, Main.currWeap, out aAmmo);
            GET_MAX_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out mAmmo);
        }
        public static void Init(SettingsFile settings)
        {
            string IncludedWeaps = settings.GetValue("INCLUDED WEAPONS", "AllRoundReloads", "");
            /*string YesPump = settings.GetValue("INCLUDED WEAPONS", "ReloadWithPump", "");
            string NoPump = settings.GetValue("INCLUDED WEAPONS", "ReloadNoPump", "");*/
            AllRoundReloads.Clear();
            foreach (var weaponName in IncludedWeaps.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                AllRoundReloads.Add(weaponType);
            }
            /*ReloadWithPump.Clear();
            foreach (var weaponName in YesPump.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                ReloadWithPump.Add(weaponType);
            }
            ReloadNoPump.Clear();
            foreach (var weaponName in NoPump.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                ReloadNoPump.Add(weaponType);
            }*/
        }
        public static void Tick()
        {
            foreach (eWeaponType weaponType in AllRoundReloads)
            {
                if (Main.currWeap == (int)weaponType)
                {
                    wAnim = Main.WeapAnim;
                    GetAmmo();
                    if (!IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch"))
                    {
                        if (pAmmo != mAmmo)
                        {
                            ClipAmmo = pAmmo;
                            TotalAmmo = aAmmo;
                        }
                        if (ClipAmmo == 1 && IS_PED_IN_COVER(Main.PlayerHandle))
                        {
                            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@shotgun", "fire"))
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@shotgun", "fire", out shotrel);
                                if (shotrel > 0.6 && shotrel < 0.64)
                                    LastShot = true;
                                else if (shotrel > 0.67 && shotrel < 0.72)
                                    LastShot = false;
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@shotgun", "fire_crouch"))
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@shotgun", "fire_crouch", out shotrel);
                                if (shotrel > 0.6 && shotrel < 0.64)
                                    LastShot = true;
                                if (shotrel > 0.67 && shotrel < 0.72)
                                    LastShot = false;
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@baretta", "fire"))
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@baretta", "fire", out shotrel);
                                if (shotrel > 0.85 && shotrel < 0.88)
                                    LastShot = true;
                                if (shotrel > 0.91 && shotrel < 0.94)
                                    LastShot = false;
                            }
                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@baretta", "fire_crouch"))
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@baretta", "fire_crouch", out shotrel);
                                if (shotrel > 0.85 && shotrel < 0.88)
                                    LastShot = true;
                                if (shotrel > 0.91 && shotrel < 0.94)
                                    LastShot = false;
                            }
                        }
                        if (pAmmo == 0)
                            LastShot = false;
                    }

                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload"))
                    {
                        if (pAmmo > ClipAmmo)
                        {
                            if (LastShot == true)
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
                        if ((pAmmo == 0 || (FirstShot) || LastShot == true) && (aAmmo - pAmmo) > 0)
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                            if (shotrel <= 0.4)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", 0.5f);
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                                FirstShot = true;
                            }
                            if (shotrel < 0.85 && shotrel >= 0.5)
                                return;

                            if (shotrel >= 0.85 && shotrel < 0.9)
                            {
                                if (FullAmmoFix)
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
                            FirstShot = false;
                            LastShot = false;
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
                        if (pAmmo > ClipAmmo)
                        {
                            if (LastShot == true && pAmmo == mAmmo)
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
                        if ((pAmmo == 0 || (FirstShot) || LastShot == true) && (aAmmo - pAmmo) > 0)
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                            if (shotrel <= 0.4)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", 0.5f);
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                                FirstShot = true;
                            }
                            if (shotrel < 0.85 && shotrel >= 0.5)
                                return;

                            if (shotrel >= 0.85 && shotrel < 0.9)
                            {
                                if (FullAmmoFix)
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
                            FirstShot = false;
                            LastShot = false;
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
            if (SpacePressed == false && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(0, GameKey.Jump) && !(IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft))) && LastShot == false && pAmmo != 0 && pAmmo != mAmmo && (aAmmo - pAmmo) > 0)
            {
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                if (((shotrel >= 0.25 && shotrel < 0.9) || shotrel <= 0.10) && ReloadStart == false)
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", 0.15f);
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= 0.38 && shotrel >= 0.15)
                    return;

                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload") && shotrel < 0.75 && shotrel > 0.15 && pAmmo != mAmmo)
                {
                    ReloadStart = false;
                    SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, ClipAmmo + 1);
                    SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                    GetAmmo();
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                if (SpacePressed == false && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && pAmmo != mAmmo && !NativeControls.IsGameKeyPressed(0, GameKey.Jump) && !(IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft))) && LastShot == false)
                {
                    ReloadStart = false;
                    return;
                }
            }
            SpacePressed = false;
            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
            if (LastShot == false && shotrel <= 0.8 && shotrel > 0.1)
            {
                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", 0.9f);
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload", out shotrel);
            }
        }
        private static void RelShotC()
        {
            if (SpacePressed == false && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(0, GameKey.Jump) && !(IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft))) && LastShot == false && pAmmo != 0 && pAmmo != mAmmo && (aAmmo - pAmmo) > 0)
            {
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                if (((shotrel >= 0.25 && shotrel < 0.9) || shotrel <= 0.10) && ReloadStart == false)
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", 0.15f);
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
                    ReloadStart = true;
                }
                if (shotrel <= 0.38 && shotrel >= 0.15)
                    return;

                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, wAnim, "reload_crouch") && shotrel < 0.75 && shotrel > 0.15 && pAmmo != mAmmo)
                {
                    ReloadStart = false;
                    SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, ClipAmmo + 1);
                    SET_CHAR_AMMO(Main.PlayerHandle, Main.currWeap, TotalAmmo);
                    GetAmmo();
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                if (pAmmo != mAmmo)
                {
                    ClipAmmo = pAmmo;
                    TotalAmmo = aAmmo;
                }
                if (SpacePressed == false && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && pAmmo != mAmmo && !NativeControls.IsGameKeyPressed(0, GameKey.Jump) && !(IS_PED_IN_COVER(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.MoveRight) || NativeControls.IsGameKeyPressed(0, GameKey.MoveLeft))) && LastShot == false)
                {
                    ReloadStart = false;
                    return;
                }
            }
            SpacePressed = false;
            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
            if (LastShot == false && shotrel <= 0.8 && shotrel > 0.1)
            {
                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", 0.9f);
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, wAnim, "reload_crouch", out shotrel);
            }
        }
    }
}
