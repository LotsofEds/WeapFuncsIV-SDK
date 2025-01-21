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
        public static bool CrouchRelFix;
        public static bool SemiAutoShotgunBlindfire;
        public static bool NPCSemiAutoShotgunBlindfire;
        public static bool ConsistentPistolBlindfireLoop;
        public static bool FullAutoPistol;
        public static bool FullAutoShotgun;
        public static bool SawnOffYeet;
        public static int gunModel;
        public static int Boolet;
        public static uint CurrEp;
        public static int currWeap;
        public static int pAmmo;
        public static int wSlot;
        //public static WeaponHandling TheWeaponHandler;
        public static IVPed PlayerPed { get; set; }
        public static uint PlayerIndex { get; set; }
        public static int PlayerHandle { get; set; }
        public static Vector3 PlayerPos { get; set; }
        public Main()
        {
            Initialized += Main_Initialized;
            Tick += Main_Tick;
            //TheWeaponHandler = new WeaponHandling();
        }

        private void Main_Initialized(object sender, EventArgs e)
        {
            LoadSettings(Settings);
            if (GlobalRateOfFire)
                RateOfFire.LoadSettings(Settings);
            BlindFireFixes.Init(Settings);
            ShotgunBlindfireFix.Init(Settings);
            ReloadSpeed.LoadSettings(Settings);
            NPCSemiAutoShot.Init(Settings);
            BlindFireFixes.Init(Settings);
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            PlayerPed = IVPed.FromUIntPtr(IVPlayerInfo.FindThePlayerPed());
            PlayerHandle = PlayerPed.GetHandle();
            PlayerPos = PlayerPed.Matrix.Pos;

            if (PlayerPed == null)
                return;

            GET_CURRENT_CHAR_WEAPON(Main.PlayerHandle, out currWeap);
            GET_AMMO_IN_CLIP(Main.PlayerHandle, currWeap, out pAmmo);
            GET_WEAPONTYPE_SLOT(currWeap, out wSlot);
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
        }
        private void LoadSettings(SettingsFile settings)
        {
            GlobalRateOfFire = settings.GetBoolean("WEAPFUNCS", "GlobalROF", false);
            ReloadInVehicles = settings.GetBoolean("WEAPFUNCS", "ReloadInVehicles", false);
            CrouchRelFix = settings.GetBoolean("WEAPFUNCS", "MP5ReloadCrouchFix", false);
            SemiAutoShotgunBlindfire = settings.GetBoolean("WEAPFUNCS", "SemiAutoShotgunBlindfire", false);
            NPCSemiAutoShotgunBlindfire = settings.GetBoolean("WEAPFUNCS", "NPCSemiAutoShotgunBlindfire", false);
            ConsistentPistolBlindfireLoop = settings.GetBoolean("WEAPFUNCS", "ConsistentPistolBlindfireLoop", false);
            FullAutoPistol = settings.GetBoolean("WEAPFUNCS", "FullAutoPistolBlindfire", false);
            FullAutoShotgun = settings.GetBoolean("WEAPFUNCS", "FullAutoShotgunBlindfire", false);
            SawnOffYeet = settings.GetBoolean("WEAPFUNCS", "SawnOffFiresSecondShellImmediately", false);
        }
    }
}
