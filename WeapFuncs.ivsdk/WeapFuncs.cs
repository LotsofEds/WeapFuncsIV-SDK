﻿using System;
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
    internal class WeapFuncs
    {
        private static bool CheckDateTime;
        private static DateTime currentDateTime;
        private static float AnimPointer;
        private static bool GotAmmo;
        private static int fireType = 0;
        private static int lastAmmo;
        private static float AnimTime;
        private static bool hasPressedButton;
        private static string pWeapAnim = "";
        private static string pBFAnim = "";
        private static int soundID = -1;
        private static int weaponModel;
        private static Vector3 WeapOff;
        private static float NumOfBullets;
        private static int pWeap = 0;
        private static int pWeapon = 0;
        public static bool FiringWeapon(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow_conv", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_big", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop"));
        public static bool FiringWeaponBike(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_chop", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_chop", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_dirt", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_dirt", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_free", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_free", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_scot", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_scot", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_spt", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_spt", "ps_aim_loop"));
        private static readonly List<eWeaponType> BurstWeaps = new List<eWeaponType>();
        //private static readonly List<eWeaponType> BurstRifles = new List<eWeaponType>();
        //private static readonly List<eWeaponType> BurstPistols = new List<eWeaponType>();
        private static List<float> Loop1 = new List<float>();
        private static List<float> Loop2 = new List<float>();
        private static List<float> Loop3 = new List<float>();
        private static List<float> Loop4 = new List<float>();
        private static List<float> Loop5 = new List<float>();
        private static List<float> Loop6 = new List<float>();
        private static List<float> Loop7 = new List<float>();
        private static List<float> Loop8 = new List<float>();
        private static float accuracyTimeBurstNum;
        private static float accuracyTimeBurstMult;
        private static float accuracyTimeSemiNum;
        private static float accuracyTimeSemiMult;
        private static bool getAccTime;
        private static float defaultAccTime;
        private static int currFireType;
        private static int weapIndex;
        private static int timeBetBurst;
        private static int weapObj;
        public static void Init(SettingsFile settings)
        {
            timeBetBurst = settings.GetInteger("SELECT FIRE", "TimeBetweenShots", 250);
            accuracyTimeBurstNum = settings.GetFloat("SELECT FIRE", "BurstAccuracyTime", 0);
            accuracyTimeBurstMult = settings.GetFloat("SELECT FIRE", "BurstAccuracyMult", 8);
            accuracyTimeSemiNum = settings.GetFloat("SELECT FIRE", "SemiAutoAccuracyTime", 0);
            accuracyTimeSemiMult = settings.GetFloat("SELECT FIRE", "SemiAutoAccuracyMult", 16);
            string weaponString = settings.GetValue("SELECT FIRE", "SelectFireWeapons", "");
            BurstWeaps.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                BurstWeaps.Add(weaponType);
            }

            Loop1.Clear();
            Loop2.Clear();
            Loop3.Clear();
            Loop4.Clear();
            Loop5.Clear();
            Loop6.Clear();
            Loop7.Clear();
            Loop8.Clear();
            string wLoop1 = settings.GetValue("SELECT FIRE", "SelectFireLoopNormal", "");
            Loop1 = wLoop1.Split(',').Select(float.Parse).ToList();
            string wLoop2 = settings.GetValue("SELECT FIRE", "SelectFireLoopDriveBy", "");
            Loop2 = wLoop2.Split(',').Select(float.Parse).ToList();
            string wLoop3 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireLHigh", "");
            Loop3 = wLoop3.Split(',').Select(float.Parse).ToList();
            string wLoop4 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireLCntr", "");
            Loop4 = wLoop4.Split(',').Select(float.Parse).ToList();
            string wLoop5 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireLCrnr", "");
            Loop5 = wLoop5.Split(',').Select(float.Parse).ToList();
            string wLoop6 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireRHigh", "");
            Loop6 = wLoop6.Split(',').Select(float.Parse).ToList();
            string wLoop7 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireRCntr", "");
            Loop7 = wLoop7.Split(',').Select(float.Parse).ToList();
            string wLoop8 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireRCrnr", "");
            Loop8 = wLoop8.Split(',').Select(float.Parse).ToList();
        }
        public static void Tick()
        {
            pWeapAnim = Main.WeapAnim;
            pBFAnim = Main.BFAnim;
            WeapOff = Main.WeapOffset;

            if (!IS_CHAR_DEAD(Main.PlayerHandle) && !IS_PED_RAGDOLL(Main.PlayerHandle) && !IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                weapObj = GET_OBJECT_PED_IS_HOLDING(Main.PlayerHandle);
                if (Main.ReloadInVehicles)
                {
                    if ((FiringWeapon(Main.PlayerPed) || (!FiringWeapon(Main.PlayerPed) && (NativeControls.IsGameKeyPressed(0, GameKey.Reload) || NativeControls.IsGameKeyPressed(2, GameKey.Reload)))) && IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && Main.aAmmo > 0 && Main.pAmmo == 0 && Main.currWeap != 56 && Main.currWeap != 46)
                    {
                        if (!IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "reload"))
                        {
                            weaponModel = (int)IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).ModelHash;
                            CREATE_OBJECT(weaponModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, WeapOff.X, WeapOff.Y, WeapOff.Z, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }
                    }
                    if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", out AnimPointer);

                    if ((AnimPointer > 0.9 || IS_CHAR_DEAD(Main.PlayerHandle) || pWeap != Main.currWeap || !IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle)) && DOES_OBJECT_EXIST(Main.gunModel))
                    {
                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", 1.0f);
                        DELETE_OBJECT(ref Main.gunModel);
                        pWeap = Main.currWeap;
                    }
                }

                if (Main.ReloadOnBikes)
                {
                    if ((FiringWeaponBike(Main.PlayerPed) || (!FiringWeaponBike(Main.PlayerPed) && (NativeControls.IsGameKeyPressed(0, GameKey.Reload) || NativeControls.IsGameKeyPressed(2, GameKey.Reload)))) && IS_CHAR_ON_ANY_BIKE(Main.PlayerHandle) && Main.aAmmo > 0 && Main.pAmmo == 0 && Main.currWeap != 56 && Main.currWeap != 46)
                    {
                        if (!IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "reload"))
                        {
                            weaponModel = (int)IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).ModelHash;
                            CREATE_OBJECT(weaponModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, WeapOff.X, WeapOff.Y, WeapOff.Z, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }
                    }
                    if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", out AnimPointer);

                    if ((AnimPointer > 0.9 || IS_CHAR_DEAD(Main.PlayerHandle) || pWeap != Main.currWeap || !IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle)) && DOES_OBJECT_EXIST(Main.gunModel))
                    {
                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", 1.0f);
                        DELETE_OBJECT(ref Main.gunModel);
                        pWeap = Main.currWeap;
                    }
                }

                if (Main.CrouchRelFix && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.MP5Anim, "reload_crouch"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.MP5Anim, "reload_crouch", out AnimPointer);
                    if (AnimPointer > 0.6 && AnimPointer < 0.85)
                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.MP5Anim, "reload_crouch", 0.9f);
                }

                if (Main.SawnOffYeet)
                {
                    if (Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack)) && !NativeControls.IsGameKeyPressed(0, GameKey.Aim) && !(Main.IsAimKeyPressedOnController() && IS_USING_CONTROLLER()))
                    {
                        if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.SawnOffAnim, "fire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.SawnOffAnim, "fire", out AnimPointer);
                            if (AnimPointer > 0.72 && AnimPointer < 0.88)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.SawnOffAnim, "fire", 0.7f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.SawnOffAnim, "fire_crouch"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.SawnOffAnim, "fire_crouch", out AnimPointer);
                            if (AnimPointer > 0.675 && AnimPointer < 0.88)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.SawnOffAnim, "fire_crouch", 0.655f);
                        }
                    }
                }

                if (Main.pAmmo < 1 || (!NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack)))
                {
                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", out AnimPointer);
                        if (AnimPointer > 0.67 && AnimPointer < 0.83)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", "shotgun_blindfire", 0.83f);
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", out AnimPointer);
                        if (AnimPointer > 0.72 && AnimPointer < 0.82)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", "shotgun_blindfire", 0.82f);
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", out AnimPointer);
                        if (AnimPointer > 0.68 && AnimPointer < 0.79)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", "shotgun_blindfire", 0.79f);
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire"))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", out AnimPointer);
                        if (AnimPointer > 0.7 && AnimPointer < 0.79)
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", "shotgun_blindfire", 0.79f);
                    }
                }

                if (Main.SelectFire)
                {
                    foreach (eWeaponType weaponType in BurstWeaps)
                    {
                        if (Main.currWeap == (int)weaponType)
                        {
                            weapIndex = BurstWeaps.IndexOf(weaponType);
                            GET_MAX_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out int pMaxAmmo);
                            if ((NativeControls.IsGameKeyPressed(0, GameKey.Aim) || (Main.IsAimKeyPressedOnController() && IS_USING_CONTROLLER())) && (NativeControls.IsGameKeyPressed(0, Main.SelectFireCtrl) || NativeControls.IsGameKeyPressed(2, Main.SelectFireCtrl)) && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack) && !hasPressedButton)
                            {
                                if (fireType < 2)
                                    fireType += 1;
                                else
                                    fireType = 0;

                                hasPressedButton = true;
                                PLAY_SOUND_FRONTEND(soundID, "GENERAL_GUNS_AK47_DRY_CLICK");
                                if (Main.ShowFireModeText)
                                {
                                    if (fireType == 0)
                                        IVGame.ShowSubtitleMessage("Full-Auto");
                                    else if (fireType == 1)
                                        IVGame.ShowSubtitleMessage("Burst");
                                    else if (fireType == 2)
                                        IVGame.ShowSubtitleMessage("Semi-Auto");
                                }
                            }
                            else if (!NativeControls.IsGameKeyPressed(0, Main.SelectFireCtrl) && !NativeControls.IsGameKeyPressed(2, Main.SelectFireCtrl) && hasPressedButton)
                                hasPressedButton = false;

                            if (pWeapon == Main.currWeap)
                            {
                                if (currFireType > 0)
                                {
                                    if (currFireType == 1)
                                    {
                                        if (!getAccTime)
                                        {
                                            defaultAccTime = IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime;
                                            //IVGame.ShowSubtitleMessage(defaultAccTime.ToString());
                                            if (accuracyTimeBurstNum > 0)
                                                IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = accuracyTimeBurstNum;
                                            else
                                                IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime * accuracyTimeBurstMult;
                                            getAccTime = true;
                                        }
                                        NumOfBullets = Main.ShotsPerBurst;
                                    }

                                    else
                                    {
                                        if (!getAccTime)
                                        {
                                            defaultAccTime = IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime;
                                            //IVGame.ShowSubtitleMessage(defaultAccTime.ToString());
                                            if (accuracyTimeSemiNum > 0)
                                                IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = accuracyTimeSemiNum;
                                            else
                                                IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime * accuracyTimeSemiMult;
                                            getAccTime = true;
                                        }
                                        NumOfBullets = 1;
                                    }

                                    if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack))
                                    {
                                        if ((!GotAmmo && Main.pAmmo != 0) || Main.pAmmo == pMaxAmmo)
                                        {
                                            lastAmmo = Main.pAmmo;
                                            GotAmmo = true;

                                            CheckDateTime = false;
                                        }
                                        if ((lastAmmo - Main.pAmmo) == NumOfBullets)
                                        {
                                            //IVGame.ShowSubtitleMessage(Loop1[weapIndex].ToString());
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", Loop1[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", Loop1[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", Loop1[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", Loop1[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", Loop1[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", Loop1[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire", Loop2[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire_l", Loop2[weapIndex]);

                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, Loop3[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, Loop4[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, Loop5[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, Loop6[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, Loop7[weapIndex]);
                                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, Loop8[weapIndex]);
                                            /*foreach (eWeaponType rifleType in BurstRifles)
                                            {
                                                if (Main.currWeap == (int)rifleType)
                                                {
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", 0.88f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", 0.88f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", 0.88f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", 0.88f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", 0.88f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", 0.88f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire", 0.71f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire_l", 0.71f);

                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, 0.81f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, 0.83f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, 0.81f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, 0.8f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, 0.87f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, 0.9f);
                                                }
                                            }
                                            foreach (eWeaponType pistolType in BurstPistols)
                                            {
                                                if (Main.currWeap == (int)pistolType)
                                                {
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", 0.92f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", 0.92f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", 0.92f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", 0.92f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", 0.92f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", 0.92f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire", 0.71f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire_l", 0.71f);

                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, 0.6f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, 0.6f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, 0.6f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, 0.6f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, 0.6f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, 0.6f);
                                                }
                                            }*/

                                            if (!Main.PressToFire || IS_PED_IN_COVER(Main.PlayerHandle) || IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                                            {
                                                if (CheckDateTime == false)
                                                {
                                                    currentDateTime = DateTime.Now;
                                                    CheckDateTime = true;
                                                }
                                                if (DateTime.Now.Subtract(currentDateTime).TotalMilliseconds > timeBetBurst)
                                                {
                                                    CheckDateTime = false;

                                                    /*SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", 0.82f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", 0.82f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", 0.82f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", 0.82f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", 0.82f);
                                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", 0.82f);*/

                                                    lastAmmo = Main.pAmmo;
                                                }
                                            }
                                        }
                                    }

                                    else if (lastAmmo != Main.pAmmo)
                                    {
                                        CheckDateTime = false;
                                        lastAmmo = Main.pAmmo;
                                    }
                                }
                                if (currFireType != fireType)
                                {
                                    if (getAccTime)
                                    {
                                        IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
                                        getAccTime = false;
                                    }
                                    currFireType = fireType;
                                }
                                /*else if (getAccTime)
                                {
                                    //IVGame.ShowSubtitleMessage("piss");
                                    IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
                                    getAccTime = false;
                                }*/
                            }
                            else if (pWeapon != Main.currWeap)
                            {
                                //IVGame.ShowSubtitleMessage("piss  " + defaultAccTime.ToString());
                                if (getAccTime)
                                {
                                    IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
                                    defaultAccTime = IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).AimingAccuracyTime;
                                    getAccTime = false;
                                }
                                pWeapon = Main.currWeap;
                            }
                        }
                    }
                }
            }
        }
    }
}
