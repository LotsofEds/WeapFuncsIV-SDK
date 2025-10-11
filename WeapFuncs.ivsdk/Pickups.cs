using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using IVSDKDotNet.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Runtime;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class Pickups
    {
        // IniShit
        private static bool pickupEnable;
        private static bool enableDrop;
        private static bool sharedAmmo;
        public static bool limitedLoadout;
        private static GameKey pickupKey;
        private static GameKey dropKey;
        private static int maxPickups = 0;
        private static float despawnDist = 0;
        private static int maxLoadout = 0;
        private static string weapPickSound = "";
        private static Color glowColor;

        // Lists
        private static List<int> pedList = new List<int>();
        private static List<int> weaponList = new List<int>();
        private static List<int> ammoList = new List<int>();
        private static List<int> pickupList = new List<int>();
        private static List<int> pWeaponList = new List<int>();
        private static List<int> pAmmoList = new List<int>();

        // OtherShit
        private static int pWeapObj = 0;
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
        }

        public static void GetMaxLoadout(SettingsFile settings)
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

            if (level5Prog >= level5Req)
                maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel5", 36);
            if (level4Prog >= level4Req)
                maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel4", 32);
            else if (level3Prog >= level3Req)
                maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel3", 24);
            else if (level2Prog >= level2Req)
                maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel2", 18);
            else
                maxLoadout = settings.GetInteger("PICKUPS", "MaxLoadoutLevel1", 12);
        }

        public static void Init(SettingsFile settings)
        {
            pickupEnable = settings.GetBoolean("PICKUPS", "RevampedPickups", false);
            enableDrop = settings.GetBoolean("PICKUPS", "DropWeapons", false);
            sharedAmmo = settings.GetBoolean("PICKUPS", "SharedAmmo", false);
            limitedLoadout = settings.GetBoolean("PICKUPS", "LimitedLoadout", false);

            pickupKey = (GameKey)settings.GetInteger("PICKUPS", "PickupControlKey", 23);
            dropKey = (GameKey)settings.GetInteger("PICKUPS", "DropControlKey", 78);
            despawnDist = settings.GetFloat("PICKUPS", "DespawnDistance", 30);
            maxPickups = settings.GetInteger("PICKUPS", "MaxPickups", 20);
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

            CREATE_OBJECT((int)wModel, Vector3.Zero, out int wPickup, true);
            ADD_OBJECT_TO_INTERIOR_ROOM_BY_KEY(wPickup, roomKey);
            ATTACH_OBJECT_TO_PED(wPickup, Main.PlayerHandle, (uint)eBone.BONE_RIGHT_HAND, Vector3.Zero, Vector3.Zero, 0);
            SET_OBJECT_COORDINATES(wPickup, pos);

            if (pickupEnable)
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
            GET_GAME_TIMER(out uint gTimer);

            if (limitedLoadout)
            {
                level2Prog = GET_FLOAT_STAT(level2Stat);
                level3Prog = GET_FLOAT_STAT(level3Stat);
                level4Prog = GET_FLOAT_STAT(level4Stat);
                level5Prog = GET_FLOAT_STAT(level5Stat);

                currLoadout = 0;
                for (int i = 1; i < Main.numOfWeapIDs; i++)
                {
                    if (HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, i))
                    {
                        currWeaponSpace = Main.wConfFile.GetInteger(i.ToString(), "WeaponSpace", 0);
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
                if (IS_HUD_PREFERENCE_SWITCHED_ON() && gTimer > 0 && gTimer <= (aTimer + 5000))
                {
                    if (gTimer > (aTimer + 4000))
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

            if (enableDrop && IS_PLAYER_CONTROL_ON((int)Main.PlayerIndex))
            {
                if (Main.currWeap > 0 && Main.IsPressingAimButton() && (Main.IsAimingAnimPlaying() || IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponSlot == 1 || IVWeaponInfo.GetWeaponInfo((uint)Main.currWeap).WeaponSlot == 8) && (IS_CONTROL_JUST_PRESSED(0, (int)dropKey) || IS_CONTROL_JUST_PRESSED(2, (int)dropKey)))
                {
                    DropCurrWeap(Main.currWeap);

                    REMOVE_WEAPON_FROM_CHAR(Main.PlayerHandle, Main.currWeap);
                }
                if (DOES_OBJECT_EXIST(pWeapObj))
                {
                    GET_OBJECT_COORDINATES(pWeapObj, out Vector3 objPos);

                    if (!LOCATE_CHAR_ANY_MEANS_3D(Main.PlayerHandle, objPos.X, objPos.Y, objPos.Z, despawnDist, despawnDist, despawnDist, false))
                        DELETE_OBJECT(ref pWeapObj);
                }
            }

            if (pickupEnable)
            {
                SET_DEAD_PEDS_DROP_WEAPONS(false);
                GET_GROUND_Z_FOR_3D_COORD(Main.PlayerPos, out float pGroundZ);

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

                    if ((IS_CHAR_INJURED(ped) || IS_CHAR_DEAD(ped)) && !IS_CHAR_IN_ANY_CAR(ped) && weaponList[pedList.IndexOf(ped)] > 0 && (weaponList[pedList.IndexOf(ped)] < 46 || weaponList[pedList.IndexOf(ped)] > 57))
                    {
                        int wPickup;
                        int veh;
                        bool missionPed = false;
                        bool missionVeh = false;

                        if (HAS_CHAR_GOT_WEAPON(ped, weaponList[pedList.IndexOf(ped)]))
                            REMOVE_WEAPON_FROM_CHAR(ped, weaponList[pedList.IndexOf(ped)]);

                        GET_PED_BONE_POSITION(ped, (uint)eBone.BONE_RIGHT_HAND, Vector3.Zero, out Vector3 pos);

                        veh = GET_CLOSEST_CAR(pos, 10, 0, 71);

                        if (!IS_PED_A_MISSION_PED(ped))
                            SET_CHAR_AS_MISSION_CHAR(ped);
                        else
                            missionPed = true;

                        if (DOES_VEHICLE_EXIST(veh) && !IS_CAR_A_MISSION_CAR(veh))
                            SET_CAR_AS_MISSION_CAR(veh);
                        else
                            missionVeh = true;

                        //CREATE_OBJECT((int)wModel, pos, out wPickup, true);
                        CREATE_OBJECT((int)wModel, Vector3.Zero, out wPickup, true);
                        ADD_OBJECT_TO_INTERIOR_ROOM_BY_KEY(wPickup, roomKey);
                        ATTACH_OBJECT_TO_PED(wPickup, ped, (uint)eBone.BONE_RIGHT_HAND, Vector3.Zero, Vector3.Zero, 0);
                        SET_OBJECT_COORDINATES(wPickup, pos);

                        if (!missionPed)
                            MARK_CHAR_AS_NO_LONGER_NEEDED(ped);

                        if (!missionVeh)
                            MARK_CAR_AS_NO_LONGER_NEEDED(veh);

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
                            SET_OBJECT_COLLISION(pickupList[i], true);
                            DETACH_OBJECT(pickupList[i], true);

                            GET_OBJECT_SPEED(objID, out float speed);
                            if (speed < 0.1)
                                APPLY_FORCE_TO_OBJECT(objID, 3, 0, 0.01f, 0, 0, 0, 0, 0, 1, 1, 1);

                            GET_OBJECT_COORDINATES(objID, out Vector3 objPos);
                            GET_GROUND_Z_FOR_3D_COORD(objPos, out float objGroundZ);

                            glowColor = Color.FromName(Main.wConfFile.GetValue(pWeaponList[pickupList.IndexOf(objID)].ToString(), "GlowColor", ""));
                            //LightHelper.AddPointLight(objPos, Color.OrangeRed, 40.0f, 1.5f, false, UIntPtr.Zero);
                            LightHelper.AddPointLight(objPos, glowColor, 40.0f, 1.5f, false, UIntPtr.Zero);

                            GET_DISTANCE_BETWEEN_COORDS_3D(Main.PlayerPos.X, Main.PlayerPos.Y, pGroundZ, objPos.X, objPos.Y, objGroundZ, out float pDist);
                            GET_WEAPONTYPE_SLOT(pWeaponList[pickupList.IndexOf(objID)], out int pSlot);

                            if (DOES_OBJECT_EXIST(objID) && pDist >= despawnDist)
                            {
                                DELETE_OBJECT(ref objID);

                                pWeaponList.RemoveAt(i);
                                pAmmoList.RemoveAt(i);
                                pickupList.RemoveAt(i);
                            }

                            if (DOES_OBJECT_EXIST(objID) && pDist < 0.75 && !IS_CHAR_IN_AIR(Main.PlayerHandle) && !Main.IsAimingAnimPlaying() && !IS_CHAR_SHOOTING(Main.PlayerHandle))
                            {
                                if (limitedLoadout)
                                    weaponSpace = Main.wConfFile.GetInteger(pWeaponList[pickupList.IndexOf(objID)].ToString(), "WeaponSpace", 0);

                                if (IS_PLAYER_CONTROL_ON((int)Main.PlayerIndex))
                                {
                                    if (maxLoadout >= currLoadout + weaponSpace || !limitedLoadout)
                                    {
                                        if (!IS_HELP_MESSAGE_BEING_DISPLAYED() && !HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                            DISPLAY_HELP_TEXT_THIS_FRAME("PU_CF1", false);
                                    }
                                    else if (!IS_HELP_MESSAGE_BEING_DISPLAYED() && !HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                    {
                                        IVText.TheIVText.ReplaceTextOfTextLabel("TM_17_3", "~r~ You cannot carry any more weapons.");
                                        DISPLAY_HELP_TEXT_THIS_FRAME("TM_17_3", false);
                                    }
                                }
                                for (int slot = 1; slot < 12; slot++)
                                {
                                    GET_CHAR_WEAPON_IN_SLOT(Main.PlayerHandle, slot, out int weapInSlot, out int cAmmoInSlot, out int ammoInSlot);

                                    if (pSlot == slot && sharedAmmo && weapInSlot > 0 && pAmmoList[pickupList.IndexOf(objID)] > 0)
                                    {
                                        PLAY_SOUND(-1, "BODY_ARMOUR_BUY");
                                        SET_CHAR_AMMO(Main.PlayerHandle, weapInSlot, cAmmoInSlot + pAmmoList[pickupList.IndexOf(objID)]);
                                        pAmmoList[pickupList.IndexOf(objID)] = 0;
                                    }

                                    else if (IS_CONTROL_JUST_PRESSED(0, (int)pickupKey) || IS_CONTROL_JUST_PRESSED(2, (int)pickupKey) || HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                    {
                                        GET_GAME_TIMER(out aTimer);
                                        if (pSlot == slot && ((maxLoadout >= currLoadout + weaponSpace) || !limitedLoadout || HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)])))
                                        {
                                            if (gTimer >= (fTimer + 100))
                                            {
                                                GET_GAME_TIMER(out fTimer);

                                                weapPickSound = Main.wConfFile.GetValue(pWeaponList[pickupList.IndexOf(objID)].ToString(), "PickupSound", "");

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
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
