using CCL;
using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using IVSDKDotNet.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Policy;
using System.Windows.Forms;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WeapFuncs.ivsdk
{
    internal class LockOn
    {
        //IniShit
        private static int lockOnWeapon;
        private static float maxAttackDistance;
        private static string rocketModel;
        private static float turnStrength;
        private static int lockOnTime;

        private static List<int> rocketObjs = new List<int>();
        private static List<int> targetVehList = new List<int>();

        private static bool isAiming;
        private static bool bLaunched;
        private static Int32 BeepSID;
        private static int targetVeh;
        private static int tmpTargetVeh;
        private static Vector2 targetPosScrn;
        private static float timeLocked;
        private static uint bTimer;
        private static int rootCam;
        private static int tex;

        public static void Init(SettingsFile settings)
        {
            lockOnWeapon = settings.GetInteger("HEATSEEKER", "WeaponID", 0);
            maxAttackDistance = settings.GetFloat("HEATSEEKER", "MaxAttackDistance", 250);
            rocketModel = settings.GetValue("HEATSEEKER", "RocketModel", "cj_rpg_rocket");
            lockOnTime = settings.GetInteger("HEATSEEKER", "LockOnTime", 1000);
            turnStrength = settings.GetFloat("HEATSEEKER", "TrackingDelta", 3.0f);
        }
        public static void UnInit(SettingsFile settings)
        {
            rocketObjs.Clear();
            targetVehList.Clear();
            RELEASE_SOUND_ID(BeepSID);
        }

        public static void Tick()
        {
            //IVGame.ShowSubtitleMessage(rocketObjs.Count().ToString() + "  " + targetVehList.Count().ToString());

            GET_ROOT_CAM(out rootCam);

            if (Main.currWeap == lockOnWeapon)
            {
                if (!bLaunched)
                {
                    if (!isAiming && WeaponHelper.IsPressingAimButton() && WeaponHelper.IsAimingAnimPlaying())
                    {
                        timeLocked = 0;
                        targetVeh = -1;
                        tmpTargetVeh = -1;
                        isAiming = true;
                    }

                    if (IS_CHAR_SHOOTING(Main.PlayerHandle))
                    {
                        if (timeLocked >= lockOnTime)
                        {
                            foreach (var obj in ObjectHelper.ObjHandles)
                            {
                                int objHandle = obj.Value;

                                if (!DOES_OBJECT_EXIST(objHandle))
                                    continue;
                                if (rocketObjs.Contains(objHandle))
                                    continue;

                                GET_OBJECT_COORDINATES(objHandle, out Vector3 objPos);

                                if (Vector3.Distance(objPos, Main.PlayerPos) > 5.0f)
                                    continue;

                                GET_OBJECT_MODEL(objHandle, out uint objModel);

                                if (objModel != (uint)GET_HASH_KEY(rocketModel))
                                    continue;

                                rocketObjs.Add(objHandle);
                                targetVehList.Add(targetVeh);
                            }
                        }

                        bLaunched = true;
                    }
                }
            }

            if (!bLaunched && (!WeaponHelper.IsAimingAnimPlaying() || WeaponHelper.IsReloadAnimPlaying() || !WeaponHelper.IsPressingAimButton()))
            {
                targetVeh = -1;
                tmpTargetVeh = -1;
                isAiming = false;
                timeLocked = 0;
            }
            else if (Main.currWeap == lockOnWeapon && !IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                GET_AMMO_IN_CLIP(Main.PlayerHandle, Main.currWeap, out int currAmmo);
                if (!bLaunched)
                {
                    GET_CAM_ROT(rootCam, out Vector3 camRot);

                    if (DOES_VEHICLE_EXIST(tmpTargetVeh))
                    {
                        GraphicsHandler();
                        GET_CAR_COORDINATES(tmpTargetVeh, out Vector3 tmpVehPos);
                        float vehDist = Vector3.Distance(tmpVehPos, (Main.PlayerPos + Helper.RotationToDirection(camRot) * Vector3.Distance(tmpVehPos, Main.PlayerPos)));
                        
                        if ((vehDist > (0.05f * Vector3.Distance(tmpVehPos, Main.PlayerPos))) || !CanPedSeeVehicle(Main.PlayerHandle, tmpTargetVeh, maxAttackDistance, 180))
                        {
                            targetVeh = -1;
                            tmpTargetVeh = -1;
                            timeLocked = 0;
                        }
                        else if (timeLocked >= lockOnTime && CanPedSeeVehicle(Main.PlayerHandle, tmpTargetVeh, maxAttackDistance, 180))
                        {
                            targetVeh = tmpTargetVeh;

                            if (Main.gTimer >= bTimer + 50)
                            {
                                GET_GAME_TIMER(out bTimer);
                                STOP_SOUND(BeepSID);
                                PLAY_SOUND_FROM_PED(BeepSID, "GENERAL_FRONTEND_GAME_ELECTRIC_ALARM", Main.PlayerHandle);
                            }
                        }
                        else if (CanPedSeeVehicle(Main.PlayerHandle, tmpTargetVeh, maxAttackDistance, 180))
                        {
                            timeLocked += (1000 * Main.frameTime);

                            if (Main.gTimer >= bTimer + 200)
                            {
                                GET_GAME_TIMER(out bTimer);
                                STOP_SOUND(BeepSID);
                                PLAY_SOUND_FROM_PED(BeepSID, "GENERAL_FRONTEND_GAME_ELECTRIC_ALARM", Main.PlayerHandle);
                            }
                        }
                        else
                            timeLocked = 0;
                    }

                    else if (!DOES_VEHICLE_EXIST(tmpTargetVeh))
                    {
                        targetVeh = -1;
                        timeLocked = 0;

                        if (HAS_STREAMED_TXD_LOADED("darts"))
                            MARK_STREAMED_TXD_AS_NO_LONGER_NEEDED("darts");
                        if (tex > 0)
                        {
                            RELEASE_TEXTURE(tex);
                            tex = 0;
                        }
                        VehHelper.GrabAllVehs();

                        foreach (var veh in VehHelper.VehHandles)
                        {
                            int vehHandle = veh.Value;

                            if (!DOES_VEHICLE_EXIST(vehHandle))
                                continue;

                            GET_CAR_COORDINATES(vehHandle, out Vector3 vehPos);

                            if (Vector3.Distance(Main.PlayerPos, vehPos) > maxAttackDistance)
                                continue;

                            float vehDist = Vector3.Distance(vehPos, (Main.PlayerPos + Helper.RotationToDirection(camRot) * Vector3.Distance(vehPos, Main.PlayerPos)));

                            if (CanPedSeeVehicle(Main.PlayerHandle, vehHandle, maxAttackDistance, 180) && !IS_CAR_DEAD(vehHandle) && Vector3.Distance(vehPos, Main.PlayerPos) > 2.0f && vehDist <= (0.05f * Vector3.Distance(vehPos, Main.PlayerPos)))
                            {
                                tmpTargetVeh = vehHandle;
                                timeLocked = 0;

                                //GET_CAR_MODEL(vehHandle, out uint vModel);
                                //GET_MODEL_DIMENSIONS(vModel, out Vector3 vMinDim, out Vector3 vMaxDim);
                                //targetVehOffsetH = (vMaxDim - vMinDim).Z / 4;
                                break;
                            }
                        }
                    }
                }
                else if (bLaunched && currAmmo > 0)
                {
                    bLaunched = false;
                }
            }

            foreach (var rocketObj in rocketObjs)
            {
                if (!DOES_OBJECT_EXIST(rocketObj))
                {
                    targetVehList.RemoveAt(rocketObjs.IndexOf(rocketObj));
                    rocketObjs.Remove(rocketObj);
                    return;
                }

                else
                {
                    GET_OBJECT_COORDINATES(rocketObj, out Vector3 rocketPos);

                    int targetV = targetVehList[rocketObjs.IndexOf(rocketObj)];

                    Vector3 tmpDir = Vector3.Zero;
                    Vector3 tmpPos = Vector3.Zero;

                    if (DOES_VEHICLE_EXIST(targetV))
                    {
                        GET_CAR_COORDINATES(targetV, out Vector3 vehPos);

                        GET_CAR_MODEL(targetV, out uint vModel);
                        GET_MODEL_DIMENSIONS(vModel, out Vector3 vMinDim, out Vector3 vMaxDim);
                        float targetVehOffsetH = (vMaxDim - vMinDim).Z / 4;

                        tmpDir = Vector3.Normalize(vehPos + (Vector3.UnitZ * targetVehOffsetH) - rocketPos);
                        tmpPos = vehPos;

                        float tmpDiv;

                        if (Vector3.Distance(rocketPos, tmpPos) > 3.0f)
                            tmpDiv = turnStrength;
                        else
                            tmpDiv = turnStrength * 5;

                        GET_OBJECT_QUATERNION(rocketObj, out float objX, out float objY, out float objZ, out float objW);
                        Vector3 objRotation = GenHelp.QuaternionToRotation(objX, objY, objZ, objW);

                        Vector3 tmpRot = GenHelp.DirectionToRotation(tmpDir, 0);
                        float incValue = (Math.Abs(objRotation.X - tmpRot.X) * Main.frameTime * 100) / tmpDiv;

                        if (objRotation.X > tmpRot.X)
                            objRotation.X -= incValue;
                        else if (objRotation.X < tmpRot.X)
                            objRotation.X += incValue;
                        incValue = (Math.Abs(objRotation.Z - tmpRot.Z) * Main.frameTime * 100) / tmpDiv;

                        if (objRotation.Z > tmpRot.Z)
                            objRotation.Z -= incValue;
                        else if (objRotation.Z < tmpRot.Z)
                            objRotation.Z += incValue;

                        //IVGame.ShowSubtitleMessage(fixRotation.ToString() + "  " + tmpRot.ToString() + "  " + incValue.ToString());

                        SET_OBJECT_ROTATION(rocketObj, objRotation);
                    }
                }
            }
        }
        private static Vector2 CoordToScreen(Vector3 posOn3D)
        {
            GET_GAME_VIEWPORT_ID(out int viewportID);
            GET_VIEWPORT_POSITION_OF_COORD(posOn3D, viewportID, out Vector2 screenPos);
            return screenPos;
        }
        private static void GraphicsHandler()
        {
            if (DOES_VEHICLE_EXIST(tmpTargetVeh) || DOES_VEHICLE_EXIST(targetVeh))
            {
                Vector3 pos;
                Color tmpColor;

                if (timeLocked >= lockOnTime)
                {
                    tmpColor = Color.FromArgb(100, 192, 0, 0);
                }
                else
                    tmpColor = Color.FromArgb(100, 192, (int)(192 - (80 * (timeLocked / lockOnTime))), (int)(192 - (80 * (timeLocked / lockOnTime))));

                if (DOES_VEHICLE_EXIST(targetVeh))
                    GET_CAR_COORDINATES(targetVeh, out pos);
                else if (DOES_VEHICLE_EXIST(tmpTargetVeh))
                    GET_CAR_COORDINATES(tmpTargetVeh, out pos);
                else
                    pos = Vector3.Zero;

                targetPosScrn = CoordToScreen(pos);

                if (tex <= 0)
                {
                    if (!HAS_STREAMED_TXD_LOADED("darts"))
                        LOAD_TXD("darts");
                    tex = GET_TEXTURE_FROM_STREAMED_TXD("darts", "target");
                }

                DRAW_SPRITE((uint)tex, (targetPosScrn.X / IVGame.Resolution.Width), (targetPosScrn.Y / IVGame.Resolution.Height), ((float)IVGame.Resolution.Height / (float)IVGame.Resolution.Width) * (0.15f - 0.05f * (timeLocked / lockOnTime)), 0.15f - 0.05f * (timeLocked / lockOnTime), 0, tmpColor.R, tmpColor.G, tmpColor.B, 255);
            }
        }
        public static bool CanPedSeeVehicle(int ped, int targetVehicle, float sourcePedViewDistance, float sourcePedFOV)
        {
            if (!DOES_CHAR_EXIST(ped))
                return false;

            if (!DOES_VEHICLE_EXIST(targetVehicle))
                return false;

            GET_OFFSET_FROM_CHAR_IN_WORLD_COORDS(ped, new Vector3(0, 0, 0.5f), out var pPos);
            GET_CAR_MODEL(targetVehicle, out var pValue);
            GET_MODEL_DIMENSIONS(pValue, out var pMinVector, out var pMaxVector);
            GET_OFFSET_FROM_CAR_IN_WORLD_COORDS(targetVehicle, new Vector3(0f, 0f, pMaxVector.Z + 0.1f), out var offset2);
            Vector3 value = offset2 - pPos;

            if (Vector3.Distance(pPos, offset2) > sourcePedViewDistance)
                return false;

            value = Vector3.Normalize(value);
            GET_CAM_ROT(rootCam, out Vector3 camRot);

            //IVGame.ShowSubtitleMessage(((float)Math.Acos(Vector3.Dot(Helper.RotationToDirection(camRot), value)) * (180f / (float)Math.PI)).ToString() + "  " + (sourcePedFOV / 2f).ToString());

            if ((float)Math.Acos(Vector3.Dot(Helper.RotationToDirection(camRot), value)) * (180f / (float)Math.PI) > sourcePedFOV / 2f)
                return false;

            IVLineOfSightResults pResults;
            return !IVWorld.ProcessLineOfSight(pPos, offset2, out pResults, 1u);
        }
    }
}
