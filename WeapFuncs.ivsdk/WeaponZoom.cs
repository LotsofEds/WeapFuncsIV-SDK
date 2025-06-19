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

        static float PistolZoom;
        static float SilencedZoom;
        static float DeagleZoom;
        static float PumpShotZoom;
        static float CombatShotZoom;
        static float UziZoom;
        static float MP5Zoom;
        static float AK47Zoom;
        static float M4Zoom;
        static float SnipZoom;
        static float PsgZoom;
        static float RpgZoom;
        static float FThrowerZoom;
        static float AutoPZoom;
        static float SawnOffZoom;
        static float AssaultShotZoom;
        static float GrndLaunchZoom;
        static float Pistol44Zoom;
        static float AA12Zoom;
        static float AA12ExpZoom;
        static float P90Zoom;
        static float GoldUziZoom;
        static float M249Zoom;
        static float AdvSnipZoom;
        static float Episodic3Zoom;
        static float Episodic22Zoom;
        static float Episodic23Zoom;
        static float Episodic24Zoom;
        static float Addon1Zoom;
        static float Addon2Zoom;
        static float Addon3Zoom;
        static float Addon4Zoom;
        static float Addon5Zoom;
        static float Addon6Zoom;
        static float Addon7Zoom;
        static float Addon8Zoom;
        static float Addon9Zoom;
        static float Addon10Zoom;
        static float Addon11Zoom;
        static float Addon12Zoom;
        static float Addon13Zoom;
        static float Addon14Zoom;
        static float Addon15Zoom;
        static float Addon16Zoom;
        static float Addon17Zoom;
        static float Addon18Zoom;
        static float Addon19Zoom;
        static float Addon20Zoom;
        static float Unused0Zoom;
        static float BatZoom;
        static float PoolcueZoom;
        static float KnifeZoom;
        static float GrenadeZoom;
        static float MolotovZoom;
        static float RocketZoom;
        static float Episodic4Zoom;
        static float Episodic5Zoom;
        static float Episodic8Zoom;
        static float Episodic16Zoom;
        static float Episodic17Zoom;
        static float Episodic18Zoom;
        static float Episodic19Zoom;
        static float Episodic20Zoom;
        static float Episodic21Zoom;
        static float CameraZoom;
        static float ObjectZoom;
        public static void Init(SettingsFile settings)
        {
            PistolZoom = settings.GetFloat("GLOCK", "Zoom", 1.0f);
            SilencedZoom = settings.GetFloat("SILENCED", "Zoom", 1.0f);
            DeagleZoom = settings.GetFloat("DEAGLE", "Zoom", 1.0f);
            PumpShotZoom = settings.GetFloat("PUMP SHOTGUN", "Zoom", 1.0f);
            CombatShotZoom = settings.GetFloat("SEMIAUTO SHOTGUN", "Zoom", 1.0f);
            UziZoom = settings.GetFloat("MICRO UZI", "Zoom", 1.0f);
            MP5Zoom = settings.GetFloat("MP5", "Zoom", 1.0f);
            AK47Zoom = settings.GetFloat("AK47", "Zoom", 1.0f);
            M4Zoom = settings.GetFloat("M4", "Zoom", 1.0f);
            SnipZoom = settings.GetFloat("BOLTACTION SNIPER", "Zoom", 1.0f);
            PsgZoom = settings.GetFloat("SEMIAUTO SNIPER", "Zoom", 1.0f);
            RpgZoom = settings.GetFloat("RPG", "Zoom", 1.0f);
            AutoPZoom = settings.GetFloat("FULLAUTO PISTOL", "Zoom", 1.0f);
            SawnOffZoom = settings.GetFloat("SAWNOFF SHOTGUN", "Zoom", 1.0f);
            AssaultShotZoom = settings.GetFloat("ASSAULT SHOTGUN", "Zoom", 1.0f);
            GrndLaunchZoom = settings.GetFloat("GRENADE LAUNCHER", "Zoom", 1.0f);
            Pistol44Zoom = settings.GetFloat("PISTOL 44", "Zoom", 1.0f);
            AA12Zoom = settings.GetFloat("AA12", "Zoom", 1.0f);
            AA12ExpZoom = settings.GetFloat("AA12 EXP", "Zoom", 1.0f);
            P90Zoom = settings.GetFloat("P90", "Zoom", 1.0f);
            GoldUziZoom = settings.GetFloat("GOLD UZI", "Zoom", 1.0f);
            M249Zoom = settings.GetFloat("M249", "Zoom", 1.0f);
            AdvSnipZoom = settings.GetFloat("ADV SNIPER", "Zoom", 1.0f);
            FThrowerZoom = settings.GetFloat("FTHROWER", "Zoom", 1.0f);
            Episodic3Zoom = settings.GetFloat("EPISODIC 3", "Zoom", 1.0f);
            Episodic22Zoom = settings.GetFloat("EPISODIC 22", "Zoom", 1.0f);
            Episodic23Zoom = settings.GetFloat("EPISODIC 23", "Zoom", 1.0f);
            Episodic24Zoom = settings.GetFloat("EPISODIC 24", "Zoom", 1.0f);
            Addon1Zoom = settings.GetFloat("ADDONWEAPON 1", "Zoom", 1.0f);
            Addon2Zoom = settings.GetFloat("ADDONWEAPON 2", "Zoom", 1.0f);
            Addon3Zoom = settings.GetFloat("ADDONWEAPON 3", "Zoom", 1.0f);
            Addon4Zoom = settings.GetFloat("ADDONWEAPON 4", "Zoom", 1.0f);
            Addon5Zoom = settings.GetFloat("ADDONWEAPON 5", "Zoom", 1.0f);
            Addon6Zoom = settings.GetFloat("ADDONWEAPON 6", "Zoom", 1.0f);
            Addon7Zoom = settings.GetFloat("ADDONWEAPON 7", "Zoom", 1.0f);
            Addon8Zoom = settings.GetFloat("ADDONWEAPON 8", "Zoom", 1.0f);
            Addon9Zoom = settings.GetFloat("ADDONWEAPON 9", "Zoom", 1.0f);
            Addon10Zoom = settings.GetFloat("ADDONWEAPON 10", "Zoom", 1.0f);
            Addon11Zoom = settings.GetFloat("ADDONWEAPON 11", "Zoom", 1.0f);
            Addon12Zoom = settings.GetFloat("ADDONWEAPON 12", "Zoom", 1.0f);
            Addon13Zoom = settings.GetFloat("ADDONWEAPON 13", "Zoom", 1.0f);
            Addon14Zoom = settings.GetFloat("ADDONWEAPON 14", "Zoom", 1.0f);
            Addon15Zoom = settings.GetFloat("ADDONWEAPON 15", "Zoom", 1.0f);
            Addon16Zoom = settings.GetFloat("ADDONWEAPON 16", "Zoom", 1.0f);
            Addon17Zoom = settings.GetFloat("ADDONWEAPON 17", "Zoom", 1.0f);
            Addon18Zoom = settings.GetFloat("ADDONWEAPON 18", "Zoom", 1.0f);
            Addon19Zoom = settings.GetFloat("ADDONWEAPON 19", "Zoom", 1.0f);
            Addon20Zoom = settings.GetFloat("ADDONWEAPON 20", "Zoom", 1.0f);
            Unused0Zoom = settings.GetFloat("UNUSED0", "Zoom", 1.0f);
            Episodic4Zoom = settings.GetFloat("EPISODIC 4", "Zoom", 1.0f);
            Episodic5Zoom = settings.GetFloat("EPISODIC 5", "Zoom", 1.0f);
            Episodic8Zoom = settings.GetFloat("EPISODIC 8", "Zoom", 1.0f);
            Episodic16Zoom = settings.GetFloat("EPISODIC 16", "Zoom", 1.0f);
            Episodic17Zoom = settings.GetFloat("EPISODIC 17", "Zoom", 1.0f);
            Episodic18Zoom = settings.GetFloat("EPISODIC 18", "Zoom", 1.0f);
            Episodic19Zoom = settings.GetFloat("EPISODIC 19", "Zoom", 1.0f);
            Episodic20Zoom = settings.GetFloat("EPISODIC 20", "Zoom", 1.0f);
            Episodic21Zoom = settings.GetFloat("EPISODIC 21", "Zoom", 1.0f);
            CameraZoom = settings.GetFloat("CAMERA", "Zoom", 1.0f);
            ObjectZoom = settings.GetFloat("OBJECT", "Zoom", 1.0f);
            BatZoom = settings.GetFloat("BASEBALL BAT", "Zoom", 1.0f);
            PoolcueZoom = settings.GetFloat("POOLCUE", "Zoom", 1.0f);
            KnifeZoom = settings.GetFloat("KNIFE", "Zoom", 1.0f);
            GrenadeZoom = settings.GetFloat("GRENADE", "Zoom", 1.0f);
            MolotovZoom = settings.GetFloat("MOLOTOV", "Zoom", 1.0f);
            RocketZoom = settings.GetFloat("ROCKET", "Zoom", 1.0f);
        }
        public static void Tick()
        {
            IVCam cam = IVCamera.TheFinalCam;
            NativeCamera gameCam = NativeCamera.GetGameCam();

            switch (Main.currWeap)
            {
                case 1:
                    weaponZoom = BatZoom;
                    break;
                case 2:
                    weaponZoom = PoolcueZoom;
                    break;
                case 3:
                    weaponZoom = KnifeZoom;
                    break;
                case 4:
                    weaponZoom = GrenadeZoom;
                    break;
                case 5:
                    weaponZoom = MolotovZoom;
                    break;
                case 6:
                    weaponZoom = RocketZoom;
                    break;
                case 7:
                    weaponZoom = PistolZoom;
                    break;
                case 8:
                    weaponZoom = Unused0Zoom;
                    break;
                case 9:
                    weaponZoom = DeagleZoom;
                    break;
                case 10:
                    weaponZoom = PumpShotZoom;
                    break;
                case 11:
                    weaponZoom = CombatShotZoom;
                    break;
                case 12:
                    weaponZoom = UziZoom;
                    break;
                case 13:
                    weaponZoom = MP5Zoom;
                    break;
                case 14:
                    weaponZoom = AK47Zoom;
                    break;
                case 15:
                    weaponZoom = M4Zoom;
                    break;
                case 16:
                    weaponZoom = PsgZoom;
                    break;
                case 17:
                    weaponZoom = SnipZoom;
                    break;
                case 18:
                    weaponZoom = RpgZoom;
                    break;
                case 19:
                    weaponZoom = FThrowerZoom;
                    break;
                case 20:
                    weaponZoom = SilencedZoom;
                    break;
                case 21:
                    weaponZoom = GrndLaunchZoom;
                    break;
                case 22:
                    weaponZoom = AssaultShotZoom;
                    break;
                case 23:
                    weaponZoom = Episodic3Zoom;
                    break;
                case 24:
                    weaponZoom = Episodic4Zoom;
                    break;
                case 25:
                    weaponZoom = Episodic5Zoom;
                    break;
                case 26:
                    weaponZoom = SawnOffZoom;
                    break;
                case 27:
                    weaponZoom = AutoPZoom;
                    break;
                case 28:
                    weaponZoom = Episodic8Zoom;
                    break;
                case 29:
                    weaponZoom = Pistol44Zoom;
                    break;
                case 30:
                    weaponZoom = AA12ExpZoom;
                    break;
                case 31:
                    weaponZoom = AA12Zoom;
                    break;
                case 32:
                    weaponZoom = P90Zoom;
                    break;
                case 33:
                    weaponZoom = GoldUziZoom;
                    break;
                case 34:
                    weaponZoom = M249Zoom;
                    break;
                case 35:
                    weaponZoom = AdvSnipZoom;
                    break;
                case 36:
                    weaponZoom = Episodic16Zoom;
                    break;
                case 37:
                    weaponZoom = Episodic17Zoom;
                    break;
                case 38:
                    weaponZoom = Episodic18Zoom;
                    break;
                case 39:
                    weaponZoom = Episodic19Zoom;
                    break;
                case 40:
                    weaponZoom = Episodic20Zoom;
                    break;
                case 41:
                    weaponZoom = Episodic21Zoom;
                    break;
                case 42:
                    weaponZoom = Episodic22Zoom;
                    break;
                case 43:
                    weaponZoom = Episodic23Zoom;
                    break;
                case 44:
                    weaponZoom = Episodic24Zoom;
                    break;
                case 45:
                    weaponZoom = CameraZoom;
                    break;
                case 46:
                    weaponZoom = ObjectZoom;
                    break;
                case 47:
                    weaponZoom = SilencedZoom;
                    break;
                case 48:
                    weaponZoom = SilencedZoom;
                    break;
                case 49:
                    weaponZoom = SilencedZoom;
                    break;
                case 50:
                    weaponZoom = SilencedZoom;
                    break;
                case 51:
                    weaponZoom = SilencedZoom;
                    break;
                case 52:
                    weaponZoom = SilencedZoom;
                    break;
                case 53:
                    weaponZoom = SilencedZoom;
                    break;
                case 54:
                    weaponZoom = SilencedZoom;
                    break;
                case 55:
                    weaponZoom = SilencedZoom;
                    break;
                case 56:
                    weaponZoom = SilencedZoom;
                    break;
                case 57:
                    weaponZoom = SilencedZoom;
                    break;
                case 58:
                    weaponZoom = Addon1Zoom;
                    break;
                case 59:
                    weaponZoom = Addon2Zoom;
                    break;
                case 60:
                    weaponZoom = Addon3Zoom;
                    break;
                case 61:
                    weaponZoom = Addon4Zoom;
                    break;
                case 62:
                    weaponZoom = Addon5Zoom;
                    break;
                case 63:
                    weaponZoom = Addon6Zoom;
                    break;
                case 64:
                    weaponZoom = Addon7Zoom;
                    break;
                case 65:
                    weaponZoom = Addon8Zoom;
                    break;
                case 66:
                    weaponZoom = Addon9Zoom;
                    break;
                case 67:
                    weaponZoom = Addon10Zoom;
                    break;
                case 68:
                    weaponZoom = Addon11Zoom;
                    break;
                case 69:
                    weaponZoom = Addon12Zoom;
                    break;
                case 70:
                    weaponZoom = Addon13Zoom;
                    break;
                case 71:
                    weaponZoom = Addon14Zoom;
                    break;
                case 72:
                    weaponZoom = Addon15Zoom;
                    break;
                case 73:
                    weaponZoom = Addon16Zoom;
                    break;
                case 74:
                    weaponZoom = Addon17Zoom;
                    break;
                case 75:
                    weaponZoom = Addon18Zoom;
                    break;
                case 76:
                    weaponZoom = Addon19Zoom;
                    break;
                case 77:
                    weaponZoom = Addon20Zoom;
                    break;
            }

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
