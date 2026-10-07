using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CCL.GTAIV;
using IVSDKDotNet;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class CombatRoll
    {
        private static uint fTimer;
        public static void IngameStart()
        {
            fTimer = 0;
        }
        public static void Tick()
        {
            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "ev_dives", "plyr_roll_left") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "ev_dives", "plyr_roll_right"))
            {
                SET_CHAR_HEADING(Main.PlayerHandle, NativeCamera.GetGameCam().Rotation.Z);
                if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) && Main.pAmmo > 0)
                {
                    FirePlyrWeap();
                }
            }
            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "jump_std", "jump_land_roll"))
            {
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "jump_std", "jump_land_roll", out float animTime);
                if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) && Main.pAmmo > 0 && animTime > 0.35f && animTime < 0.55f)
                {
                    FirePlyrWeap();
                }
            }
        }
        private static void FirePlyrWeap()
        {
            GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS(Main.PlayerHandle, new Vector3(0, IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponRange, 0), out Vector3 offPos);
            if (Main.gTimer >= fTimer + IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).TimeBetweenShots)
            {
                GET_GAME_TIMER(out fTimer);
                FIRE_PED_WEAPON(Main.PlayerHandle, offPos);
            }
        }
    }
}
