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
    internal class Silence
    {
        private static int dummyObj;
        private static int soundID = -1;
        public static void UnInit()
        {
            DELETE_OBJECT(ref dummyObj);
        }
        public static void Tick()
        {
            if (!DOES_OBJECT_EXIST(dummyObj))
            {
                //GET_GAME_CAM(out int cam);
                //GET_CAM_ROT(cam, out Vector3 cRot);
                GET_CHAR_HEADING(Main.PlayerHandle, out float pHdng);
                GET_PED_BONE_POSITION(Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, Vector3.Zero, out Vector3 pos);

                CREATE_OBJECT(GET_HASH_KEY("ec_nf_ghostball"), 0, 0, 0, out dummyObj, true);
                SET_OBJECT_HEADING(dummyObj, pHdng);
                SET_OBJECT_COORDINATES(dummyObj, pos);
                ATTACH_OBJECT_TO_PED(dummyObj, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.12f, 0f, 0f, 0, 0, 0, 0);
            }
            /*else
            {
                GET_GAME_CAM(out int cam);
                GET_CAM_ROT(cam, out Vector3 cRot);
                GET_PED_BONE_POSITION(Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, 0.12f, 0f, 0f, out Vector3 pos);
                SET_OBJECT_COORDINATES(dummyObj, pos);

                SET_OBJECT_ROTATION(Main.PlayerHandle, cRot.X, cRot.Y, cRot.Z);
            }*/
            if (Main.currWeap == 7)
            {
                //GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.WeapAnim, "fire", out float wTime);
                //if (wTime > 0.02 && wTime < 0.82)
                //{
                if (IS_CONTROL_JUST_PRESSED(0, (int)GameKey.RadarZoom))
                //if (NativeControls.IsGameKeyPressed(0, GameKey.EnterCar))
                {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.WeapAnim, "fire", 0.82f);
                    GET_OFFSET_FROM_OBJECT_IN_WORLD_COORDS(dummyObj, new Vector3(0.0f, 0.0f, -0.0f), out Vector3 pOffA);
                    GET_OFFSET_FROM_OBJECT_IN_WORLD_COORDS(dummyObj, new Vector3(0.0f, 20.0f, -0.0f), out Vector3 pOffB);
                    FIRE_SINGLE_BULLET(pOffA, pOffB, 7);
                    PLAY_SOUND_FROM_OBJECT(soundID, "SILENCED_PISTOL_FIRE", dummyObj);
                }
                /*if (wTime < 0.82)
                SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 0);
                else if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) || wTime >= 0.82)
                {
                    SET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, 15);
                }*/
                //}
            }
        }
    }
}
