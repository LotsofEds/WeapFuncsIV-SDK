using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using System.Numerics;
using CCL;
using IVSDKDotNet.Enums;
using System.Threading;
using System.Runtime;
using CCL.GTAIV;
using System;
using CCL.GTAIV.Extensions;

namespace WeapFuncs.ivsdk
{
    internal class ObjectTest
    {
        private static float wAnimTime;
        private static float pAnimTime;
        private static float pTime;
        public static void Tick()
        {
            foreach (var obj in ObjectHelper.ObjHandles)
            {
                int objHandle = obj.Value;

                GET_OBJECT_COORDINATES(objHandle, out float objX, out float objY, out float objZ);

                GET_DISTANCE_BETWEEN_COORDS_3D(Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z, objX, objY, objZ, out float Dist);
                if (Dist > 5)
                    continue;

                GET_OBJECT_MODEL(objHandle, out uint pModel);
                //IVGame.ShowSubtitleMessage(pModel.ToString());

                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@baretta", "weapon_reload", out pAnimTime);
                /*if (pModel == 3719476653)
                    SET_OBJECT_ANIM_CURRENT_TIME(objHandle, "gun@baretta", "0xE84461E7", pAnimTime);*/

                IVDynamicEntity thisObj = (IVDynamicEntity)IVDynamicEntity.FromUIntPtr((UIntPtr)obj.Key);
                //IVDynamicEntity dick = (IVDynamicEntity)thisObj;
                
                if (pModel == 3719476653)
                    GET_OBJECT_ANIM_CURRENT_TIME(objHandle, "gun@baretta", "weapon_reload", out wAnimTime);
                else if (pModel == 1846597315)
                    GET_OBJECT_ANIM_CURRENT_TIME(objHandle, "gun@shotgun", "weapon_reload", out wAnimTime);
                //GET_OBJECT_ANIM_CURRENT_TIME(objHandle, "gun@baretta", "weapon_reload", out animTime);
                //GET_OBJECT_ANIM_CURRENT_TIME(objHandle, "gun@baretta", "weapon_reload", out animTime);
                IVGame.ShowSubtitleMessage(pModel.ToString() + "  " + (wAnimTime * 100).ToString() + "  ");
                //GET_OBJECT_ANIM_CURRENT_TIME(objHandle, "gun@baretta", "weapon_fire", out float time);
            }
        }
    }
}
