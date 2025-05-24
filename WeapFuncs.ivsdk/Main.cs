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
        public static bool SelectFire;
        public static bool ShowFireModeText;
        public static bool SwitchWeaponNoReload;
        public static bool PressToFire;
        public static bool LoseAmmoInMag;
        public static int ShotsPerBurst;
        public static GameKey SelectFireCtrl;
        public static bool AllRoundReload;
        public static bool HeadShotty;

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

        public static string PistolAnim;
        public static string SilencedAnim;
        public static string DeagleAnim;
        public static string PumpShotAnim;
        public static string CombatShotAnim;
        public static string UziAnim;
        public static string MP5Anim;
        public static string AK47Anim;
        public static string M4Anim;
        public static string SnipAnim;
        public static string PsgAnim;
        public static string RpgAnim;
        public static string FThrowerAnim;
        public static string AutoPAnim;
        public static string SawnOffAnim;
        public static string AssaultShotAnim;
        public static string GrndLaunchAnim;
        public static string Pistol44Anim;
        public static string AA12Anim;
        public static string AA12ExpAnim;
        public static string P90Anim;
        public static string GoldUziAnim;
        public static string M249Anim;
        public static string AdvSnipAnim;
        public static string Episodic22Anim;
        public static string Episodic23Anim;
        public static string Episodic24Anim;
        public static string Episodic3Anim;
        public static string Addon1Anim;
        public static string Addon2Anim;
        public static string Addon3Anim;
        public static string Addon4Anim;
        public static string Addon5Anim;
        public static string Addon6Anim;
        public static string Addon7Anim;
        public static string Addon8Anim;
        public static string Addon9Anim;
        public static string Addon10Anim;
        public static string Addon11Anim;
        public static string Addon12Anim;
        public static string Addon13Anim;
        public static string Addon14Anim;
        public static string Addon15Anim;
        public static string Addon16Anim;
        public static string Addon17Anim;
        public static string Addon18Anim;
        public static string Addon19Anim;
        public static string Addon20Anim;
        public static string Unused0Anim;
        public static string BatAnim;
        public static string PoolcueAnim;
        public static string KnifeAnim;
        public static string GrenadeAnim;
        public static string MolotovAnim;
        public static string RocketAnim;
        public static string Episodic4Anim;
        public static string Episodic5Anim;
        public static string Episodic8Anim;
        public static string Episodic16Anim;
        public static string Episodic17Anim;
        public static string Episodic18Anim;
        public static string Episodic19Anim;
        public static string Episodic20Anim;
        public static string Episodic21Anim;
        public static string CameraAnim;
        public static string ObjectAnim;
        //public static WeaponHandling TheWeaponHandler;

        static float PistolReload;
        static float SilencedReload;
        static float DeagleReload;
        static float PumpShotReload;
        static float CombatShotReload;
        static float UziReload;
        static float MP5Reload;
        static float AK47Reload;
        static float M4Reload;
        static float SnipReload;
        static float PsgReload;
        static float RpgReload;
        static float FThrowerReload;
        static float AutoPReload;
        static float SawnOffReload;
        static float AssaultShotReload;
        static float GrndLaunchReload;
        static float Pistol44Reload;
        static float AA12Reload;
        static float AA12ExpReload;
        static float P90Reload;
        static float GoldUziReload;
        static float M249Reload;
        static float AdvSnipReload;
        static float Episodic3Reload;
        static float Episodic22Reload;
        static float Episodic23Reload;
        static float Episodic24Reload;
        static float Addon1Reload;
        static float Addon2Reload;
        static float Addon3Reload;
        static float Addon4Reload;
        static float Addon5Reload;
        static float Addon6Reload;
        static float Addon7Reload;
        static float Addon8Reload;
        static float Addon9Reload;
        static float Addon10Reload;
        static float Addon11Reload;
        static float Addon12Reload;
        static float Addon13Reload;
        static float Addon14Reload;
        static float Addon15Reload;
        static float Addon16Reload;
        static float Addon17Reload;
        static float Addon18Reload;
        static float Addon19Reload;
        static float Addon20Reload;
        static float Unused0Reload;
        static float BatReload;
        static float PoolcueReload;
        static float KnifeReload;
        static float GrenadeReload;
        static float MolotovReload;
        static float RocketReload;
        static float Episodic4Reload;
        static float Episodic5Reload;
        static float Episodic8Reload;
        static float Episodic16Reload;
        static float Episodic17Reload;
        static float Episodic18Reload;
        static float Episodic19Reload;
        static float Episodic20Reload;
        static float Episodic21Reload;
        static float CameraReload;
        static float ObjectReload;

        static Vector3 PistolOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 SilencedOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 DeagleOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 PumpShotOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 CombatShotOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 UziOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 MP5Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 AK47Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 M4Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 SnipOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 PsgOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 RpgOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 FThrowerOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 AutoPOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 SawnOffOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 AssaultShotOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 GrndLaunchOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Pistol44Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 AA12Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 AA12ExpOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 P90Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 GoldUziOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 M249Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 AdvSnipOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic3Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic22Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic23Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic24Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon1Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon2Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon3Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon4Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon5Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon6Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon7Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon8Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon9Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon10Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon11Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon12Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon13Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon14Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon15Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon16Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon17Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon18Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon19Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Addon20Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Unused0Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 BatOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 PoolcueOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 KnifeOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 GrenadeOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 MolotovOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 RocketOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic4Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic5Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic8Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic16Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic17Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic18Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic19Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic20Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 Episodic21Offset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 CameraOffset = new Vector3(0.0f, 0.0f, 0.0f);
        static Vector3 ObjectOffset = new Vector3(0.0f, 0.0f, 0.0f);
        public static IVPed PlayerPed { get; set; }
        public static uint PlayerIndex { get; set; }
        public static int PlayerHandle { get; set; }
        public static Vector3 PlayerPos { get; set; }
        public Main()
        {
            Initialized += Main_Initialized;
            Tick += Main_Tick;
            //ProcessCamera += Main_ProcessCamera;
            //TheWeaponHandler = new WeaponHandling();
        }

        private void Main_Initialized(object sender, EventArgs e)
        {
            LoadSettings(Settings);
            if (GlobalRateOfFire)
                RateOfFire.LoadSettings(Settings);
            BlindFireFixes.Init(Settings);
            if (SemiAutoShotgunBlindfire)
                ShotgunBlindfireFix.Init(Settings);
            if (NPCSemiAutoShotgunBlindfire)
                NPCSemiAutoShot.Init(Settings);
            BlindFireFixes.Init(Settings);
            WeapFuncs.Init(Settings);
            SwitchWeapNoReload.Init();
            if (AllRoundReload)
                ShotgunRel.Init(Settings);
            if (HeadShotty)
                ShottyHeadShot.Init(Settings);
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            PlayerPed = IVPed.FromUIntPtr(IVPlayerInfo.FindThePlayerPed());
            PlayerHandle = PlayerPed.GetHandle();
            PlayerPos = PlayerPed.Matrix.Pos;

            if (PlayerPed == null)
                return;

            GET_CURRENT_CHAR_WEAPON(PlayerHandle, out currWeap);
            GET_AMMO_IN_CLIP(PlayerHandle, currWeap, out pAmmo);
            GET_AMMO_IN_CHAR_WEAPON(PlayerHandle, currWeap, out aAmmo);
            GET_MAX_AMMO_IN_CLIP(PlayerHandle, currWeap, out mAmmo);
            GET_WEAPONTYPE_SLOT(currWeap, out wSlot);
            bool twoHanded = (IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.TreatAsTwoHandedInCover || IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.TwoHanded);

            switch (currWeap)
            {
                case 1:
                    WeapAnim = BatAnim;
                    weapReload = BatReload;
                    WeapOffset = BatOffset;
                    break;
                case 2:
                    WeapAnim = PoolcueAnim;
                    weapReload = PoolcueReload;
                    WeapOffset = PoolcueOffset;
                    break;
                case 3:
                    WeapAnim = KnifeAnim;
                    weapReload = KnifeReload;
                    WeapOffset = KnifeOffset;
                    break;
                case 4:
                    WeapAnim = GrenadeAnim;
                    weapReload = GrenadeReload;
                    WeapOffset = GrenadeOffset;
                    break;
                case 5:
                    WeapAnim = MolotovAnim;
                    weapReload = MolotovReload;
                    WeapOffset = MolotovOffset;
                    break;
                case 6:
                    WeapAnim = RocketAnim;
                    weapReload = RocketReload;
                    WeapOffset = RocketOffset;
                    break;
                case 7:
                    WeapAnim = PistolAnim;
                    weapReload = PistolReload;
                    WeapOffset = PistolOffset;
                    break;
                case 8:
                    WeapAnim = Unused0Anim;
                    weapReload = Unused0Reload;
                    WeapOffset = Unused0Offset;
                    break;
                case 9:
                    WeapAnim = DeagleAnim;
                    weapReload = DeagleReload;
                    WeapOffset = DeagleOffset;
                    break;
                case 10:
                    WeapAnim = PumpShotAnim;
                    weapReload = PumpShotReload;
                    WeapOffset = PumpShotOffset;
                    break;
                case 11:
                    WeapAnim = CombatShotAnim;
                    weapReload = CombatShotReload;
                    WeapOffset = CombatShotOffset;
                    break;
                case 12:
                    WeapAnim = UziAnim;
                    weapReload = UziReload;
                    WeapOffset = UziOffset;
                    break;
                case 13:
                    WeapAnim = MP5Anim;
                    weapReload = MP5Reload;
                    WeapOffset = MP5Offset;
                    break;
                case 14:
                    WeapAnim = AK47Anim;
                    weapReload = AK47Reload;
                    WeapOffset = AK47Offset;
                    break;
                case 15:
                    WeapAnim = M4Anim;
                    weapReload = M4Reload;
                    WeapOffset = M4Offset;
                    break;
                case 16:
                    WeapAnim = PsgAnim;
                    weapReload = PsgReload;
                    WeapOffset = PsgOffset;
                    break;
                case 17:
                    WeapAnim = SnipAnim;
                    weapReload = SnipReload;
                    WeapOffset = SnipOffset;
                    break;
                case 18:
                    WeapAnim = RpgAnim;
                    weapReload = RpgReload;
                    WeapOffset = RpgOffset;
                    break;
                case 19:
                    WeapAnim = FThrowerAnim;
                    weapReload = FThrowerReload;
                    WeapOffset = FThrowerOffset;
                    break;
                case 20:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 21:
                    WeapAnim = GrndLaunchAnim;
                    weapReload = GrndLaunchReload;
                    WeapOffset = GrndLaunchOffset;
                    break;
                case 22:
                    WeapAnim = AssaultShotAnim;
                    weapReload = AssaultShotReload;
                    WeapOffset = AssaultShotOffset;
                    break;
                case 23:
                    WeapAnim = Episodic3Anim;
                    weapReload = Episodic3Reload;
                    WeapOffset = Episodic3Offset;
                    break;
                case 24:
                    WeapAnim = Episodic4Anim;
                    weapReload = Episodic4Reload;
                    WeapOffset = Episodic4Offset;
                    break;
                case 25:
                    WeapAnim = Episodic5Anim;
                    weapReload = Episodic5Reload;
                    WeapOffset = Episodic5Offset;
                    break;
                case 26:
                    WeapAnim = SawnOffAnim;
                    weapReload = SawnOffReload;
                    WeapOffset = SawnOffOffset;
                    break;
                case 27:
                    WeapAnim = AutoPAnim;
                    weapReload = AutoPReload;
                    WeapOffset = AutoPOffset;
                    break;
                case 28:
                    WeapAnim = Episodic8Anim;
                    weapReload = Episodic8Reload;
                    WeapOffset = Episodic8Offset;
                    break;
                case 29:
                    WeapAnim = Pistol44Anim;
                    weapReload = Pistol44Reload;
                    WeapOffset = Pistol44Offset;
                    break;
                case 30:
                    WeapAnim = AA12ExpAnim;
                    weapReload = AA12ExpReload;
                    WeapOffset = AA12ExpOffset;
                    break;
                case 31:
                    WeapAnim = AA12Anim;
                    weapReload = AA12Reload;
                    WeapOffset = AA12Offset;
                    break;
                case 32:
                    WeapAnim = P90Anim;
                    weapReload = P90Reload;
                    WeapOffset = P90Offset;
                    break;
                case 33:
                    WeapAnim = GoldUziAnim;
                    weapReload = GoldUziReload;
                    WeapOffset = GoldUziOffset;
                    break;
                case 34:
                    WeapAnim = M249Anim;
                    weapReload = M249Reload;
                    WeapOffset = M249Offset;
                    break;
                case 35:
                    WeapAnim = AdvSnipAnim;
                    weapReload = AdvSnipReload;
                    WeapOffset = AdvSnipOffset;
                    break;
                case 36:
                    WeapAnim = Episodic16Anim;
                    weapReload = Episodic16Reload;
                    WeapOffset = Episodic16Offset;
                    break;
                case 37:
                    WeapAnim = Episodic17Anim;
                    weapReload = Episodic17Reload;
                    WeapOffset = Episodic17Offset;
                    break;
                case 38:
                    WeapAnim = Episodic18Anim;
                    weapReload = Episodic18Reload;
                    WeapOffset = Episodic18Offset;
                    break;
                case 39:
                    WeapAnim = Episodic19Anim;
                    weapReload = Episodic19Reload;
                    WeapOffset = Episodic19Offset;
                    break;
                case 40:
                    WeapAnim = Episodic20Anim;
                    weapReload = Episodic20Reload;
                    WeapOffset = Episodic20Offset;
                    break;
                case 41:
                    WeapAnim = Episodic21Anim;
                    weapReload = Episodic21Reload;
                    WeapOffset = Episodic21Offset;
                    break;
                case 42:
                    WeapAnim = Episodic22Anim;
                    weapReload = Episodic22Reload;
                    WeapOffset = Episodic22Offset;
                    break;
                case 43:
                    WeapAnim = Episodic23Anim;
                    weapReload = Episodic23Reload;
                    WeapOffset = Episodic23Offset;
                    break;
                case 44:
                    WeapAnim = Episodic24Anim;
                    weapReload = Episodic24Reload;
                    WeapOffset = Episodic24Offset;
                    break;
                case 45:
                    WeapAnim = CameraAnim;
                    weapReload = CameraReload;
                    WeapOffset = CameraOffset;
                    break;
                case 46:
                    WeapAnim = ObjectAnim;
                    weapReload = ObjectReload;
                    WeapOffset = ObjectOffset;
                    break;
                case 47:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 48:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 49:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 50:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 51:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 52:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 53:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 54:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 55:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 56:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 57:
                    WeapAnim = SilencedAnim;
                    weapReload = SilencedReload;
                    WeapOffset = SilencedOffset;
                    break;
                case 58:
                    WeapAnim = Addon1Anim;
                    weapReload = Addon1Reload;
                    WeapOffset = Addon1Offset;
                    break;
                case 59:
                    WeapAnim = Addon2Anim;
                    weapReload = Addon2Reload;
                    WeapOffset = Addon2Offset;
                    break;
                case 60:
                    WeapAnim = Addon3Anim;
                    weapReload = Addon3Reload;
                    WeapOffset = Addon3Offset;
                    break;
                case 61:
                    WeapAnim = Addon4Anim;
                    weapReload = Addon4Reload;
                    WeapOffset = Addon4Offset;
                    break;
                case 62:
                    WeapAnim = Addon5Anim;
                    weapReload = Addon5Reload;
                    WeapOffset = Addon5Offset;
                    break;
                case 63:
                    WeapAnim = Addon6Anim;
                    weapReload = Addon6Reload;
                    WeapOffset = Addon6Offset;
                    break;
                case 64:
                    WeapAnim = Addon7Anim;
                    weapReload = Addon7Reload;
                    WeapOffset = Addon7Offset;
                    break;
                case 65:
                    WeapAnim = Addon8Anim;
                    weapReload = Addon8Reload;
                    WeapOffset = Addon8Offset;
                    break;
                case 66:
                    WeapAnim = Addon9Anim;
                    weapReload = Addon9Reload;
                    WeapOffset = Addon9Offset;
                    break;
                case 67:
                    WeapAnim = Addon10Anim;
                    weapReload = Addon10Reload;
                    WeapOffset = Addon10Offset;
                    break;
                case 68:
                    WeapAnim = Addon11Anim;
                    weapReload = Addon11Reload;
                    WeapOffset = Addon11Offset;
                    break;
                case 69:
                    WeapAnim = Addon12Anim;
                    weapReload = Addon12Reload;
                    WeapOffset = Addon12Offset;
                    break;
                case 70:
                    WeapAnim = Addon13Anim;
                    weapReload = Addon13Reload;
                    WeapOffset = Addon13Offset;
                    break;
                case 71:
                    WeapAnim = Addon14Anim;
                    weapReload = Addon14Reload;
                    WeapOffset = Addon14Offset;
                    break;
                case 72:
                    WeapAnim = Addon15Anim;
                    weapReload = Addon15Reload;
                    WeapOffset = Addon15Offset;
                    break;
                case 73:
                    WeapAnim = Addon16Anim;
                    weapReload = Addon16Reload;
                    WeapOffset = Addon16Offset;
                    break;
                case 74:
                    WeapAnim = Addon17Anim;
                    weapReload = Addon17Reload;
                    WeapOffset = Addon17Offset;
                    break;
                case 75:
                    WeapAnim = Addon18Anim;
                    weapReload = Addon18Reload;
                    WeapOffset = Addon18Offset;
                    break;
                case 76:
                    WeapAnim = Addon19Anim;
                    weapReload = Addon19Reload;
                    WeapOffset = Addon19Offset;
                    break;
                case 77:
                    WeapAnim = Addon20Anim;
                    weapReload = Addon20Reload;
                    WeapOffset = Addon20Offset;
                    break;
            }

            switch (wSlot)
            {
                case 2:
                    if (twoHanded)
                        BFAnim = "ak47_blindfire";
                    else
                        BFAnim = "pistol_blindfire";
                    break;
                case 3:
                    BFAnim = "shotgun_blindfire";
                    break;
                case 4:
                    if (twoHanded)
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

            PedHelper.GrabAllPeds();
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
        private void Main_ProcessCamera(object sender, EventArgs e)
        {
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
        private void LoadSettings(SettingsFile settings)
        {
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
            SelectFire = settings.GetBoolean("SELECT FIRE", "SelectFire", false);
            ShowFireModeText = settings.GetBoolean("SELECT FIRE", "ShowFireModeText", false);
            ShotsPerBurst = settings.GetInteger("SELECT FIRE", "ShotsPerBurst", 3);
            SwitchWeaponNoReload = settings.GetBoolean("RELOADS", "SwitchWeaponNoReload", false);
            PressToFire = settings.GetBoolean("SELECT FIRE", "PressToFire", false);
            LoseAmmoInMag = settings.GetBoolean("RELOADS", "LoseAmmoInMag", false);
            SelectFireCtrl = (GameKey)settings.GetInteger("SELECT FIRE", "SelectFireControl", 23);
            AllRoundReload = settings.GetBoolean("RELOADS", "AllRoundReload", false);
            HeadShotty = settings.GetBoolean("OTHER", "LethalShotgunHeadshot", false);

            PistolAnim = settings.GetValue("GLOCK", "NormalAnim", "");
            SilencedAnim = settings.GetValue("SILENCED", "NormalAnim", "");
            DeagleAnim = settings.GetValue("DEAGLE", "NormalAnim", "");
            PumpShotAnim = settings.GetValue("PUMP SHOTGUN", "NormalAnim", "");
            CombatShotAnim = settings.GetValue("SEMIAUTO SHOTGUN", "NormalAnim", "");
            UziAnim = settings.GetValue("MICRO UZI", "NormalAnim", "");
            MP5Anim = settings.GetValue("MP5", "NormalAnim", "");
            AK47Anim = settings.GetValue("AK47", "NormalAnim", "");
            M4Anim = settings.GetValue("M4", "NormalAnim", "");
            PsgAnim = settings.GetValue("SEMIAUTO SNIPER", "NormalAnim", "");
            SnipAnim = settings.GetValue("BOLTACTION SNIPER", "NormalAnim", "");
            RpgAnim = settings.GetValue("RPG", "NormalAnim", "");
            FThrowerAnim = settings.GetValue("FTHROWER", "NormalAnim", "");
            AutoPAnim = settings.GetValue("FULLAUTO PISTOL", "NormalAnim", "");
            SawnOffAnim = settings.GetValue("SAWNOFF SHOTGUN", "NormalAnim", "");
            AssaultShotAnim = settings.GetValue("ASSAULT SHOTGUN", "NormalAnim", "");
            GrndLaunchAnim = settings.GetValue("GRENADE LAUNCHER", "NormalAnim", "");
            Pistol44Anim = settings.GetValue("PISTOL 44", "NormalAnim", "");
            AA12Anim = settings.GetValue("AA12", "NormalAnim", "");
            AA12ExpAnim = settings.GetValue("AA12 EXP", "NormalAnim", "");
            P90Anim = settings.GetValue("P90", "NormalAnim", "");
            GoldUziAnim = settings.GetValue("GOLD UZI", "NormalAnim", "");
            M249Anim = settings.GetValue("M249", "NormalAnim", "");
            AdvSnipAnim = settings.GetValue("ADV SNIPER", "NormalAnim", "");
            Episodic22Anim = settings.GetValue("EPISODIC 22", "NormalAnim", "");
            Episodic23Anim = settings.GetValue("EPISODIC 23", "NormalAnim", "");
            Episodic24Anim = settings.GetValue("EPISODIC 24", "NormalAnim", "");
            Episodic3Anim = settings.GetValue("EPISODIC 3", "NormalAnim", "");
            Addon1Anim = settings.GetValue("ADDONWEAPON 1", "NormalAnim", "");
            Addon2Anim = settings.GetValue("ADDONWEAPON 2", "NormalAnim", "");
            Addon3Anim = settings.GetValue("ADDONWEAPON 3", "NormalAnim", "");
            Addon4Anim = settings.GetValue("ADDONWEAPON 4", "NormalAnim", "");
            Addon5Anim = settings.GetValue("ADDONWEAPON 5", "NormalAnim", "");
            Addon6Anim = settings.GetValue("ADDONWEAPON 6", "NormalAnim", "");
            Addon7Anim = settings.GetValue("ADDONWEAPON 7", "NormalAnim", "");
            Addon8Anim = settings.GetValue("ADDONWEAPON 8", "NormalAnim", "");
            Addon9Anim = settings.GetValue("ADDONWEAPON 9", "NormalAnim", "");
            Addon10Anim = settings.GetValue("ADDONWEAPON 10", "NormalAnim", "");
            Addon11Anim = settings.GetValue("ADDONWEAPON 11", "NormalAnim", "");
            Addon12Anim = settings.GetValue("ADDONWEAPON 12", "NormalAnim", "");
            Addon13Anim = settings.GetValue("ADDONWEAPON 13", "NormalAnim", "");
            Addon14Anim = settings.GetValue("ADDONWEAPON 14", "NormalAnim", "");
            Addon15Anim = settings.GetValue("ADDONWEAPON 15", "NormalAnim", "");
            Addon16Anim = settings.GetValue("ADDONWEAPON 16", "NormalAnim", "");
            Addon17Anim = settings.GetValue("ADDONWEAPON 17", "NormalAnim", "");
            Addon18Anim = settings.GetValue("ADDONWEAPON 18", "NormalAnim", "");
            Addon19Anim = settings.GetValue("ADDONWEAPON 19", "NormalAnim", "");
            Addon20Anim = settings.GetValue("ADDONWEAPON 20", "NormalAnim", "");
            Unused0Anim = settings.GetValue("UNUSED0", "NormalAnim", "");
            Episodic4Anim = settings.GetValue("EPISODIC 4", "NormalAnim", "");
            Episodic5Anim = settings.GetValue("EPISODIC 5", "NormalAnim", "");
            Episodic8Anim = settings.GetValue("EPISODIC 8", "NormalAnim", "");
            Episodic16Anim = settings.GetValue("EPISODIC 16", "NormalAnim", "");
            Episodic17Anim = settings.GetValue("EPISODIC 17", "NormalAnim", "");
            Episodic18Anim = settings.GetValue("EPISODIC 18", "NormalAnim", "");
            Episodic19Anim = settings.GetValue("EPISODIC 19", "NormalAnim", "");
            Episodic20Anim = settings.GetValue("EPISODIC 20", "NormalAnim", "");
            Episodic21Anim = settings.GetValue("EPISODIC 21", "NormalAnim", "");
            CameraAnim = settings.GetValue("CAMERA", "NormalAnim", "");
            ObjectAnim = settings.GetValue("OBJECT", "NormalAnim", "");
            BatAnim = settings.GetValue("BASEBALL BAT", "NormalAnim", "");
            PoolcueAnim = settings.GetValue("POOLCUE", "NormalAnim", "");
            KnifeAnim = settings.GetValue("KNIFE", "NormalAnim", "");
            GrenadeAnim = settings.GetValue("GRENADE", "NormalAnim", "");
            MolotovAnim = settings.GetValue("MOLOTOV", "NormalAnim", "");
            RocketAnim = settings.GetValue("ROCKET", "NormalAnim", "");

            PistolReload = settings.GetFloat("GLOCK", "Reload", 1.0f);
            SilencedReload = settings.GetFloat("SILENCED", "Reload", 1.0f);
            DeagleReload = settings.GetFloat("DEAGLE", "Reload", 1.0f);
            PumpShotReload = settings.GetFloat("PUMP SHOTGUN", "Reload", 1.0f);
            CombatShotReload = settings.GetFloat("SEMIAUTO SHOTGUN", "Reload", 1.0f);
            UziReload = settings.GetFloat("MICRO UZI", "Reload", 1.0f);
            MP5Reload = settings.GetFloat("MP5", "Reload", 1.0f);
            AK47Reload = settings.GetFloat("AK47", "Reload", 1.0f);
            M4Reload = settings.GetFloat("M4", "Reload", 1.0f);
            SnipReload = settings.GetFloat("BOLTACTION SNIPER", "Reload", 1.0f);
            PsgReload = settings.GetFloat("SEMIAUTO SNIPER", "Reload", 1.0f);
            RpgReload = settings.GetFloat("RPG", "Reload", 1.0f);
            AutoPReload = settings.GetFloat("FULLAUTO PISTOL", "Reload", 1.0f);
            SawnOffReload = settings.GetFloat("SAWNOFF SHOTGUN", "Reload", 1.0f);
            AssaultShotReload = settings.GetFloat("ASSAULT SHOTGUN", "Reload", 1.0f);
            GrndLaunchReload = settings.GetFloat("GRENADE LAUNCHER", "Reload", 1.0f);
            Pistol44Reload = settings.GetFloat("PISTOL 44", "Reload", 1.0f);
            AA12Reload = settings.GetFloat("AA12", "Reload", 1.0f);
            AA12ExpReload = settings.GetFloat("AA12 EXP", "Reload", 1.0f);
            P90Reload = settings.GetFloat("P90", "Reload", 1.0f);
            GoldUziReload = settings.GetFloat("GOLD UZI", "Reload", 1.0f);
            M249Reload = settings.GetFloat("M249", "Reload", 1.0f);
            AdvSnipReload = settings.GetFloat("ADV SNIPER", "Reload", 1.0f);
            FThrowerReload = settings.GetFloat("FTHROWER", "Reload", 1.0f);
            Episodic3Reload = settings.GetFloat("EPISODIC 3", "Reload", 1.0f);
            Episodic22Reload = settings.GetFloat("EPISODIC 22", "Reload", 1.0f);
            Episodic23Reload = settings.GetFloat("EPISODIC 23", "Reload", 1.0f);
            Episodic24Reload = settings.GetFloat("EPISODIC 24", "Reload", 1.0f);
            Addon1Reload = settings.GetFloat("ADDONWEAPON 1", "Reload", 1.0f);
            Addon2Reload = settings.GetFloat("ADDONWEAPON 2", "Reload", 1.0f);
            Addon3Reload = settings.GetFloat("ADDONWEAPON 3", "Reload", 1.0f);
            Addon4Reload = settings.GetFloat("ADDONWEAPON 4", "Reload", 1.0f);
            Addon5Reload = settings.GetFloat("ADDONWEAPON 5", "Reload", 1.0f);
            Addon6Reload = settings.GetFloat("ADDONWEAPON 6", "Reload", 1.0f);
            Addon7Reload = settings.GetFloat("ADDONWEAPON 7", "Reload", 1.0f);
            Addon8Reload = settings.GetFloat("ADDONWEAPON 8", "Reload", 1.0f);
            Addon9Reload = settings.GetFloat("ADDONWEAPON 9", "Reload", 1.0f);
            Addon10Reload = settings.GetFloat("ADDONWEAPON 10", "Reload", 1.0f);
            Addon11Reload = settings.GetFloat("ADDONWEAPON 11", "Reload", 1.0f);
            Addon12Reload = settings.GetFloat("ADDONWEAPON 12", "Reload", 1.0f);
            Addon13Reload = settings.GetFloat("ADDONWEAPON 13", "Reload", 1.0f);
            Addon14Reload = settings.GetFloat("ADDONWEAPON 14", "Reload", 1.0f);
            Addon15Reload = settings.GetFloat("ADDONWEAPON 15", "Reload", 1.0f);
            Addon16Reload = settings.GetFloat("ADDONWEAPON 16", "Reload", 1.0f);
            Addon17Reload = settings.GetFloat("ADDONWEAPON 17", "Reload", 1.0f);
            Addon18Reload = settings.GetFloat("ADDONWEAPON 18", "Reload", 1.0f);
            Addon19Reload = settings.GetFloat("ADDONWEAPON 19", "Reload", 1.0f);
            Addon20Reload = settings.GetFloat("ADDONWEAPON 20", "Reload", 1.0f);
            Unused0Reload = settings.GetFloat("UNUSED0", "Reload", 1.0f);
            Episodic4Reload = settings.GetFloat("EPISODIC 4", "Reload", 1.0f);
            Episodic5Reload = settings.GetFloat("EPISODIC 5", "Reload", 1.0f);
            Episodic8Reload = settings.GetFloat("EPISODIC 8", "Reload", 1.0f);
            Episodic16Reload = settings.GetFloat("EPISODIC 16", "Reload", 1.0f);
            Episodic17Reload = settings.GetFloat("EPISODIC 17", "Reload", 1.0f);
            Episodic18Reload = settings.GetFloat("EPISODIC 18", "Reload", 1.0f);
            Episodic19Reload = settings.GetFloat("EPISODIC 19", "Reload", 1.0f);
            Episodic20Reload = settings.GetFloat("EPISODIC 20", "Reload", 1.0f);
            Episodic21Reload = settings.GetFloat("EPISODIC 21", "Reload", 1.0f);
            CameraReload = settings.GetFloat("CAMERA", "Reload", 1.0f);
            ObjectReload = settings.GetFloat("OBJECT", "Reload", 1.0f);
            BatReload = settings.GetFloat("BASEBALL BAT", "Reload", 1.0f);
            PoolcueReload = settings.GetFloat("POOLCUE", "Reload", 1.0f);
            KnifeReload = settings.GetFloat("KNIFE", "Reload", 1.0f);
            GrenadeReload = settings.GetFloat("GRENADE", "Reload", 1.0f);
            MolotovReload = settings.GetFloat("MOLOTOV", "Reload", 1.0f);
            RocketReload = settings.GetFloat("ROCKET", "Reload", 1.0f);

            PistolOffset = settings.GetVector3("GLOCK", "Offset", new Vector3(0f, 0f, 0f));
            SilencedOffset = settings.GetVector3("SILENCED", "Offset", new Vector3(0f, 0f, 0f));
            DeagleOffset = settings.GetVector3("DEAGLE", "Offset", new Vector3(0f, 0f, 0f));
            PumpShotOffset = settings.GetVector3("PUMP SHOTGUN", "Offset", new Vector3(0f, 0f, 0f));
            CombatShotOffset = settings.GetVector3("SEMIAUTO SHOTGUN", "Offset", new Vector3(0f, 0f, 0f));
            UziOffset = settings.GetVector3("MICRO UZI", "Offset", new Vector3(0f, 0f, 0f));
            MP5Offset = settings.GetVector3("MP5", "Offset", new Vector3(0f, 0f, 0f));
            AK47Offset = settings.GetVector3("AK47", "Offset", new Vector3(0f, 0f, 0f));
            M4Offset = settings.GetVector3("M4", "Offset", new Vector3(0f, 0f, 0f));
            SnipOffset = settings.GetVector3("BOLTACTION SNIPER", "Offset", new Vector3(0f, 0f, 0f));
            PsgOffset = settings.GetVector3("SEMIAUTO SNIPER", "Offset", new Vector3(0f, 0f, 0f));
            RpgOffset = settings.GetVector3("RPG", "Offset", new Vector3(0f, 0f, 0f));
            AutoPOffset = settings.GetVector3("FULLAUTO PISTOL", "Offset", new Vector3(0f, 0f, 0f));
            SawnOffOffset = settings.GetVector3("SAWNOFF SHOTGUN", "Offset", new Vector3(0f, 0f, 0f));
            AssaultShotOffset = settings.GetVector3("ASSAULT SHOTGUN", "Offset", new Vector3(0f, 0f, 0f));
            GrndLaunchOffset = settings.GetVector3("GRENADE LAUNCHER", "Offset", new Vector3(0f, 0f, 0f));
            Pistol44Offset = settings.GetVector3("PISTOL 44", "Offset", new Vector3(0f, 0f, 0f));
            AA12Offset = settings.GetVector3("AA12", "Offset", new Vector3(0f, 0f, 0f));
            AA12ExpOffset = settings.GetVector3("AA12 EXP", "Offset", new Vector3(0f, 0f, 0f));
            P90Offset = settings.GetVector3("P90", "Offset", new Vector3(0f, 0f, 0f));
            GoldUziOffset = settings.GetVector3("GOLD UZI", "Offset", new Vector3(0f, 0f, 0f));
            M249Offset = settings.GetVector3("M249", "Offset", new Vector3(0f, 0f, 0f));
            AdvSnipOffset = settings.GetVector3("ADV SNIPER", "Offset", new Vector3(0f, 0f, 0f));
            FThrowerOffset = settings.GetVector3("FTHROWER", "Offset", new Vector3(0f, 0f, 0f));
            Episodic3Offset = settings.GetVector3("EPISODIC 3", "Offset", new Vector3(0f, 0f, 0f));
            Episodic22Offset = settings.GetVector3("EPISODIC 22", "Offset", new Vector3(0f, 0f, 0f));
            Episodic23Offset = settings.GetVector3("EPISODIC 23", "Offset", new Vector3(0f, 0f, 0f));
            Episodic24Offset = settings.GetVector3("EPISODIC 24", "Offset", new Vector3(0f, 0f, 0f));
            Unused0Offset = settings.GetVector3("UNUSED0", "Offset", new Vector3(0f, 0f, 0f));
            Addon1Offset = settings.GetVector3("ADDONWEAPON 1", "Offset", new Vector3(0f, 0f, 0f));
            Addon2Offset = settings.GetVector3("ADDONWEAPON 2", "Offset", new Vector3(0f, 0f, 0f));
            Addon3Offset = settings.GetVector3("ADDONWEAPON 3", "Offset", new Vector3(0f, 0f, 0f));
            Addon4Offset = settings.GetVector3("ADDONWEAPON 4", "Offset", new Vector3(0f, 0f, 0f));
            Addon5Offset = settings.GetVector3("ADDONWEAPON 5", "Offset", new Vector3(0f, 0f, 0f));
            Addon6Offset = settings.GetVector3("ADDONWEAPON 6", "Offset", new Vector3(0f, 0f, 0f));
            Addon7Offset = settings.GetVector3("ADDONWEAPON 7", "Offset", new Vector3(0f, 0f, 0f));
            Addon8Offset = settings.GetVector3("ADDONWEAPON 8", "Offset", new Vector3(0f, 0f, 0f));
            Addon9Offset = settings.GetVector3("ADDONWEAPON 9", "Offset", new Vector3(0f, 0f, 0f));
            Addon10Offset = settings.GetVector3("ADDONWEAPON 10", "Offset", new Vector3(0f, 0f, 0f));
            Addon11Offset = settings.GetVector3("ADDONWEAPON 11", "Offset", new Vector3(0f, 0f, 0f));
            Addon12Offset = settings.GetVector3("ADDONWEAPON 12", "Offset", new Vector3(0f, 0f, 0f));
            Addon13Offset = settings.GetVector3("ADDONWEAPON 13", "Offset", new Vector3(0f, 0f, 0f));
            Addon14Offset = settings.GetVector3("ADDONWEAPON 14", "Offset", new Vector3(0f, 0f, 0f));
            Addon15Offset = settings.GetVector3("ADDONWEAPON 15", "Offset", new Vector3(0f, 0f, 0f));
            Addon16Offset = settings.GetVector3("ADDONWEAPON 16", "Offset", new Vector3(0f, 0f, 0f));
            Addon17Offset = settings.GetVector3("ADDONWEAPON 17", "Offset", new Vector3(0f, 0f, 0f));
            Addon18Offset = settings.GetVector3("ADDONWEAPON 18", "Offset", new Vector3(0f, 0f, 0f));
            Addon19Offset = settings.GetVector3("ADDONWEAPON 19", "Offset", new Vector3(0f, 0f, 0f));
            Addon20Offset = settings.GetVector3("ADDONWEAPON 20", "Offset", new Vector3(0f, 0f, 0f));
            Unused0Offset = settings.GetVector3("UNUSED0", "Offset", new Vector3(0f, 0f, 0f));
            Episodic4Offset = settings.GetVector3("EPISODIC 4", "Offset", new Vector3(0f, 0f, 0f));
            Episodic5Offset = settings.GetVector3("EPISODIC 5", "Offset", new Vector3(0f, 0f, 0f));
            Episodic8Offset = settings.GetVector3("EPISODIC 8", "Offset", new Vector3(0f, 0f, 0f));
            Episodic16Offset = settings.GetVector3("EPISODIC 16", "Offset", new Vector3(0f, 0f, 0f));
            Episodic17Offset = settings.GetVector3("EPISODIC 17", "Offset", new Vector3(0f, 0f, 0f));
            Episodic18Offset = settings.GetVector3("EPISODIC 18", "Offset", new Vector3(0f, 0f, 0f));
            Episodic19Offset = settings.GetVector3("EPISODIC 19", "Offset", new Vector3(0f, 0f, 0f));
            Episodic20Offset = settings.GetVector3("EPISODIC 20", "Offset", new Vector3(0f, 0f, 0f));
            Episodic21Offset = settings.GetVector3("EPISODIC 21", "Offset", new Vector3(0f, 0f, 0f));
            CameraOffset = settings.GetVector3("CAMERA", "Offset", new Vector3(0f, 0f, 0f));
            ObjectOffset = settings.GetVector3("OBJECT", "Offset", new Vector3(0f, 0f, 0f));
            BatOffset = settings.GetVector3("BASEBALL BAT", "Offset", new Vector3(0f, 0f, 0f));
            PoolcueOffset = settings.GetVector3("POOLCUE", "Offset", new Vector3(0f, 0f, 0f));
            KnifeOffset = settings.GetVector3("KNIFE", "Offset", new Vector3(0f, 0f, 0f));
            GrenadeOffset = settings.GetVector3("GRENADE", "Offset", new Vector3(0f, 0f, 0f));
            MolotovOffset = settings.GetVector3("MOLOTOV", "Offset", new Vector3(0f, 0f, 0f));
            RocketOffset = settings.GetVector3("ROCKET", "Offset", new Vector3(0f, 0f, 0f));
        }
    }
}
