using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;
using System.Numerics;

namespace WeapFuncs.ivsdk
{
    internal class ShoulderSwap
    {
        private static bool swapSide = false;
        private static int blockObj;
        //private static Vector3 offset;
        public static void UnInit()
        {
            if (DOES_OBJECT_EXIST(blockObj))
                DELETE_OBJECT(ref blockObj);

            blockObj = -1;
        }
        public static void IngameStart()
        {
            UnInit();
        }
        public static void Tick()
        {
            if (IS_CONTROL_JUST_PRESSED(0, (int)eGameKey.GAME_KEY_ACTION) && WeaponHelper.IsAimingAnimPlaying() && WeaponHelper.IsPressingAimButton())
            {
                swapSide = !swapSide;
            }
            REQUEST_MODEL(GET_HASH_KEY("cj_bowling_pin"));
            //REQUEST_MODEL(GET_HASH_KEY("cj_game_cube_1"));
            ProcessCam();
        }
        public static void ProcessCam()
        {
            if (swapSide)
            {
                //IVGame.ShowSubtitleMessage("swapped", 100);
                //GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS(Main.PlayerHandle, new Vector3(-0.45f, -0.7f, 0.6f), out Vector3 offset);

                if (!DOES_OBJECT_EXIST(blockObj))
                {
                    CREATE_OBJECT(GET_HASH_KEY("cj_bowling_pin"), Vector3.Zero, out blockObj, true);
                    ATTACH_OBJECT_TO_PED(blockObj, Main.PlayerHandle, (int)eBone.BONE_ROOT, new Vector3(0.45f, -0.1f, 0.4f), Vector3.Zero, 1);
                    SET_OBJECT_DYNAMIC(blockObj, false);
                    SET_OBJECT_COLLISION(blockObj, true);
                }

                //NativeCamera.GetGameCam().Position = offset;
            }
            else if (DOES_OBJECT_EXIST(blockObj))
            {
                DELETE_OBJECT(ref blockObj);
                blockObj = -1;
            }
        }
    }
}
