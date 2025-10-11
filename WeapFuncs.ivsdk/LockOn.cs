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
using static IVSDKDotNet.Native.Natives;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WeapFuncs.ivsdk
{
    internal class LockOn
    {
        //IniShit
        private static int lockOnWeapon;
        private static uint tex1, tex2, tex3, tex4, tex5, tex6, tex7, tex8, tex9, tex11, tex12, tex13, tex14, tex15, tex16, tex17, tex18, tex19, texTarget;
        private static float maxAttackDistance;
        private static float rocketSpeed;
        private static Vector3 offSetPTFX;
        private static Vector3 rocketOffsetPos;
        private static string rocketModel;
        private static string rocketTrail;
        private static float fuseTime;
        private static float firingReactRange;
        private static float explosionReactRange;

        private static int rocketObj;
        private static bool isAiming;
        private static bool bLaunched;
        private static Vector3 fixRotation;
        private static Vector3 fixDirection;
        private static Vector3 fixDirectionAux;
        private static Vector3 initialPosition;
        private static Int32 PTFXSmoke, PTFXExp;
        private static Int32 LaunchSID, MoveSID, BeepSID;
        private static int targetVeh;
        private static int tmpTargetVeh;
        private static float targetVehOffsetH;
        private static Vector3 midPos;
        private static Vector2 targetPosScrn;
        private static Vector3 vehPos;
        private static float timeLocked;
        private static float timeLockingBeep;
        private static Vector3 rocketPos;
        private static uint gTimer;
        private static uint fTimer;
        private static int rootCam;

        private static string soundLocked = "";
        private static string soundLocking = "";
        private static System.Media.SoundPlayer sndJLaunch = new System.Media.SoundPlayer();
        private static System.Media.SoundPlayer sndLocking = new System.Media.SoundPlayer();
        private static System.Media.SoundPlayer sndLocked = new System.Media.SoundPlayer();

        public static void Init(SettingsFile settings)
        {
            /*tex1 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim1.png"));
            tex2 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim2.png"));
            tex3 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim3.png"));
            tex4 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim4.png"));
            tex5 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim5.png"));
            tex6 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim6.png"));
            tex7 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim7.png"));
            tex8 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim8.png"));
            tex9 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim9.png"));
            tex11 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim11.png"));
            tex12 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim12.png"));
            tex13 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim13.png"));
            tex14 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim14.png"));
            tex15 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim15.png"));
            tex16 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim16.png"));
            tex17 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim17.png"));
            tex18 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim18.png"));
            tex19 = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllgreenAnim19.png"));
            texTarget = new Texture(File.ReadAllBytes(".\\scripts\\RocketLockFiles\\hudllredAnim.png"));*/

            lockOnWeapon = settings.GetInteger("LOCKON", "WeaponID", 0);
            maxAttackDistance = settings.GetFloat("LOCKON", "MaxAttackDistance", 200);
            rocketModel = settings.GetValue("LOCKON", "RocketModel", "w_e2_rocket");
            rocketTrail = settings.GetValue("LOCKON", "RocketTrail", "weap_rocket_player");
            soundLocking = settings.GetValue("LOCKON", "LockingOnSound", "");
            soundLocked = settings.GetValue("LOCKON", "LockedOnSound", "");
            rocketOffsetPos = settings.GetVector3("LOCKON", "RocketOffsetPosition", Vector3.Zero);
            offSetPTFX = settings.GetVector3("LOCKON", "TrailOffset", Vector3.Zero);
            fuseTime = settings.GetFloat("LOCKON", "FuseTime", 2000);
            rocketSpeed = settings.GetFloat("LOCKON", "RocketSpeed", 15);
            firingReactRange = settings.GetFloat("LOCKON", "FiringReactRange", 20.0f);
            explosionReactRange = settings.GetFloat("LOCKON", "ExplosionReactRange", 30.0f);
        }

        private static void Tick()
        {
            CreateRocket();

            GET_GAME_TIMER(out gTimer);
            GET_ROOT_CAM(out rootCam);
            if (Main.currWeap == lockOnWeapon && !isAiming && Main.IsPressingAimButton() && !bLaunched && !Main.IsReloadAnimPlaying() && Main.IsAimingAnimPlaying())
            {
                targetVeh = -1;
                tmpTargetVeh = -1;
                isAiming = true;
                SET_OBJECT_COLLISION(rocketObj, false);
            }

            if (DOES_OBJECT_EXIST(rocketObj))
                GET_OBJECT_COORDINATES(rocketObj, out rocketPos);

            if (NativeControls.IsGameKeyPressed(0, GameKey.Attack) && Main.currWeap == lockOnWeapon && !bLaunched && !Main.IsReloadAnimPlaying() && (Main.IsAimingAnimPlaying() || IS_PED_RAGDOLL(Main.PlayerHandle)))
            {
                SET_OBJECT_COLLISION(rocketObj, true);
                SET_OBJECT_RECORDS_COLLISIONS(rocketObj, false);

                DETACH_OBJECT(rocketObj, true);
                SET_OBJECT_COORDINATES(Main.PlayerHandle, rocketOffsetPos);
                SET_OBJECT_VISIBLE(rocketObj, true);

                GET_CAM_ROT(rootCam, out fixRotation);
                fixDirection = Helper.RotationToDirection(fixRotation);
                GET_CHAR_HEADING(Main.PlayerHandle, out float pHeading);
                fixDirectionAux = Helper.HeadingToDirection(pHeading);
                initialPosition = Main.PlayerPos;
                bLaunched = true;

                STOP_SOUND(LaunchSID);
                PLAY_SOUND_FROM_OBJECT(LaunchSID, "ROCKET_GRENADE_LAUNCH", rocketObj);

                STOP_PTFX(PTFXSmoke);
                PTFXSmoke = START_PTFX_ON_OBJ(rocketTrail, rocketObj, offSetPTFX.X, offSetPTFX.Y, offSetPTFX.Z, 0, 0, 0, 1.0f);

                STOP_SOUND(MoveSID);
                PLAY_SOUND_FROM_OBJECT(MoveSID, "GENERAL_WEAPONS_ROCKET_LOOP", rocketObj);

                GET_GAME_TIMER(out fTimer);

                APPLY_FORCE_TO_OBJECT(rocketObj, 3, fixDirection * rocketSpeed, Vector3.Zero, 0, 0, 1, 1);
                SET_OBJECT_ROTATION(rocketObj, fixRotation);

                bool bWanted = false;

                foreach (var ped in PedHelper.PedHandles)
                {
                    int pedHandle = ped.Value;
                    if (pedHandle == Main.PlayerHandle)
                        continue;
                    if (!DOES_CHAR_EXIST(pedHandle) || IS_CHAR_DEAD(pedHandle))
                        continue;

                    GET_CHAR_COORDINATES(pedHandle, out Vector3 pedPos);
                    if (Vector3.Distance(pedPos, Main.PlayerPos) > firingReactRange)
                        continue;

                    GET_CHAR_MODEL(pedHandle, out uint pedModel);
                    GET_CURRENT_BASIC_COP_MODEL(out uint copModel);

                    if (pedModel != copModel)
                        _TASK_SMART_FLEE_CHAR(pedHandle, Main.PlayerHandle, 100.0f, 15000);
                    else
                        bWanted = true;
                }

                if (bWanted)
                {
                    ALTER_WANTED_LEVEL_NO_DROP((int)Main.PlayerIndex, 1);
                    APPLY_WANTED_LEVEL_CHANGE_NOW((int)Main.PlayerIndex);
                }
                if (timeLocked >= 1000)
                    CalcMidPos();
            }

            if (bLaunched)
            {
                if (DOES_VEHICLE_EXIST(targetVeh))
                    GET_CAR_COORDINATES(targetVeh, out vehPos);

                if (HAS_OBJECT_COLLIDED_WITH_ANYTHING(rocketObj) || gTimer >= (fTimer + fuseTime) || Vector3.Distance(rocketPos, vehPos) < 0.5f || Vector3.Distance(rocketPos, Main.PlayerPos) > 200.0f)
                {
                    bool bWanted = false;

                    foreach (var ped in PedHelper.PedHandles)
                    {
                        int pedHandle = ped.Value;
                        if (pedHandle == Main.PlayerHandle)
                            continue;
                        if (!DOES_CHAR_EXIST(pedHandle) || IS_CHAR_DEAD(pedHandle))
                            continue;

                        GET_CHAR_COORDINATES(pedHandle, out Vector3 pedPos);
                        if (Vector3.Distance(pedPos, Main.PlayerPos) > explosionReactRange)
                            continue;

                        GET_CHAR_MODEL(pedHandle, out uint pedModel);
                        GET_CURRENT_BASIC_COP_MODEL(out uint copModel);

                        if (pedModel != copModel)
                            continue;

                        bWanted = true;
                    }

                    if (bWanted)
                    {
                        ALTER_WANTED_LEVEL_NO_DROP((int)Main.PlayerIndex, 2);
                        APPLY_WANTED_LEVEL_CHANGE_NOW((int)Main.PlayerIndex);
                    }

                    STOP_PTFX(PTFXExp);

                    STOP_SOUND(MoveSID);
                    PLAY_SOUND_FROM_POSITION(MoveSID, "PAYPHONE_INSERT_COIN", rocketPos.X, rocketPos.Y, (rocketPos.Z + 100f));

                    ADD_EXPLOSION(rocketPos, (int)eExplosion.EXPLOSION_ROCKET, 1.0f, true, false, 1.0f);

                    PTFXExp = START_PTFX("exp_rocket", rocketPos.X, rocketPos.Y, rocketPos.Z, 0, 0, 0, 1.0f);

                    ResetRocket();
                    bLaunched = false;
                }
                else
                {
                    if (timeLocked >= 1000)
                    {
                        Vector3 tmpDir = Vector3.Zero;
                        Vector3 tmpPos = Vector3.Zero;

                        if (DOES_VEHICLE_EXIST(targetVeh))
                        {
                            tmpDir = Vector3.Normalize(vehPos + Vector3.UnitZ * targetVehOffsetH - rocketPos);

                            APPLY_FORCE_TO_OBJECT(rocketObj, 3, tmpDir * rocketSpeed, Vector3.Zero, 0, 0, 1, 1);

                            tmpPos = vehPos;
                        }

                        if (Vector3.Distance(rocketPos, Main.PlayerPos) > 1.0f)
                            SET_OBJECT_RECORDS_COLLISIONS(rocketObj, true);

                        Int16 tmpDiv;

                        if (Vector3.Distance(rocketPos, tmpPos) > 3.0f)
                            tmpDiv = 10;
                        else
                            tmpDiv = 50;

                        Vector3 tmpRot = Main.DirectionToRotation(tmpDir, 0);
                        float incValue = Math.Abs(fixRotation.X - tmpRot.X) / (float)tmpDiv;

                        if (fixRotation.X > tmpRot.X)
                            fixRotation.X -= incValue;
                        else if (fixRotation.X < tmpRot.X)
                            fixRotation.X += incValue;
                        incValue = Math.Abs(fixRotation.Y - tmpRot.Y) / (float)tmpDiv;

                        if (fixRotation.Y > tmpRot.Y)
                            fixRotation.Y -= incValue;
                        else if (fixRotation.Y < tmpRot.Y)
                            fixRotation.Y += incValue;
                        fixRotation = Main.DirectionToRotation(fixDirection, 0);

                        SET_OBJECT_ROTATION(rocketObj, fixRotation);
                    }
                    else
                    {
                        APPLY_FORCE_TO_OBJECT(rocketObj, 3, fixDirection * rocketSpeed, Vector3.Zero, 0, 0, 1, 1);

                        if (Vector3.Distance(rocketPos, Main.PlayerPos) > 1.0f)
                            SET_OBJECT_RECORDS_COLLISIONS(rocketObj, true);

                        SET_OBJECT_ROTATION(rocketObj, fixRotation);
                    }
                }
            }
            else
            {
                SET_OBJECT_VISIBLE(rocketObj, false);

                if ((Vector3.Distance(rocketPos, Main.PlayerPos) > 2 || Main.IsReloadAnimPlaying()) && !isAiming)
                    ResetRocket();
            }

            if (!Main.IsPressingAimButton() && !bLaunched && !Main.IsAimingAnimPlaying() || Main.IsReloadAnimPlaying())
            {
                isAiming = false;
                timeLocked = 0;
            }
            else if (Main.currWeap == lockOnWeapon && !IS_CHAR_GETTING_UP(Main.PlayerHandle))
            {
                if (!bLaunched)
                {
                    GET_CAM_ROT(rootCam, out Vector3 camRot);

                    if (DOES_VEHICLE_EXIST(tmpTargetVeh))
                    {
                        GET_CAR_COORDINATES(tmpTargetVeh, out Vector3 tmpVehPos);
                        float vehDist = Vector3.Distance(tmpVehPos , (Main.PlayerPos + Helper.RotationToDirection(camRot) * Vector3.Distance(tmpVehPos, Main.PlayerPos)));

                        if (vehDist > 0.5f * Vector3.Distance(tmpVehPos, Main.PlayerPos))
                        {
                            targetVeh = -1;
                            tmpTargetVeh = -1;
                            timeLocked = 0;
                        }
                        if (timeLocked >= 1000 && CanPedSeeVehicle(Main.PlayerHandle, tmpTargetVeh, 200, 45))
                        {
                            targetVeh = tmpTargetVeh;

                            if (soundLocked == "")
                            {
                                STOP_SOUND(BeepSID);
                                    PLAY_SOUND_FROM_PED(BeepSID, "GENERAL_FRONTEND_GAME_ELECTRIC_ALARM", Main.PlayerHandle);
                                }
                            else
                            {
                                sndLocked.Stop();
                                sndLocked.Play();
                            }
                        }
                        else if (CanPedSeeVehicle(Main.PlayerHandle, tmpTargetVeh, 200, 45))
                        {
                            timeLocked += (1000 * Main.frameTime);

                            if ((soundLocking != ""))
                            {
                                if (timeLockingBeep <= 0)
                                {
                                    timeLockingBeep = 200;
                                    sndLocking.Play();
                                }
                                else
                                    timeLockingBeep -=(1000 * Main.frameTime);
                            }
                        }
                        else
                            timeLocked = 0;
                    }

                    else if (!DOES_VEHICLE_EXIST(tmpTargetVeh))
                    {
                        targetVeh = -1;
                        timeLocked = 0;

                        foreach (var veh in VehHelper.VehHandles)
                        {
                            int vehHandle = veh.Value;

                            if (!DOES_VEHICLE_EXIST(vehHandle))
                                continue;

                            GET_CAR_COORDINATES(vehHandle, out Vector3 vehPos);

                            if (Vector3.Distance(Main.PlayerPos, vehPos) > maxAttackDistance)
                                continue;

                            float vehDist = Vector3.Distance(vehPos, (Main.PlayerPos + Helper.RotationToDirection(camRot) * Vector3.Distance(vehPos, Main.PlayerPos)));

                            if (IS_CAR_ON_SCREEN(vehHandle) && !IS_CAR_DEAD(vehHandle) && Vector3.Distance(vehPos, Main.PlayerPos) > 2.0f && vehDist <= 0.5f * Vector3.Distance(vehPos, Main.PlayerPos))
                            {
                                tmpTargetVeh = vehHandle;
                                timeLocked = 0;

                                GET_CAR_MODEL(vehHandle, out uint vModel);
                                GET_MODEL_DIMENSIONS(vModel, out Vector3 vMinDim, out Vector3 vMaxDim);
                                targetVehOffsetH = (vMaxDim - vMinDim).Z / 4;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private static void CreateRocket()
        {
            if (!DOES_OBJECT_EXIST(rocketObj))
            {
                MoveSID = GET_SOUND_ID();
                LaunchSID = GET_SOUND_ID();
                BeepSID = GET_SOUND_ID();

                CREATE_OBJECT(GET_HASH_KEY(rocketModel), Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 10, out rocketObj, true);
                SET_OBJECT_VISIBLE(rocketObj, false);
                ATTACH_OBJECT_TO_PED(rocketObj, Main.PlayerHandle, (uint)eBone.BONE_SPINE, Vector3.Zero, Vector3.Zero, 0);

                sndJLaunch.SoundLocation = ".\\scripts\\RocketLockFiles\\jlaunch.wav";
                sndJLaunch.Load();

                if (soundLocking != "")
                {
                    sndLocking.SoundLocation = ".\\scripts\\RocketLockFiles\\" + soundLocking;
                    sndLocking.Load();
                }
                if (soundLocked != "")
                {
                    sndLocked.SoundLocation = ".\\scripts\\RocketLockFiles\\" + soundLocked;
                    sndLocked.Load();
                }
            }
        }
        private static bool CalcMidPos()
        {
            Vector3 tmpPos;
            Vector3 tmpVel = Vector3.Zero;
            if (DOES_VEHICLE_EXIST(targetVeh))
            {
                IVVehicle veh = NativeWorld.GetVehicleInstanceFromHandle(targetVeh);
                GET_CAR_COORDINATES(targetVeh, out tmpPos);
                veh.GetVelocity(tmpVel);
            }
            else
                return false;

            GET_DISTANCE_BETWEEN_COORDS_3D(initialPosition.X, initialPosition.Y, initialPosition.Z, tmpPos.X, tmpPos.Y, tmpPos.Z, out float pDist);

            midPos = initialPosition + (fixDirectionAux * (float)(pDist * 0.3));
            midPos.Z += 1;

            return true;
        }
        private static void DrawSprite(uint tex, float x, float y, float w, float h, float rot, Color col, int a)
        {
            DRAW_SPRITE(tex, x, y, w, h, rot, col.R, col.G, col.B, a);
        }
        private static Vector2 CoordToScreen(Vector3 posOn3D)
        {
            GET_GAME_VIEWPORT_ID(out int viewportID);
            GET_VIEWPORT_POSITION_OF_COORD(posOn3D, viewportID, out Vector2 screenPos);
            return screenPos;
        }
        private static void GraphicsHandler()
        {
            if (NativeControls.IsGameKeyPressed(0, GameKey.Aim) && !IS_CHAR_DEAD(Main.PlayerHandle) && !Main.IsReloadAnimPlaying() && Main.IsAimingAnimPlaying())
            {
                if (DOES_VEHICLE_EXIST(tmpTargetVeh) || DOES_VEHICLE_EXIST(targetVeh))
                {
                    Vector3 pos;
                    Color tmpColor;

                    if (timeLocked >= 1000)
                    {
                        tmpColor = Color.FromArgb(100, 255, 0, 0);
                    }
                    else if (timeLocked > 0)
                        tmpColor = Color.FromArgb(100, 255, 255, 255);

                    if (DOES_VEHICLE_EXIST(targetVeh))
                        GET_CAR_COORDINATES(targetVeh, out pos);
                    else if (DOES_VEHICLE_EXIST(tmpTargetVeh))
                        GET_CAR_COORDINATES(tmpTargetVeh, out pos);
                    else
                        pos = Vector3.Zero;

                    targetPosScrn = CoordToScreen(pos);
                }
            }
        }
        private static void ResetRocket()
        {
            STOP_PTFX(PTFXSmoke);
            STOP_SOUND(MoveSID);

            SET_OBJECT_COLLISION(rocketObj, false);
            SET_OBJECT_VISIBLE(rocketObj, false);
            SET_OBJECT_RECORDS_COLLISIONS(rocketObj, false);

            ATTACH_OBJECT_TO_PED(rocketObj, Main.PlayerHandle, (uint)eBone.BONE_SPINE, Vector3.Zero, Vector3.Zero, 0);

            targetVeh = -1;
            tmpTargetVeh = -1;
        }
        public static bool CanPedSeeVehicle(int ped, int targetVehicle, float sourcePedViewDistance = 50f, float sourcePedFOV = 90f)
        {
            if (!DOES_CHAR_EXIST(ped))
                return false;

            if (!DOES_VEHICLE_EXIST(targetVehicle))
                return false;

            GET_CHAR_COORDINATES(ped, out var pPos);
            Natives.GET_CAR_MODEL(targetVehicle, out var pValue);
            Natives.GET_MODEL_DIMENSIONS(pValue, out var pMinVector, out var pMaxVector);
            Natives.GET_OFFSET_FROM_CAR_IN_WORLD_COORDS(targetVehicle, new Vector3(0f, 0f, pMaxVector.Z + 0.1f), out var offset2);
            Vector3 value = offset2 - pPos;

            if (Vector3.Distance(pPos, offset2) > sourcePedViewDistance)
                return false;

            value = Vector3.Normalize(value);
            GET_CHAR_HEADING(ped, out float pHeading);
            if ((float)Math.Acos(Vector3.Dot(Helper.HeadingToDirection(pHeading), value)) * (180f / (float)Math.PI) > sourcePedFOV / 2f)
            {
                return false;
            }

            IVLineOfSightResults pResults;
            return !IVWorld.ProcessLineOfSight(pPos, offset2, out pResults, 1u);
        }
    }
}
