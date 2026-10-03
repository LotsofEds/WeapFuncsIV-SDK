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
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class LessSuckyMiniguns
    {
        // IniShit
        private static int primaryKey;
        private static int secondaryKey;

        // OtherShit
        private static SettingsFile vehGunSettings;
        private static List<VehicleGunData> vehGunData = new List<VehicleGunData>();
        private static List<int> bodyCount = new List<int>();
        private static uint currWanted;
        private static uint cachedWantedLvl;
        private static uint fTimer;

        public static void Init(SettingsFile settings)
        {
            UnInit();

            primaryKey = settings.GetInteger("CUSTOM VEHICLE GUN", "PrimaryShootKey", 1);
            secondaryKey = settings.GetInteger("CUSTOM VEHICLE GUN", "SecondaryShootKey", 1);
            vehGunSettings = new SettingsFile(string.Format("{0}\\IVSDKDotNet\\scripts\\WeapFuncs\\VehicleGunConfig.ini", IVGame.GameStartupPath));
            vehGunSettings.Load();
            LoadVehicleSettings();
        }
        public static void UnInit()
        {
            vehGunData.Clear();
            bodyCount.Clear();
            currWanted = 0;
            cachedWantedLvl = 0;
        }
        private static void LoadVehicleSettings()
        {
            for (int i = 0; i < vehGunSettings.GetSectionNames().Count(); i++)
            {
                vehGunData.Add(new VehicleGunData());

                vehGunData[i].VehModel = vehGunSettings.GetSectionNames()[i];
                vehGunData[i].TimeBetShots = vehGunSettings.GetUInteger(vehGunSettings.GetSectionNames()[i], "TimeBetweenShots", 50);
                vehGunData[i].DamagePerShot = vehGunSettings.GetUInteger(vehGunSettings.GetSectionNames()[i], "DamagePerShot", 100);
                vehGunData[i].BulletSpread = vehGunSettings.GetFloat(vehGunSettings.GetSectionNames()[i], "Bulletspread", 1.0f);
                vehGunData[i].BulletRange = vehGunSettings.GetFloat(vehGunSettings.GetSectionNames()[i], "EffectiveRange", 300.0f);
                vehGunData[i].CenterAmount = vehGunSettings.GetFloat(vehGunSettings.GetSectionNames()[i], "CenteringAmount", 8.0f);
                vehGunData[i].MuzzleFXScale = vehGunSettings.GetFloat(vehGunSettings.GetSectionNames()[i], "MuzzleFXScale", 0.5f);
                vehGunData[i].TracerFXScale = vehGunSettings.GetFloat(vehGunSettings.GetSectionNames()[i], "TracerFXScale", 2.0f);

                string vehOffsets = vehGunSettings.GetValue(vehGunSettings.GetSectionNames()[i], "ShotOffsets", "");

                List<string> offsetList = new List<string>();
                foreach (var offsetVector in vehOffsets.Split(':'))
                    offsetList.Add(offsetVector.Trim());

                for (int v = 0; v < offsetList.Count; v++)
                {
                    float x = float.Parse(offsetList[v].Split(',')[0].Trim());
                    float y = float.Parse(offsetList[v].Split(',')[1].Trim());
                    float z = float.Parse(offsetList[v].Split(',')[2].Trim());

                    vehGunData[i].Offsets.Add(new Vector3(x, y, z));
                }
            }
        }
        public static void Tick()
        {
            if (IS_CHAR_IN_ANY_CAR(Main.PlayerHandle) && NativeControls.IsGameKeyPressed(0, (GameKey)primaryKey))
            {
                GET_CAR_CHAR_IS_USING(Main.PlayerHandle, out int pVeh);
                GET_CAR_MODEL(pVeh, out uint vModel);

                for (int i = 0; i < vehGunData.Count(); i++)
                {
                    if (vModel == GET_HASH_KEY(vehGunData[i].VehModel) && Main.gTimer >= fTimer + vehGunData[i].TimeBetShots)
                    {
                        GET_VEHICLE_QUATERNION(pVeh, out Quaternion vQuat);
                        Vector3 vRot = GenHelp.QuaternionToRotation(vQuat.X, vQuat.Y, vQuat.Z, vQuat.W);

                        for (int v = 0; v < vehGunData[i].Offsets.Count; v++)
                            ProcessMinigunFire(i, pVeh, vehGunData[i].Offsets[v], vRot);
                        /*ProcessMinigunFire(i, pVeh, new Vector3(2.0f, 2.0f, 0.1f), vRot);
                        ProcessMinigunFire(i, pVeh, new Vector3(-2.0f, 2.0f, 0.1f), vRot);
                        ProcessMinigunFire(i, pVeh, new Vector3(2.65f, 2.0f, 0.15f), vRot);
                        ProcessMinigunFire(i, pVeh, new Vector3(-2.65f, 2.0f, 0.15f), vRot);*/

                        uint intervalTime = Main.gTimer - fTimer;

                        GET_GAME_TIMER(out fTimer);

                        if (intervalTime > vehGunData[i].TimeBetShots && intervalTime < vehGunData[i].TimeBetShots * 2)
                            fTimer -= (intervalTime - vehGunData[i].TimeBetShots);

                        CheckWitness(Main.PlayerPos, vehGunData[i].BulletRange, 50);
                    }
                }
            }
        }
        private static void ProcessMinigunFire(int i, int veh, Vector3 pos, Vector3 rot)
        {
            float randAccuracyX;
            float randAccuracyZ;

            randAccuracyX = GENERATE_RANDOM_FLOAT_IN_RANGE(-vehGunData[i].BulletSpread, vehGunData[i].BulletSpread);
            randAccuracyZ = GENERATE_RANDOM_FLOAT_IN_RANGE(-vehGunData[i].BulletSpread, vehGunData[i].BulletSpread);
            GET_OFFSET_FROM_CAR_IN_WORLD_COORDS(veh, pos, out Vector3 startPos);
            GET_OFFSET_FROM_CAR_IN_WORLD_COORDS(veh, new Vector3(pos.X + (pos.X > 0 ? -vehGunData[i].CenterAmount : vehGunData[i].CenterAmount) + randAccuracyX, pos.Y + vehGunData[i].BulletRange, pos.Z + randAccuracyZ), out Vector3 endPos);

            float tracerRot = (pos.X > 0 ? ((vehGunData[i].CenterAmount / vehGunData[i].BulletRange) * 45) : ((-vehGunData[i].CenterAmount / vehGunData[i].BulletRange) * 45));
            TRIGGER_PTFX("wpn_bullet_trace", startPos.X, startPos.Y, startPos.Z, rot.X - 90 + randAccuracyZ, rot.Y, rot.Z + tracerRot - randAccuracyX, vehGunData[i].TracerFXScale);
            TRIGGER_PTFX("wpn_bullet_smoke", startPos.X, startPos.Y, startPos.Z, rot.X - 90 + randAccuracyZ, rot.Y, rot.Z + tracerRot - randAccuracyX, vehGunData[i].TracerFXScale);

            TRIGGER_PTFX("muz_minigun", startPos.X, startPos.Y, startPos.Z, rot.X, rot.Y, rot.Z, vehGunData[i].MuzzleFXScale);
            TRIGGER_PTFX("muz_minigun_smoke", startPos.X, startPos.Y, startPos.Z, rot.X, rot.Y, rot.Z, vehGunData[i].MuzzleFXScale);

            int soundID = GET_SOUND_ID();
            PLAY_SOUND_FROM_POSITION(soundID, "HELI_MINIGUN_SHOT", startPos);
            RELEASE_SOUND_ID(soundID);

            FIRE_SINGLE_BULLET(startPos, endPos, vehGunData[i].DamagePerShot);
        }
        private static void CheckWitness(Vector3 pos, float distance, float copDistance)
        {
            //IVGame.ShowSubtitleMessage(bodyCount.Count.ToString());
            if (distance > 200)
                distance = 200;
            else if (distance < 50)
                distance = 50;

            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;

                if (!DOES_CHAR_EXIST(pedHandle))
                    continue;
                if (IS_CHAR_INJURED(pedHandle))
                    continue;
                if (bodyCount.Contains(pedHandle))
                    continue;

                GET_CHAR_COORDINATES(pedHandle, out Vector3 pedPos);
                GET_DISTANCE_BETWEEN_COORDS_3D(pos.X, pos.Y, pos.Z, pedPos.X, pedPos.Y, pedPos.Z, out float dist);

                if (dist <= distance && IS_CHAR_FACING_CHAR(Main.PlayerHandle, pedHandle, 5) && bodyCount.Count < 200)
                    bodyCount.Add(pedHandle);

                GET_PED_TYPE(pedHandle, out uint pedType);

                if (pedType == (uint)ePedType.PED_TYPE_COP)
                {
                    if (dist <= copDistance || IS_CHAR_FACING_CHAR(Main.PlayerHandle, pedHandle, 5))
                    {
                        ALTER_WANTED_LEVEL_NO_DROP((int)Main.PlayerIndex, 2);
                        APPLY_WANTED_LEVEL_CHANGE_NOW((int)Main.PlayerIndex);
                    }
                }
            }
            STORE_WANTED_LEVEL((int)Main.PlayerIndex, out currWanted);

            if (cachedWantedLvl > currWanted)
            {
                currWanted = 0;
                cachedWantedLvl = 0;
                bodyCount.Clear();
            }

            GetBodyCount(200, 6);
            GetBodyCount(125, 5);
            GetBodyCount(80, 4);
            GetBodyCount(40, 3);
            GetBodyCount(25, 2);
            GetBodyCount(8, 1);
        }
        private static void GetBodyCount(int deaths, uint wantedLvl)
        {
            if (currWanted >= wantedLvl)
                return;

            if (bodyCount.Count >= deaths)
            {
                ALTER_WANTED_LEVEL_NO_DROP((int)Main.PlayerIndex, wantedLvl);
                APPLY_WANTED_LEVEL_CHANGE_NOW((int)Main.PlayerIndex);
                cachedWantedLvl = wantedLvl;
            }
        }
    }
    public class VehicleGunData
    {
        public string VehModel;
        public List<Vector3> Offsets = new List<Vector3>();
        public uint TimeBetShots;
        public uint DamagePerShot;
        public float BulletSpread;
        public float BulletRange;
        public float CenterAmount;
        public float MuzzleFXScale;
        public float TracerFXScale;
        public VehicleGunData() { }
    }
}
