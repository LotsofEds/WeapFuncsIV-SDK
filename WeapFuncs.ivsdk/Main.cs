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

        // IniShit
        public static bool GlobalRateOfFire;
        public static bool ReloadInVehicles;
        public static bool ReloadOnBikes;
        public static bool CrouchRelFix;
        public static bool SemiAutoShotgunBlindfire;
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
        public static int numOfWeapIDs;
        public static bool flameOn;
        public static bool equipGun;
        public static bool enableStun;

        // Other variables n shit
        public static int gunModel;
        public static int Boolet;
        public static uint CurrEp;
        public static int currWeap;
        public static int pAmmo;
        public static int aAmmo;
        public static int mAmmo;
        public static int wSlot;
        public static float frameTime;
        public static string WeapAnim = "";
        public static string BFAnim = "";
        public static float weapReload = 1.0f;
        public static Vector3 WeapOffset = new Vector3(0, 0, 0);

        public static DelayedCalling TheDelayedCaller;
        public static IVPed PlayerPed { get; set; }
        public static uint PlayerIndex { get; set; }
        public static int PlayerHandle { get; set; }
        public static Vector3 PlayerPos { get; set; }

        // SettingsFiles
        public static SettingsFile attachmentConfig;
        public static SettingsFile wfAttachConfig;
        public Main()
        {
            Uninitialize += Main_Uninitialize;
            Initialized += Main_Initialized;
            GameLoad += Main_GameLoad;
            Tick += Main_Tick;
            ProcessCamera += Main_ProcessCamera;
            TheDelayedCaller = new DelayedCalling();
            //TheWeaponHandler = new WeaponHandling();
        }

        private void Main_GameLoad(object sender, EventArgs e)
        {
            GLaunchAttachment.OnGameLoad();
            WeaponZoom.OnGameLoad();
        }

        private void Main_Uninitialize(object sender, EventArgs e)
        {
            if (TheDelayedCaller != null)
            {
                TheDelayedCaller.ClearAll();
                TheDelayedCaller = null;
            }
            WeapFuncs.UnInit();
            GLaunchAttachment.UnInit();
            EquipGun.UnInit();
            Pickups.UnInit();
            SwitchWeapNoReload.UnInit();
            //Silence.UnInit();
        }

        private void Main_Initialized(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(string.Format("{0}\\IVSDKDotNet\\scripts\\ImprovedGunStores\\Attachments.ini", IVGame.GameStartupPath)))
            {
                attachmentConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\ImprovedGunStores\\Attachments.ini", IVGame.GameStartupPath));
            }
            else
                attachmentConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\Attachments.ini", IVGame.GameStartupPath));
            attachmentConfig.Load();

            wfAttachConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\Attachments.ini", IVGame.GameStartupPath));
            wfAttachConfig.Load();

            LoadSettings(Settings);
            if (GlobalRateOfFire)
                RateOfFire.Init(Settings);
            BlindFireFixes.Init(Settings);
            if (SemiAutoShotgunBlindfire)
                ShotgunBlindfireFix.Init(Settings);
            BlindFireFixes.Init(Settings);
            SwitchWeapNoReload.Init(Settings);
            if (AllRoundReload)
                ShotgunRel.Init(Settings);
            if (HeadShotty)
                ShottyHeadShot.Init(Settings);
            WeaponZoom.Init(Settings);
            if (FireMode)
                SelectFire.Init(Settings);
            if (GLaunchEnable)
                GLaunchAttachment.Init(Settings);
            if (flameOn)
                Flames.Init(Settings);
            if (equipGun)
                EquipGun.Init(Settings);
            Pickups.Init(Settings);
            if (enableStun)
                Flashbang.Init(Settings);
            Recoil.Init(Settings);
            TapFireSpreadFix.Init(Settings);
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

            GET_FRAME_TIME(out frameTime);
            TheDelayedCaller.Process();
            GET_CURRENT_CHAR_WEAPON(PlayerHandle, out currWeap);
            GET_AMMO_IN_CLIP(PlayerHandle, currWeap, out pAmmo);
            GET_AMMO_IN_CHAR_WEAPON(PlayerHandle, currWeap, out aAmmo);
            GET_MAX_AMMO_IN_CLIP(PlayerHandle, currWeap, out mAmmo);
            GET_WEAPONTYPE_SLOT(currWeap, out wSlot);
            if (currWeap > 0)
                LoadWeaponConfig(currWeap);

            PedHelper.GrabAllPeds();
            //ObjectHelper.GrabAllObjs();
            //VehHelper.GrabAllVehs();
            if (GlobalRateOfFire)
                RateOfFire.Tick();
            ReloadSpeed.Tick();
            BlindFireFixes.Tick();
            if (SemiAutoShotgunBlindfire)
                ShotgunBlindfireFix.Tick();
            WeapFuncs.Tick();
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
            if (flameOn)
                Flames.Tick();
            if (equipGun)
                EquipGun.Tick();
            Pickups.Tick();
            if (enableStun)
                Flashbang.Tick();
            Recoil.Tick();
            TapFireSpreadFix.Tick();

            if (Pickups.limitedLoadout)
                Pickups.GetMaxLoadout(Settings);

            //Silence.Tick();
            //ObjectTest.Tick();
        }
        public static void WriteBooleanToINI(SettingsFile settings, string name, bool booleShit)
        {
            if (!settings.DoesSectionExists(IVGenericGameStorage.ValidSaveName))
                settings.AddSection(IVGenericGameStorage.ValidSaveName);
            if (!settings.DoesKeyExists(IVGenericGameStorage.ValidSaveName, name))
                settings.AddKeyToSection(IVGenericGameStorage.ValidSaveName, name);
            settings.SetBoolean(IVGenericGameStorage.ValidSaveName, name, booleShit);
        }
        public static void WriteIntToINI(SettingsFile settings, string name, int integerShit)
        {
            if (!settings.DoesSectionExists(IVGenericGameStorage.ValidSaveName))
                settings.AddSection(IVGenericGameStorage.ValidSaveName);
            if (!settings.DoesKeyExists(IVGenericGameStorage.ValidSaveName, name))
                settings.AddKeyToSection(IVGenericGameStorage.ValidSaveName, name);
            settings.SetInteger(IVGenericGameStorage.ValidSaveName, name, integerShit);
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
        public static bool IsAimingAnimPlaying()
        {
            if (IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "fire") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "fire_crouch") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "fire_crouch_alt") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "fire_up") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "fire_down"))
                return true;
            else
                return false;
        }
        public static bool IsReloadAnimPlaying()
        {
            if (IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "reload") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "p_load") || IS_CHAR_PLAYING_ANIM(PlayerHandle, WeapAnim, "reload_crouch"))
                return true;
            else
                return false;
        }
        public static Vector3 DirectionToRotation(Vector3 dir, float roll)
        {
            dir = Vector3.Normalize(dir);
            Vector3 result = default(Vector3);
            result.Z = (float)(Math.Atan2(dir.X, dir.Y) * (-180.0 / Math.PI));
            Vector3 vector = new Vector3(dir.X, dir.Y, 0f);
            Vector3 vector2 = new Vector3(dir.Z, vector.Length(), 0f);
            Vector3 vector3 = Vector3.Normalize(vector2);
            result.X = (float)(Math.Atan2(vector3.X, vector3.Y) * (180.0 / Math.PI));
            result.Y = roll;
            return result;
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
        public static bool IsPressingAimButton()
        {
            if ((IsAimKeyPressedOnController() && IS_USING_CONTROLLER()) || NativeControls.IsGameKeyPressed(0, GameKey.Aim) || NativeControls.IsGameKeyPressed(2, GameKey.Aim))
                return true;
            else
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
            wConfFile = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\WeaponConfigs.ini", IVGame.GameStartupPath));
            wConfFile.Load();
            numOfWeapIDs = settings.GetInteger("MAIN", "NumOfWeaponIDs", 60);
            GlobalRateOfFire = settings.GetBoolean("MAIN", "GlobalROF", false);
            ReloadInVehicles = settings.GetBoolean("RELOADS", "ReloadInVehicles", false);
            ReloadOnBikes = settings.GetBoolean("RELOADS", "ReloadOnBikes", false);
            CrouchRelFix = settings.GetBoolean("RELOADS", "MP5ReloadCrouchFix", false);
            SemiAutoShotgunBlindfire = settings.GetBoolean("BLINDFIRING", "SemiAutoShotgunBlindfire", false);
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
            flameOn = settings.GetBoolean("OTHER", "FlameEnable", false);
            equipGun = settings.GetBoolean("OTHER", "HolsteredWeaponsOnPlayer", false);
            enableStun = settings.GetBoolean("OTHER", "StunGrenadeEnable", false);
        }
    }
}
