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
    internal class Flames
    {
        // IniShit
        private static int flameWeapon;
        private static Vector3 flameOffset;
        private static float flameFxScale;
        private static float maxRange;
        private static int flameExplosion;
        private static float flameSpeed;

        // ListShit
        private static List<FlamethrowerData> flameList = new List<FlamethrowerData>();

        public static void Init(SettingsFile settings)
        {
            flameWeapon = settings.GetInteger("FLAMETHROWER", "FlameWeaponID", 19);
            flameFxScale = settings.GetFloat("FLAMETHROWER", "FlameFxScale", 2.0f);
            flameOffset = settings.GetVector3("FLAMETHROWER", "FlameOffset", Vector3.Zero);
            maxRange = settings.GetFloat("FLAMETHROWER", "FlameMaxRange", 7.5f);
            flameExplosion = settings.GetInteger("FLAMETHROWER", "FlameExplosionID", 23);
            flameSpeed = settings.GetFloat("FLAMETHROWER", "FlameSpeed", 8);
        }
        public static void Tick()
        {
            //GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, "gun@fthrower", "fire", out float pTime);
            //IVGame.ShowSubtitleMessage(pTime.ToString());
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (!DOES_CHAR_EXIST(pedHandle)) continue;
                if (IS_CHAR_INJURED(pedHandle)) continue;
                if (IS_CHAR_DEAD(pedHandle)) continue;

                if (flameList.Find(x => x.Ped == pedHandle) != null) continue;

                GET_CURRENT_CHAR_WEAPON(pedHandle, out int pedWeap);
                if (pedWeap == flameWeapon)
                {
                    flameList.Add(new FlamethrowerData(pedHandle));
                }
            }
            //IVGame.ShowSubtitleMessage(flameList.Count().ToString());

            for (int i = 0; i < flameList.Count(); i++)
            {
                if (Main.gTimer >= flameList[i].FlameTime + 250)
                {
                    PLAY_SOUND_FROM_POSITION(flameList[i].SoundID, "PAYPHONE_PICK_UP_A", Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 500);
                    RELEASE_SOUND_ID(flameList[i].SoundID);
                    flameList[i].SoundID = -1;
                }
                for (int p = 0; p < flameList[i].PTFXTime.Count(); p++)
                {
                    if (Main.gTimer >= flameList[i].PTFXTime[p] + 150)
                    {
                        STOP_PTFX(flameList[i].PTFX[p]);
                        if (Main.gTimer >= flameList[i].PTFXTime[p] + 1150)
                        {
                            REMOVE_PTFX(flameList[i].PTFX[p]);
                            flameList[i].PTFX.RemoveAt(p);
                            flameList[i].PTFXTime.RemoveAt(p);
                        }
                    }
                }
                if (!DOES_CHAR_EXIST(flameList[i].Ped) || IS_CHAR_INJURED(flameList[i].Ped))
                {
                    for (int p = 0; p < flameList[i].Projectile.Count(); p++)
                    {
                        if (DOES_OBJECT_EXIST(flameList[i].Projectile[p]))
                        {
                            int proj = flameList[i].Projectile[p];
                            DELETE_OBJECT(ref proj);
                        }
                    }
                    for (int p = 0; p < flameList[i].Projectile.Count(); p++)
                    {
                        REMOVE_PTFX(flameList[i].PTFX[p]);
                    }
                    PLAY_SOUND_FROM_POSITION(flameList[i].SoundID, "PAYPHONE_PICK_UP_A", Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 500);
                    RELEASE_SOUND_ID(flameList[i].SoundID);
                    flameList.RemoveAt(i);
                }
                else
                {
                    //IVGame.ShowSubtitleMessage(flameList[i].Projectile.Count.ToString() + "  " + flameList[i].PTFX.Count.ToString() + "  " + flameList[i].PTFXTime.Count.ToString());
                    GET_CURRENT_CHAR_WEAPON(flameList[i].Ped, out int pWeap);
                    if (pWeap == flameWeapon)
                    {
                        if (IS_CHAR_SHOOTING(flameList[i].Ped))
                        {
                            GET_PED_BONE_POSITION(flameList[i].Ped, (uint)eBone.BONE_RIGHT_HAND, flameOffset, out Vector3 bonePos);

                            if (flameList[i].SoundID == -1)
                            {
                                flameList[i].SoundID = GET_SOUND_ID();
                                PLAY_SOUND_FROM_POSITION(flameList[i].SoundID, "FIRE_GAS_BURNER", bonePos.X, bonePos.Y, bonePos.Z);
                            }
                            flameList[i].PTFX.Add(START_PTFX_ON_PED_BONE("shot_directed_flame", flameList[i].Ped, flameOffset.X, flameOffset.Y, flameOffset.Z, 0.0f, 90.0f, 0, 0x4D0, flameFxScale));

                            CREATE_OBJECT(GET_HASH_KEY("bm_cluckin_burg"), 0, 0, 0, out int proj, true);
                            flameList[i].Projectile.Add(proj);
                            SET_OBJECT_VISIBLE(proj, false);
                            ATTACH_OBJECT_TO_PED(proj, flameList[i].Ped, (uint)eBone.BONE_RIGHT_HAND, flameOffset.X, flameOffset.Y, flameOffset.Z, 0f, 0f, 0f, 0);
                            SET_OBJECT_COLLISION(proj, true);
                            SET_OBJECT_RECORDS_COLLISIONS(proj, true);
                            EXTINGUISH_OBJECT_FIRE(proj);

                            GET_GAME_TIMER(out uint flameTime);
                            flameList[i].FlameTime = flameTime;
                            flameList[i].PTFXTime.Add(flameTime);
                        }
                        else
                        {
                            for (int p = 0; p < flameList[i].Projectile.Count(); p++)
                            {
                                if (DOES_OBJECT_EXIST(flameList[i].Projectile[p]))
                                {
                                    GET_CHAR_COORDINATES(flameList[i].Ped, out Vector3 pos);
                                    GET_OBJECT_COORDINATES(flameList[i].Projectile[p], out Vector3 objPos);
                                    DETACH_OBJECT(flameList[i].Projectile[p], false);
                                    GET_OBJECT_SPEED(flameList[i].Projectile[p], out float objSpd);
                                    if (objSpd <= 0)
                                        APPLY_FORCE_TO_OBJECT(flameList[i].Projectile[p], 3u, new Vector3(flameSpeed, 0, 0), new Vector3(0, 0, 0), 0, 1, 1, 1);
                                    GET_DISTANCE_BETWEEN_COORDS_3D(pos.X, pos.Y, pos.Z, objPos.X, objPos.Y, objPos.Z, out float Dist);
                                    GET_OFFSET_FROM_OBJECT_IN_WORLD_COORDS(flameList[i].Projectile[p], new Vector3(0.0f, 0.1f, 0.0f), out Vector3 clsOff);
                                    if (HAS_OBJECT_COLLIDED_WITH_ANYTHING(flameList[i].Projectile[p]) || (Dist > maxRange))
                                    {
                                        ADD_EXPLOSION(clsOff.X, clsOff.Y, clsOff.Z, flameExplosion, 1.0f, false, true, 0.0f);
                                        SET_OBJECT_RECORDS_COLLISIONS(flameList[i].Projectile[p], false);
                                        int proj = flameList[i].Projectile[p];
                                        DELETE_OBJECT(ref proj);
                                    }
                                }
                                else
                                {
                                    flameList[i].Projectile.RemoveAt(p);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int p = 0; p < flameList[i].Projectile.Count(); p++)
                        {
                            if (DOES_OBJECT_EXIST(flameList[i].Projectile[p]))
                            {
                                int proj = flameList[i].Projectile[p];
                                DELETE_OBJECT(ref proj);
                            }
                        }
                        for (int p = 0; p < flameList[i].Projectile.Count(); p++)
                        {
                            REMOVE_PTFX(flameList[i].PTFX[p]);
                        }
                        PLAY_SOUND_FROM_POSITION(flameList[i].SoundID, "PAYPHONE_PICK_UP_A", Main.PlayerPos.X, Main.PlayerPos.Y, Main.PlayerPos.Z + 500);
                        RELEASE_SOUND_ID(flameList[i].SoundID);
                        flameList.RemoveAt(i);
                    }
                }
            }
        }
    }
    public class FlamethrowerData
    {
        public int Ped { get; set; }
        public List<int> Projectile = new List<int>();
        public int SoundID { get; set; }
        public List<int> PTFX = new List<int>();
        public uint FlameTime { get; set; }
        public List<uint> PTFXTime = new List<uint>();
        public FlamethrowerData(int ped)
        {
            Ped = ped;
        }
    }
}
