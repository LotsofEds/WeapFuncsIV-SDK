using CCL.GTAIV;
using IVSDKDotNet;
using System;
using System.Numerics;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class WeaponZoom
    {
        private static int msWhl;
        private static float currentFOV = 1.0f;
        private static float weaponZoom;
        private static float zoomAmt;
        private static bool isButtonPressed;
        private static bool isZoomOn;
        private static int pWeap;
        private static bool isAiming => (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_up") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_down") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload_crouch") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "p_load") || IS_PED_IN_COVER(Main.PlayerHandle));
        public static void Init(SettingsFile settings)
        {
        }
        public static void LoadWeaponConfig(int weapon)
        {
            /*if (Main.wConfFile.DoesSectionExists(weapon.ToString()))
            {
                if (Main.wfAttachConfig.DoesSectionExists(weapon.ToString()))
                    weaponZoom = Main.wfAttachConfig.GetFloat(weapon.ToString(), "ScopeMagnification", 1.0f);
                else
                    weaponZoom = Main.wConfFile.GetFloat(weapon.ToString(), "Zoom", 1.0f);
            }*/
            weaponZoom = Main.wConfFile.GetFloat(weapon.ToString(), "Zoom", 1.0f);
        }
        public static void Tick()
        {
            IVCam cam = IVCamera.TheFinalCam;
            NativeCamera gameCam = NativeCamera.GetGameCam();

            LoadWeaponConfig(Main.currWeap);

            if (Main.IsHoldingGun())
            {
                if (pWeap == Main.currWeap && !IS_PED_RAGDOLL(Main.PlayerHandle) && !IS_CHAR_SWIMMING(Main.PlayerHandle) && !IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && (NativeControls.IsGameKeyPressed(0, GameKey.Aim) || (Main.IsAimKeyPressedOnController() && IS_USING_CONTROLLER())))
                {
                    if ((NativeControls.IsGameKeyPressed(0, GameKey.LookBehind) || NativeControls.IsGameKeyPressed(2, GameKey.LookBehind)) && !isButtonPressed)
                    {
                        isZoomOn = !isZoomOn;
                        isButtonPressed = true;
                    }
                    else if (!NativeControls.IsGameKeyPressed(0, GameKey.LookBehind) && !NativeControls.IsGameKeyPressed(2, GameKey.LookBehind) && isButtonPressed)
                        isButtonPressed = false;

                    GET_MOUSE_WHEEL(out msWhl);
                    if ((msWhl < 0 || isZoomOn || gameCam.FOV <= 35) && isAiming && zoomAmt != weaponZoom)
                    {
                        isZoomOn = true;
                        zoomAmt = weaponZoom;
                    }
                    else if ((msWhl > 0 || !isZoomOn || (gameCam.FOV > 35 && IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponSlot != 3)) && zoomAmt != 1.0)
                    {
                        isZoomOn = false;
                        zoomAmt = 1.0f;
                    }
                }

                else if (!isAiming || IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) || (!NativeControls.IsGameKeyPressed(0, GameKey.Aim) && !(Main.IsAimKeyPressedOnController() && IS_USING_CONTROLLER())))
                {
                    isZoomOn = false;
                    isButtonPressed = false;
                    zoomAmt = 1.0f;
                }
                pWeap = Main.currWeap;

                if (cam == null)
                    return;

                //IVGame.ShowSubtitleMessage(gameCam.FOV.ToString() + "   " + zoomAmt.ToString());
                currentFOV = Main.SmoothStep(currentFOV, zoomAmt, 0.5f);
                cam.FOV /= currentFOV;
            }
        }
    }
}
