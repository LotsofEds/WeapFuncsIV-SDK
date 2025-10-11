using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Windows.Forms;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class Flashbang
    {
        private static float camIntensity;
        private static float blindRad;
        private static int ragdollDuration;
        private static int expID;
        private static int weapID;

        private static int alpha = 255;
        private static uint fTimer;
        private static bool faceExplosion = false;
        public static void Init(SettingsFile settings)
        {
            expID = settings.GetInteger("OTHER", "StunExplosionID", 0);
            weapID = settings.GetInteger("OTHER", "StunWeaponID", 0);
            camIntensity = settings.GetFloat("OTHER", "CamShakeIntensity", 0);
            blindRad = settings.GetFloat("OTHER", "FlashRadius", 0);
            ragdollDuration = settings.GetInteger("OTHER", "StunDuration", 0);
        }
        public static void Tick()
        {
            GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS(Main.PlayerHandle, new Vector3(0, blindRad, 0), out Vector3 plyrPos);
            if (IS_EXPLOSION_IN_SPHERE(expID, plyrPos, blindRad))
            {
                alpha = 255;
                GET_GAME_TIMER(out fTimer);
                faceExplosion = true;
            }
            if (faceExplosion)
            {
                GET_GAME_TIMER(out uint gTimer);
                if (IS_HUD_PREFERENCE_SWITCHED_ON() && gTimer > 0 && gTimer <= (fTimer + 5000))
                {
                    if (gTimer > (fTimer + 2000))
                        alpha -= ((int)(Main.frameTime * 120f));
                    else
                        alpha = 255;

                    if (alpha <= 0)
                        alpha = 0;

                    DRAW_RECT(0.5f, 0.5f, 1, 1, 255, 255, 255, alpha);
                }
                else
                    faceExplosion = false;
            }
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (HAS_CHAR_BEEN_DAMAGED_BY_WEAPON(pedHandle, weapID))
                {
                    if (!IS_PED_RAGDOLL(pedHandle))
                    {
                        SWITCH_PED_TO_RAGDOLL_WITH_FALL(pedHandle, ragdollDuration, ragdollDuration, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                        if (pedHandle == Main.PlayerHandle)
                        {
                            GET_GAME_CAM(out int cam);
                            SET_DRUNK_CAM(cam, camIntensity, (ragdollDuration + 1000));
                        }
                    }
                    if (IS_PED_RAGDOLL(pedHandle))
                    {
                        float randFloat = GENERATE_RANDOM_FLOAT_IN_RANGE(1f, 2f);
                        APPLY_FORCE_TO_PED(pedHandle, 3, randFloat, (2f - randFloat), -2f, 0, 0, 0, 0, 1, 1, 1);
                        CLEAR_CHAR_LAST_WEAPON_DAMAGE(pedHandle);
                    }
                }
            }
        }
    }
}
