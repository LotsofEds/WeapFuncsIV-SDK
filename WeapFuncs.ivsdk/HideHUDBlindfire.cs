using IVSDKDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class HideHUDBlindfire
    {
        public static void Tick()
        {
            bool HudIsOn = IVMenuManager.HudOn;

            if (WeaponHelper.IsPedBlindfiring(Main.PlayerHandle) && HudIsOn)
                DISPLAY_HUD(false);
            else if (!WeaponHelper.IsPedBlindfiring(Main.PlayerHandle) && HudIsOn)
                DISPLAY_HUD(true);
        }
    }
}
