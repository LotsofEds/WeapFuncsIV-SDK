using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using IVSDKDotNet.Hooking;
using System;
using System.Numerics;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class WeaponZoom
    {
        private static float weaponZoom;

        private static bool[] attachmentUnlocks;

        private static int msWhl;
        private static float currentFOV = 1.0f;
        private static float zoomAmt;
        private static bool isButtonPressed;
        private static bool isZoomOn;
        private static int pWeap;
        private static bool scopeOn;

        private static IVCam cam;
        private static NativeCamera gameCam;
        public static void Init(SettingsFile settings)
        {
            attachmentUnlocks = new bool[Main.numOfWeapIDs];
        }
        public static void IngameStart()
        {
            pWeap = 0;
            for (int i = 0; i < Main.numOfWeapIDs; i++)
            {
                if (Main.attachmentConfig.DoesSectionExists(i.ToString()))
                {
                    Main.wfAttachConfig.SetBoolean(IVGenericGameStorage.ValidSaveName, i.ToString() + "HasScopeAttachment", Main.attachmentConfig.GetBoolean(IVGenericGameStorage.ValidSaveName, i.ToString() + "HasScopeAttachment", false));
                }
            }
            Main.wfAttachConfig.Save();
            Main.wfAttachConfig.Load();
        }
        public static void LoadWeaponConfig(int weapon)
        {
            /*if (Main.wConfFile.DoesSectionExists(weapon.ToString()))
            {
                if (Main.wfAttachConfig.DoesSectionExists(weapon.ToString()))
                {
                    hasAttachment = Main.wfAttachConfig.GetBoolean(IVGenericGameStorage.ValidSaveName, weapon.ToString() + "HasScopeAttachment", false);
                    if (hasAttachment)
                        weaponZoom = Main.wfAttachConfig.GetFloat(weapon.ToString(), "ScopeMagnification", 1.0f);
                    else
                        weaponZoom = Main.wConfFile.GetFloat(weapon.ToString(), "Zoom", 1.0f);

                    scopeOn = Main.wfAttachConfig.GetBoolean(weapon.ToString(), "FirstPerson", false);
                }
                else
                {
                    weaponZoom = Main.wConfFile.GetFloat(weapon.ToString(), "Zoom", 1.0f);
                    scopeOn = false;
                }
            }
            Main.wfAttachConfig.Load();*/

            int weapIndex = Main.weaponData.FindIndex(w => w.ID == weapon);
            Main.wfAttachConfig.Load();
            if (Main.wfAttachConfig.DoesSectionExists(weapon.ToString()))
            {
                bool hasAttachment = Main.wfAttachConfig.GetBoolean(IVGenericGameStorage.ValidSaveName, weapon.ToString() + "HasScopeAttachment", false);
                if (hasAttachment)
                    weaponZoom = Main.wfAttachConfig.GetFloat(weapon.ToString(), "ScopeMagnification", 1.0f);
                else
                {
                    if (weapIndex >= 0)
                        weaponZoom = Main.weaponData[weapIndex].Zoom;
                }

                scopeOn = Main.wfAttachConfig.GetBoolean(weapon.ToString(), "FirstPerson", false);
            }
            else
            {
                if (weapIndex >= 0)
                    weaponZoom = Main.weaponData[weapIndex].Zoom;
                scopeOn = false;
            }
        }
        public static void Tick()
        {
            cam = IVCamera.TheFinalCam;
            gameCam = NativeCamera.GetGameCam();

            LoadWeaponConfig(Main.currWeap);

            if (WeaponHelper.IsHoldingGun())
            {
                if (pWeap == Main.currWeap && !IS_PED_RAGDOLL(Main.PlayerHandle) && !IS_CHAR_SWIMMING(Main.PlayerHandle) && !IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && WeaponHelper.IsPressingAimButton())
                {
                    if ((NativeControls.IsGameKeyPressed(0, GameKey.LookBehind) || NativeControls.IsGameKeyPressed(2, GameKey.LookBehind)) && !isButtonPressed)
                    {
                        isZoomOn = !isZoomOn;
                        isButtonPressed = true;
                    }
                    else if (!NativeControls.IsGameKeyPressed(0, GameKey.LookBehind) && !NativeControls.IsGameKeyPressed(2, GameKey.LookBehind) && isButtonPressed)
                        isButtonPressed = false;

                    GET_MOUSE_WHEEL(out msWhl);
                    if ((msWhl < 0 || isZoomOn || gameCam.FOV <= 40) && WeaponHelper.IsAimingAnimPlaying() && zoomAmt != weaponZoom)
                    {
                        isZoomOn = true;
                        zoomAmt = weaponZoom;
                    }
                    else if ((msWhl > 0 || !isZoomOn || (gameCam.FOV > 40 && IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponSlot != 3)) && zoomAmt != 1.0)
                    {
                        isZoomOn = false;
                        zoomAmt = 1.0f;
                    }
                }

                else if (!WeaponHelper.IsAimingAnimPlaying() || IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) || !WeaponHelper.IsPressingAimButton())
                {
                    isZoomOn = false;
                    isButtonPressed = false;
                    zoomAmt = 1.0f;
                }

                if (pWeap != Main.currWeap)
                {
                    pWeap = Main.currWeap;

                    if (scopeOn)
                        IVWeaponInfo.GetWeaponInfo((uint)pWeap).WeaponFlags.FirstPerson = true;
                }
            }
        }

        public static void ProcessCam()
        {
            if (cam == null)
                return;

            if (WeaponHelper.IsHoldingGun())
            {
                //IVGame.ShowSubtitleMessage(gameCam.FOV.ToString() + "   " + zoomAmt.ToString());
                currentFOV = GenHelp.SmoothStep(currentFOV, zoomAmt, 15f * Main.frameTime);

                cam.FOV /= currentFOV;
            }
        }
    }
}
