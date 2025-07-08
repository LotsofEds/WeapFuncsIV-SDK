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
    internal class WeapFuncs
    {
        private static float AnimPointer;
        private static float ReloadTime;
        private static float AnimTime;
        private static string pWeapAnim = "";
        private static string pBFAnim = "";
        private static int weaponModel;
        private static Vector3 WeapOff;
        private static int pWeap = 0;
        public static bool FiringWeapon(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow_conv", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_big", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "br_aim_loop"));
        public static bool FiringWeaponBike(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_chop", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_chop", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_dirt", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_dirt", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_free", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_free", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_scot", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_scot", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_spt", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_spt", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_chop_s", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_chop_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_dirt_s", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_dirt_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_free_s", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_free_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_scot_s", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_scot_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_spt_s", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_spt_s", "ps_aim_loop"));
        
        public static void Tick()
        {
            pWeapAnim = Main.WeapAnim;
            pBFAnim = Main.BFAnim;
            WeapOff = Main.WeapOffset;

            if (!IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                if (Main.ReloadInVehicles)
                {
                    if ((FiringWeapon(Main.PlayerPed) || (!FiringWeapon(Main.PlayerPed) && (NativeControls.IsGameKeyPressed(0, GameKey.Reload) || NativeControls.IsGameKeyPressed(2, GameKey.Reload)))) && IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && Main.aAmmo > 0 && Main.pAmmo == 0 && Main.currWeap != 56 && Main.currWeap != 46)
                    {
                        if (!IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "reload"))
                        {
                            weaponModel = (int)IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).ModelHash;
                            if (!DOES_OBJECT_EXIST(Main.gunModel))
                                CREATE_OBJECT(weaponModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            //SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, WeapOff.X, WeapOff.Y, WeapOff.Z, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }
                    }
                    if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", out ReloadTime);
                    else
                        ReloadTime = 0;

                    if (ReloadTime > 0.9 || IS_CHAR_DEAD(Main.PlayerHandle) || pWeap != Main.currWeap || IS_PED_RAGDOLL(Main.PlayerHandle))
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
                            if (!DOES_OBJECT_EXIST(Main.gunModel))
                                CREATE_OBJECT(weaponModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            //SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, WeapOff.X, WeapOff.Y, WeapOff.Z, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }
                    }
                    if (IS_CHAR_ON_ANY_BIKE(Main.PlayerHandle))
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", out ReloadTime);
                    else
                        ReloadTime = 0;

                    if (ReloadTime > 0.9 || IS_CHAR_DEAD(Main.PlayerHandle) || pWeap != Main.currWeap || IS_PED_RAGDOLL(Main.PlayerHandle))
                    {
                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", 1.0f);
                        DELETE_OBJECT(ref Main.gunModel);
                        pWeap = Main.currWeap;
                    }
                }

                if (Main.CrouchRelFix && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@mp5k", "reload_crouch"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@mp5k", "reload_crouch", out AnimPointer);
                    if (AnimPointer > 0.6 && AnimPointer < 0.85)
                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@mp5k", "reload_crouch", 0.9f);
                }

                if (Main.SawnOffYeet)
                {
                    if (Main.pAmmo > 0 && (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack)) && !NativeControls.IsGameKeyPressed(0, GameKey.Aim) && !(Main.IsAimKeyPressedOnController() && IS_USING_CONTROLLER()))
                    {
                        if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@sawnoff", "fire"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire", out AnimPointer);
                            if (AnimPointer > 0.72 && AnimPointer < 0.88)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire", 0.7f);
                        }
                        else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@sawnoff", "fire_crouch"))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire_crouch", out AnimPointer);
                            if (AnimPointer > 0.675 && AnimPointer < 0.88)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire_crouch", 0.655f);
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
            }
        }
    }
}
