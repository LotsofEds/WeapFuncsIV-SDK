using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using System.Threading;
using System.Runtime;
using System.Numerics;
using CCL.GTAIV;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;

namespace WeapFuncs.ivsdk
{
    internal class GLaunchAttachment
    {
        private static uint fTimer;
        private static uint gTimer;
        private static bool hasPressedButton;
        private static int grenObj;
        private static Vector3 grndPos;
        private static int grndForce;

        private static bool cantFire;
        private static bool isReloading;
        private static bool animPlaying;
        private static bool ammoDisplay;
        private static int currWeap;

        private static string projModel;
        private static string reloadAnim;
        private static Vector3 grndOffset;
        private static GameKey GrndFireCtrl;
        private static int fuseTime;
        private static int expType;
        private static int gAmmo;
        private static int wIndex;
        private static uint alpha = 255;

        private static List<eWeaponType> GLaunchWeaps = new List<eWeaponType>();
        private static List<int> expTypes = new List<int>();
        public static void Init(SettingsFile settings)
        {
            projModel = settings.GetValue("ATTACHMENTS", "GrenadeModel", "");
            reloadAnim = settings.GetValue("ATTACHMENTS", "ReloadAnim", "");
            grndOffset = settings.GetVector3("ATTACHMENTS", "GrenadeOffset", Vector3.Zero);
            GrndFireCtrl = (GameKey)settings.GetInteger("ATTACHMENTS", "FireGrenadeControl", 7);
            fuseTime = settings.GetInteger("ATTACHMENTS", "GrenadeFuseTime", 4000);
            expType = settings.GetInteger("ATTACHMENTS", "ExplosionType", 0);
            grndForce = settings.GetInteger("ATTACHMENTS", "ProjectileForce", 40);
            gAmmo = settings.GetInteger("ATTACHMENTS", "GrenadeAmmo", 4);

            string weaponString = settings.GetValue("ATTACHMENTS", "WeaponsWithGrenadeLauncherAttachment", "");
            GLaunchWeaps.Clear();
            foreach (var weaponName in weaponString.Split(','))
            {
                eWeaponType weaponType = (eWeaponType)Enum.Parse(typeof(eWeaponType), weaponName.Trim(), true);
                GLaunchWeaps.Add(weaponType);
            }
            expTypes.Clear();
            string expString = settings.GetValue("ATTACHMENTS", "ExplosionTypes", "");
            expTypes = expString.Split(',').Select(int.Parse).ToList();
        }
        public static void OnButtonPress()
        {
            if (!IS_PED_RAGDOLL(Main.PlayerHandle) && gAmmo > 0 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "p_load") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload_crouch") && (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_up") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_down") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "dbfire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "dbfire_l")))
            {
                if (!cantFire && (NativeControls.IsGameKeyPressed(0, GrndFireCtrl)) && !hasPressedButton)
                {
                    GET_PED_BONE_POSITION(Main.PlayerHandle, 1232, grndOffset, out grndPos);
                    CREATE_OBJECT(GET_HASH_KEY(projModel), grndPos, out grenObj, true);
                    ATTACH_OBJECT_TO_PED(grenObj, Main.PlayerHandle, 1232, grndOffset.X, grndOffset.Y, grndOffset.Z, 0.0f, -0.75f, 0.0f, 0);
                    GET_CURRENT_CHAR_WEAPON(Main.PlayerHandle, out currWeap);
                    _TASK_PLAY_ANIM_SECONDARY_UPPER_BODY(Main.PlayerHandle, "reload", reloadAnim, 4.0f, 0, 0, 0, 0, 0);
                    gAmmo --;

                    GET_GAME_TIMER(out fTimer);

                    isReloading = true;
                    hasPressedButton = true;
                }
            }
            if (!(NativeControls.IsGameKeyPressed(0, GrndFireCtrl)) && hasPressedButton)
                hasPressedButton = false;
        }
        public static void Tick()
        {
            if (!HAVE_ANIMS_LOADED(reloadAnim))
                REQUEST_ANIMS(reloadAnim);

            GET_GAME_TIMER(out gTimer);
            foreach (eWeaponType weaponType in GLaunchWeaps)
            {
                if (Main.currWeap == (int)weaponType)
                {
                    OnButtonPress();
                    wIndex = GLaunchWeaps.IndexOf(weaponType);
                }
            }
            if (IS_HUD_PREFERENCE_SWITCHED_ON() && gTimer > 0 && gTimer <= (fTimer + 5000))
            {
                GET_FRAME_TIME(out float frameTime);

                if (gTimer > (fTimer + 4000))
                    alpha -= ((uint)(frameTime * 250f));
                else
                    alpha = 255;

                if (!IS_FONT_LOADED(4))
                    LOAD_TEXT_FONT(4);

                SET_TEXT_SCALE(0.225f, 0.45f);
                if (IS_FONT_LOADED(4))
                    SET_TEXT_FONT(4);

                SET_TEXT_PROPORTIONAL(true);
                SET_TEXT_DRAW_BEFORE_FADE(true);
                SET_TEXT_EDGE(true, 10, 10, 10, 5);
                SET_TEXT_CENTRE(true);

                SET_TEXT_COLOUR(255, 255, 255, alpha);

                DISPLAY_TEXT_WITH_NUMBER(0.944f, 0.18f, "NUMBER", gAmmo);
                //DRAW_SPRITE
            }
            else if (IS_FONT_LOADED(4))
                UNLOAD_TEXT_FONT();

            if (isReloading && IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, reloadAnim, "reload"))
            {
                IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.Gun = false;
                animPlaying = true;
            }

            if (animPlaying && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, reloadAnim, "reload"))
            {
                //IVGame.ShowSubtitleMessage("shit");
                //SET_CURRENT_CHAR_WEAPON(Main.PlayerHandle, currWeap, true);
                IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.Gun = true;
                animPlaying = false;
                isReloading = false;
            }

            if (DOES_OBJECT_EXIST(grenObj))
            {
                if (!cantFire)
                {
                    GET_GAME_CAM(out int cam);
                    GET_CAM_ROT(cam, out Vector3 cRot);
                    if (IS_OBJECT_ATTACHED(grenObj))
                    {
                        DETACH_OBJECT(grenObj, true);
                        SET_OBJECT_COORDINATES(grenObj, grndPos);
                        SET_OBJECT_DYNAMIC(grenObj, true);
                        SET_OBJECT_COLLISION(grenObj, true);
                        SET_OBJECT_RECORDS_COLLISIONS(grenObj, true);
                        SET_OBJECT_ROTATION(grenObj, cRot);
                    }
                    else
                    {
                        //IVGame.ShowSubtitleMessage(cRot.ToString());
                        APPLY_FORCE_TO_OBJECT(grenObj, 1, 0, grndForce, 3, 0, 0, 0, 1, 1, 1, 1);
                        //SET_OBJECT_ROTATION(grenObj, cRot.X - 5, cRot.Y - 15, cRot.Z + 80);
                        SET_OBJECT_ROTATION(grenObj, cRot.X, cRot.Y - 50, cRot.Z + 70);
                        cantFire = true;
                    }
                }

                if (HAS_OBJECT_COLLIDED_WITH_ANYTHING(grenObj) || (gTimer >= (fTimer + fuseTime)))
                {
                    cantFire = false;
                    GET_OBJECT_COORDINATES(grenObj, out Vector3 gPos);
                    ADD_EXPLOSION(gPos.X, gPos.Y, gPos.Z, expTypes[wIndex], 15, true, false, 1.0f);
                    DELETE_OBJECT(ref grenObj);
                }
            }
        }
    }
}