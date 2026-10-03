using CCL.GTAIV;
using IVSDKDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk.Helpers
{
    internal class WeaponHelper
    {
        public static bool IsHoldingGun()
        {
            if (IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponFlags.Gun == true)
                return true;
            else
                return false;
        }
        public static bool TwoHanded(int weapon)
        {
            if (IVWeaponInfo.GetWeaponInfo((uint)weapon).WeaponFlags.TreatAsTwoHandedInCover || IVWeaponInfo.GetWeaponInfo((uint)weapon).WeaponFlags.TwoHanded)
                return true;
            else
                return false;
        }
        public static bool IsAimingAnimPlaying()
        {
            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "fire_crouch")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "fire_crouch_alt")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "fire_up") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "fire_down"))
                return true;
            else
                return false;
        }
        public static bool IsReloadAnimPlaying()
        {
            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "p_load")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload_crouch"))
                return true;
            else
                return false;
        }
        public static bool IsAimKeyPressedOnController()
        {
            uint standard = 0;
            var settings = IVMenuManager.GetSetting(IVSDKDotNet.Enums.eSettings.SETTING_CONFIGURATION);
            ControllerButton button;

            if (settings == standard
                && !IS_CHAR_IN_ANY_CAR(Main.PlayerPed.GetHandle()))
                button = ControllerButton.BUTTON_TRIGGER_LEFT;
            else
                button = ControllerButton.BUTTON_BUMPER_LEFT;


            if (NativeControls.IsControllerButtonPressed(2, button))
                return true;

            return false;
        }
        public static bool IsPressingAimButton()
        {
            if ((IsAimKeyPressedOnController() && IS_USING_CONTROLLER()) || NativeControls.IsGameKeyPressed(0, GameKey.Aim) || NativeControls.IsGameKeyPressed(2, GameKey.Aim))
                return true;
            else
                return false;
        }
        public static int PlayerAmmo()
        {
            GET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out int currAmmo);
            return currAmmo;
        }
        public static bool DBFiringWeapon(IVPed ped)
        {
            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow", "ps_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "ps_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebylow_conv", "ps_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ds_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "ps_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "bl_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebytruck", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyairtug", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebystd", "br_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyvan", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop_1h")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_big", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop_1h")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop_1h") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_spee", "ps_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "bl_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyboat_stnd", "br_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "bl_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebyheli", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "bl_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@driveby_teststd", "br_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_chop", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_chop", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_dirt", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_dirt", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_free", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_free", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_scot", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_scot", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_spt", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybike_spt", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_chop_s", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_chop_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_dirt_s", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_dirt_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_free_s", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_free_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_scot_s", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_scot_s", "ps_aim_loop") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_spt_s", "ds_aim_loop")
                || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "veh@drivebybk_spt_s", "ps_aim_loop"))
                return true;

            return false;
        }

    }
    public class WeaponData
    {
        public int ID { get; set; }
        public string Anim { get; set; }
        public string BFAnim { get; set; }
        public float FireRate { get; set; }
        public float DBFireRate { get; set; }
        public float BFFireRate { get; set; }
        public float ReloadSpd { get; set; }
        public Vector3 Offset { get; set; }
        public string PickupSound { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }
        public float Zoom { get; set; }
        public float RecoilAmpMin { get; set; }
        public float RecoilAmpMax { get; set; }
        public int RecoilTime { get; set; }
        public float RecoilAdd { get; set; }
        public float RecoilDecay { get; set; }
        public float MaxRecoil { get; set; }
        public float RecoilCrouch { get; set; }

        public WeaponData()
        {
        }
    }
}
