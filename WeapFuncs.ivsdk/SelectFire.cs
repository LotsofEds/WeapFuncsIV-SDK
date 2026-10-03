using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Windows.Forms;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class SelectFire
    {
        // IniShit
        private static bool showFireModeText;
        private static bool pressToFire;

        private static int timeBetBurst;
        private static float accuracyTimeBurstNum;
        private static float accuracyTimeBurstMult;
        private static float accuracyTimeSemiNum;
        private static float accuracyTimeSemiMult;
        private static int frameTimeSkip;

        // OtherShit
        private static int shotsPerBurst;
        private static int selectFireCtrl;
        private static int fireTypes;
        private static int switchType;

        private static bool checkTime;
        private static uint fTimer;

        private static bool getAccTime;

        private static float totalTime;
        private static float animTime = -1;
        private static float frameSkipTime;

        private static int weapIndex;

        private static float defaultAccTime;
        private static int currFireType;
        private static float numOfBullets;
        private static int fireType = 0;
        private static int lastAmmo;
        private static int soundID = -1;
        private static int pWeapon = 0;
        private static string pWeapAnim = "";
        private static string pBFAnim = "";
        private static float timeBeforeShot = 0;
        private static float animPointer;

        // ListShit
        private static readonly List<int> BurstWeaps = new List<int>();
        private static List<float> AnimLoopEnd = new List<float>();

        private static readonly List<int> PumpToSemiList = new List<int>();
        private static List<float> P2SAnimLoopStart = new List<float>();
        private static List<float> P2SAnimLoopEnd = new List<float>();

        private static readonly List<int> DTList = new List<int>();

        public static void Init(SettingsFile settings)
        {
            showFireModeText = settings.GetBoolean("SELECT FIRE", "ShowFireModeText", false);
            pressToFire = settings.GetBoolean("SELECT FIRE", "PressToFire", false);
            selectFireCtrl = settings.GetInteger("SELECT FIRE", "SelectFireControl", 23);

            BurstInit(Main.wfConfig);
            Pump2SemiInit(Main.wfConfig);
            DoubleTriggerInit(Main.wfConfig);
        }
        private static void BurstInit(SettingsFile settings)
        {
            shotsPerBurst = settings.GetInteger("SELECT FIRE", "ShotsPerBurst", 3);
            timeBetBurst = settings.GetInteger("SELECT FIRE", "TimeBetweenShots", 250);
            accuracyTimeBurstNum = settings.GetFloat("SELECT FIRE", "BurstAccuracyTime", 0);
            accuracyTimeBurstMult = settings.GetFloat("SELECT FIRE", "BurstAccuracyMult", 8);
            accuracyTimeSemiNum = settings.GetFloat("SELECT FIRE", "SemiAutoAccuracyTime", 0);
            accuracyTimeSemiMult = settings.GetFloat("SELECT FIRE", "SemiAutoAccuracyMult", 16);
            frameTimeSkip = settings.GetInteger("SELECT FIRE", "FrameCheckRate", 200);

            string weaponString = settings.GetValue("SELECT FIRE", "BurstSemiWeapons", "");
            BurstWeaps.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                int weaponID = Int32.Parse(weaponName.Trim());
                BurstWeaps.Add(weaponID);
            }

            AnimLoopEnd.Clear();
            string wLoop = settings.GetValue("SELECT FIRE", "BurstSemiAnimEnd", "");
            AnimLoopEnd = wLoop.Split(',').Select(float.Parse).ToList();
        }
        private static void Pump2SemiInit(SettingsFile settings)
        {
            string weaponString = settings.GetValue("SELECT FIRE", "PumpSemiWeapons", "");
            PumpToSemiList.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                int weaponID = Int32.Parse(weaponName.Trim());
                PumpToSemiList.Add(weaponID);
            }

            P2SAnimLoopStart.Clear();
            string sLoop = settings.GetValue("SELECT FIRE", "PumpSemiAnimStart", "");
            P2SAnimLoopStart = sLoop.Split(',').Select(float.Parse).ToList();

            P2SAnimLoopEnd.Clear();
            string eLoop = settings.GetValue("SELECT FIRE", "PumpSemiAnimEnd", "");
            P2SAnimLoopEnd = eLoop.Split(',').Select(float.Parse).ToList();
        }
        private static void DoubleTriggerInit(SettingsFile settings)
        {
            string weaponString = settings.GetValue("SELECT FIRE", "DoubleTriggerWeapons", "");
            DTList.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                int weaponID = Int32.Parse(weaponName.Trim());
                DTList.Add(weaponID);
            }
        }
        public static void UnInit()
        {
            IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
        }
        public static void Tick()
        {
            pWeapAnim = Main.wAnim;
            pBFAnim = Main.bfAnim;

            BurstSemiAuto();
            PumpToSemi();
            DoubleTrigger();
        }
        private static void BurstSemiAuto()
        {
            foreach (int weaponType in BurstWeaps)
            {
                if (Main.currWeap == (int)weaponType)
                {
                    fireTypes = 2;
                    weapIndex = BurstWeaps.IndexOf(weaponType);

                    switchType = 1;
                    SwitchModes();
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
                                numOfBullets = shotsPerBurst;
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
                                numOfBullets = 1;
                            }

                            if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) && Main.pAmmo > 0)
                            {
                                if (IS_CHAR_SHOOTING(Main.PlayerHandle) && animTime == -1)
                                {
                                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire"))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, pWeapAnim, "fire", out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_alt"))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_up"))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_down"))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_crouch"))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt"))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_high_corner", pBFAnim))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_centre", pBFAnim))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_l_low_corner", pBFAnim))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_high_corner", pBFAnim))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_centre", pBFAnim))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, out animTime);
                                    }
                                    else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, "cover_r_low_corner", pBFAnim))
                                    {
                                        GET_CHAR_ANIM_TOTAL_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, out totalTime);
                                        GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, out animTime);
                                    }
                                    lastAmmo = Main.pAmmo + 1;

                                    checkTime = false;
                                }
                                if ((lastAmmo - Main.pAmmo) == numOfBullets)
                                {
                                    if ((frameTimeSkip * Main.frameTime) < 4)
                                        frameSkipTime = ((int)Math.Floor(totalTime * 0.03f * animTime) - 4) / (totalTime * 0.03f);
                                    else
                                        frameSkipTime = ((int)Math.Floor(totalTime * 0.03f * animTime) - (int)Math.Ceiling(frameTimeSkip * Main.frameTime)) / (totalTime * 0.03f);

                                    if (IS_PED_IN_COVER(Main.PlayerHandle))
                                        FREEZE_CHAR_POSITION(Main.PlayerHandle, true);

                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire", 0);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "dbfire_l", 0);

                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_high_corner", pBFAnim, frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_centre", pBFAnim, frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_l_low_corner", pBFAnim, frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_high_corner", pBFAnim, frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_centre", pBFAnim, frameSkipTime);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "cover_r_low_corner", pBFAnim, frameSkipTime);

                                    if (!pressToFire || IS_PED_IN_COVER(Main.PlayerHandle) || IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                                    {
                                        if (checkTime == false)
                                        {
                                            GET_GAME_TIMER(out fTimer);
                                            checkTime = true;
                                        }

                                        if (Main.gTimer >= (fTimer + timeBetBurst))
                                        {
                                            if (IS_PED_IN_COVER(Main.PlayerHandle))
                                                FREEZE_CHAR_POSITION(Main.PlayerHandle, false);
                                            checkTime = false;
                                            lastAmmo = Main.pAmmo;
                                        }
                                    }
                                }
                            }

                            else
                            {
                                if (IS_PED_IN_COVER(Main.PlayerHandle))
                                    FREEZE_CHAR_POSITION(Main.PlayerHandle, false);
                                if (lastAmmo != Main.pAmmo)
                                {
                                    animTime = -1;
                                    checkTime = false;
                                    lastAmmo = Main.pAmmo;

                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", AnimLoopEnd[weapIndex]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", AnimLoopEnd[weapIndex]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", AnimLoopEnd[weapIndex]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", AnimLoopEnd[weapIndex]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", AnimLoopEnd[weapIndex]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", AnimLoopEnd[weapIndex]);
                                }
                            }
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
        private static void PumpToSemi()
        {
            foreach (int weaponType in PumpToSemiList)
            {
                if (Main.currWeap == weaponType)
                {
                    fireTypes = 1;

                    if (pWeapon == Main.currWeap)
                    {
                        switchType = 2;
                        SwitchModes();
                        if (currFireType > 0)
                        {
                            if (IS_CHAR_SHOOTING(Main.PlayerHandle) && P2SAnimLoopStart[PumpToSemiList.IndexOf(weaponType)] < 0)
                            {
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                                SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                            }

                            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire"))
                            {
                                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", out float animTime);
                                if (animTime > P2SAnimLoopStart[PumpToSemiList.IndexOf(weaponType)] && animTime < P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)])
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", P2SAnimLoopEnd[PumpToSemiList.IndexOf(weaponType)]);
                            }
                            ShotgunBlindfireFix.FixShotgunBF(Main.PlayerHandle);
                        }
                        if (currFireType != fireType)
                            currFireType = fireType;
                    }
                    else if (pWeapon != Main.currWeap)
                        pWeapon = Main.currWeap;
                }
            }
        }
        private static void DoubleTrigger()
        {
            foreach (int weaponType in DTList)
            {
                if (Main.currWeap == weaponType)
                {
                    fireTypes = 1;
                    switchType = 3;
                    SwitchModes();

                    if (currFireType > 0)
                    {
                        if (Main.pAmmo > 0 && NativeControls.IsGameKeyPressed(0, GameKey.Attack))
                        {
                            timeBeforeShot = GenHelp.Clamp(Main.frameTime, 0.02f, 1f);

                            if (IS_CHAR_SHOOTING(Main.PlayerHandle))
                            {
                                if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", out animPointer);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", animPointer - timeBeforeShot);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_alt"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", out animPointer);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", animPointer - timeBeforeShot);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_up"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", out animPointer);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", animPointer - timeBeforeShot);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_down"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", out animPointer);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", animPointer - timeBeforeShot);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_crouch"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", out animPointer);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", animPointer - timeBeforeShot);
                                }
                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", out animPointer);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", animPointer - timeBeforeShot);
                                }
                            }
                        }
                    }
                    if (currFireType != fireType)
                        currFireType = fireType;
                }
                else if (pWeapon != Main.currWeap)
                    pWeapon = Main.currWeap;
            }
        }
        private static void SwitchModes()
        {
            if (WeaponHelper.IsPressingAimButton() && IS_CONTROL_JUST_PRESSED(0, selectFireCtrl) && !NativeControls.IsGameKeyPressed(0, GameKey.Attack))
            {
                if (fireType < fireTypes)
                    fireType++;
                else
                    fireType = 0;

                PLAY_SOUND_FRONTEND(soundID, "GENERAL_GUNS_AK47_DRY_CLICK");
                if (showFireModeText)
                {
                    switch (fireType)
                    {
                        case 0:
                            if (switchType == 1)
                                IVGame.ShowSubtitleMessage("Full-Auto");
                            else if (switchType == 2)
                                IVGame.ShowSubtitleMessage("Pump");
                            else if (switchType == 3)
                                IVGame.ShowSubtitleMessage("Single");
                            break;
                        case 1:
                            if (switchType == 1)
                                IVGame.ShowSubtitleMessage("Burst");
                            else if (switchType == 2)
                                IVGame.ShowSubtitleMessage("Semi-Auto");
                            else if (switchType == 3)
                                IVGame.ShowSubtitleMessage("Simultaneous");
                            break;
                        case 2:
                            IVGame.ShowSubtitleMessage("Semi-Auto");
                            break;
                    }
                }
            }
        }
    }
}
