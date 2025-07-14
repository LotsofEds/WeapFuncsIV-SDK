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
        public static void Tick()
        {
            if (Main.currWeap == 7)
            {
                //GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.WeapAnim, "fire", out float wTime);
                //if (wTime > 0.02 && wTime < 0.82)
                //{
                    if (IS_CONTROL_JUST_PRESSED(0, (int)GameKey.RadarZoom))
                    //if (NativeControls.IsGameKeyPressed(0, GameKey.EnterCar))
                    {
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.WeapAnim, "fire", 0.82f);
                    //GET_CAM_ROT();
                    GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS(Main.PlayerHandle, new Vector3(0.5f, 0.0f, 0.0f), out Vector3 pOffA);
                    GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS(Main.PlayerHandle, new Vector3(0.5f, 20.0f, 0.0f), out Vector3 pOffB);
                        FIRE_SINGLE_BULLET(pOffA, pOffB, 7);
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
