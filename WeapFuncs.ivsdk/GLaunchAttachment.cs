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
        // IniShit
        private static bool hasAttachment;
        private static string glModel;
        private static Vector3 glModelOff;
        private static Vector3 glModelRot;
        private static string projModel;
        private static string reloadAnim = "";
        private static Vector3 grndOffset;
        private static Vector3 grndRot;
        private static GameKey GrndFireCtrl;
        private static int grndForce;
        private static int fuseTime;
        private static int expType;
        private static int gAmmo;
        private static string fireSound;
        private static string muzFxName;
        private static Vector3 muzFxOff;
        private static string trailFxName;

        // Lists,Arrays
        private static bool[] attachmentUnlocks;
        private static int[] grenadeAmmo;

        // BooleShit
        private static bool hasPressedButton;
        private static bool cantFire;
        private static bool isReloading;
        private static bool animPlaying;
        private static bool ammoDisplay;
        private static bool weapInHand;

        // Some Other Shit
        private static int currWeap;
        private static uint fTimer;
        private static uint gTimer;
        private static int glAttachProp;
        private static int grenObj;
        private static Vector3 attachPos;
        private static Vector3 grndPos;
        private static int wIndex;
        private static int soundID = -1;
        private static uint alpha = 255;
        private static int muzFxID = -1;
        private static int trailFxID = -1;
        private static uint pModel;
        public static void Init(SettingsFile settings)
        {
            attachmentUnlocks = new bool[Main.numOfWeapIDs];
            grenadeAmmo = new int[Main.numOfWeapIDs];

            GrndFireCtrl = (GameKey)settings.GetInteger("ATTACHMENTS", "FireGrenadeControl", 7);
        }
        List<eWeaponType> GLaunchWeaps = new List<eWeaponType>();
        public static void UnInit()
        {
            DELETE_OBJECT(ref glAttachProp);
        }
        public static void OnGameLoad()
        {
            for (int i = 0; i < Main.numOfWeapIDs; i++)
            {
                if (Main.attachmentConfig.DoesSectionExists(i.ToString()))
                {
                    Main.wfAttachConfig.SetBoolean(IVGenericGameStorage.ValidSaveName, i.ToString() + "HasGLAttachment", Main.attachmentConfig.GetBoolean(IVGenericGameStorage.ValidSaveName, i.ToString() + "HasGLAttachment", false));
                    Main.wfAttachConfig.SetInteger(IVGenericGameStorage.ValidSaveName, i.ToString() + "GrenadeAmmo", Main.attachmentConfig.GetInteger(IVGenericGameStorage.ValidSaveName, i.ToString() + "GrenadeAmmo", 0));
                }
            }
            Main.wfAttachConfig.Save();
            Main.wfAttachConfig.Load();
        }
        private static void SaveAmmo(int weapon)
        {
            Main.wfAttachConfig.SetInteger(IVGenericGameStorage.ValidSaveName, weapon.ToString() + "GrenadeAmmo", gAmmo);
            Main.wfAttachConfig.Save();
        }
        private static void LoadWeaponConfig(int weapon)
        {
            if (Main.wfAttachConfig.DoesSectionExists(weapon.ToString()))
            {
                //IVGame.ShowSubtitleMessage(Main.attachmentConfig.ToString());
                //IVGame.ShowSubtitleMessage(Main.attachmentConfig.GetBoolean(IVGenericGameStorage.ValidSaveName, weapon.ToString() + "HasGLAttachment", false).ToString());
                hasAttachment = Main.wfAttachConfig.GetBoolean(IVGenericGameStorage.ValidSaveName, weapon.ToString() + "HasGLAttachment", false);
                glModel = Main.wfAttachConfig.GetValue(weapon.ToString(), "GrenadeLauncherModel", "");
                projModel = Main.wfAttachConfig.GetValue(weapon.ToString(), "GrenadeModel", "");
                reloadAnim = Main.wfAttachConfig.GetValue(weapon.ToString(), "GrenadeLauncherReloadAnim", "");
                glModelOff = Main.wfAttachConfig.GetVector3(weapon.ToString(), "GrenadeLauncherOffset", Vector3.Zero);
                glModelRot = Main.wfAttachConfig.GetVector3(weapon.ToString(), "GrenadeLauncherRot", Vector3.Zero);
                grndOffset = Main.wfAttachConfig.GetVector3(weapon.ToString(), "GrenadeOffset", Vector3.Zero);
                grndRot = Main.wfAttachConfig.GetVector3(weapon.ToString(), "GrenadeRot", Vector3.Zero);
                gAmmo = Main.wfAttachConfig.GetInteger(IVGenericGameStorage.ValidSaveName, weapon.ToString() + "GrenadeAmmo", 0);
                fireSound = Main.wfAttachConfig.GetValue(weapon.ToString(), "FireSound", "");
                muzFxName = Main.wfAttachConfig.GetValue(weapon.ToString(), "MuzzleFx", "");
                muzFxOff = Main.wfAttachConfig.GetVector3(weapon.ToString(), "MuzzleFxOffset", Vector3.Zero);
                trailFxName = Main.wfAttachConfig.GetValue(weapon.ToString(), "TrailFx", "");

                Main.wfAttachConfig.Load();
            }
        }
        private static void LoadGrenadeConfig(int weapon)
        {
            fuseTime = Main.wfAttachConfig.GetInteger(weapon.ToString(), "GrenadeFuseTime", 4000);
            expType = Main.wfAttachConfig.GetInteger(weapon.ToString(), "ExplosionType", 0);
            grndForce = Main.wfAttachConfig.GetInteger(weapon.ToString(), "ProjectileForce", 40);
        }
        public static void OnButtonPress()
        {
            if (!IS_PED_RAGDOLL(Main.PlayerHandle) && gAmmo > 0 && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "p_load") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload_crouch") && (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_crouch_alt") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_up") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "fire_down") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "dbfire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "dbfire_l")))
            {
                if (!cantFire && (NativeControls.IsGameKeyPressed(0, GrndFireCtrl)) && !hasPressedButton)
                {
                    GET_PED_BONE_POSITION(Main.PlayerHandle, 1232, grndOffset, out grndPos);
                    CREATE_OBJECT(GET_HASH_KEY(projModel), grndPos, out grenObj, true);
                    ATTACH_OBJECT_TO_PED(grenObj, Main.PlayerHandle, 1232, grndOffset.X, grndOffset.Y, grndOffset.Z, grndRot.X, grndRot.Y, grndRot.Z, 0);
                    GET_CURRENT_CHAR_WEAPON(Main.PlayerHandle, out currWeap);
                    _TASK_PLAY_ANIM_SECONDARY_UPPER_BODY(Main.PlayerHandle, "fire", reloadAnim, 8.0f, 0, 0, 0, 0, 0);
                    PLAY_SOUND_FROM_PED(soundID, fireSound, Main.PlayerHandle);
                    muzFxID = START_PTFX_ON_PED_BONE(muzFxName, Main.PlayerHandle, muzFxOff.X, muzFxOff.Y, muzFxOff.Z, 0.0f, 0.0f, 0.0f, (int)eBone.BONE_RIGHT_HAND, 1.0f);
                    trailFxID = START_PTFX_ON_OBJ(trailFxName, grenObj, 0, 0, 0, 0, 0, 0, 1.0f);
                    gAmmo--;
                    SaveAmmo(currWeap);

                    GET_GAME_TIMER(out fTimer);
                    LoadGrenadeConfig(currWeap);

                    isReloading = true;
                    hasPressedButton = true;
                }
            }
            if (!(NativeControls.IsGameKeyPressed(0, GrndFireCtrl)) && hasPressedButton)
                hasPressedButton = false;
        }
        public static void Tick()
        {
            GET_GAME_TIMER(out gTimer);

            LoadWeaponConfig((int)Main.currWeap);
            if (hasAttachment && wIndex == Main.currWeap && !isReloading)
            {
                if (!HAVE_ANIMS_LOADED(reloadAnim))
                    REQUEST_ANIMS(reloadAnim);
                if (!HAS_MODEL_LOADED(GET_HASH_KEY(glModel)))
                    REQUEST_MODEL(GET_HASH_KEY(glModel));

                attachmentUnlocks[Main.currWeap] = true;
                grenadeAmmo[Main.currWeap] = gAmmo;

                GET_WEAPONTYPE_MODEL(Main.currWeap, out uint wModel);
                foreach (var obj in ObjectHelper.ObjHandles)
                {
                    int objHandle = obj.Value;

                    GET_OBJECT_COORDINATES(objHandle, out float objX, out float objY, out float objZ);

                    GET_DISTANCE_BETWEEN_COORDS_3D(Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z, objX, objY, objZ, out float Dist);
                    if (Dist > 1)
                        continue;

                    GET_OBJECT_MODEL(objHandle, out pModel);

                    if (!DOES_OBJECT_EXIST(objHandle) || pModel != wModel)
                        weapInHand = false;

                    if (pModel == wModel)
                        weapInHand = true;
                }
                if (weapInHand)
                {
                    if (!DOES_OBJECT_EXIST(glAttachProp))
                    {
                        GET_PED_BONE_POSITION(Main.PlayerHandle, 1232, glModelOff, out attachPos);
                        CREATE_OBJECT(GET_HASH_KEY(glModel), attachPos, out glAttachProp, true);
                        ATTACH_OBJECT_TO_PED(glAttachProp, Main.PlayerHandle, 1232, glModelOff.X, glModelOff.Y, glModelOff.Z, glModelRot.X, glModelRot.Y, glModelRot.Z, 0);
                    }
                }
                wIndex = Main.currWeap;
                OnButtonPress();
            }
            else if (wIndex != Main.currWeap)
            {
                hasAttachment = false;
                DELETE_OBJECT(ref glAttachProp);
                wIndex = Main.currWeap;
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
            }
            else if (IS_FONT_LOADED(4))
                UNLOAD_TEXT_FONT();

            if (isReloading && (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, reloadAnim, "fire") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, reloadAnim, "reload")))
            {
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, reloadAnim, "fire", out float fireTime);
                if (fireTime < 0.85)
                    SET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, reloadAnim, "fire", 0.85f);
                else if (fireTime > 0.91)
                {
                    _TASK_PLAY_ANIM_SECONDARY_UPPER_BODY(Main.PlayerHandle, "reload", reloadAnim, 4.0f, 0, 0, 0, 0, 0);
                    animPlaying = true;
                }
                IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.Gun = false;
            }

            if (animPlaying && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, reloadAnim, "fire") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, reloadAnim, "reload"))
            {
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
                        APPLY_FORCE_TO_OBJECT(grenObj, 1, 0, grndForce, 3, 0, 0, 0, 1, 1, 1, 1);
                        SET_OBJECT_ROTATION(grenObj, cRot.X, cRot.Y - 50, cRot.Z + 70);
                        cantFire = true;
                    }
                }

                if (HAS_OBJECT_COLLIDED_WITH_ANYTHING(grenObj) || (gTimer >= (fTimer + fuseTime)))
                {
                    cantFire = false;
                    GET_OBJECT_COORDINATES(grenObj, out Vector3 gPos);
                    ADD_EXPLOSION(gPos.X, gPos.Y, gPos.Z, expType, 15, true, false, 1.0f);
                    STOP_PTFX(trailFxID);
                    REMOVE_PTFX(trailFxID);
                    DELETE_OBJECT(ref grenObj);
                }
            }
            if (DID_SAVE_COMPLETE_SUCCESSFULLY() && GET_IS_DISPLAYINGSAVEMESSAGE())
            {
                for (int i = 0; i < Main.numOfWeapIDs; i++)
                {
                    if (Main.wfAttachConfig.DoesSectionExists(i.ToString()))
                    {
                        Main.WriteBooleanToINI(Main.wfAttachConfig, i.ToString() + "HasGLAttachment", attachmentUnlocks[i]);
                        Main.WriteIntToINI(Main.wfAttachConfig, i.ToString() + "GrenadeAmmo", grenadeAmmo[i]);
                    }
                    if (Main.attachmentConfig.DoesSectionExists(i.ToString()))
                    {
                        Main.WriteBooleanToINI(Main.attachmentConfig, i.ToString() + "HasGLAttachment", attachmentUnlocks[i]);
                        Main.WriteIntToINI(Main.attachmentConfig, i.ToString() + "GrenadeAmmo", grenadeAmmo[i]);
                    }
                }
                Main.wfAttachConfig.Save();
                Main.attachmentConfig.Save();
            }
        }
    }
}