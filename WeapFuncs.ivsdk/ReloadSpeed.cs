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
            SET_CHAR_ANIM_SPEED(Main.PlayerHandle, Main.wAnim, "reload", (Main.wReload));
            SET_CHAR_ANIM_SPEED(Main.PlayerHandle, Main.wAnim, "reload_crouch", (Main.wReload));
            SET_CHAR_ANIM_SPEED(Main.PlayerHandle, Main.wAnim, "p_load", (Main.wReload));
        }
    }
}
