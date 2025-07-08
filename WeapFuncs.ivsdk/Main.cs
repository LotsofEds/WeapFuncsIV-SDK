using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using CCL.GTAIV;

namespace WeapFuncs.ivsdk
{
    public class Main : Script
    {
        public static SettingsFile wConfFile;
        public static bool GlobalRateOfFire;
        public static bool ReloadInVehicles;
        public static bool ReloadOnBikes;
        public static bool CrouchRelFix;
        public static bool SemiAutoShotgunBlindfire;
        public static bool NPCSemiAutoShotgunBlindfire;
        public static bool ConsistentPistolBlindfireLoop;
        public static bool FullAutoPistol;
        public static bool FullAutoShotgun;
        public static bool SawnOffYeet;
        public static bool FireMode;
        public static bool ShowFireModeText;
        public static bool SwitchWeaponNoReload;
        public static bool PressToFire;
        public static bool LoseAmmoInMag;
        public static int ShotsPerBurst;
        public static GameKey SelectFireCtrl;
        public static bool AllRoundReload;
        public static bool HeadShotty;
        public static bool GLaunchEnable;

        public static int gunModel;
        public static int Boolet;
        public static uint CurrEp;
        public static int currWeap;
        public static int pAmmo;
        public static int aAmmo;
        public static int mAmmo;
        public static int wSlot;
        public static string WeapAnim = "";
        public static string BFAnim = "";
        public static float weapReload = 1.0f;
        public static Vector3 WeapOffset = new Vector3(0, 0, 0);
        public static IVPed PlayerPed { get; set; }
        public static uint PlayerIndex { get; set; }
        public static int PlayerHandle { get; set; }
        public static Vector3 PlayerPos { get; set; }
        public Main()
        {
            Initialized += Main_Initialized;
            Tick += Main_Tick;
            ProcessCamera += Main_ProcessCamera;
            //TheWeaponHandler = new WeaponHandling();
        }
        private void Main_Initialized(object sender, EventArgs e)
        {
            LoadSettings(Settings);
            //if (GlobalRateOfFire)
                //RateOfFire.LoadSettings(Settings);
            BlindFireFixes.Init(Settings);
            if (SemiAutoShotgunBlindfire)
                ShotgunBlindfireFix.Init(Settings);
            if (NPCSemiAutoShotgunBlindfire)
                NPCSemiAutoShot.Init(Settings);
            BlindFireFixes.Init(Settings);
            SwitchWeapNoReload.Init();
            if (AllRoundReload)
                ShotgunRel.Init(Settings);
            if (HeadShotty)
                ShottyHeadShot.Init(Settings);
            WeaponZoom.Init(Settings);
            if (FireMode)
                SelectFire.Init(Settings);
            if (GLaunchEnable)
                GLaunchAttachment.Init(Settings);
        }
        public static bool InitialChecks()
        {
            if (IS_SCREEN_FADED_OUT()) return false;
            if (IS_PAUSE_MENU_ACTIVE()) return false;
            return true;
        }
        public static bool IsHoldingGun()
        {
            if (IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponFlags.Gun == true)
                return true;
            else
                return false;
        }
        private void Main_Tick(object sender, EventArgs e)
        {
            PlayerPed = IVPed.FromUIntPtr(IVPlayerInfo.FindThePlayerPed());
            PlayerHandle = PlayerPed.GetHandle();
            PlayerIndex = GET_PLAYER_ID();
            PlayerPos = PlayerPed.Matrix.Pos;

            if (!InitialChecks())
                return;
            if (PlayerPed == null)
                return;

            GET_CURRENT_CHAR_WEAPON(PlayerHandle, out currWeap);
            GET_AMMO_IN_CLIP(PlayerHandle, currWeap, out pAmmo);
            GET_AMMO_IN_CHAR_WEAPON(PlayerHandle, currWeap, out aAmmo);
            GET_MAX_AMMO_IN_CLIP(PlayerHandle, currWeap, out mAmmo);
            GET_WEAPONTYPE_SLOT(currWeap, out wSlot);
            if (currWeap > 0)
                LoadWeaponConfig(currWeap);

            PedHelper.GrabAllPeds();
            //ObjectHelper.GrabAllObjs();
            if (GlobalRateOfFire)
                RateOfFire.Tick();
            ReloadSpeed.Tick();
            BlindFireFixes.Tick();
            if (SemiAutoShotgunBlindfire)
                ShotgunBlindfireFix.Tick();
            WeapFuncs.Tick();
            if (NPCSemiAutoShotgunBlindfire)
                NPCSemiAutoShot.Tick();
            if (SwitchWeaponNoReload)
                SwitchWeapNoReload.Tick();
            if (AllRoundReload)
                ShotgunRel.Tick();
            if (HeadShotty)
                ShottyHeadShot.Tick();
            if (FireMode)
                SelectFire.Tick();
            if (GLaunchEnable)
                GLaunchAttachment.Tick();
            //ObjectTest.Tick();
        }

        // Credits to catsmackaroo for these helpers, couldn't be assed to make my own from scratch.
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        public static float SmoothStep(float start, float end, float amount)
        {
            amount = Clamp(amount, 0, 1);
            amount = amount * amount * (3 - 2 * amount);
            return start + (end - start) * amount;
        }
        public static bool TwoHanded(int weapon)
        {
            if (IVWeaponInfo.GetWeaponInfo((uint)weapon).WeaponFlags.TreatAsTwoHandedInCover || IVWeaponInfo.GetWeaponInfo((uint)weapon).WeaponFlags.TwoHanded)
                return true;
            else
                return false;
        }
        private void Main_ProcessCamera(object sender, EventArgs e)
        {
            if (!InitialChecks())
                return;
            WeaponZoom.Tick();
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
        public static void LoadWeaponConfig(int weapon)
        {
            if (wConfFile.DoesSectionExists(weapon.ToString()))
            {
                WeapAnim = wConfFile.GetValue(weapon.ToString(), "Anim", "");
                weapReload = wConfFile.GetFloat(weapon.ToString(), "Reload", 1);
                WeapOffset = wConfFile.GetVector3(weapon.ToString(), "Offset", Vector3.Zero);
                switch (IVWeaponInfo.GetWeaponInfo((uint)weapon).WeaponSlot)
                {
                    case 2:
                        if (TwoHanded(weapon))
                            BFAnim = "ak47_blindfire";
                        else
                            BFAnim = "pistol_blindfire";
                        break;
                    case 3:
                        BFAnim = "shotgun_blindfire";
                        break;
                    case 4:
                        if (TwoHanded(weapon))
                            BFAnim = "ak47_blindfire";
                        else
                            BFAnim = "uzi_blindfire";
                        break;
                    case 5:
                        BFAnim = "ak47_blindfire";
                        break;
                    case 6:
                        BFAnim = "rifle_blindfire";
                        break;
                    case 7:
                        BFAnim = "rocket_blindfire";
                        break;
                }
            }
        }
        private void LoadSettings(SettingsFile settings)
        {
            wConfFile = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\WeaponConfigs.txt", IVGame.GameStartupPath));
            wConfFile.Load();
            GlobalRateOfFire = settings.GetBoolean("OTHER", "GlobalROF", false);
            ReloadInVehicles = settings.GetBoolean("RELOADS", "ReloadInVehicles", false);
            ReloadOnBikes = settings.GetBoolean("RELOADS", "ReloadOnBikes", false);
            CrouchRelFix = settings.GetBoolean("RELOADS", "MP5ReloadCrouchFix", false);
            SemiAutoShotgunBlindfire = settings.GetBoolean("BLINDFIRING", "SemiAutoShotgunBlindfire", false);
            NPCSemiAutoShotgunBlindfire = settings.GetBoolean("BLINDFIRING", "NPCSemiAutoShotgunBlindfire", false);
            ConsistentPistolBlindfireLoop = settings.GetBoolean("BLINDFIRING", "ConsistentPistolBlindfireLoop", false);
            FullAutoPistol = settings.GetBoolean("BLINDFIRING", "FullAutoPistolBlindfire", false);
            FullAutoShotgun = settings.GetBoolean("BLINDFIRING", "FullAutoShotgunBlindfire", false);
            SawnOffYeet = settings.GetBoolean("OTHER", "SawnOffFiresSecondShellImmediately", false);
            FireMode = settings.GetBoolean("SELECT FIRE", "SelectFire", false);
            ShowFireModeText = settings.GetBoolean("SELECT FIRE", "ShowFireModeText", false);
            ShotsPerBurst = settings.GetInteger("SELECT FIRE", "ShotsPerBurst", 3);
            SwitchWeaponNoReload = settings.GetBoolean("RELOADS", "SwitchWeaponNoReload", false);
            PressToFire = settings.GetBoolean("SELECT FIRE", "PressToFire", false);
            LoseAmmoInMag = settings.GetBoolean("RELOADS", "LoseAmmoInMag", false);
            SelectFireCtrl = (GameKey)settings.GetInteger("SELECT FIRE", "SelectFireControl", 23);
            AllRoundReload = settings.GetBoolean("RELOADS", "AllRoundReload", false);
            HeadShotty = settings.GetBoolean("OTHER", "LethalShotgunHeadshot", false);
            GLaunchEnable = settings.GetBoolean("ATTACHMENTS", "GrenadeLauncherAttachment", false);
        }
    }
}
