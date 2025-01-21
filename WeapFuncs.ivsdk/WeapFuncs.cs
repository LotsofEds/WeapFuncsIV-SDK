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
    internal class WeapFuncs
    {
        private static float AnimPointer;
        private static string pWeapAnim = "";
        private static bool OneHandedIsOut(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow_conv", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_big", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop_1h"));
        private static bool TwoHandedIsOut(IVPed ped) => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop"));
        public static void Tick()
        {
            if (!IS_CHAR_DEAD(Main.PlayerHandle) && !IS_PED_RAGDOLL(Main.PlayerHandle) && !IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                if (Main.ReloadInVehicles)
                {
                    if (OneHandedIsOut(Main.PlayerPed) && IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && Main.pAmmo == 0)
                    {
                        if (Main.currWeap == 7 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@handgun", "reload"))
                        {
                            pWeapAnim = "gun@handgun";
                            CREATE_OBJECT(GET_HASH_KEY("w_glock"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.14f, 0.0f, 0.0f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 9 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@deagle", "reload"))
                        {
                            pWeapAnim = "gun@deagle";
                            CREATE_OBJECT(GET_HASH_KEY("w_eagle"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.14f, 0.0f, 0.0f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 12 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@uzi", "reload"))
                        {
                            pWeapAnim = "gun@uzi";
                            CREATE_OBJECT(GET_HASH_KEY("w_uzi"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.14f, 0.0f, 0.0f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 13 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@mp5k", "reload"))
                        {
                            pWeapAnim = "gun@mp5k";
                            CREATE_OBJECT(GET_HASH_KEY("w_mp5"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.0f, 0.0f, 0.0f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 27 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@cz75", "reload"))
                        {
                            pWeapAnim = "gun@cz75";
                            CREATE_OBJECT(GET_HASH_KEY("w_e1_cz75"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.04f, -0.02f, -0.01f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 29 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@44a", "reload"))
                        {
                            pWeapAnim = "gun@44a";
                            CREATE_OBJECT(GET_HASH_KEY("w_e2_44amag"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.0f, -0.02f, -0.02f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 32 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@p90", "reload"))
                        {
                            pWeapAnim = "gun@p90";
                            CREATE_OBJECT(GET_HASH_KEY("w_e2_p90"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.0f, 0.0f, 0.02f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 33 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@gold_uzi", "reload"))
                        {
                            pWeapAnim = "gun@gold_uzi";
                            CREATE_OBJECT(GET_HASH_KEY("w_e2_uzi"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.13f, 0.0f, 0.02f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }
                    }
                    else if (TwoHandedIsOut(Main.PlayerPed) && IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && Main.pAmmo == 0)
                    {
                        if (Main.currWeap == 14 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@ak47", "reload"))
                        {
                            pWeapAnim = "gun@ak47";
                            CREATE_OBJECT(GET_HASH_KEY("w_ak47"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.3f, 0.0f, -0.02f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 15 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@ak47", "reload"))
                        {
                            pWeapAnim = "gun@ak47";
                            CREATE_OBJECT(GET_HASH_KEY("w_m4"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.23f, 0.0f, -0.02f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }

                        else if (Main.currWeap == 34 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@m249", "reload"))
                        {
                            pWeapAnim = "gun@m249";
                            CREATE_OBJECT(GET_HASH_KEY("w_e2_m249"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out Main.gunModel, true);
                            SET_OBJECT_COLLISION(Main.gunModel, false);
                            ATTACH_OBJECT_TO_PED(Main.gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.08f, 0.0f, 0.03f, 0f, 0f, 0f, 0);
                            _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                        }
                    }

                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "reload") && IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                    {
                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", out AnimPointer);
                        if (AnimPointer > 0.9 && DOES_OBJECT_EXIST(Main.gunModel))
                        {
                            SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", 1.0f);
                            DELETE_OBJECT(ref Main.gunModel);
                        }
                    }
                }

                if (Main.CrouchRelFix && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@mp5k", "reload_crouch"))
                {
                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@mp5k", "reload_crouch", out AnimPointer);
                    if (AnimPointer > 0.6 && AnimPointer < 0.85)
                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@mp5k", "reload_crouch", 0.9f);
                }

                else if (Main.SawnOffYeet)
                {
                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@sawnoff", "fire"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(0, GameKey.Aim))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire", out AnimPointer);
                            if (AnimPointer > 0.72 && AnimPointer < 0.88)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire", 0.7f);
                        }
                    }
                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "gun@sawnoff", "fire_crouch"))
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(0, GameKey.Aim))
                        {
                            GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire_crouch", out AnimPointer);
                            if (AnimPointer > 0.675 && AnimPointer < 0.88)
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@sawnoff", "fire_crouch", 0.655f);
                        }
                    }
                }

                else if ((Main.pAmmo < 1 || !NativeControls.IsGameKeyPressed(0, GameKey.Attack)))
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
