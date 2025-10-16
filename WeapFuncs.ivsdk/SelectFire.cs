using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using CCL.GTAIV;
using System.Security.Policy;
using System.Numerics;
using System.Linq;

namespace WeapFuncs.ivsdk
{
    internal class SelectFire
    {
        private static bool CheckTime;
        private static uint fTimer;

        private static bool hasPressedButton;
        private static bool GotAmmo;
        private static bool getAccTime;

        private static int weapIndex;
        private static float accuracyTimeBurstNum;
        private static float accuracyTimeBurstMult;
        private static float accuracyTimeSemiNum;
        private static float accuracyTimeSemiMult;
        private static float defaultAccTime;
        private static int timeBetBurst;
        private static int currFireType;
        private static float NumOfBullets;
        private static int fireType = 0;
        private static int lastAmmo;
        private static int soundID = -1;
        private static int pWeapon = 0;
        private static string pWeapAnim = "";
        private static string pBFAnim = "";

        private static readonly List<eWeaponType> BurstWeaps = new List<eWeaponType>();

        private static List<float> Loop1 = new List<float>();
        private static List<float> Loop2 = new List<float>();
        private static List<float> Loop3 = new List<float>();
        private static List<float> Loop4 = new List<float>();
        private static List<float> Loop5 = new List<float>();
        private static List<float> Loop6 = new List<float>();
        private static List<float> Loop7 = new List<float>();
        private static List<float> Loop8 = new List<float>();
        public static void Init(SettingsFile settings)
        {
            timeBetBurst = settings.GetInteger("SELECT FIRE", "TimeBetweenShots", 250);
            accuracyTimeBurstNum = settings.GetFloat("SELECT FIRE", "BurstAccuracyTime", 0);
            accuracyTimeBurstMult = settings.GetFloat("SELECT FIRE", "BurstAccuracyMult", 8);
            accuracyTimeSemiNum = settings.GetFloat("SELECT FIRE", "SemiAutoAccuracyTime", 0);
            accuracyTimeSemiMult = settings.GetFloat("SELECT FIRE", "SemiAutoAccuracyMult", 16);
            string weaponString = settings.GetValue("SELECT FIRE", "SelectFireWeapons", "");
            BurstWeaps.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                BurstWeaps.Add(weaponType);
            }

            Loop1.Clear();
            Loop2.Clear();
            Loop3.Clear();
            Loop4.Clear();
            Loop5.Clear();
            Loop6.Clear();
            Loop7.Clear();
            Loop8.Clear();
            string wLoop1 = settings.GetValue("SELECT FIRE", "SelectFireLoopNormal", "");
            Loop1 = wLoop1.Split(',').Select(float.Parse).ToList();
            string wLoop2 = settings.GetValue("SELECT FIRE", "SelectFireLoopDriveBy", "");
            Loop2 = wLoop2.Split(',').Select(float.Parse).ToList();
            string wLoop3 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireLHigh", "");
            Loop3 = wLoop3.Split(',').Select(float.Parse).ToList();
            string wLoop4 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireLCntr", "");
            Loop4 = wLoop4.Split(',').Select(float.Parse).ToList();
            string wLoop5 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireLCrnr", "");
            Loop5 = wLoop5.Split(',').Select(float.Parse).ToList();
            string wLoop6 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireRHigh", "");
            Loop6 = wLoop6.Split(',').Select(float.Parse).ToList();
            string wLoop7 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireRCntr", "");
            Loop7 = wLoop7.Split(',').Select(float.Parse).ToList();
            string wLoop8 = settings.GetValue("SELECT FIRE", "SelectFireLoopBlindFireRCrnr", "");
            Loop8 = wLoop8.Split(',').Select(float.Parse).ToList();
        }
        public static void Tick()
        {
            pWeapAnim = Main.WeapAnim;
            pBFAnim = Main.BFAnim;

            if (Main.FireMode)
            {
                foreach (eWeaponType weaponType in BurstWeaps)
                {
                    if (Main.currWeap == (int)weaponType)
                    {
                        weapIndex = BurstWeaps.IndexOf(weaponType);
                        GET_MAX_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out int pMaxAmmo);
                        if (Main.IsPressingAimButton() && (NativeControls.IsGameKeyPressed(0, Main.SelectFireCtrl) || NativeControls.IsGameKeyPressed(2, Main.SelectFireCtrl)) && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack) && !hasPressedButton)
                        {
                            if (fireType < 2)
                                fireType += 1;
                            else
                                fireType = 0;

                            hasPressedButton = true;
                            PLAY_SOUND_FRONTEND(soundID, "GENERAL_GUNS_AK47_DRY_CLICK");
                            if (Main.ShowFireModeText)
                            {
                                if (fireType == 0)
                                    IVGame.ShowSubtitleMessage("Full-Auto");
                                else if (fireType == 1)
                                    IVGame.ShowSubtitleMessage("Burst");
                                else if (fireType == 2)
                                    IVGame.ShowSubtitleMessage("Semi-Auto");
                            }
                        }
                        else if (!NativeControls.IsGameKeyPressed(0, Main.SelectFireCtrl) && !NativeControls.IsGameKeyPressed(2, Main.SelectFireCtrl) && hasPressedButton)
                            hasPressedButton = false;

                        if (pWeapon == Main.currWeap)
                        {
                            if (currFireType > 0)
                            {
                                if (currFireType == 1)
                                {
                                    if (!getAccTime)
                                    {
                                        defaultAccTime = IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime;
                                        if (accuracyTimeBurstNum > 0)
                                            IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = accuracyTimeBurstNum;
                                        else
                                            IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime * accuracyTimeBurstMult;
                                        getAccTime = true;
                                    }
                                    NumOfBullets = Main.ShotsPerBurst;
                                }

                                else
                                {
                                    if (!getAccTime)
                                    {
                                        defaultAccTime = IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime;
                                        if (accuracyTimeSemiNum > 0)
                                            IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = accuracyTimeSemiNum;
                                        else
                                            IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime * accuracyTimeSemiMult;
                                        getAccTime = true;
                                    }
                                    NumOfBullets = 1;
                                }

                                if ((NativeControls.IsGameKeyPressed(0, GameKey.Attack) || NativeControls.IsGameKeyPressed(2, GameKey.Attack)) && Main.pAmmo > 0)
                                {
                                    if ((!GotAmmo && Main.pAmmo != 0) || Main.pAmmo == pMaxAmmo)
                                    {
                                        lastAmmo = Main.pAmmo;
                                        GotAmmo = true;

                                        CheckTime = false;
                                    }
                                    if ((lastAmmo - Main.pAmmo) == NumOfBullets)
                                    {
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", Loop1[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", Loop1[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", Loop1[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", Loop1[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", Loop1[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", Loop1[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire", Loop2[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire_l", Loop2[weapIndex]);

                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, Loop3[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, Loop4[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, Loop5[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, Loop6[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, Loop7[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, Loop8[weapIndex]);

                                        if (!Main.PressToFire || IS_PED_IN_COVER(Main.PlayerHandle) || IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                                        {
                                            GET_GAME_TIMER(out uint gTimer);

                                            if (CheckTime == false)
                                            {
                                                GET_GAME_TIMER(out fTimer);
                                                CheckTime = true;
                                            }

                                            //IVGame.ShowSubtitleMessage(gTimer.ToString() + "  " + fTimer.ToString());
                                            if (gTimer >= (fTimer + timeBetBurst))
                                            {
                                                CheckTime = false;
                                                lastAmmo = Main.pAmmo;
                                            }
                                        }
                                    }
                                }

                                else if (lastAmmo != Main.pAmmo)
                                {
                                    CheckTime = false;
                                    lastAmmo = Main.pAmmo;
                                }
                                // This almost works if anim time is changed, just needs tweaking.
                                /*else
                                {
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire"))
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", 0.9f);
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_alt"))
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", 0.9f);
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_up"))
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", 0.9f);
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_down"))
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", 0.9f);
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_crouch"))
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", 0.9f);
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt"))
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", 0.9f);

                                }*/
                            }
                            if (currFireType != fireType)
                            {
                                if (getAccTime)
                                {
                                    IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
                                    getAccTime = false;
                                }
                                currFireType = fireType;
                            }
                        }
                        else if (pWeapon != Main.currWeap)
                        {
                            if (getAccTime)
                            {
                                IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
                                defaultAccTime = IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).AimingAccuracyTime;
                                getAccTime = false;
                            }
                            pWeapon = Main.currWeap;
                        }
                    }
                }
            }
        }
    }
}
