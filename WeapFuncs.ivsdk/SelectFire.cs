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
        private static bool getAccTime;

        private static float totalTime;
        private static float animTime = -1;
        private static float frameSkipTime;

        private static int weapIndex;
        private static int frameTimeSkip;

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
        private static List<float> AnimLoopEnd = new List<float>();

        public static void Init(SettingsFile settings)
        {
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
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                BurstWeaps.Add(weaponType);
            }

            AnimLoopEnd.Clear();
            string wLoop = settings.GetValue("SELECT FIRE", "BurstSemiAnimEnd", "");
            AnimLoopEnd = wLoop.Split(',').Select(float.Parse).ToList();

        }
        public static void UnInit()
        {
            IVWeaponInfo.GetWeaponInfo((uint)pWeapon).AimingAccuracyTime = defaultAccTime;
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

                                        CheckTime = false;
                                    }
                                    if ((lastAmmo - Main.pAmmo) == NumOfBullets)
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

                                        if (!Main.PressToFire || IS_PED_IN_COVER(Main.PlayerHandle) || IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
                                        {
                                            if (CheckTime == false)
                                            {
                                                GET_GAME_TIMER(out fTimer);
                                                CheckTime = true;
                                            }

                                            //IVGame.ShowSubtitleMessage(gTimer.ToString() + "  " + fTimer.ToString());
                                            if (Main.gTimer >= (fTimer + timeBetBurst))
                                            {
                                                if (IS_PED_IN_COVER(Main.PlayerHandle))
                                                    FREEZE_CHAR_POSITION(Main.PlayerHandle, false);
                                                CheckTime = false;
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
                                        CheckTime = false;
                                        lastAmmo = Main.pAmmo;

                                        //IVGame.ShowSubtitleMessage(animTime.ToString() + "  " + totalTime.ToString() + "  " + ((totalTime * 0.03f * animTime) - 3) / (totalTime * 0.03f) + "  " + (frameTimeSkip * Main.frameTime).ToString());

                                        //CLEAR_CHAR_TASKS(Main.PlayerHandle);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", AnimLoopEnd[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", AnimLoopEnd[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", AnimLoopEnd[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", AnimLoopEnd[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", AnimLoopEnd[weapIndex]);
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", AnimLoopEnd[weapIndex]);
                                    }
                                }
                                // This almost works if anim time is changed, just needs tweaking.
                                //else
                                //{
                                //}
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
