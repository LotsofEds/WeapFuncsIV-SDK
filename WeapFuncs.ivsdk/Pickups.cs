using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using IVSDKDotNet.Hooking;
using IVSDKDotNet.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Runtime;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class Pickups
    {
        // IniShit
        private static bool enableDrop;
        private static bool sharedAmmo;
        public static bool limitedLoadout;
        private static int pickupKey;
        private static int dropKey;
        private static uint dropHold;
        private static int maxPickups = 0;
        private static float despawnDist = 0;
        private static int maxLoadout = 0;
        private static string weapPickSound = "";

        private static string moneyColor;
        private static string armorColor;
        private static string healthColor;
        private static string pidgeonColor;

        // Lists
        private static List<int> pedList = new List<int>();
        private static List<int> weaponList = new List<int>();
        private static List<int> ammoList = new List<int>();
        private static List<int> pickupList = new List<int>();
        private static List<int> pWeaponList = new List<int>();
        private static List<int> pAmmoList = new List<int>();

        // OtherShit
        private static int pWeapObj = 0;
        private static uint dTimer = 0;
        private static uint aTimer = 0;
        private static uint fTimer = 0;
        private static uint alpha = 255;
        private static int weaponSpace = 0;
        private static int currWeaponSpace = 0;
        private static int currLoadout = 0;

        // Limited Loadout
        private static int level2Stat = 0;
        private static int level3Stat = 0;
        private static int level4Stat = 0;
        private static int level5Stat = 0;

        private static float level2Req = 0;
        private static float level3Req = 0;
        private static float level4Req = 0;
        private static float level5Req = 0;

        private static float level2Prog = 0;
        private static float level3Prog = 0;
        private static float level4Prog = 0;
        private static float level5Prog = 0;
        public static void UnInit()
        {
            if (pickupList.Count > 0)
            {
                for (int i = 0; i < pickupList.Count; i++)
                {
                    int obj = pickupList[i];
                    DELETE_OBJECT(ref obj);
                }
            }
            ClearLists();

            aTimer = 0;
            fTimer = 0;
        }
        public static void IngameStart()
        {
            UnInit();
            GetLoadoutSettings(Main.wfConfig);
        }

        private static void GetLoadoutSettings(SettingsFile settings)
        {
            if (Main.CurrEp == 0)
            {
                level2Stat = settings.GetInteger("PICKUPS", "IVLoadoutLevel2Stat", 10);
                level3Stat = settings.GetInteger("PICKUPS", "IVLoadoutLevel3Stat", 22);
                level4Stat = settings.GetInteger("PICKUPS", "IVLoadoutLevel4Stat", 23);
                level5Stat = settings.GetInteger("PICKUPS", "IVLoadoutLevel5Stat", 0);

                level2Req = settings.GetFloat("PICKUPS", "IVLoadoutLevel2Unlock", 40);
                level3Req = settings.GetFloat("PICKUPS", "IVLoadoutLevel3Unlock", 70);
                level4Req = settings.GetFloat("PICKUPS", "IVLoadoutLevel4Unlock", 70);
                level5Req = settings.GetFloat("PICKUPS", "IVLoadoutLevel5Unlock", 100);
            }
            else if (Main.CurrEp == 1)
            {
                level2Stat = settings.GetInteger("PICKUPS", "TLADLoadoutLevel2Stat", 121);
                level3Stat = settings.GetInteger("PICKUPS", "TLADLoadoutLevel3Stat", 121);
                level4Stat = settings.GetInteger("PICKUPS", "TLADLoadoutLevel4Stat", 127);
                level5Stat = settings.GetInteger("PICKUPS", "TLADLoadoutLevel5Stat", 133);

                level2Req = settings.GetFloat("PICKUPS", "TLADLoadoutLevel2Unlock", 70);
                level3Req = settings.GetFloat("PICKUPS", "TLADLoadoutLevel3Unlock", 90);
                level4Req = settings.GetFloat("PICKUPS", "TLADLoadoutLevel4Unlock", 70);
                level5Req = settings.GetFloat("PICKUPS", "TLADLoadoutLevel5Unlock", 100);
            }
            else if (Main.CurrEp == 2)
            {
                level2Stat = settings.GetInteger("PICKUPS", "TBOGTLoadoutLevel2Stat", 187);
                level3Stat = settings.GetInteger("PICKUPS", "TBOGTLoadoutLevel3Stat", 197);
                level4Stat = settings.GetInteger("PICKUPS", "TBOGTLoadoutLevel4Stat", 188);
                level5Stat = settings.GetInteger("PICKUPS", "TBOGTLoadoutLevel5Stat", 187);

                level2Req = settings.GetFloat("PICKUPS", "TBOGTLoadoutLevel2Unlock", 0);
                level3Req = settings.GetFloat("PICKUPS", "TBOGTLoadoutLevel3Unlock", 40);
                level4Req = settings.GetFloat("PICKUPS", "TBOGTLoadoutLevel4Unlock", 70);
                level5Req = settings.GetFloat("PICKUPS", "TBOGTLoadoutLevel5Unlock", 100);
            }
        }
        private static void GetMaxLoadout(SettingsFile settings)
        {
            if (IS_PLAYER_CONTROL_ON((int)Main.PlayerIndex) && IS_SCREEN_FADED_IN())
            {
                if (level5Prog >= level5Req)
                    maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel5", 36);
                else if (level4Prog >= level4Req)
                    maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel4", 32);
                else if (level3Prog >= level3Req)
                    maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel3", 24);
                else if (level2Prog >= level2Req)
                    maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel2", 18);
                else
                    maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel1", 12);
            }
        }
        public static void Init(SettingsFile settings)
        {
            enableDrop = settings.GetBoolean("PICKUPS", "DropWeapons", false);
            sharedAmmo = settings.GetBoolean("PICKUPS", "SharedAmmo", false);
            limitedLoadout = settings.GetBoolean("PICKUPS", "LimitedLoadout", false);

            pickupKey = settings.GetInteger("PICKUPS", "PickupControlKey", 23);
            dropKey = settings.GetInteger("PICKUPS", "DropControlKey", 78);
            dropHold = settings.GetUInteger("PICKUPS", "DropHoldTime", 500);
            despawnDist = settings.GetFloat("PICKUPS", "DespawnDistance", 30);
            maxPickups = settings.GetInteger("PICKUPS", "MaxPickups", 20);

            enableDrop = settings.GetBoolean("PICKUPS", "DropWeapons", false);

            moneyColor = settings.GetValue("PICKUPS", "MoneyColor", "YellowGreen");
            healthColor = settings.GetValue("PICKUPS", "HealthColor", "YellowGreen");
            armorColor = settings.GetValue("PICKUPS", "ArmorColor", "OrangeRed");
            pidgeonColor = settings.GetValue("PICKUPS", "PidgeonColor", "OrangeRed");
            ClearLists();
        }
        private static void ClearLists()
        {
            pedList.Clear();
            ammoList.Clear();
            weaponList.Clear();
            pickupList.Clear();
            pWeaponList.Clear();
            pAmmoList.Clear();
        }
        private static void DropCurrWeap(int weap)
        {
            DELETE_OBJECT(ref pWeapObj);
            GET_WEAPONTYPE_MODEL(weap, out uint wModel);
            GET_AMMO_IN_CHAR_WEAPON(Main.PlayerHandle, weap, out int pAmmo);

            GET_KEY_FOR_CHAR_IN_ROOM(Main.PlayerHandle, out uint roomKey);
            GET_PED_BONE_POSITION(Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, Vector3.Zero, out Vector3 pos);

            int wIndex = Main.weaponData.FindIndex(w => w.ID == weap);
            Vector3 offset = Main.weaponData[wIndex].Offset;

            CREATE_OBJECT((int)wModel, Vector3.Zero, out int wPickup, true);
            ADD_OBJECT_TO_INTERIOR_ROOM_BY_KEY(wPickup, roomKey);
            ATTACH_OBJECT_TO_PED(wPickup, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, offset, Vector3.Zero, 0);

            if (Main.pickupEnable)
            {
                pickupList.Add(wPickup);
                pWeaponList.Add(weap);
                pAmmoList.Add(pAmmo);
            }
            else
            {
                pWeapObj = wPickup;
                DETACH_OBJECT(wPickup, true);
            }
        }
        public static void Tick()
        {
            int weapPickupID = 0;
            for (int i = 0; i < IVPickups.Pickups.Count(); i++)
            {
                int pickupHandle = IVPickups.ConvertIndexToHandle(i);
                int pickObjHandle = IVObject.FromUIntPtr(IVPickups.Pickups[i].WorldObject).GetHandle();

                Color colorGlow = Color.FromName("None");
                if (DOES_OBJECT_EXIST(pickObjHandle))
                {
                    GET_OBJECT_MODEL(pickObjHandle, out uint objMdl);
                    for (int w = 0; w < 65; w++)
                    {
                        GET_WEAPONTYPE_MODEL(w, out uint wModel);
                        if (wModel == objMdl)
                        {
                            weapPickupID = w;
                            break;
                        }
                    }

                    int colorIndex = Main.weaponData.FindIndex(w => w.ID == weapPickupID);
                    if (colorIndex >= 0)
                        colorGlow = Color.FromName(Main.weaponData[colorIndex].Color);
                    if (IVPickups.Pickups[i].Type == (byte)ePickupType.PICKUP_TYPE_MONEY || IVPickups.Pickups[i].Type == (byte)ePickupType.PICKUP_TYPE_MONEY2)
                        colorGlow = Color.FromName(moneyColor);
                    if (IVPickups.Pickups[i].Type == (byte)ePickupType.PICKUP_TYPE_PIGEON)
                        colorGlow = Color.FromName(pidgeonColor);
                    if (objMdl == GET_HASH_KEY_2("ec_bpjacket"))
                        colorGlow = Color.FromName(armorColor);
                    if (objMdl == GET_HASH_KEY_2("cj_first_aid_pickup"))
                        colorGlow = Color.FromName(healthColor);

                    GET_PICKUP_COORDINATES(pickupHandle, out Vector3 pickupCoords);

                    LightHelper.AddPointLight(pickupCoords, colorGlow, 30.0f, 1.5f, true, UIntPtr.Zero);
                }
            }

            if (Main.pickupEnable)
                ProcessPickups();

            if (enableDrop && IS_PLAYER_CONTROL_ON((int)Main.PlayerIndex))
                ProcessDropWeapons();

            if (limitedLoadout)
                ProcessLoadout();
        }
        private static void ProcessLoadout()
        {
            level2Prog = GET_FLOAT_STAT(level2Stat);
            level3Prog = GET_FLOAT_STAT(level3Stat);
            level4Prog = GET_FLOAT_STAT(level4Stat);
            level5Prog = GET_FLOAT_STAT(level5Stat);

            //IVGame.ShowSubtitleMessage(level2Prog.ToString() + "  " + level3Prog.ToString() + "  " + level4Prog.ToString() + "  " + level5Prog.ToString());

            GetMaxLoadout(Main.wfConfig);

            currLoadout = 0;
            for (int i = 1; i < Main.numOfWeapIDs; i++)
            {
                if (HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, i))
                {
                    if (i == 57 || i == 56)
                        continue;
                    int wIndex = Main.weaponData.FindIndex(w => w.ID == i);
                    if (wIndex >= 0)
                    currWeaponSpace = Main.weaponData[wIndex].Weight;

                    currLoadout += currWeaponSpace;

                    if (currLoadout > maxLoadout && maxLoadout > 0 && IS_PLAYER_CONTROL_ON((int)Main.PlayerIndex))
                    {
                        if (IVWeaponInfo.GetWeaponInfo((uint)i).WeaponFlags.Gun)
                        {
                            DropCurrWeap(i);

                            REMOVE_WEAPON_FROM_CHAR(Main.PlayerHandle, i);
                        }
                    }
                }
            }
            if (IS_HUD_PREFERENCE_SWITCHED_ON() && (Main.gTimer <= (aTimer + 5000) || NativeControls.IsGameKeyPressed(0, GameKey.RadarZoom)))
            {
                if (Main.gTimer > (aTimer + 4000) && !NativeControls.IsGameKeyPressed(0, GameKey.RadarZoom))
                    alpha -= ((uint)(Main.frameTime * 250f));
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

                DISPLAY_TEXT_WITH_2_NUMBERS(0.934f, 0.24f, "NUM_OUTOF_NUM", currLoadout, maxLoadout);
            }
            else if (IS_FONT_LOADED(4))
                UNLOAD_TEXT_FONT();
        }
        private static void ProcessDropWeapons()
        {
            if (Main.currWeap > 0 && WeaponHelper.IsPressingAimButton() && (WeaponHelper.IsAimingAnimPlaying() || IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponSlot == 1
                || IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponSlot == 8) && (IS_CONTROL_PRESSED(0, dropKey) || (dropHold == 0 && IS_CONTROL_JUST_PRESSED(0, dropKey))))
            {
                if (Main.gTimer >= dTimer + dropHold || dropHold == 0)
                {
                    DropCurrWeap(Main.currWeap);
                    REMOVE_WEAPON_FROM_CHAR(Main.PlayerHandle, Main.currWeap);
                    GET_GAME_TIMER(out dTimer);
                }
            }
            else
                GET_GAME_TIMER(out dTimer);

            if (DOES_OBJECT_EXIST(pWeapObj))
            {
                GET_OBJECT_COORDINATES(pWeapObj, out Vector3 objPos);

                if (!LOCATE_CHAR_ANY_MEANS_3D(Main.PlayerHandle, objPos.X, objPos.Y, objPos.Z, despawnDist, despawnDist, despawnDist, false))
                    DELETE_OBJECT(ref pWeapObj);
            }
        }
        private static void ProcessPickups()
        {
            SET_DEAD_PEDS_DROP_WEAPONS(false);

            if (pickupList.Count > 0)
            {
                if (pickupList.Count > maxPickups)
                {
                    int objDelete = pickupList[0];
                    DELETE_OBJECT(ref objDelete);

                    pWeaponList.RemoveAt(0);
                    pAmmoList.RemoveAt(0);
                    pickupList.RemoveAt(0);
                }
                int currWeapSpace = 0;
                bool isNearWeap = false;
                bool isSwitchingWeap = false;
                for (int i = 0; i < pickupList.Count; i++)
                {
                    if (!DOES_OBJECT_EXIST(pickupList[i]))
                    {
                        pWeaponList.RemoveAt(i);
                        pAmmoList.RemoveAt(i);
                        pickupList.RemoveAt(i);
                    }
                    else
                    {
                        int objID = pickupList[i];
                        //IVGame.ShowSubtitleMessage(pWeaponList[pickupList.IndexOf(objID)].ToString());

                        GET_OBJECT_COORDINATES(objID, out Vector3 objPos);
                        if (IS_OBJECT_ATTACHED(objID))
                        {
                            SET_OBJECT_COLLISION(pickupList[i], true);
                            DETACH_OBJECT(pickupList[i], true);
                        }
                        else
                        {
                            GET_OBJECT_SPEED(objID, out float speed);
                            if (speed < 0.01)
                                APPLY_FORCE_TO_OBJECT(objID, 3, 0, 0.001f * Main.frameTime, 0, 0, 0, 0, 0, 1, 1, 1);

                            int colorIndex = Main.weaponData.FindIndex(w => w.ID == pWeaponList[pickupList.IndexOf(objID)]);
                            Color glowColor = Color.FromName(Main.weaponData[colorIndex].Color);
                            
                            LightHelper.AddPointLight(objPos, glowColor, 40.0f, 1.5f, false, UIntPtr.Zero);

                            GET_DISTANCE_BETWEEN_COORDS_3D(Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z, objPos.X, objPos.Y, objPos.Z, out float pDist);
                            GET_WEAPONTYPE_SLOT(pWeaponList[pickupList.IndexOf(objID)], out int pSlot);

                            if (DOES_OBJECT_EXIST(objID) && pDist >= despawnDist)
                            {
                                DELETE_OBJECT(ref objID);

                                pWeaponList.RemoveAt(i);
                                pAmmoList.RemoveAt(i);
                                pickupList.RemoveAt(i);
                            }

                            if (DOES_OBJECT_EXIST(objID) && LOCATE_CHAR_ON_FOOT_3D(Main.PlayerHandle, objPos.X, objPos.Y, objPos.Z, 0.75f, 0.75f, 1.0f, false)
                                && !IS_CHAR_IN_ANY_CAR(Main.PlayerHandle) && !IS_CHAR_IN_AIR(Main.PlayerHandle) && !WeaponHelper.IsAimingAnimPlaying() && !IS_CHAR_SHOOTING(Main.PlayerHandle))
                            {
                                if (limitedLoadout)
                                {
                                    int wIndex = Main.weaponData.FindIndex(w => w.ID == pWeaponList[pickupList.IndexOf(objID)]);
                                    if (wIndex >= 0)
                                        weaponSpace = Main.weaponData[wIndex].Weight;

                                    //IVGame.ShowSubtitleMessage(pWeaponList[pickupList.IndexOf(objID)].ToString());
                                }
                                for (int slot = 1; slot < 12; slot++)
                                {
                                    GET_CHAR_WEAPON_IN_SLOT(Main.PlayerHandle, slot, out int weapInSlot, out int cAmmoInSlot, out int ammoInSlot);

                                    int currIndex = Main.weaponData.FindIndex(w => w.ID == weapInSlot);
                                    if (currIndex >= 0 && pSlot == slot)
                                        currWeapSpace = Main.weaponData[currIndex].Weight;

                                    if (IS_PLAYER_CONTROL_ON((int)Main.PlayerIndex))
                                    {
                                        isNearWeap = true;
                                        if (maxLoadout >= currLoadout + weaponSpace - currWeapSpace || !limitedLoadout)
                                        {
                                            if ((!IS_HELP_MESSAGE_BEING_DISPLAYED() || IS_THIS_HELP_MESSAGE_BEING_DISPLAYED("TM_17_3")) && !HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                                PRINT_HELP_FOREVER_WITH_STRING_NO_SOUND("PU_CF1", "");
                                        }
                                        else if (!IS_HELP_MESSAGE_BEING_DISPLAYED() && !HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                        {
                                            IVText.TheIVText.ReplaceTextOfTextLabel("TM_17_3", "~r~You cannot carry any more weapons.");
                                            PRINT_HELP_FOREVER_WITH_STRING_NO_SOUND("TM_17_3", "");
                                        }
                                    }

                                    if (pSlot == slot && sharedAmmo && weapInSlot > 0 && pAmmoList[pickupList.IndexOf(objID)] > 0)
                                    {
                                        PLAY_SOUND(-1, "BODY_ARMOUR_BUY");
                                        SET_CHAR_AMMO(Main.PlayerHandle, weapInSlot, cAmmoInSlot + pAmmoList[pickupList.IndexOf(objID)]);
                                        pAmmoList[pickupList.IndexOf(objID)] = 0;
                                    }

                                    else if (IS_CONTROL_JUST_PRESSED(0, pickupKey) || HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                    {
                                        GET_GAME_TIMER(out aTimer);
                                        if (pSlot == slot && ((maxLoadout >= currLoadout + weaponSpace - currWeapSpace) || !limitedLoadout || HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)])))
                                        {
                                            if (Main.gTimer >= (fTimer + 100))
                                            {
                                                GET_GAME_TIMER(out fTimer);

                                                int wIndex = Main.weaponData.FindIndex(w => w.ID == pWeaponList[pickupList.IndexOf(objID)]);
                                                weapPickSound = Main.weaponData[wIndex].PickupSound;

                                                if (!HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                                    PLAY_SOUND(-1, weapPickSound);
                                                else
                                                    PLAY_SOUND(-1, "BODY_ARMOUR_BUY");

                                                if (weapInSlot > 0 && HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, weapInSlot) && weapInSlot != pWeaponList[pickupList.IndexOf(objID)])
                                                {
                                                    DropCurrWeap(weapInSlot);

                                                    if (!sharedAmmo)
                                                        REMOVE_WEAPON_FROM_CHAR(Main.PlayerHandle, weapInSlot);
                                                }

                                                GIVE_DELAYED_WEAPON_TO_CHAR(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)], pAmmoList[pickupList.IndexOf(objID)], false);
                                                DELETE_OBJECT(ref objID);
                                                isSwitchingWeap = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (isSwitchingWeap)
                                    break;
                            }

                            else if (!isNearWeap && (IS_THIS_HELP_MESSAGE_BEING_DISPLAYED("PU_CF1") || IS_THIS_HELP_MESSAGE_BEING_DISPLAYED("TM_17_3")))
                                CLEAR_HELP();
                        }
                    }
                }
            }
            else if (IS_THIS_HELP_MESSAGE_BEING_DISPLAYED("PU_CF1") || IS_THIS_HELP_MESSAGE_BEING_DISPLAYED("TM_17_3"))
                CLEAR_HELP();

            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (pedHandle == Main.PlayerHandle)
                    continue;
                if (!DOES_CHAR_EXIST(pedHandle) || IS_CHAR_INJURED(pedHandle) || IS_CHAR_DEAD(pedHandle) || pedList.Contains(pedHandle))
                    continue;

                GET_CURRENT_CHAR_WEAPON(pedHandle, out int pWeap);

                if (pWeap <= 0 || (pWeap >= 46 && pWeap <= 57))
                    continue;

                GET_MAX_AMMO_IN_CLIP(pedHandle, pWeap, out int pMaxAmmo);

                pedList.Add(pedHandle);
                weaponList.Add(pWeap);
                ammoList.Add(pMaxAmmo);
            }
            foreach (var ped in pedList)
            {
                if (!DOES_CHAR_EXIST(ped))
                    continue;

                GET_CURRENT_CHAR_WEAPON(ped, out int pWeap);

                if (pWeap != weaponList[pedList.IndexOf(ped)] && pWeap > 0 && (pWeap < 46 || pWeap > 57))
                    weaponList[pedList.IndexOf(ped)] = pWeap;

                GET_MAX_AMMO_IN_CLIP(ped, weaponList[pedList.IndexOf(ped)], out int pMaxAmmo);
                GET_WEAPONTYPE_MODEL(weaponList[pedList.IndexOf(ped)], out uint wModel);

                GET_AMMO_IN_CLIP(ped, weaponList[pedList.IndexOf(ped)], out int pAmmo);
                if (IS_CHAR_SHOOTING(ped))
                {
                    if (pAmmo > 1 && pAmmo != pMaxAmmo)
                        ammoList[pedList.IndexOf(ped)] = pAmmo;
                    else
                        ammoList[pedList.IndexOf(ped)] = 0;
                }

                GET_KEY_FOR_CHAR_IN_ROOM(ped, out uint roomKey);

                if ((IS_CHAR_INJURED(ped) || IS_CHAR_DEAD(ped)) && !IS_CHAR_IN_ANY_CAR(ped) && weaponList[pedList.IndexOf(ped)] > 0
                    && (weaponList[pedList.IndexOf(ped)] < 46 || weaponList[pedList.IndexOf(ped)] > 57))
                {
                    int wPickup;

                    if (HAS_CHAR_GOT_WEAPON(ped, weaponList[pedList.IndexOf(ped)]))
                        REMOVE_WEAPON_FROM_CHAR(ped, weaponList[pedList.IndexOf(ped)]);

                    int wIndex = Main.weaponData.FindIndex(w => w.ID == weaponList[pedList.IndexOf(ped)]);
                    Vector3 offset = Main.weaponData[wIndex].Offset;

                    CREATE_OBJECT((int)wModel, Vector3.Zero, out wPickup, true);
                    ADD_OBJECT_TO_INTERIOR_ROOM_BY_KEY(wPickup, roomKey);
                    ATTACH_OBJECT_TO_PED(wPickup, ped, (uint)eBone.BONE_RIGHT_HAND, offset, Vector3.Zero, 0);

                    pickupList.Add(wPickup);
                    pWeaponList.Add(weaponList[pedList.IndexOf(ped)]);
                    pAmmoList.Add(ammoList[pedList.IndexOf(ped)]);
                }
            }

            if (pedList.Count > 0)
            {
                for (int i = 0; i < pedList.Count; i++)
                {
                    int pedWeap = 0;
                    if (DOES_CHAR_EXIST(pedList[i]))
                        GET_CURRENT_CHAR_WEAPON(pedList[i], out pedWeap);

                    if (!DOES_CHAR_EXIST(pedList[i]) || IS_CHAR_INJURED(pedList[i]) || IS_CHAR_DEAD(pedList[i]) || pedWeap <= 0)
                    {
                        weaponList.RemoveAt(i);
                        ammoList.RemoveAt(i);
                        pedList.RemoveAt(i);
                    }
                }
            }
        }
    }
}
