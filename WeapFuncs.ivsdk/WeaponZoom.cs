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

namespace WeapFuncs.ivsdk
{
    internal class WeaponZoom
    {
        private static int msWhl;
        private static float currentFOV = 1.0f;
        private static float zoomAmt;
        private static bool isAiming = (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_up") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_down"));

        public static void Init(SettingsFile settings)
        {
            //zoomAmt = settings.GetFloat("");
        }
        public static void Tick()
        {
            NativeCamera cam = NativeCamera.GetGameCam();
            if (isAiming)
            {
                GET_MOUSE_WHEEL(out msWhl);
                if (msWhl < 0)
                    zoomAmt += 0.25f;
                else if (msWhl > 0)
                    zoomAmt -= 0.25f;
            }

            if (zoomAmt < 1.0f || !isAiming)
                zoomAmt = 1.0f;

            else if (zoomAmt > 1.25f)
                zoomAmt = 1.25f;

            if (cam == null)
                return;

            //IVGame.ShowSubtitleMessage(cam.ToString() + "   " +cam.FOV.ToString() + "   " + zoomAmt.ToString());
            currentFOV = Main.SmoothStep(currentFOV, zoomAmt, 0.5f);
            cam.FOV /= currentFOV;
        }
    }
}
