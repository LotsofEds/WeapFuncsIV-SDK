using CCL.GTAIV;
using CCL.GTAIV.Extensions;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class ReloadInVeh
    {
        // IniShit
        public static bool reloadOnBikes;

        private static string pWeapAnim = "";
        private static Vector3 weapOff;
        private static int weaponModel;
        private static float reloadTime;
        private static int pWeap = 0;
        private static int gunModel;
        public static void IngameStart()
        {
            DeleteProp();
        }
        public static void UnInit()
        {
            DeleteProp();
        }
        public static void Init(SettingsFile settings)
        {
            reloadOnBikes = settings.GetBoolean("RELOADS", "ReloadOnBikes", false);
        }
        public static void Tick()
        {
            pWeapAnim = Main.wAnim;
            weapOff = Main.wOffset;

            if ((WeaponHelper.DBFiringWeapon(Main.PlayerPed) || (!WeaponHelper.DBFiringWeapon(Main.PlayerPed) && NativeControls.IsGameKeyPressed(0, GameKey.Reload))) && (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) || (reloadOnBikes && IS_CHAR_ON_ANY_BIKE(Main.PlayerHandle))) && Main.aAmmo > 0 && Main.pAmmo == 0 && Main.currWeap != 56 && Main.currWeap != 46)
            {
                if (!IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "reload"))
                {
                    weaponModel = (int)IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).ModelHash;
                    if (!DOES_OBJECT_EXIST(gunModel))
                        CREATE_OBJECT(weaponModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out gunModel, true);
                    ATTACH_OBJECT_TO_PED(gunModel, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, weapOff.X, weapOff.Y, weapOff.Z, 0f, 0f, 0f, 0);
                    _TASK_PLAY_ANIM_UPPER_BODY(Main.PlayerHandle, "reload", pWeapAnim, 4.0f, 0, 0, 0, 0, -1);
                }
            }
            if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) || IS_CHAR_ON_ANY_BIKE(Main.PlayerHandle))
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", out reloadTime);
            else
            {
                reloadTime = 0;
                DeleteProp();
            }

            if (reloadTime > 0.9 || IS_CHAR_DEAD(Main.PlayerHandle) || pWeap != Main.currWeap || IS_PED_RAGDOLL(Main.PlayerHandle))
            {
                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "reload", 1.0f);
                DeleteProp();
                pWeap = Main.currWeap;
            }
        }
        private static void DeleteProp()
        {
            if (DOES_OBJECT_EXIST(gunModel))
                DELETE_OBJECT(ref gunModel);
        }
    }
}
