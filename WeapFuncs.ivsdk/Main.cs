using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Hooking;
using IVSDKDotNet.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    public class Main : Script
    {
        // IniShit
        public static int numOfWeapIDs;
        public static bool globalRateOfFire;
        public static bool reloadSpeedEnable;
        public static bool reloadInVehicles;
        public static bool blindfireFixes;
        public static bool fireMode;
        public static bool switchWeaponNoReload;
        public static bool LoseAmmoInMag;
        public static bool allRoundReload;
        public static bool headShotty;
        public static bool tapFireFixEnable;
        public static bool gLaunchEnable;
        public static bool flameOn;
        public static bool equipGun;
        public static bool enableStun;
        public static bool enableHeatseeker;
        public static bool recoilEnable;
        public static bool heliMinigunEnable;
        public static bool weaponZoomEnable;
        public static bool aimCamShakeEnable;
        public static bool pickupEnable;

        // Other variables n shit
        public static bool resetShit;
        public static int Boolet;
        public static int currWeap;
        public static int pAmmo;
        public static int aAmmo;
        public static int mAmmo;
        public static int wSlot;
        public static uint CurrEp;
        public static uint gTimer;
        public static float frameTime;

        public static string wAnim = "";
        public static string bfAnim = "";
        public static float wReload = 1.0f;
        public static Vector3 wOffset = new Vector3(0, 0, 0);
        public static IVPed PlayerPed { get; set; }
        public static uint PlayerIndex { get; set; }
        public static int PlayerHandle { get; set; }
        public static Vector3 PlayerPos { get; set; }

        // SettingsFiles
        public static SettingsFile wConfFile;
        public static SettingsFile wfConfig;
        public static SettingsFile attachmentConfig;
        public static SettingsFile wfAttachConfig;

        public static List<WeaponData> weaponData = new List<WeaponData>();
        public Main()
        {
            Uninitialize += Main_Uninitialize;
            Initialized += Main_Initialized;
            IngameStartup += Main_IngameStartup;
            //GameLoad += Main_GameLoad;
            Tick += Main_Tick;
            ProcessCamera += Main_ProcessCamera;
            GameHooks.OnDoPickupGlow += new GameHooks.OnDoPickupGlowDelegate(OnDoPickupGlow);
            //TheWeaponHandler = new WeaponHandling();
        }
        private HookCallback<int> OnDoPickupGlow() => new HookCallback<int>(pickupEnable? true : false);
        private void Main_IngameStartup(object sender, EventArgs e)
        {
            resetShit = false;
            currWeap = 0;
            wAnim = "";
            bfAnim = "";

            GLaunchAttachment.IngameStart();
            WeaponZoom.IngameStart();
            Pickups.IngameStart();
            SwitchWeapNoReload.IngameStart();
        }
        private void Main_Uninitialize(object sender, EventArgs e)
        {
            ReloadInVeh.UnInit();
            GLaunchAttachment.UnInit();
            EquipGun.UnInit();
            Pickups.UnInit();
            SwitchWeapNoReload.UnInit();
            SelectFire.UnInit();
            //ShoulderSwap.UnInit();
            //Silence.UnInit();
        }
        private void Main_Initialized(object sender, EventArgs e)
        {
            wfConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs.ini", IVGame.GameStartupPath));
            wfConfig.Load();

            wfAttachConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\Attachments.ini", IVGame.GameStartupPath));
            wfAttachConfig.Load();

            if (System.IO.File.Exists(string.Format("{0}\\IVSDKDotNet\\scripts\\ImprovedGunStores\\Attachments.ini", IVGame.GameStartupPath)))
            {
                attachmentConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\ImprovedGunStores\\Attachments.ini", IVGame.GameStartupPath));
            }
            else
                attachmentConfig = wfAttachConfig;
            attachmentConfig.Load();

            LoadSettings(Settings);
            GetWeaponData();

            if (globalRateOfFire)
                RateOfFire.Init(Settings);
            if (blindfireFixes)
                BlindFireFixes.Init(Settings);
            if (allRoundReload)
                ShotgunRel.Init(Settings);
            if (switchWeaponNoReload)
                SwitchWeapNoReload.Init(Settings);
            if (headShotty)
                ShottyHeadShot.Init(Settings);
            if (weaponZoomEnable)
                WeaponZoom.Init(Settings);
            if (fireMode)
                SelectFire.Init(Settings);
            if (gLaunchEnable)
                GLaunchAttachment.Init(Settings);
            if (flameOn)
                Flames.Init(Settings);
            if (equipGun)
                EquipGun.Init(Settings);
            Pickups.Init(Settings);
            if (enableStun)
                Flashbang.Init(Settings);
            if (recoilEnable)
                Recoil.Init(Settings);
            if (tapFireFixEnable)
                TapFireSpreadFix.Init(Settings);
            if (enableHeatseeker)
                LockOn.Init(Settings);
            if (aimCamShakeEnable)
                AimCamShake.Init(Settings);
            if (heliMinigunEnable)
                LessSuckyMiniguns.Init(Settings);
        }
        public static bool InitialChecks()
        {
            if (IS_SCREEN_FADED_OUT()) return false;
            if (IS_PAUSE_MENU_ACTIVE()) return false;
            return true;
        }
        private void Main_Tick(object sender, EventArgs e)
        {
            PlayerPed = IVPed.FromUIntPtr(IVPlayerInfo.FindThePlayerPed());
            PlayerHandle = PlayerPed.GetHandle();
            PlayerIndex = GET_PLAYER_ID();
            PlayerPos = PlayerPed.Matrix.Pos;

            GET_GAME_TIMER(out gTimer);

            if (!InitialChecks())
                return;
            if (PlayerPed == null)
                return;

            CurrEp = GET_CURRENT_EPISODE();

            if (!resetShit)
            {
                currWeap = 0;
                wAnim = "";
                bfAnim = "";

                ReloadInVeh.IngameStart();
                GLaunchAttachment.IngameStart();
                WeaponZoom.IngameStart();
                Pickups.IngameStart();
                //ShoulderSwap.UnInit();

                resetShit = true;
            }

            GET_FRAME_TIME(out frameTime);
            GET_CURRENT_CHAR_WEAPON(PlayerHandle, out currWeap);
            GET_AMMO_IN_CLIP(PlayerHandle, currWeap, out pAmmo);
            GET_AMMO_IN_CHAR_WEAPON(PlayerHandle, currWeap, out aAmmo);
            GET_MAX_AMMO_IN_CLIP(PlayerHandle, currWeap, out mAmmo);
            GET_WEAPONTYPE_SLOT(currWeap, out wSlot);

            if (currWeap > 0)
                LoadWeaponConfig(currWeap);

            PedHelper.GrabAllPeds();
            ObjectHelper.GrabAllObjs();

            if (globalRateOfFire)
                RateOfFire.Tick();
            if (reloadSpeedEnable)
                ReloadSpeed.Tick();
            if (reloadInVehicles)
                ReloadInVeh.Tick();
            if (blindfireFixes)
                BlindFireFixes.Tick();
            if (switchWeaponNoReload)
                SwitchWeapNoReload.Tick();
            if (allRoundReload)
                ShotgunRel.Tick();
            if (headShotty)
                ShottyHeadShot.Tick();
            if (fireMode)
                SelectFire.Tick();
            if (gLaunchEnable)
                GLaunchAttachment.Tick();
            if (flameOn)
                Flames.Tick();
            if (equipGun)
                EquipGun.Tick();

            if (enableStun)
                Flashbang.Tick();
            if (recoilEnable)
                Recoil.Tick();
            if (tapFireFixEnable)
                TapFireSpreadFix.Tick();
            if (enableHeatseeker)
                LockOn.Tick();
            if (heliMinigunEnable)
                LessSuckyMiniguns.Tick();

            Pickups.Tick();
            if (weaponZoomEnable)
                WeaponZoom.Tick();
            if (aimCamShakeEnable)
                AimCamShake.Tick();
            //NightVision.Tick();

            //Silence.Tick();
            //ObjectTest.Tick();
            //ShoulderSwap.Tick();
        }
        private void Main_ProcessCamera(object sender, EventArgs e)
        {
            if (!InitialChecks())
                return;
            WeaponZoom.ProcessCam();
            //ShoulderSwap.ProcessCam();
        }
        public static void LoadWeaponConfig(int weapon)
        {
            int wIndex = Main.weaponData.FindIndex(w => w.ID == currWeap);
            if (wIndex >= 0)
            {
                WeaponData weapData = Main.weaponData[wIndex];

                wAnim = weapData.Anim;
                bfAnim = weapData.BFAnim;
                wReload = weapData.ReloadSpd;
                wOffset = weapData.Offset;
            }
        }
        public static void GetWeaponData()
        {
            string[] weaponDatas = wConfFile.GetSectionNames();

            for (int i = 0; i < weaponDatas.Count(); i++)
            {
                weaponData.Add(new WeaponData());

                int weapID = Int32.Parse(weaponDatas[i].Trim());

                weaponData[i].ID = weapID;
                weaponData[i].Anim = Main.wConfFile.GetValue(weapID.ToString(), "Anim", "");

                string bfAnim = "";
                switch (IVWeaponInfo.GetWeaponInfo((uint)weapID).WeaponSlot)
                {
                    case 2:
                        if (WeaponHelper.TwoHanded(weapID))
                            bfAnim = "ak47_blindfire";
                        else
                            bfAnim = "pistol_blindfire";
                        break;
                    case 3:
                        bfAnim = "shotgun_blindfire";
                        break;
                    case 4:
                        if (WeaponHelper.TwoHanded(weapID))
                            bfAnim = "ak47_blindfire";
                        else
                            bfAnim = "uzi_blindfire";
                        break;
                    case 5:
                        bfAnim = "ak47_blindfire";
                        break;
                    case 6:
                        bfAnim = "rifle_blindfire";
                        break;
                    case 7:
                        bfAnim = "rocket_blindfire";
                        break;
                }

                weaponData[i].BFAnim = bfAnim;
                weaponData[i].FireRate = Main.wConfFile.GetFloat(weapID.ToString(), "NormalROF", 1);
                weaponData[i].DBFireRate = Main.wConfFile.GetFloat(weapID.ToString(), "DrivebyROF", 1);
                weaponData[i].BFFireRate = Main.wConfFile.GetFloat(weapID.ToString(), "InCoverROF", 1);
                weaponData[i].ReloadSpd = Main.wConfFile.GetFloat(weapID.ToString(), "Reload", 1.0f);
                weaponData[i].Offset = Main.wConfFile.GetVector3(weapID.ToString(), "Offset", Vector3.Zero);
                weaponData[i].PickupSound = Main.wConfFile.GetValue(weapID.ToString(), "PickupSound", "");
                weaponData[i].Weight = Main.wConfFile.GetInteger(weapID.ToString(), "WeaponSpace", 0);
                weaponData[i].Color = Main.wConfFile.GetValue(weapID.ToString(), "GlowColor", "");
                weaponData[i].Zoom = Main.wConfFile.GetFloat(weapID.ToString(), "Zoom", 1.0f);

                weaponData[i].RecoilAmpMin = Main.wConfFile.GetFloat(weapID.ToString(), "RecoilAmpMin", 0.0f);
                weaponData[i].RecoilAmpMax = Main.wConfFile.GetFloat(weapID.ToString(), "RecoilAmpMax", 0.0f);
                weaponData[i].RecoilTime = Main.wConfFile.GetInteger(weapID.ToString(), "RecoilTime", 0);

                weaponData[i].RecoilAdd = Main.wConfFile.GetFloat(weapID.ToString(), "AdditionalRecoil", 0.0f);
                weaponData[i].RecoilDecay = Main.wConfFile.GetFloat(weapID.ToString(), "DecayRate", 0.0f);
                weaponData[i].MaxRecoil = Main.wConfFile.GetFloat(weapID.ToString(), "MaximumRecoil", 0.0f);
                weaponData[i].RecoilCrouch = Main.wConfFile.GetFloat(weapID.ToString(), "CrouchMultiplier", 0.0f);

                if (weaponData[i].ID >= numOfWeapIDs)
                    numOfWeapIDs = weaponData[i].ID;
            }
        }
        private void LoadSettings(SettingsFile settings)
        {
            wConfFile = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\WeaponConfigs.ini", IVGame.GameStartupPath));
            wConfFile.Load();
            //numOfWeapIDs = settings.GetInteger("MAIN", "NumOfWeaponIDs", 60);
            globalRateOfFire = settings.GetBoolean("MAIN", "GlobalROF", false);
            reloadSpeedEnable = settings.GetBoolean("RELOADS", "CustomReloadSpeed", false);
            reloadInVehicles = settings.GetBoolean("RELOADS", "ReloadInVehicles", false);
            blindfireFixes = settings.GetBoolean("BLINDFIRING", "Enable", false);
            switchWeaponNoReload = settings.GetBoolean("RELOADS", "SwitchWeaponNoReload", false);

            fireMode = settings.GetBoolean("SELECT FIRE", "SelectFire", false);
            LoseAmmoInMag = settings.GetBoolean("RELOADS", "LoseAmmoInMag", false);
            allRoundReload = settings.GetBoolean("RELOADS", "AllRoundReload", false);
            headShotty = settings.GetBoolean("OTHER", "LethalShotgunHeadshot", false);
            tapFireFixEnable = settings.GetBoolean("RECOIL & BULLETSPREAD", "TapFireBulletspreadFix", false);
            gLaunchEnable = settings.GetBoolean("ATTACHMENTS", "GrenadeLauncherAttachment", false);
            flameOn = settings.GetBoolean("FLAMETHROWER", "FlameEnable", false);
            equipGun = settings.GetBoolean("OTHER", "HolsteredWeaponsOnPlayer", false);
            enableStun = settings.GetBoolean("FLASHBANG", "StunGrenadeEnable", false);
            enableHeatseeker = settings.GetBoolean("HEATSEEKER", "HeatseekerEnable", false);
            recoilEnable = settings.GetBoolean("RECOIL & BULLETSPREAD", "WeaponRecoil", false);
            heliMinigunEnable = settings.GetBoolean("CUSTOM VEHICLE GUN", "Enable", false);
            weaponZoomEnable = settings.GetBoolean("OTHER", "WeaponZoom", false);
            aimCamShakeEnable = settings.GetBoolean("AIM CAMERA SHAKE", "Enable", false);
            pickupEnable = settings.GetBoolean("PICKUPS", "RevampedPickups", false);
        }
    }
}
