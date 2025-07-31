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
        private static GameKey pickupKey;
        private static GameKey dropKey;
        private static int maxPickups = 0;
        private static float despawnDist = 0;

        // Lists
        private static List<int> pedList = new List<int>();
        private static List<int> weaponList = new List<int>();
        private static List<int> ammoList = new List<int>();
        private static List<int> pickupList = new List<int>();
        private static List<int> pWeaponList = new List<int>();
        private static List<int> pAmmoList = new List<int>();

        // OtherShit
        private static string WeapPickSound = "";
        private static int pWeapObj = 0;
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

        public static void Init(SettingsFile settings)
        {
            pickupEnable = settings.GetBoolean("PICKUPS", "RevampedPickups", false);
            enableDrop = settings.GetBoolean("PICKUPS", "DropWeapons", false);
            sharedAmmo = settings.GetBoolean("PICKUPS", "SharedAmmo", false);
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
            if (enableDrop)
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

                    if (pWeap <= 0)
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

                    if (pWeap != weaponList[pedList.IndexOf(ped)] && pWeap > 0)
                    {
                        weaponList[pedList.IndexOf(ped)] = pWeap;
                    }
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

                    if ((IS_CHAR_INJURED(ped) || IS_CHAR_DEAD(ped)) && !IS_CHAR_IN_ANY_CAR(ped) && weaponList[pedList.IndexOf(ped)] > 0)
                    {
                        int wPickup;
                        int veh;
                        bool missionPed = false;
                        bool missionVeh = false;

                        if (HAS_CHAR_GOT_WEAPON(ped, pWeap))
                            REMOVE_WEAPON_FROM_CHAR(ped, pWeap);

                        GET_PED_BONE_POSITION(ped, (uint)eBone.BONE_RIGHT_HAND, Vector3.Zero, out Vector3 pos);

                        veh = GET_CLOSEST_CAR(pos, 5, 0, 71);

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
                            if (IS_OBJECT_ATTACHED(pickupList[i]))
                                DETACH_OBJECT(pickupList[i], true);

                            GET_OBJECT_COORDINATES(objID, out Vector3 objPos);
                            GET_GROUND_Z_FOR_3D_COORD(objPos, out float objGroundZ);

                            LightHelper.AddPointLight(objPos, Color.OrangeRed, 40.0f, 1.5f, false, UIntPtr.Zero);

                            GET_DISTANCE_BETWEEN_COORDS_3D(Main.PlayerPos.X, Main.PlayerPos.Y, pGroundZ, objPos.X, objPos.Y, objGroundZ, out float pDist);
                            GET_WEAPONTYPE_SLOT(pWeaponList[pickupList.IndexOf(objID)], out int pSlot);

                            if (DOES_OBJECT_EXIST(objID) && pDist >= despawnDist)
                            {
                                DELETE_OBJECT(ref objID);

                                pWeaponList.RemoveAt(i);
                                pAmmoList.RemoveAt(i);
                                pickupList.RemoveAt(i);
                            }    

                            if (DOES_OBJECT_EXIST(objID) && pDist < 0.75 && !IS_CHAR_IN_AIR(Main.PlayerHandle) && (objPos.Z - objGroundZ) < 0.2f && !Main.IsAimingAnimPlaying() && !IS_CHAR_SHOOTING(Main.PlayerHandle))
                            {
                                if (!IS_THIS_HELP_MESSAGE_BEING_DISPLAYED("PU_CF1") && !HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                    DISPLAY_HELP_TEXT_THIS_FRAME("PU_CF1", false);

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
                                        if (pSlot == slot)
                                        {
                                            WeapPickSound = Main.wConfFile.GetValue(pWeaponList[pickupList.IndexOf(objID)].ToString(), "PickupSound", "");

                                            if (!HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, pWeaponList[pickupList.IndexOf(objID)]))
                                                PLAY_SOUND(-1, WeapPickSound);
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
