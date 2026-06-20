using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime;
using System.Threading;
using System.Windows.Forms;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class EquipGun
    {
        private static int weapSlot;
        private static Vector3 weapOff;
        private static Vector3 weapRot;
        private static int boneAttach;

        private static int weapModel;

        private static int weapObjA;
        private static int weapObjB;
        private static int weapObjC;
        private static int weapObjD;
        private static int weapObjE;
        private static int weapObjF;
        private static int weapObjG;
        private static int weapObjH;

        private static int weapIDA;
        private static int weapIDB;
        private static int weapIDC;
        private static int weapIDD;
        private static int weapIDE;
        private static int weapIDF;
        private static int weapIDG;
        private static int weapIDH;

        private static SettingsFile holsterConfig;
        public static void Init(SettingsFile settings)
        {
            holsterConfig = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\WeaponHolster.ini", IVGame.GameStartupPath));
            holsterConfig.Load();
        }
        public static void LoadConfig(int weapon)
        {
            if (holsterConfig.DoesSectionExists(weapon.ToString()))
            {
                weapSlot = holsterConfig.GetInteger(weapon.ToString(), "HolsterSlot", 0);
                weapOff = holsterConfig.GetVector3(weapon.ToString(), "HolsterOffset", Vector3.Zero);
                weapRot = holsterConfig.GetVector3(weapon.ToString(), "HolsterRot", Vector3.Zero);
            }
        }
        public static void LoadBone(int slot)
        {
            if (holsterConfig.DoesKeyExists("MAIN", "Slot" + slot + "Bone"))
                boneAttach = holsterConfig.GetInteger("MAIN", "Slot" + slot + "Bone", 0);
        }

        public static void UnInit()
        {
            DELETE_OBJECT(ref weapObjA);
            DELETE_OBJECT(ref weapObjB);
            DELETE_OBJECT(ref weapObjC);
            DELETE_OBJECT(ref weapObjD);
            DELETE_OBJECT(ref weapObjE);
            DELETE_OBJECT(ref weapObjF);
            DELETE_OBJECT(ref weapObjG);
            DELETE_OBJECT(ref weapObjH);
        }
        public static void Tick()
        {
            for (int i = 0; i < Main.numOfWeapIDs; i++)
            {
                if (!holsterConfig.DoesSectionExists(i.ToString()))
                    continue;
                
                if (HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, i))
                {
                    if (Main.currWeap == i)
                    {
                        LoadConfig(i);
                        //IVGame.ShowSubtitleMessage(weapIDA.ToString() + "  " + weapIDB.ToString() + "  " + weapIDC.ToString() + "  " + weapIDD.ToString() + "  " + weapSlot.ToString());
                        switch (weapSlot)
                        {
                            case 1:
                                if (weapIDA == i)
                                    DELETE_OBJECT(ref weapObjA);
                                break;
                            case 2:
                                if (weapIDB == i)
                                    DELETE_OBJECT(ref weapObjB);
                                break;
                            case 3:
                                if (weapIDC == i)
                                    DELETE_OBJECT(ref weapObjC);
                                break;
                            case 4:
                                if (weapIDD == i)
                                    DELETE_OBJECT(ref weapObjD);
                                break;
                            case 5:
                                if (weapIDE == i)
                                    DELETE_OBJECT(ref weapObjE);
                                break;
                            case 6:
                                if (weapIDF == i)
                                    DELETE_OBJECT(ref weapObjF);
                                break;
                            case 7:
                                if (weapIDG == i)
                                    DELETE_OBJECT(ref weapObjG);
                                break;
                            case 8:
                                if (weapIDH == i)
                                    DELETE_OBJECT(ref weapObjH);
                                break;
                        }
                    }
                    else
                    {
                        LoadConfig(i);
                        LoadBone(weapSlot);
                        weapModel = (int)IVWeaponInfo.GetWeaponInfo((uint)i).ModelHash;
                        switch (weapSlot)
                        {
                            case 1:
                                if (!DOES_OBJECT_EXIST(weapObjA) && weapModel != 0)
                                {
                                    CREATE_OBJECT((int)weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjA, true);
                                    ATTACH_OBJECT_TO_PED(weapObjA, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDA = i;
                                }
                                break;
                            case 2:
                                if (!DOES_OBJECT_EXIST(weapObjB) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjB, true);
                                    ATTACH_OBJECT_TO_PED(weapObjB, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDB = i;
                                }
                                break;
                            case 3:
                                if (!DOES_OBJECT_EXIST(weapObjC) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjC, true);
                                    ATTACH_OBJECT_TO_PED(weapObjC, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDC = i;
                                }
                                break;
                            case 4:
                                if (!DOES_OBJECT_EXIST(weapObjD) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjD, true);
                                    ATTACH_OBJECT_TO_PED(weapObjD, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDD = i;
                                }
                                break;
                            case 5:
                                if (!DOES_OBJECT_EXIST(weapObjE) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjE, true);
                                    ATTACH_OBJECT_TO_PED(weapObjE, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDE = i;
                                }
                                break;
                            case 6:
                                if (!DOES_OBJECT_EXIST(weapObjF) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjF, true);
                                    ATTACH_OBJECT_TO_PED(weapObjF, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDF = i;
                                }
                                break;
                            case 7:
                                if (!DOES_OBJECT_EXIST(weapObjG) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjG, true);
                                    ATTACH_OBJECT_TO_PED(weapObjG, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDG = i;
                                }
                                break;
                            case 8:
                                if (!DOES_OBJECT_EXIST(weapObjH) && weapModel != 0)
                                {
                                    CREATE_OBJECT(weapModel, Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10f, out weapObjH, true);
                                    ATTACH_OBJECT_TO_PED(weapObjH, Main.PlayerHandle, (uint)boneAttach, weapOff.X, weapOff.Y, weapOff.Z, weapRot.X, weapRot.Y, weapRot.Z, 0);
                                    weapIDH = i;
                                }
                                break;
                        }
                    }
                }
                else if (!HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, i))
                {
                    if (i == weapIDA)
                        DELETE_OBJECT(ref weapObjA);
                    else if (i == weapIDB)
                        DELETE_OBJECT(ref weapObjB);
                    else if (i == weapIDC)
                        DELETE_OBJECT(ref weapObjC);
                    else if (i == weapIDD)
                        DELETE_OBJECT(ref weapObjD);
                    else if (i == weapIDE)
                        DELETE_OBJECT(ref weapObjE);
                    else if (i == weapIDF)
                        DELETE_OBJECT(ref weapObjF);
                    else if (i == weapIDG)
                        DELETE_OBJECT(ref weapObjG);
                    else if (i == weapIDH)
                        DELETE_OBJECT(ref weapObjH);
                }
            }
        }
    }
}
