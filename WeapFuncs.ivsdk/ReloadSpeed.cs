using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using CCL.GTAIV;

namespace WeapFuncs.ivsdk
{
    internal class ReloadSpeed
    {
        public static void Tick()
        {
            SET_CHAR_ANIM_SPEED(Main.PlayerHandle, Main.WeapAnim, "reload", (Main.weapReload));
            SET_CHAR_ANIM_SPEED(Main.PlayerHandle, Main.WeapAnim, "reload_crouch", (Main.weapReload));
            SET_CHAR_ANIM_SPEED(Main.PlayerHandle, Main.WeapAnim, "p_load", (Main.weapReload));
        }
    }
}
