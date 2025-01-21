using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using CCL.GTAIV;

namespace WeapFuncs.ivsdk
{
    internal class ReloadSpeed
    {
        //In hindsight this shouldve been made using a switch statement
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
        public static void Tick()
        {
            if (Main.currWeap == 7)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@handgun", "reload", (PistolReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@handgun", "reload_crouch", (PistolReload));
            }

            else if (Main.currWeap == 8)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.SilencedAnim, "reload", (SilencedReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.SilencedAnim, "reload_crouch", (SilencedReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.SilencedAnim, "p_load", (SilencedReload));
            }

            else if (Main.currWeap == 9)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@deagle", "reload", (DeagleReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@deagle", "reload_crouch", (DeagleReload));
            }

            else if (Main.currWeap == 10)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@shotgun", "reload", (PumpShotReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@shotgun", "reload_crouch", (PumpShotReload));
            }

            else if (Main.currWeap == 11)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@baretta", "reload", (CombatShotReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@baretta", "reload_crouch", (CombatShotReload));
            }

            else if (Main.currWeap == 12)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@uzi", "reload", (UziReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@uzi", "reload_crouch", (UziReload));
            }

            else if (Main.currWeap == 13)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@mp5k", "reload", (MP5Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@mp5k", "reload_crouch", (MP5Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@mp5k", "p_load", (MP5Reload));
            }

            else if (Main.currWeap == 14)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@ak47", "reload", (AK47Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@ak47", "reload_crouch", (AK47Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@ak47", "p_load", (AK47Reload));
            }

            else if (Main.currWeap == 15)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@ak47", "reload", (M4Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@ak47", "reload_crouch", (M4Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@ak47", "p_load", (M4Reload));
            }

            else if (Main.currWeap == 16)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rifle", "reload", (PsgReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rifle", "reload_crouch", (PsgReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rifle", "p_load", (PsgReload));
            }

            else if (Main.currWeap == 17)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rifle", "reload", (SnipReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rifle", "reload_crouch", (SnipReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rifle", "p_load", (SnipReload));
            }

            else if (Main.currWeap == 18)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rocket", "reload", (RpgReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@rocket", "reload_crouch", (RpgReload));
            }

            else if (Main.currWeap == 19)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.FThrowerAnim, "reload", (FThrowerReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.FThrowerAnim, "reload_crouch", (FThrowerReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.FThrowerAnim, "p_load", (FThrowerReload));
            }

            else if (Main.currWeap == 21)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@grnde_launch", "reload", (GrndLaunchReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@grnde_launch", "reload_crouch", (GrndLaunchReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@grnde_launch", "p_load", (GrndLaunchReload));
            }

            else if (Main.currWeap == 22)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@test_gun", "reload", (AssaultShotReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@test_gun", "reload_crouch", (AssaultShotReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@test_gun", "p_load", (AssaultShotReload));
            }

            else if (Main.currWeap == 26)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@sawnoff", "reload", (SawnOffReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@sawnoff", "reload_crouch", (SawnOffReload));
            }

            else if (Main.currWeap == 27)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@cz75", "reload", (AutoPReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@cz75", "reload_crouch", (AutoPReload));
            }

            else if (Main.currWeap == 29)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@44a", "reload", (Pistol44Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@44a", "reload_crouch", (Pistol44Reload));
            }

            else if (Main.currWeap == 30)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@aa12", "reload", (AA12ExpReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@aa12", "reload_crouch", (AA12ExpReload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@aa12", "p_load", (AA12ExpReload));
            }

            else if (Main.currWeap == 31)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@aa12", "reload", (AA12Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@aa12", "reload_crouch", (AA12Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@aa12", "p_load", (AA12Reload));
            }

            else if (Main.currWeap == 32)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@p90", "reload", (P90Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@p90", "reload_crouch", (P90Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@p90", "p_load", (P90Reload));
            }

            else if (Main.currWeap == 33)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@gold_uzi", "reload", (GoldUziReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@gold_uzi", "reload_crouch", (GoldUziReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@gold_uzi", "p_load", (GoldUziReload));
            }

            else if (Main.currWeap == 34)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@m249", "reload", (M249Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@m249", "reload_crouch", (M249Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@m249", "p_load", (M249Reload));
            }

            else if (Main.currWeap == 35)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@dsr1", "reload", (AdvSnipReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@dsr1", "reload_crouch", (AdvSnipReload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, "gun@dsr1", "p_load", (AdvSnipReload));
            }

            else if (Main.currWeap == 23)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic3Anim, "reload", (Episodic3Reload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic3Anim, "reload_crouch", (Episodic3Reload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic3Anim, "p_load", (Episodic3Reload));
            }

            else if (Main.currWeap == 42)
            {
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic22Anim, "reload", (Episodic22Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic22Anim, "reload_crouch", (Episodic22Reload));
                 SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic22Anim, "p_load", (Episodic22Reload));
            }

            else if (Main.currWeap == 43)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic23Anim, "reload", (Episodic23Reload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic23Anim, "reload_crouch", (Episodic23Reload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic23Anim, "p_load", (Episodic23Reload));
            }

            else if (Main.currWeap == 44)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic24Anim, "reload", (Episodic24Reload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic24Anim, "reload_crouch", (Episodic24Reload));
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Episodic24Anim, "p_load", (Episodic24Reload));
            }

            else if (Main.currWeap == 58)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon1Anim, "reload", Addon1Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon1Anim, "reload_crouch", Addon1Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon1Anim, "p_load", (Addon1Reload));
            }

            else if (Main.currWeap == 59)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon2Anim, "reload", Addon2Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon2Anim, "reload_crouch", Addon2Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon2Anim, "p_load", (Addon2Reload));
            }

            else if (Main.currWeap == 60)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon3Anim, "reload", Addon3Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon3Anim, "reload_crouch", Addon3Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon3Anim, "p_load", (Addon3Reload));
            }

            else if (Main.currWeap == 61)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon4Anim, "reload", Addon4Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon4Anim, "reload_crouch", Addon4Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon4Anim, "p_load", (Addon4Reload));
            }

            else if (Main.currWeap == 62)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon5Anim, "reload", Addon5Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon5Anim, "reload_crouch", Addon5Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon5Anim, "p_load", (Addon5Reload));
            }

            else if (Main.currWeap == 63)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon6Anim, "reload", Addon6Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon6Anim, "reload_crouch", Addon6Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon6Anim, "p_load", (Addon6Reload));
            }

            else if (Main.currWeap == 64)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon7Anim, "reload", Addon7Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon7Anim, "reload_crouch", Addon7Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon7Anim, "p_load", (Addon7Reload));
            }

            else if (Main.currWeap == 65)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon8Anim, "reload", Addon8Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon8Anim, "reload_crouch", Addon8Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon8Anim, "p_load", (Addon8Reload));
            }

            else if (Main.currWeap == 66)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon9Anim, "reload", Addon9Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon9Anim, "reload_crouch", Addon9Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon9Anim, "p_load", (Addon9Reload));
            }

            else if (Main.currWeap == 67)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon10Anim, "reload", Addon10Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon10Anim, "reload_crouch", Addon10Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon10Anim, "p_load", (Addon10Reload));
            }

            else if (Main.currWeap == 68)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon11Anim, "reload", Addon11Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon11Anim, "reload_crouch", Addon11Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon11Anim, "p_load", (Addon11Reload));
            }

            else if (Main.currWeap == 69)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon12Anim, "reload", Addon12Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon12Anim, "reload_crouch", Addon12Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon12Anim, "p_load", (Addon12Reload));
            }

            else if (Main.currWeap == 70)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon13Anim, "reload", Addon13Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon13Anim, "reload_crouch", Addon13Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon13Anim, "p_load", (Addon13Reload));
            }

            else if (Main.currWeap == 71)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon14Anim, "reload", Addon14Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon14Anim, "reload_crouch", Addon14Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon14Anim, "p_load", (Addon14Reload));
            }

            else if (Main.currWeap == 72)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon15Anim, "reload", Addon15Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon15Anim, "reload_crouch", Addon15Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon15Anim, "p_load", (Addon15Reload));
            }

            else if (Main.currWeap == 73)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon16Anim, "reload", Addon16Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon16Anim, "reload_crouch", Addon16Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon16Anim, "p_load", (Addon16Reload));
            }

            else if (Main.currWeap == 74)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon17Anim, "reload", Addon17Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon17Anim, "reload_crouch", Addon17Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon17Anim, "p_load", (Addon17Reload));
            }

            else if (Main.currWeap == 75)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon18Anim, "reload", Addon18Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon18Anim, "reload_crouch", Addon18Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon18Anim, "p_load", (Addon18Reload));
            }

            else if (Main.currWeap == 76)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon19Anim, "reload", Addon19Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon19Anim, "reload_crouch", Addon19Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon19Anim, "p_load", (Addon19Reload));
            }

            else if (Main.currWeap == 77)
            {
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon20Anim, "reload", Addon20Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon20Anim, "reload_crouch", Addon20Reload);
                SET_CHAR_ANIM_SPEED(Main.PlayerHandle, RateOfFire.Addon20Anim, "p_load", (Addon20Reload));
            }
        }
        public static void LoadSettings(SettingsFile settings)
        {
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
        }
    }
}
