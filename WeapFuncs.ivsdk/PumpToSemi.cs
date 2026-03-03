using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Windows.Forms;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class PumpToSemi
    {
        private static bool hasPressedButton;

        private static int currFireType;
        private static int fireType = 0;
        private static int soundID = -1;
        private static int pWeapon = 0;
        private static string pWeapAnim = "";
        private static string pBFAnim = "";

        private static readonly List<eWeaponType> WeapList = new List<eWeaponType>();
        private static List<float> AnimLoopStart = new List<float>();
        private static List<float> AnimLoopEnd = new List<float>();
        public static void Init(SettingsFile settings)
        {
            string weaponString = settings.GetValue("SELECT FIRE", "PumpSemiWeapons", "");
            WeapList.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                WeapList.Add(weaponType);
            }

            AnimLoopStart.Clear();
            string sLoop = settings.GetValue("SELECT FIRE", "PumpSemiAnimStart", "");
            AnimLoopStart = sLoop.Split(',').Select(float.Parse).ToList();

            AnimLoopEnd.Clear();
            string eLoop = settings.GetValue("SELECT FIRE", "PumpSemiAnimEnd", "");
            AnimLoopEnd = eLoop.Split(',').Select(float.Parse).ToList();
        }
        public static void Tick()
        {
            pWeapAnim = Main.WeapAnim;
            pBFAnim = Main.BFAnim;

            if (Main.FireMode)
            {
                foreach (eWeaponType weaponType in WeapList)
                {
                    if (Main.currWeap == (int)weaponType)
                    {
                        GET_MAX_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out int pMaxAmmo);
                        if (Main.IsPressingAimButton() && (NativeControls.IsGameKeyPressed(0, Main.SelectFireCtrl) || NativeControls.IsGameKeyPressed(2, Main.SelectFireCtrl)) && !NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack) && !hasPressedButton)
                        {
                            if (fireType < 1)
                                fireType += 1;
                            else
                                fireType = 0;

                            hasPressedButton = true;
                            PLAY_SOUND_FRONTEND(soundID, "GENERAL_GUNS_AK47_DRY_CLICK");
                            if (Main.ShowFireModeText)
                            {
                                if (fireType == 0)
                                    IVGame.ShowSubtitleMessage("Pump");
                                else if (fireType == 1)
                                    IVGame.ShowSubtitleMessage("Semi-Auto");
                            }
                        }
                        else if (!NativeControls.IsGameKeyPressed(0, Main.SelectFireCtrl) && !NativeControls.IsGameKeyPressed(2, Main.SelectFireCtrl) && hasPressedButton)
                            hasPressedButton = false;

                        if (pWeapon == Main.currWeap)
                        {
                            if (currFireType > 0)
                            {
                                /*if (IS_CHAR_SHOOTING(Main.PlayerHandle))
                                {
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", 0.8f);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", 0.8f);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", 0.8f);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", 0.8f);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", 0.8f);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", 0.8f);
                                }*/
                                if (IS_CHAR_SHOOTING(Main.PlayerHandle) && AnimLoopStart[WeapList.IndexOf(weaponType)] < 0)
                                {
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_alt", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_up", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_down", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
                                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire_crouch_alt", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
                                }

                                else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, pWeapAnim, "fire"))
                                {
                                    GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", out float animTime);
                                    if (animTime > AnimLoopStart[WeapList.IndexOf(weaponType)] && animTime < AnimLoopEnd[WeapList.IndexOf(weaponType)])
                                        SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, pWeapAnim, "fire", AnimLoopEnd[WeapList.IndexOf(weaponType)]);
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
        }
    }
}

