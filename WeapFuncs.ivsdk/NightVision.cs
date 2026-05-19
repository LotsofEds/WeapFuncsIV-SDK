using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using System;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class NightVision
    {
        private static string timecycMod;
        private static bool nvOn;

        public static void Init(SettingsFile settings)
        {
        }

        public static void Tick()
        {
            if (IS_CONTROL_JUST_PRESSED(0, (int)GameKey.SoundHorn))
            {
                nvOn = !nvOn;
                if (nvOn)
                    SET_TIMECYCLE_MODIFIER("Sketch");
                else
                    CLEAR_TIMECYCLE_MODIFIER();
            }
            //if (nvOn)
                //DRAW_RECT(0.5f, 0.5f, 1.0f, 1.0f, 0, 255, 0, 100);
        }
    }
}
