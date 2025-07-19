using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using System.Threading;
using System.Runtime;
using System.Numerics;
using CCL.GTAIV;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;

namespace WeapFuncs.ivsdk
{
    internal class Flames
    {
        private static int flameWeapon;
        private static Vector3 flameOffset;
        private static int flameExplosion;
        private static float flameSpeed;

        private static Int32 flameFx = -1;
        private static int currClip = 0;
        private static int soundID = -1;
        private static bool FlameKeyHeldDown;
        private static bool gotAmmo;
        private static bool soundPlaying;
        private static int ObjHandle = 0;

        public static void Init(SettingsFile settings)
        {
            flameWeapon = settings.GetInteger("OTHER", "FlameWeaponID", 19);
            flameOffset = settings.GetVector3("OTHER", "FlameOffset", Vector3.Zero);
            flameExplosion = settings.GetInteger("OTHER", "FlameExplosion", 23);
            flameSpeed = settings.GetFloat("OTHER", "FlameSpeed", 8);
        }
        public static void Tick()
        {
            if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !FlameKeyHeldDown)
                FlameKeyHeldDown = true;

            else if (!NativeControls.IsGameKeyPressed(0, GameKey.Attack) && FlameKeyHeldDown)
                FlameKeyHeldDown = false;

            if (FlameKeyHeldDown == true && Main.currWeap == flameWeapon)
            {
                //IVGame.ShowSubtitleMessage(flameFx.ToString());
                GET_PED_BONE_POSITION(Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, flameOffset, out Vector3 bonePos);
                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", Main.BFAnim) || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", Main.BFAnim) || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", Main.BFAnim) || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", Main.BFAnim) || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", Main.BFAnim) || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", Main.BFAnim) || IS_PED_RAGDOLL(Main.PlayerHandle))
                {
                    if (soundID == -1)
                        soundID = GET_SOUND_ID();

                    if (currClip > Main.pAmmo)
                    {
                        if (flameFx == -1)
                        {
                            STOP_PTFX(flameFx);
                            REMOVE_PTFX(flameFx);
                            flameFx = START_PTFX_ON_PED_BONE("shot_directed_flame", Main.PlayerHandle, flameOffset.X, flameOffset.Y, flameOffset.Z, 0.0f, 90.0f, 0, 0x4D0, 1.0f);
                        }
                        
                        if (ObjHandle == 0)
                            CREATE_OBJECT(GET_HASH_KEY("bm_cluckin_burg"), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z, out ObjHandle, true);
                        else if (ObjHandle != 0)
                        {
                            SET_OBJECT_VISIBLE(ObjHandle, false);
                            ATTACH_OBJECT_TO_PED(ObjHandle, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, flameOffset.X, flameOffset.Y, flameOffset.Z, 0f, 0f, 0f, 0);
                            SET_OBJECT_COLLISION(ObjHandle, true);
                            SET_OBJECT_RECORDS_COLLISIONS(ObjHandle, true);
                            EXTINGUISH_OBJECT_FIRE(ObjHandle);

                            if (!soundPlaying)
                            {
                                PLAY_SOUND_FROM_POSITION(soundID, "FIRE_GAS_BURNER", bonePos.X, bonePos.Y, bonePos.Z);
                                soundPlaying = true;
                            }

                            gotAmmo = false;
                        }
                    }
                    else if (!gotAmmo)
                    {
                        if (ObjHandle != 0)
                        {
                            GET_OBJECT_COORDINATES(ObjHandle, out Vector3 ObjPos);
                            DETACH_OBJECT(ObjHandle, false);
                            GET_OBJECT_SPEED(ObjHandle, out float objSpd);
                            if (objSpd <= 0)
                                APPLY_FORCE_TO_OBJECT(ObjHandle, 3u, new Vector3(flameSpeed, 0, 0), new Vector3(0, 0, 0), 0, 1, 1, 1);
                            GET_DISTANCE_BETWEEN_COORDS_3D(Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z, ObjPos.X, ObjPos.Y, ObjPos.Z, out float Dist);
                            GET_OFFSET_FROM_OBJECT_IN_WORLD_COORDS(ObjHandle, new Vector3(0.0f, 0.1f, 0.0f), out Vector3 clsOff);
                            if ((HAS_OBJECT_COLLIDED_WITH_ANYTHING(ObjHandle) || (Dist > 4)) && DOES_OBJECT_EXIST(ObjHandle))
                            {
                                ADD_EXPLOSION(clsOff.X, clsOff.Y, clsOff.Z, flameExplosion, 1.0f, false, true, 0.0f);
                                SET_OBJECT_RECORDS_COLLISIONS(ObjHandle, false);
                                MARK_OBJECT_AS_NO_LONGER_NEEDED(ObjHandle);
                                DELETE_OBJECT(ref ObjHandle);
                            }
                        }
                        currClip = Main.pAmmo;
                    }
                }
            }
            else if (FlameKeyHeldDown == false || Main.currWeap != flameWeapon)
            {
                //IVGame.ShowSubtitleMessage(flameFx.ToString());
                STOP_PTFX(flameFx);
                REMOVE_PTFX(flameFx);
                if (soundPlaying)
                {
                    PLAY_SOUND_FROM_PED(soundID, "PAYPHONE_PICK_UP_A", Main.PlayerHandle);
                    soundPlaying = false;
                }
                RELEASE_SOUND_ID(soundID);
                soundID = -1;
                flameFx = -1;
                if (ObjHandle != 0)
                {
                    MARK_OBJECT_AS_NO_LONGER_NEEDED(ObjHandle);
                    DELETE_OBJECT(ref ObjHandle);
                }
            }
        }
    }
}
