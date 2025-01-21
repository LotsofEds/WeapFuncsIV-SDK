using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using System.Threading;
using System.Runtime;

namespace WeapFuncs.ivsdk
{
    internal class RateOfFire
    {
        // Dont try to make the reload work with this, dumbass me
        static int Radius;
        static float PistolFire;
        static float PistolFireDb;
        static float PistolFireCover;
        static float SilencedFire;
        static float SilencedFireDb;
        static float SilencedFireCover;
        public static string SilencedAnim;
        static string SilencedAnimCover;
        static float DeagleFire;
        static float DeagleFireDb;
        static float DeagleFireCover;
        static float PumpShotFire;
        static float PumpShotFireCover;
        static float CombatShotFire;
        static float CombatShotFireCover;
        static float UziFire;
        static float UziFireDb;
        static float UziFireCover;
        static float MP5Fire;
        static float MP5FireDb;
        static float MP5FireCover;
        static float AK47Fire;
        static float AK47FireDb;
        static float AK47FireCover;
        static float M4Fire;
        static float M4FireDb;
        static float M4FireCover;
        static float SnipFire;
        static float SnipFireDb;
        static float SnipFireCover;
        static float PsgFire;
        static float PsgFireDb;
        static float PsgFireCover;
        static float RpgFire;
        static float RpgFireCover;
        static float FThrowerFire;
        static float FThrowerFireDb;
        static float FThrowerFireCover;
        public static string FThrowerAnim;
        static string FThrowerAnimCover;
        static float AutoPFire;
        static float AutoPFireDb;
        static float AutoPFireCover;
        static float SawnOffFire;
        static float SawnOffFireDb;
        static float SawnOffFireCover;
        static float AssaultShotFire;
        static float AssaultShotFireDb;
        static float AssaultShotFireCover;
        static float GrndLaunchFire;
        static float GrndLaunchFireCover;
        static float Pistol44Fire;
        static float Pistol44FireDb;
        static float Pistol44FireCover;
        static float AA12Fire;
        static float AA12FireCover;
        static float AA12ExpFire;
        static float AA12ExpFireCover;
        static float P90Fire;
        static float P90FireDb;
        static float P90FireCover;
        static float GoldUziFire;
        static float GoldUziFireDb;
        static float GoldUziFireCover;
        static float M249Fire;
        static float M249FireDb;
        static float M249FireCover;
        static float AdvSnipFire;
        static float AdvSnipFireDb;
        static float AdvSnipFireCover;
        static float Episodic22Fire;
        static float Episodic22FireDb;
        static float Episodic22FireCover;
        public static string Episodic22Anim;
        static string Episodic22AnimCover;
        static float Episodic23Fire;
        static float Episodic23FireDb;
        static float Episodic23FireCover;
        public static string Episodic23Anim;
        static string Episodic23AnimCover;
        static float Episodic24Fire;
        static float Episodic24FireDb;
        static float Episodic24FireCover;
        public static string Episodic24Anim;
        static string Episodic24AnimCover;
        static float Episodic3Fire;
        static float Episodic3FireDb;
        static float Episodic3FireCover;
        public static string Episodic3Anim;
        static string Episodic3AnimCover;
        static float Addon1Fire;
        static float Addon1FireDb;
        static float Addon1FireCover;
        public static string Addon1Anim;
        static string Addon1AnimCover;
        static float Addon2Fire;
        static float Addon2FireDb;
        static float Addon2FireCover;
        public static string Addon2Anim;
        static string Addon2AnimCover;
        static float Addon3Fire;
        static float Addon3FireDb;
        static float Addon3FireCover;
        public static string Addon3Anim;
        static string Addon3AnimCover;
        static float Addon4Fire;
        static float Addon4FireDb;
        static float Addon4FireCover;
        public static string Addon4Anim;
        static string Addon4AnimCover;
        static float Addon5Fire;
        static float Addon5FireDb;
        static float Addon5FireCover;
        public static string Addon5Anim;
        static string Addon5AnimCover;
        static float Addon6Fire;
        static float Addon6FireDb;
        static float Addon6FireCover;
        public static string Addon6Anim;
        static string Addon6AnimCover;
        static float Addon7Fire;
        static float Addon7FireDb;
        static float Addon7FireCover;
        public static string Addon7Anim;
        static string Addon7AnimCover;
        static float Addon8Fire;
        static float Addon8FireDb;
        static float Addon8FireCover;
        public static string Addon8Anim;
        static string Addon8AnimCover;
        static float Addon9Fire;
        static float Addon9FireDb;
        static float Addon9FireCover;
        public static string Addon9Anim;
        static string Addon9AnimCover;
        static float Addon10Fire;
        static float Addon10FireDb;
        static float Addon10FireCover;
        public static string Addon10Anim;
        static string Addon10AnimCover;
        static float Addon11Fire;
        static float Addon11FireDb;
        static float Addon11FireCover;
        public static string Addon11Anim;
        static string Addon11AnimCover;
        static float Addon12Fire;
        static float Addon12FireDb;
        static float Addon12FireCover;
        public static string Addon12Anim;
        static string Addon12AnimCover;
        static float Addon13Fire;
        static float Addon13FireDb;
        static float Addon13FireCover;
        public static string Addon13Anim;
        static string Addon13AnimCover;
        static float Addon14Fire;
        static float Addon14FireDb;
        static float Addon14FireCover;
        public static string Addon14Anim;
        static string Addon14AnimCover;
        static float Addon15Fire;
        static float Addon15FireDb;
        static float Addon15FireCover;
        public static string Addon15Anim;
        static string Addon15AnimCover;
        static float Addon16Fire;
        static float Addon16FireDb;
        static float Addon16FireCover;
        public static string Addon16Anim;
        static string Addon16AnimCover;
        static float Addon17Fire;
        static float Addon17FireDb;
        static float Addon17FireCover;
        public static string Addon17Anim;
        static string Addon17AnimCover;
        static float Addon18Fire;
        static float Addon18FireDb;
        static float Addon18FireCover;
        public static string Addon18Anim;
        static string Addon18AnimCover;
        static float Addon19Fire;
        static float Addon19FireDb;
        static float Addon19FireCover;
        public static string Addon19Anim;
        static string Addon19AnimCover;
        static float Addon20Fire;
        static float Addon20FireDb;
        static float Addon20FireCover;
        public static string Addon20Anim;
        static string Addon20AnimCover;

        //private static int weapID;
        //private static int numWeaps;
        //static float FireRate;
        //static float FireRateDb;
        //static float BlindFireRate;

        //public static void GetSettings(SettingsFile settings)
        //{
        //    settings.GetFloat(weapID.ToString(), "fire", 1.0f);
        //}
        //public static void Init()
        //{
        //for (int i = 0; i < numWeaps; i++)
        //{
        //    Main.TheWeaponHandler.Add(i, Addon1Anim, "fire", () =>
        //    {
        //        IVGame.ShowSubtitleMessage(" ");
        //    });
        //    break;
        //}
        //}
        //public static void SetWeaponData(int pedHandle, int weapID, string AnimSet1, string AnimSet2)
        //{
        //    GET_CURRENT_CHAR_WEAPON(pedHandle, out int currWeap);
        //    if (currWeap == weapID)
        //    {
        //        SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "fire", (PistolFire));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "fire_crouch", (PistolFire));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "dbfire", (PistolFireDb));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "dbfire_l", (PistolFireDb));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon1AnimCover, (PistolFireCover));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon1AnimCover, (PistolFireCover));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon1AnimCover, (PistolFireCover));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon1AnimCover, (PistolFireCover));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon1AnimCover, (PistolFireCover));
        //        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon1AnimCover, (PistolFireCover));
        //    }
        //}
        public static void Tick()
        {
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (IS_CHAR_DEAD(pedHandle))
                    continue;
                if (!IS_CHAR_SHOOTING(pedHandle))
                    continue;

                //Main.TheWeaponHandler.Process();
                GET_CURRENT_CHAR_WEAPON(pedHandle, out int currWeap);

                if (currWeap == 7)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "fire", (PistolFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "fire_crouch", (PistolFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "dbfire", (PistolFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@handgun", "dbfire_l", (PistolFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "pistol_blindfire", (PistolFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "pistol_blindfire", (PistolFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "pistol_blindfire", (PistolFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "pistol_blindfire", (PistolFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "pistol_blindfire", (PistolFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "pistol_blindfire", (PistolFireCover));
                }

                if (currWeap == 8)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "fire", (SilencedFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "fire_crouch", (SilencedFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "fire_alt", (SilencedFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "fire_crouch_alt", (SilencedFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "fire_up", (SilencedFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "fire_down", (SilencedFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "dbfire", (SilencedFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, SilencedAnim, "dbfire_l", (SilencedFireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", SilencedAnimCover, (SilencedFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", SilencedAnimCover, (SilencedFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", SilencedAnimCover, (SilencedFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", SilencedAnimCover, (SilencedFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", SilencedAnimCover, (SilencedFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", SilencedAnimCover, (SilencedFireCover));
                    }
                }

                else if (currWeap == 9)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@deagle", "fire", (DeagleFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@deagle", "fire_crouch", (DeagleFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@deagle", "dbfire", (DeagleFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@deagle", "dbfire_l", (DeagleFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "pistol_blindfire", (DeagleFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "pistol_blindfire", (DeagleFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "pistol_blindfire", (DeagleFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "pistol_blindfire", (DeagleFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "pistol_blindfire", (DeagleFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "pistol_blindfire", (DeagleFireCover));
                }

                else if (currWeap == 10)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@shotgun", "fire", (PumpShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@shotgun", "fire_crouch", (PumpShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "shotgun_blindfire", (PumpShotFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "shotgun_blindfire", (PumpShotFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "shotgun_blindfire", (PumpShotFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "shotgun_blindfire", (PumpShotFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "shotgun_blindfire", (PumpShotFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "shotgun_blindfire", (PumpShotFireCover));
                }

                else if (currWeap == 11)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@baretta", "fire", (CombatShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@baretta", "fire_crouch", (CombatShotFire));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "shotgun_blindfire", (CombatShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "shotgun_blindfire", (CombatShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "shotgun_blindfire", (CombatShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "shotgun_blindfire", (CombatShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "shotgun_blindfire", (CombatShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "shotgun_blindfire", (CombatShotFireCover));
                    }
                }

                else if (currWeap == 12)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@uzi", "fire", (UziFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@uzi", "fire_crouch", (UziFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@uzi", "dbfire", (UziFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@uzi", "dbfire_l", (UziFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "uzi_blindfire", (UziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "uzi_blindfire", (UziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "uzi_blindfire", (UziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "uzi_blindfire", (UziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "uzi_blindfire", (UziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "uzi_blindfire", (UziFireCover));
                }

                else if (currWeap == 13)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@mp5k", "fire", (MP5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@mp5k", "fire_crouch", (MP5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@mp5k", "dbfire", (MP5FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@mp5k", "dbfire_l", (MP5FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "ak47_blindfire", (MP5FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "ak47_blindfire", (MP5FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "ak47_blindfire", (MP5FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "ak47_blindfire", (MP5FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "ak47_blindfire", (MP5FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "ak47_blindfire", (MP5FireCover));
                }

                else if (currWeap == 14)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire", (AK47Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire_alt", (AK47Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire_crouch", (AK47Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire_crouch_alt", (AK47Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "ak47_blindfire", (AK47FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "ak47_blindfire", (AK47FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "ak47_blindfire", (AK47FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "ak47_blindfire", (AK47FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "ak47_blindfire", (AK47FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "dbfire", (AK47FireDb));
                }

                else if (currWeap == 15)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire", (M4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire_alt", (M4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire_crouch", (M4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "fire_crouch_alt", (M4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "ak47_blindfire", (M4FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "ak47_blindfire", (M4FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "ak47_blindfire", (M4FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "ak47_blindfire", (M4FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "ak47_blindfire", (M4FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "ak47_blindfire", (M4FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@ak47", "dbfire", (M4FireDb));
                }

                else if (currWeap == 16)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire", (PsgFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire_alt", (PsgFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire_crouch", (PsgFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire_crouch_alt", (PsgFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "rifle_blindfire", (PsgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "rifle_blindfire", (PsgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "rifle_blindfire", (PsgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "rifle_blindfire", (PsgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "rifle_blindfire", (PsgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "rifle_blindfire", (PsgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "dbfire", (PsgFireDb));
                }

                else if (currWeap == 17)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire", (SnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire_alt", (SnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire_crouch", (SnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "fire_crouch_alt", (SnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "rifle_blindfire", (SnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "rifle_blindfire", (SnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "rifle_blindfire", (SnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "rifle_blindfire", (SnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "rifle_blindfire", (SnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "rifle_blindfire", (SnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rifle", "dbfire", (SnipFireDb));
                }

                else if (currWeap == 18)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rocket", "fire", (RpgFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@rocket", "fire_crouch", (RpgFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "rocket_blindfire", (RpgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "rocket_blindfire", (RpgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "rocket_blindfire", (RpgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "rocket_blindfire", (RpgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "rocket_blindfire", (RpgFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "rocket_blindfire", (RpgFireCover));
                }

                else if (currWeap == 19)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "fire", (FThrowerFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "fire_crouch", (FThrowerFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "fire_alt", (FThrowerFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "fire_crouch_alt", (FThrowerFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "fire_up", (FThrowerFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "fire_down", (FThrowerFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "dbfire", (FThrowerFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, FThrowerAnim, "dbfire_l", (FThrowerFireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", FThrowerAnimCover, (FThrowerFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", FThrowerAnimCover, (FThrowerFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", FThrowerAnimCover, (FThrowerFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", FThrowerAnimCover, (FThrowerFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", FThrowerAnimCover, (FThrowerFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", FThrowerAnimCover, (FThrowerFireCover));
                    }
                }

                else if (currWeap == 21)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@grnde_launch", "fire", (GrndLaunchFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@grnde_launch", "fire_crouch", (GrndLaunchFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "rocket_blindfire", (GrndLaunchFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "rocket_blindfire", (GrndLaunchFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "rocket_blindfire", (GrndLaunchFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "rocket_blindfire", (GrndLaunchFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "rocket_blindfire", (GrndLaunchFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "rocket_blindfire", (GrndLaunchFireCover));
                }

                else if (currWeap == 22)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@test_gun", "fire", (AssaultShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@test_gun", "fire_up", (AssaultShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@test_gun", "fire_down", (AssaultShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@test_gun", "fire_crouch", (AssaultShotFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@test_gun", "dbfire", (AssaultShotFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@test_gun", "dbfire_l", (AssaultShotFireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "shotgun_blindfire", (AssaultShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "shotgun_blindfire", (AssaultShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "shotgun_blindfire", (AssaultShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "shotgun_blindfire", (AssaultShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "shotgun_blindfire", (AssaultShotFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "shotgun_blindfire", (AssaultShotFireCover));
                    }
                }

                else if (currWeap == 26)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@sawnoff", "fire", (SawnOffFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@sawnoff", "fire_crouch", (SawnOffFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@sawnoff", "dbfire", (SawnOffFireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "shotgun_blindfire", (SawnOffFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "shotgun_blindfire", (SawnOffFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "shotgun_blindfire", (SawnOffFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "shotgun_blindfire", (SawnOffFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "shotgun_blindfire", (SawnOffFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "shotgun_blindfire", (SawnOffFireCover));
                    }
                }

                else if (currWeap == 27)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@cz75", "fire", (AutoPFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@cz75", "fire_crouch", (AutoPFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@cz75", "dbfire", (AutoPFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@cz75", "dbfire_l", (AutoPFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "pistol_blindfire", (AutoPFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "pistol_blindfire", (AutoPFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "pistol_blindfire", (AutoPFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "pistol_blindfire", (AutoPFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "pistol_blindfire", (AutoPFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "pistol_blindfire", (AutoPFireCover));
                }

                else if (currWeap == 29)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@44a", "fire", (Pistol44Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@44a", "fire_crouch", (Pistol44Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@44a", "dbfire", (Pistol44FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@44a", "dbfire_l", (Pistol44FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "pistol_blindfire", (Pistol44FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "pistol_blindfire", (Pistol44FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "pistol_blindfire", (Pistol44FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "pistol_blindfire", (Pistol44FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "pistol_blindfire", (Pistol44FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "pistol_blindfire", (Pistol44FireCover));
                }

                else if (currWeap == 30)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@aa12", "fire", (AA12ExpFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@aa12", "fire_crouch", (AA12ExpFire));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "shotgun_blindfire", (AA12ExpFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "shotgun_blindfire", (AA12ExpFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "shotgun_blindfire", (AA12ExpFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "shotgun_blindfire", (AA12ExpFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "shotgun_blindfire", (AA12ExpFireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "shotgun_blindfire", (AA12ExpFireCover));
                    }
                }

                else if (currWeap == 31)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@aa12", "fire", (AA12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@aa12", "fire_crouch", (AA12Fire));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "shotgun_blindfire", (AA12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "shotgun_blindfire", (AA12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "shotgun_blindfire", (AA12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "shotgun_blindfire", (AA12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "shotgun_blindfire", (AA12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "shotgun_blindfire", (AA12FireCover));
                    }
                }

                else if (currWeap == 32)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@p90", "fire", (P90Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@p90", "fire_crouch", (P90Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@p90", "dbfire", (P90FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "ak47_blindfire", (P90FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "ak47_blindfire", (P90FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "ak47_blindfire", (P90FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "ak47_blindfire", (P90FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "ak47_blindfire", (P90FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "ak47_blindfire", (P90FireCover));
                }

                else if (currWeap == 33)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@gold_uzi", "fire", (GoldUziFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@gold_uzi", "fire_crouch", (GoldUziFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@gold_uzi", "dbfire", (GoldUziFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@gold_uzi", "dbfire_l", (GoldUziFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "uzi_blindfire", (GoldUziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "uzi_blindfire", (GoldUziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "uzi_blindfire", (GoldUziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "uzi_blindfire", (GoldUziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "uzi_blindfire", (GoldUziFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "uzi_blindfire", (GoldUziFireCover));
                }

                else if (currWeap == 34)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@m249", "fire", (M249Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@m249", "fire_up", (M249Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@m249", "fire_down", (M249Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@m249", "fire_crouch", (M249Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@m249", "dbfire", (M249FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "ak47_blindfire", (M249FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "ak47_blindfire", (M249FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "ak47_blindfire", (M249FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "ak47_blindfire", (M249FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "ak47_blindfire", (M249FireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "ak47_blindfire", (M249FireCover));
                }

                else if (currWeap == 35)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@dsr1", "fire", (AdvSnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@dsr1", "fire_alt", (AdvSnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@dsr1", "fire_crouch", (AdvSnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@dsr1", "fire_crouch_alt", (AdvSnipFire));
                    SET_CHAR_ANIM_SPEED(pedHandle, "gun@dsr1", "dbfire", (AdvSnipFireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", "rifle_blindfire", (AdvSnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", "rifle_blindfire", (AdvSnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", "rifle_blindfire", (AdvSnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", "rifle_blindfire", (AdvSnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", "rifle_blindfire", (AdvSnipFireCover));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", "rifle_blindfire", (AdvSnipFireCover));
                }

                else if (currWeap == 23)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "fire", (Episodic3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "fire_crouch", (Episodic3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "fire_alt", (Episodic3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "fire_crouch_alt", (Episodic3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "fire_up", (Episodic3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "fire_down", (Episodic3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "dbfire", (Episodic3FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic3Anim, "dbfire_l", (Episodic3FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Episodic3AnimCover, (Episodic3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Episodic3AnimCover, (Episodic3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Episodic3AnimCover, (Episodic3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Episodic3AnimCover, (Episodic3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Episodic3AnimCover, (Episodic3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Episodic3AnimCover, (Episodic3FireCover));
                    }
                }

                else if (currWeap == 42)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "fire", (Episodic22Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "fire_crouch", (Episodic22Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "fire_alt", (Episodic22Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "fire_crouch_alt", (Episodic22Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "fire_up", (Episodic22Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "fire_down", (Episodic22Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "dbfire", (Episodic22FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic22Anim, "dbfire_l", (Episodic22FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Episodic22AnimCover, (Episodic22FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Episodic22AnimCover, (Episodic22FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Episodic22AnimCover, (Episodic22FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Episodic22AnimCover, (Episodic22FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Episodic22AnimCover, (Episodic22FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Episodic22AnimCover, (Episodic22FireCover));
                    }
                }

                else if (currWeap == 43)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "fire", (Episodic23Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "fire_crouch", (Episodic23Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "fire_alt", (Episodic23Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "fire_crouch_alt", (Episodic23Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "fire_up", (Episodic23Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "fire_down", (Episodic23Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "dbfire", (Episodic23FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic23Anim, "dbfire_l", (Episodic23FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Episodic23AnimCover, (Episodic23FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Episodic23AnimCover, (Episodic23FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Episodic23AnimCover, (Episodic23FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Episodic23AnimCover, (Episodic23FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Episodic23AnimCover, (Episodic23FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Episodic23AnimCover, (Episodic23FireCover));
                    }
                }

                else if (currWeap == 44)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "fire", (Episodic24Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "fire_crouch", (Episodic24Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "fire_alt", (Episodic24Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "fire_crouch_alt", (Episodic24Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "fire_up", (Episodic24Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "fire_down", (Episodic24Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "dbfire", (Episodic24FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Episodic24Anim, "dbfire_l", (Episodic24FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Episodic24AnimCover, (Episodic24FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Episodic24AnimCover, (Episodic24FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Episodic24AnimCover, (Episodic24FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Episodic24AnimCover, (Episodic24FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Episodic24AnimCover, (Episodic24FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Episodic24AnimCover, (Episodic24FireCover));
                    }
                }

                else if (currWeap == 58)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "fire", (Addon1Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "fire_crouch", (Addon1Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "fire_alt", (Addon1Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "fire_crouch_alt", (Addon1Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "fire_up", (Addon1Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "fire_down", (Addon1Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "dbfire", (Addon1FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon1Anim, "dbfire_l", (Addon1FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon1AnimCover, (Addon1FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon1AnimCover, (Addon1FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon1AnimCover, (Addon1FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon1AnimCover, (Addon1FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon1AnimCover, (Addon1FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon1AnimCover, (Addon1FireCover));
                    }
                }

                else if (currWeap == 59)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "fire", (Addon2Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "fire_crouch", (Addon2Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "fire_alt", (Addon2Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "fire_crouch_alt", (Addon2Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "fire_up", (Addon2Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "fire_down", (Addon2Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "dbfire", (Addon2FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon2Anim, "dbfire_l", (Addon2FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon2AnimCover, (Addon2FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon2AnimCover, (Addon2FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon2AnimCover, (Addon2FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon2AnimCover, (Addon2FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon2AnimCover, (Addon2FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon2AnimCover, (Addon2FireCover));
                    }
                }

                else if (currWeap == 60)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "fire", (Addon3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "fire_crouch", (Addon3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "fire_alt", (Addon3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "fire_crouch_alt", (Addon3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "fire_up", (Addon3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "fire_down", (Addon3Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "dbfire", (Addon3FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon3Anim, "dbfire_l", (Addon3FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon3AnimCover, (Addon3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon3AnimCover, (Addon3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon3AnimCover, (Addon3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon3AnimCover, (Addon3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon3AnimCover, (Addon3FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon3AnimCover, (Addon3FireCover));
                    }
                }

                else if (currWeap == 61)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "fire", (Addon4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "fire_crouch", (Addon4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "fire_alt", (Addon4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "fire_crouch_alt", (Addon4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "fire_up", (Addon4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "fire_down", (Addon4Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "dbfire", (Addon4FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon4Anim, "dbfire_l", (Addon4FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon4AnimCover, (Addon4FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon4AnimCover, (Addon4FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon4AnimCover, (Addon4FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon4AnimCover, (Addon4FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon4AnimCover, (Addon4FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon4AnimCover, (Addon4FireCover));
                    }
                }

                else if (currWeap == 62)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "fire", (Addon5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "fire_crouch", (Addon5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "fire_alt", (Addon5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "fire_crouch_alt", (Addon5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "fire_up", (Addon5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "fire_down", (Addon5Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "dbfire", (Addon5FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon5Anim, "dbfire_l", (Addon5FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon5AnimCover, (Addon5FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon5AnimCover, (Addon5FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon5AnimCover, (Addon5FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon5AnimCover, (Addon5FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon5AnimCover, (Addon5FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon5AnimCover, (Addon5FireCover));
                    }
                }

                else if (currWeap == 63)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "fire", (Addon6Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "fire_crouch", (Addon6Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "fire_alt", (Addon6Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "fire_crouch_alt", (Addon6Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "fire_up", (Addon6Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "fire_down", (Addon6Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "dbfire", (Addon6FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon6Anim, "dbfire_l", (Addon6FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon6AnimCover, (Addon6FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon6AnimCover, (Addon6FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon6AnimCover, (Addon6FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon6AnimCover, (Addon6FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon6AnimCover, (Addon6FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon6AnimCover, (Addon6FireCover));
                    }
                }

                else if (currWeap == 64)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "fire", (Addon7Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "fire_crouch", (Addon7Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "fire_alt", (Addon7Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "fire_crouch_alt", (Addon7Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "fire_up", (Addon7Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "fire_down", (Addon7Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "dbfire", (Addon7FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon7Anim, "dbfire_l", (Addon7FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon7AnimCover, (Addon7FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon7AnimCover, (Addon7FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon7AnimCover, (Addon7FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon7AnimCover, (Addon7FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon7AnimCover, (Addon7FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon7AnimCover, (Addon7FireCover));
                    }
                }

                else if (currWeap == 65)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "fire", (Addon8Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "fire_crouch", (Addon8Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "fire_alt", (Addon8Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "fire_crouch_alt", (Addon8Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "fire_up", (Addon8Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "fire_down", (Addon8Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "dbfire", (Addon8FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon8Anim, "dbfire_l", (Addon8FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon8AnimCover, (Addon8FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon8AnimCover, (Addon8FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon8AnimCover, (Addon8FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon8AnimCover, (Addon8FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon8AnimCover, (Addon8FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon8AnimCover, (Addon8FireCover));
                    }
                }

                else if (currWeap == 66)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "fire", (Addon9Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "fire_crouch", (Addon9Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "fire_alt", (Addon9Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "fire_crouch_alt", (Addon9Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "fire_up", (Addon9Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "fire_down", (Addon9Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "dbfire", (Addon9FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon9Anim, "dbfire_l", (Addon9FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon9AnimCover, (Addon9FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon9AnimCover, (Addon9FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon9AnimCover, (Addon9FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon9AnimCover, (Addon9FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon9AnimCover, (Addon9FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon9AnimCover, (Addon9FireCover));
                    }
                }

                else if (currWeap == 67)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "fire", (Addon10Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "fire_crouch", (Addon10Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "fire_alt", (Addon10Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "fire_crouch_alt", (Addon10Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "fire_up", (Addon10Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "fire_down", (Addon10Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "dbfire", (Addon10FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon10Anim, "dbfire_l", (Addon10FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon10AnimCover, (Addon10FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon10AnimCover, (Addon10FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon10AnimCover, (Addon10FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon10AnimCover, (Addon10FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon10AnimCover, (Addon10FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon10AnimCover, (Addon10FireCover));
                    }
                }

                else if (currWeap == 68)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "fire", (Addon11Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "fire_crouch", (Addon11Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "fire_alt", (Addon11Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "fire_crouch_alt", (Addon11Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "fire_up", (Addon11Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "fire_down", (Addon11Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "dbfire", (Addon11FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon11Anim, "dbfire_l", (Addon11FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon11AnimCover, (Addon11FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon11AnimCover, (Addon11FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon11AnimCover, (Addon11FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon11AnimCover, (Addon11FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon11AnimCover, (Addon11FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon11AnimCover, (Addon11FireCover));
                    }
                }

                else if (currWeap == 69)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "fire", (Addon12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "fire_crouch", (Addon12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "fire_alt", (Addon12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "fire_crouch_alt", (Addon12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "fire_up", (Addon12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "fire_down", (Addon12Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "dbfire", (Addon12FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon12Anim, "dbfire_l", (Addon12FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon12AnimCover, (Addon12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon12AnimCover, (Addon12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon12AnimCover, (Addon12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon12AnimCover, (Addon12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon12AnimCover, (Addon12FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon12AnimCover, (Addon12FireCover));
                    }
                }

                else if (currWeap == 70)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "fire", (Addon13Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "fire_crouch", (Addon13Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "fire_alt", (Addon13Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "fire_crouch_alt", (Addon13Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "fire_up", (Addon13Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "fire_down", (Addon13Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "dbfire", (Addon13FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon13Anim, "dbfire_l", (Addon13FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon13AnimCover, (Addon13FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon13AnimCover, (Addon13FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon13AnimCover, (Addon13FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon13AnimCover, (Addon13FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon13AnimCover, (Addon13FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon13AnimCover, (Addon13FireCover));
                    }
                }

                else if (currWeap == 71)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "fire", (Addon14Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "fire_crouch", (Addon14Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "fire_alt", (Addon14Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "fire_crouch_alt", (Addon14Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "fire_up", (Addon14Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "fire_down", (Addon14Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "dbfire", (Addon14FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon14Anim, "dbfire_l", (Addon14FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon14AnimCover, (Addon14FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon14AnimCover, (Addon14FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon14AnimCover, (Addon14FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon14AnimCover, (Addon14FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon14AnimCover, (Addon14FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon14AnimCover, (Addon14FireCover));
                    }
                }

                else if (currWeap == 72)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "fire", (Addon15Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "fire_crouch", (Addon15Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "fire_alt", (Addon15Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "fire_crouch_alt", (Addon15Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "fire_up", (Addon15Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "fire_down", (Addon15Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "dbfire", (Addon15FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon15Anim, "dbfire_l", (Addon15FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon15AnimCover, (Addon15FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon15AnimCover, (Addon15FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon15AnimCover, (Addon15FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon15AnimCover, (Addon15FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon15AnimCover, (Addon15FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon15AnimCover, (Addon15FireCover));
                    }
                }

                else if (currWeap == 73)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "fire", (Addon16Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "fire_crouch", (Addon16Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "fire_alt", (Addon16Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "fire_crouch_alt", (Addon16Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "fire_up", (Addon16Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "fire_down", (Addon16Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "dbfire", (Addon16FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon16Anim, "dbfire_l", (Addon16FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon16AnimCover, (Addon16FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon16AnimCover, (Addon16FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon16AnimCover, (Addon16FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon16AnimCover, (Addon16FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon16AnimCover, (Addon16FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon16AnimCover, (Addon16FireCover));
                    }
                }

                else if (currWeap == 74)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "fire", (Addon17Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "fire_crouch", (Addon17Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "fire_alt", (Addon17Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "fire_crouch_alt", (Addon17Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "fire_up", (Addon17Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "fire_down", (Addon17Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "dbfire", (Addon17FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon17Anim, "dbfire_l", (Addon17FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon17AnimCover, (Addon17FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon17AnimCover, (Addon17FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon17AnimCover, (Addon17FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon17AnimCover, (Addon17FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon17AnimCover, (Addon17FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon17AnimCover, (Addon17FireCover));
                    }
                }

                else if (currWeap == 75)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "fire", (Addon18Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "fire_crouch", (Addon18Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "fire_alt", (Addon18Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "fire_crouch_alt", (Addon18Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "fire_up", (Addon18Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "fire_down", (Addon18Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "dbfire", (Addon18FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon18Anim, "dbfire_l", (Addon18FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon18AnimCover, (Addon18FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon18AnimCover, (Addon18FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon18AnimCover, (Addon18FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon18AnimCover, (Addon18FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon18AnimCover, (Addon18FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon18AnimCover, (Addon18FireCover));
                    }
                }

                else if (currWeap == 76)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "fire", (Addon19Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "fire_crouch", (Addon19Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "fire_alt", (Addon19Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "fire_crouch_alt", (Addon19Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "fire_up", (Addon19Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "fire_down", (Addon19Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "dbfire", (Addon19FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon19Anim, "dbfire_l", (Addon19FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon19AnimCover, (Addon19FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon19AnimCover, (Addon19FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon19AnimCover, (Addon19FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon19AnimCover, (Addon19FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon19AnimCover, (Addon19FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon19AnimCover, (Addon19FireCover));
                    }
                }

                else if (currWeap == 77)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "fire", (Addon20Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "fire_crouch", (Addon20Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "fire_alt", (Addon20Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "fire_crouch_alt", (Addon20Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "fire_up", (Addon20Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "fire_down", (Addon20Fire));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "dbfire", (Addon20FireDb));
                    SET_CHAR_ANIM_SPEED(pedHandle, Addon20Anim, "dbfire_l", (Addon20FireDb));
                    if (pedHandle != Main.PlayerHandle)
                    {
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", Addon20AnimCover, (Addon20FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", Addon20AnimCover, (Addon20FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", Addon20AnimCover, (Addon20FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", Addon20AnimCover, (Addon20FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", Addon20AnimCover, (Addon20FireCover));
                        SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", Addon20AnimCover, (Addon20FireCover));
                    }
                }
            }
        }
        public static void LoadSettings(SettingsFile settings)
        {
            Radius = settings.GetInteger("GLOBAL RATE OF FIRE", "PedDistance", 75);
            PistolFire = settings.GetFloat("GLOCK", "NormalROF", 1.0f);
            PistolFireDb = settings.GetFloat("GLOCK", "DrivebyROF", 1.0f);
            PistolFireCover = settings.GetFloat("GLOCK", "InCoverROF", 1.0f);
            SilencedFire = settings.GetFloat("SILENCED", "NormalROF", 1.0f);
            SilencedFireDb = settings.GetFloat("SILENCED", "DrivebyROF", 1.0f);
            SilencedFireCover = settings.GetFloat("SILENCED", "InCoverROF", 1.0f);
            SilencedAnim = settings.GetValue("SILENCED", "NormalAnim", "");
            SilencedAnimCover = settings.GetValue("SILENCED", "InCoverAnim", "");
            DeagleFire = settings.GetFloat("DEAGLE", "NormalROF", 1.0f);
            DeagleFireDb = settings.GetFloat("DEAGLE", "DrivebyROF", 1.0f);
            DeagleFireCover = settings.GetFloat("DEAGLE", "InCoverROF", 1.0f);
            PumpShotFire = settings.GetFloat("PUMP SHOTGUN", "NormalROF", 1.0f);
            PumpShotFireCover = settings.GetFloat("PUMP SHOTGUN", "InCoverROF", 1.0f);
            CombatShotFire = settings.GetFloat("SEMIAUTO SHOTGUN", "NormalROF", 1.0f);
            CombatShotFireCover = settings.GetFloat("SEMIAUTO SHOTGUN", "InCoverROF", 1.0f);
            UziFire = settings.GetFloat("MICRO UZI", "NormalROF", 1.0f);
            UziFireDb = settings.GetFloat("MICRO UZI", "DrivebyROF", 1.0f);
            UziFireCover = settings.GetFloat("MICRO UZI", "InCoverROF", 1.0f);
            MP5Fire = settings.GetFloat("MP5", "NormalROF", 1.0f);
            MP5FireDb = settings.GetFloat("MP5", "DrivebyROF", 1.0f);
            MP5FireCover = settings.GetFloat("MP5", "InCoverROF", 1.0f);
            AK47Fire = settings.GetFloat("AK47", "NormalROF", 1.0f);
            AK47FireDb = settings.GetFloat("AK47", "DrivebyROF", 1.0f);
            AK47FireCover = settings.GetFloat("AK47", "InCoverROF", 1.0f);
            M4Fire = settings.GetFloat("M4", "NormalROF", 1.0f);
            M4FireDb = settings.GetFloat("M4", "DrivebyROF", 1.0f);
            M4FireCover = settings.GetFloat("M4", "InCoverROF", 1.0f);
            SnipFire = settings.GetFloat("BOLTACTION SNIPER", "NormalROF", 1.0f);
            SnipFireDb = settings.GetFloat("BOLTACTION SNIPER", "DrivebyROF", 1.0f);
            SnipFireCover = settings.GetFloat("BOLTACTION SNIPER", "InCoverROF", 1.0f);
            PsgFire = settings.GetFloat("SEMIAUTO SNIPER", "NormalROF", 1.0f);
            PsgFireDb = settings.GetFloat("SEMIAUTO SNIPER", "DrivebyROF", 1.0f);
            PsgFireCover = settings.GetFloat("SEMIAUTO SNIPER", "InCoverROF", 1.0f);
            RpgFire = settings.GetFloat("RPG", "NormalROF", 1.0f);
            RpgFireCover = settings.GetFloat("RPG", "InCoverROF", 1.0f);
            FThrowerFire = settings.GetFloat("FTHROWER", "NormalROF", 1.0f);
            FThrowerFireDb = settings.GetFloat("FTHROWER", "DrivebyROF", 1.0f);
            FThrowerFireCover = settings.GetFloat("FTHROWER", "InCoverROF", 1.0f);
            FThrowerAnim = settings.GetValue("FTHROWER", "NormalAnim", "");
            FThrowerAnimCover = settings.GetValue("FTHROWER", "InCoverAnim", "");
            AutoPFire = settings.GetFloat("FULLAUTO PISTOL", "NormalROF", 1.0f);
            AutoPFireDb = settings.GetFloat("FULLAUTO PISTOL", "DrivebyROF", 1.0f);
            AutoPFireCover = settings.GetFloat("FULLAUTO PISTOL", "InCoverROF", 1.0f);
            SawnOffFire = settings.GetFloat("SAWNOFF SHOTGUN", "NormalROF", 1.0f);
            SawnOffFireDb = settings.GetFloat("SAWNOFF SHOTGUN", "DrivebyROF", 1.0f);
            SawnOffFireCover = settings.GetFloat("SAWNOFF SHOTGUN", "InCoverROF", 1.0f);
            AssaultShotFire = settings.GetFloat("ASSAULT SHOTGUN", "NormalROF", 1.0f);
            AssaultShotFireDb = settings.GetFloat("ASSAULT SHOTGUN", "DrivebyROF", 1.0f);
            AssaultShotFireCover = settings.GetFloat("ASSAULT SHOTGUN", "InCoverROF", 1.0f);
            GrndLaunchFire = settings.GetFloat("GRENADE LAUNCHER", "NormalROF", 1.0f);
            GrndLaunchFireCover = settings.GetFloat("GRENADE LAUNCHER", "InCoverROF", 1.0f);
            Pistol44Fire = settings.GetFloat("PISTOL 44", "NormalROF", 1.0f);
            Pistol44FireDb = settings.GetFloat("PISTOL 44", "DrivebyROF", 1.0f);
            Pistol44FireCover = settings.GetFloat("PISTOL 44", "InCoverROF", 1.0f);
            AA12Fire = settings.GetFloat("AA12", "NormalROF", 1.0f);
            AA12FireCover = settings.GetFloat("AA12", "InCoverROF", 1.0f);
            AA12ExpFire = settings.GetFloat("AA12 EXP", "NormalROF", 1.0f);
            AA12ExpFireCover = settings.GetFloat("AA12 EXP", "InCoverROF", 1.0f);
            P90Fire = settings.GetFloat("P90", "NormalROF", 1.0f);
            P90FireDb = settings.GetFloat("P90", "DrivebyROF", 1.0f);
            P90FireCover = settings.GetFloat("P90", "InCoverROF", 1.0f);
            GoldUziFire = settings.GetFloat("GOLD UZI", "NormalROF", 1.0f);
            GoldUziFireDb = settings.GetFloat("GOLD UZI", "DrivebyROF", 1.0f);
            GoldUziFireCover = settings.GetFloat("GOLD UZI", "InCoverROF", 1.0f);
            M249Fire = settings.GetFloat("M249", "NormalROF", 1.0f);
            M249FireDb = settings.GetFloat("M249", "DrivebyROF", 1.0f);
            M249FireCover = settings.GetFloat("M249", "InCoverROF", 1.0f);
            AdvSnipFire = settings.GetFloat("ADV SNIPER", "NormalROF", 1.0f);
            AdvSnipFireDb = settings.GetFloat("ADV SNIPER", "DrivebyROF", 1.0f);
            AdvSnipFireCover = settings.GetFloat("ADV SNIPER", "InCoverROF", 1.0f);
            Episodic22Fire = settings.GetFloat("EPISODIC 22", "NormalROF", 1.0f);
            Episodic22FireDb = settings.GetFloat("EPISODIC 22", "DrivebyROF", 1.0f);
            Episodic22FireCover = settings.GetFloat("EPISODIC 22", "InCoverROF", 1.0f);
            Episodic22Anim = settings.GetValue("EPISODIC 22", "NormalAnim", "");
            Episodic22AnimCover = settings.GetValue("EPISODIC 22", "InCoverAnim", "");
            Episodic23Fire = settings.GetFloat("EPISODIC 23", "NormalROF", 1.0f);
            Episodic23FireDb = settings.GetFloat("EPISODIC 23", "DrivebyROF", 1.0f);
            Episodic23FireCover = settings.GetFloat("EPISODIC 23", "InCoverROF", 1.0f);
            Episodic23Anim = settings.GetValue("EPISODIC 23", "NormalAnim", "");
            Episodic23AnimCover = settings.GetValue("EPISODIC 23", "InCoverAnim", "");
            Episodic24Fire = settings.GetFloat("EPISODIC 24", "NormalROF", 1.0f);
            Episodic24FireDb = settings.GetFloat("EPISODIC 24", "DrivebyROF", 1.0f);
            Episodic24FireCover = settings.GetFloat("EPISODIC 24", "InCoverROF", 1.0f);
            Episodic24Anim = settings.GetValue("EPISODIC 24", "NormalAnim", "");
            Episodic24AnimCover = settings.GetValue("EPISODIC 24", "InCoverAnim", "");
            Episodic3Fire = settings.GetFloat("EPISODIC 3", "NormalROF", 1.0f);
            Episodic3FireDb = settings.GetFloat("EPISODIC 3", "DrivebyROF", 1.0f);
            Episodic3FireCover = settings.GetFloat("EPISODIC 3", "InCoverROF", 1.0f);
            Episodic3Anim = settings.GetValue("EPISODIC 3", "NormalAnim", "");
            Episodic3AnimCover = settings.GetValue("EPISODIC 3", "InCoverAnim", "");
            Addon1Fire = settings.GetFloat("ADDONWEAPON 1", "NormalROF", 1.0f);
            Addon1FireDb = settings.GetFloat("ADDONWEAPON 1", "DrivebyROF", 1.0f);
            Addon1FireCover = settings.GetFloat("ADDONWEAPON 1", "InCoverROF", 1.0f);
            Addon1Anim = settings.GetValue("ADDONWEAPON 1", "NormalAnim", "");
            Addon1AnimCover = settings.GetValue("ADDONWEAPON 1", "InCoverAnim", "");
            Addon2Fire = settings.GetFloat("ADDONWEAPON 2", "NormalROF", 1.0f);
            Addon2FireDb = settings.GetFloat("ADDONWEAPON 2", "DrivebyROF", 1.0f);
            Addon2FireCover = settings.GetFloat("ADDONWEAPON 2", "InCoverROF", 1.0f);
            Addon2Anim = settings.GetValue("ADDONWEAPON 2", "NormalAnim", "");
            Addon2AnimCover = settings.GetValue("ADDONWEAPON 2", "InCoverAnim", "");
            Addon3Fire = settings.GetFloat("ADDONWEAPON 3", "NormalROF", 1.0f);
            Addon3FireDb = settings.GetFloat("ADDONWEAPON 3", "DrivebyROF", 1.0f);
            Addon3FireCover = settings.GetFloat("ADDONWEAPON 3", "InCoverROF", 1.0f);
            Addon3Anim = settings.GetValue("ADDONWEAPON 3", "NormalAnim", "");
            Addon3AnimCover = settings.GetValue("ADDONWEAPON 3", "InCoverAnim", "");
            Addon4Fire = settings.GetFloat("ADDONWEAPON 4", "NormalROF", 1.0f);
            Addon4FireDb = settings.GetFloat("ADDONWEAPON 4", "DrivebyROF", 1.0f);
            Addon4FireCover = settings.GetFloat("ADDONWEAPON 4", "InCoverROF", 1.0f);
            Addon4Anim = settings.GetValue("ADDONWEAPON 4", "NormalAnim", "");
            Addon4AnimCover = settings.GetValue("ADDONWEAPON 4", "InCoverAnim", "");
            Addon5Fire = settings.GetFloat("ADDONWEAPON 5", "NormalROF", 1.0f);
            Addon5FireDb = settings.GetFloat("ADDONWEAPON 5", "DrivebyROF", 1.0f);
            Addon5FireCover = settings.GetFloat("ADDONWEAPON 5", "InCoverROF", 1.0f);
            Addon5Anim = settings.GetValue("ADDONWEAPON 5", "NormalAnim", "");
            Addon5AnimCover = settings.GetValue("ADDONWEAPON 5", "InCoverAnim", "");
            Addon6Fire = settings.GetFloat("ADDONWEAPON 6", "NormalROF", 1.0f);
            Addon6FireDb = settings.GetFloat("ADDONWEAPON 6", "DrivebyROF", 1.0f);
            Addon6FireCover = settings.GetFloat("ADDONWEAPON 6", "InCoverROF", 1.0f);
            Addon6Anim = settings.GetValue("ADDONWEAPON 6", "NormalAnim", "");
            Addon6AnimCover = settings.GetValue("ADDONWEAPON 6", "InCoverAnim", "");
            Addon7Fire = settings.GetFloat("ADDONWEAPON 7", "NormalROF", 1.0f);
            Addon7FireDb = settings.GetFloat("ADDONWEAPON 7", "DrivebyROF", 1.0f);
            Addon7FireCover = settings.GetFloat("ADDONWEAPON 7", "InCoverROF", 1.0f);
            Addon7Anim = settings.GetValue("ADDONWEAPON 7", "NormalAnim", "");
            Addon7AnimCover = settings.GetValue("ADDONWEAPON 7", "InCoverAnim", "");
            Addon8Fire = settings.GetFloat("ADDONWEAPON 8", "NormalROF", 1.0f);
            Addon8FireDb = settings.GetFloat("ADDONWEAPON 8", "DrivebyROF", 1.0f);
            Addon8FireCover = settings.GetFloat("ADDONWEAPON 8", "InCoverROF", 1.0f);
            Addon8Anim = settings.GetValue("ADDONWEAPON 8", "NormalAnim", "");
            Addon8AnimCover = settings.GetValue("ADDONWEAPON 8", "InCoverAnim", "");
            Addon9Fire = settings.GetFloat("ADDONWEAPON 9", "NormalROF", 1.0f);
            Addon9FireDb = settings.GetFloat("ADDONWEAPON 9", "DrivebyROF", 1.0f);
            Addon9FireCover = settings.GetFloat("ADDONWEAPON 9", "InCoverROF", 1.0f);
            Addon9Anim = settings.GetValue("ADDONWEAPON 9", "NormalAnim", "");
            Addon9AnimCover = settings.GetValue("ADDONWEAPON 9", "InCoverAnim", "");
            Addon10Fire = settings.GetFloat("ADDONWEAPON 10", "NormalROF", 1.0f);
            Addon10FireDb = settings.GetFloat("ADDONWEAPON 10", "DrivebyROF", 1.0f);
            Addon10FireCover = settings.GetFloat("ADDONWEAPON 10", "InCoverROF", 1.0f);
            Addon10Anim = settings.GetValue("ADDONWEAPON 10", "NormalAnim", "");
            Addon10AnimCover = settings.GetValue("ADDONWEAPON 10", "InCoverAnim", "");
            Addon11Fire = settings.GetFloat("ADDONWEAPON 11", "NormalROF", 1.0f);
            Addon11FireDb = settings.GetFloat("ADDONWEAPON 11", "DrivebyROF", 1.0f);
            Addon11FireCover = settings.GetFloat("ADDONWEAPON 11", "InCoverROF", 1.0f);
            Addon11Anim = settings.GetValue("ADDONWEAPON 11", "NormalAnim", "");
            Addon11AnimCover = settings.GetValue("ADDONWEAPON 11", "InCoverAnim", "");
            Addon12Fire = settings.GetFloat("ADDONWEAPON 12", "NormalROF", 1.0f);
            Addon12FireDb = settings.GetFloat("ADDONWEAPON 12", "DrivebyROF", 1.0f);
            Addon12FireCover = settings.GetFloat("ADDONWEAPON 12", "InCoverROF", 1.0f);
            Addon12Anim = settings.GetValue("ADDONWEAPON 12", "NormalAnim", "");
            Addon12AnimCover = settings.GetValue("ADDONWEAPON 12", "InCoverAnim", "");
            Addon13Fire = settings.GetFloat("ADDONWEAPON 13", "NormalROF", 1.0f);
            Addon13FireDb = settings.GetFloat("ADDONWEAPON 13", "DrivebyROF", 1.0f);
            Addon13FireCover = settings.GetFloat("ADDONWEAPON 13", "InCoverROF", 1.0f);
            Addon13Anim = settings.GetValue("ADDONWEAPON 13", "NormalAnim", "");
            Addon13AnimCover = settings.GetValue("ADDONWEAPON 13", "InCoverAnim", "");
            Addon14Fire = settings.GetFloat("ADDONWEAPON 14", "NormalROF", 1.0f);
            Addon14FireDb = settings.GetFloat("ADDONWEAPON 14", "DrivebyROF", 1.0f);
            Addon14FireCover = settings.GetFloat("ADDONWEAPON 14", "InCoverROF", 1.0f);
            Addon14Anim = settings.GetValue("ADDONWEAPON 14", "NormalAnim", "");
            Addon14AnimCover = settings.GetValue("ADDONWEAPON 14", "InCoverAnim", "");
            Addon15Fire = settings.GetFloat("ADDONWEAPON 15", "NormalROF", 1.0f);
            Addon15FireDb = settings.GetFloat("ADDONWEAPON 15", "DrivebyROF", 1.0f);
            Addon15FireCover = settings.GetFloat("ADDONWEAPON 15", "InCoverROF", 1.0f);
            Addon15Anim = settings.GetValue("ADDONWEAPON 15", "NormalAnim", "");
            Addon15AnimCover = settings.GetValue("ADDONWEAPON 15", "InCoverAnim", "");
            Addon16Fire = settings.GetFloat("ADDONWEAPON 16", "NormalROF", 1.0f);
            Addon16FireDb = settings.GetFloat("ADDONWEAPON 16", "DrivebyROF", 1.0f);
            Addon16FireCover = settings.GetFloat("ADDONWEAPON 16", "InCoverROF", 1.0f);
            Addon16Anim = settings.GetValue("ADDONWEAPON 16", "NormalAnim", "");
            Addon16AnimCover = settings.GetValue("ADDONWEAPON 16", "InCoverAnim", "");
            Addon17Fire = settings.GetFloat("ADDONWEAPON 17", "NormalROF", 1.0f);
            Addon17FireDb = settings.GetFloat("ADDONWEAPON 17", "DrivebyROF", 1.0f);
            Addon17FireCover = settings.GetFloat("ADDONWEAPON 17", "InCoverROF", 1.0f);
            Addon17Anim = settings.GetValue("ADDONWEAPON 17", "NormalAnim", "");
            Addon17AnimCover = settings.GetValue("ADDONWEAPON 17", "InCoverAnim", "");
            Addon18Fire = settings.GetFloat("ADDONWEAPON 18", "NormalROF", 1.0f);
            Addon18FireDb = settings.GetFloat("ADDONWEAPON 18", "DrivebyROF", 1.0f);
            Addon18FireCover = settings.GetFloat("ADDONWEAPON 18", "InCoverROF", 1.0f);
            Addon18Anim = settings.GetValue("ADDONWEAPON 18", "NormalAnim", "");
            Addon18AnimCover = settings.GetValue("ADDONWEAPON 18", "InCoverAnim", "");
            Addon19Fire = settings.GetFloat("ADDONWEAPON 19", "NormalROF", 1.0f);
            Addon19FireDb = settings.GetFloat("ADDONWEAPON 19", "DrivebyROF", 1.0f);
            Addon19FireCover = settings.GetFloat("ADDONWEAPON 19", "InCoverROF", 1.0f);
            Addon19Anim = settings.GetValue("ADDONWEAPON 19", "NormalAnim", "");
            Addon19AnimCover = settings.GetValue("ADDONWEAPON 19", "InCoverAnim", "");
            Addon20Fire = settings.GetFloat("ADDONWEAPON 20", "NormalROF", 1.0f);
            Addon20FireDb = settings.GetFloat("ADDONWEAPON 20", "DrivebyROF", 1.0f);
            Addon20FireCover = settings.GetFloat("ADDONWEAPON 20", "InCoverROF", 1.0f);
            Addon20Anim = settings.GetValue("ADDONWEAPON 20", "NormalAnim", "");
            Addon20AnimCover = settings.GetValue("ADDONWEAPON 20", "InCoverAnim", "");
        }
    }
}