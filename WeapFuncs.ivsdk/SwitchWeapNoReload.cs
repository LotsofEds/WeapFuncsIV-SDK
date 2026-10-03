using CCL;
using IVSDKDotNet;
using IVSDKDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

//Credits: Symbiote/AngryAmoeba for the original ScriptHook.Net mod
namespace WeapFuncs.ivsdk
{
    internal class SwitchWeapNoReload
    {
        private static List<int> gunList = new List<int>();        // List of held guns
        private static List<int> ammoList = new List<int>();       // List of each gun's last clip ammo
        private static int currWeaponIndex = 0;                    // The gunList index of the current weapon
        private static int currWeapon;                             // The current weapon
        private static int currClip = -1,                          // The current weapon's clip ammo
            bulletsFired = 0;                                      // Used to detect legitimate drops in clip ammo
        private static bool isReloading = false;
        private static List<int> exceptionList = new List<int>();  // List of lose ammo in mag exceptions
        private static float animTime;
        private static bool cancelReload;

        public static void IngameStart()
        {
            gunList.Clear();
            ammoList.Clear();
        }
        public static void UnInit()
        {
            IngameStart();
            exceptionList.Clear();
        }
        public static void Init(SettingsFile settings)
        {
            IngameStart();
            exceptionList.Clear();

            string weaponsString = settings.GetValue("RELOADS", "LoseAmmoInMagExceptions", "");
            foreach (var weaponName in weaponsString.Split(','))
            {
                int weaponType = Int32.Parse(weaponName.Trim());
                exceptionList.Add(weaponType);
            }
            cancelReload = settings.GetBoolean("RELOADS", "FillMagImmediately", false);
        }
        public static void Tick()
        {
            if (IS_CHAR_DEAD(Main.PlayerHandle))
                IngameStart();

            // Prune guns that are no longer present
            for (int i = 0; i < gunList.Count; i++)
            {
                if (!HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, gunList[i]))
                {
                    gunList.RemoveAt(i);
                    ammoList.RemoveAt(i);
                }
            }

            if (!IS_CHAR_DEAD(Main.PlayerHandle) && Main.currWeap != 56 && Main.currWeap != 46 && Main.wSlot != 8)
            {
                if (Main.pAmmo != 0 && !IS_CHAR_GETTING_IN_TO_A_CAR(Main.PlayerHandle) && !(IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && Main.pAmmo != Main.mAmmo))
                {
                    currWeapon = Main.currWeap;
                    currClip = Main.pAmmo;
                }

                if (!gunList.Contains(currWeapon) && currWeapon > 0)
                {
                    gunList.Add(currWeapon);
                    ammoList.Add(currClip);
                }
                currWeaponIndex = gunList.IndexOf(currWeapon);

                if (currWeaponIndex < 0)
                    return;

                if (currWeapon != Main.currWeap)
                    isReloading = false;

                if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && currClip != Main.mAmmo && (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "p_load") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload_crouch") || (WeaponHelper.DBFiringWeapon(Main.PlayerPed) && Main.pAmmo == 0)))
                {
                    currClip = Main.mAmmo;
                    int ammoDiff = Main.pAmmo - ammoList[currWeaponIndex];
                    if (Main.LoseAmmoInMag && !(WeaponHelper.DBFiringWeapon(Main.PlayerPed) && Main.pAmmo == 0))
                    {
                        SET_CHAR_AMMO(Main.PlayerHandle, currWeapon, (Main.aAmmo + ammoDiff));
                        SET_AMMO_IN_CLIP(Main.PlayerHandle, currWeapon, 0);
                    }
                }

                else if (bulletsFired < GET_INT_STAT(287) && currWeapon == Main.currWeap)
                {
                    if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) || IS_CHAR_GETTING_IN_TO_A_CAR(Main.PlayerHandle))
                    {
                        if (Main.pAmmo != 0)
                            currClip = Main.pAmmo;
                        else
                        {
                            currClip = Main.pAmmo;
                            isReloading = true;
                        }
                    }

                    ammoList[currWeaponIndex] = currClip;
                    if (!isReloading && Main.pAmmo == 0 && !IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && !IS_CHAR_GETTING_IN_TO_A_CAR(Main.PlayerHandle))
                    {
                        ammoList[currWeaponIndex] = Main.pAmmo;
                        currClip = ammoList[currWeaponIndex];
                        isReloading = true;
                    }
                    bulletsFired = GET_INT_STAT(287);
                }

                if ((IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "p_load") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload_crouch")) && ammoList[currWeaponIndex] != Main.mAmmo && Main.pAmmo != Main.mAmmo && !isReloading)
                {
                    GET_AMMO_IN_CHAR_WEAPON(Main.PlayerHandle, currWeapon, out int aAmmo);
                    //IVGame.ShowSubtitleMessage("ass " + currWeapon.ToString() + "  " + aAmmo.ToString());
                    if (aAmmo == 0)
                        REMOVE_WEAPON_FROM_CHAR(Main.PlayerHandle, currWeapon);

                    else
                    {
                        GetAnimTime();
                        if (animTime < 0.8f)
                        {
                            bool dontLoseAmmo = false;
                            foreach (int weaponType in exceptionList)
                            {
                                if (currWeapon == weaponType)
                                {
                                    dontLoseAmmo = true;
                                    break;
                                }
                            }
                            if (!Main.LoseAmmoInMag || dontLoseAmmo)
                                ammoList[currWeaponIndex] = Main.pAmmo;
                            else
                            {
                                SET_AMMO_IN_CLIP(Main.PlayerHandle, currWeapon, 0);
                                ammoList[currWeaponIndex] = 0;
                            }

                            currClip = ammoList[currWeaponIndex];
                            isReloading = true;
                        }
                    }
                }

                if (currWeapon == Main.currWeap && currClip != ammoList[currWeaponIndex] && IVWeaponInfo.GetWeaponInfo((uint)currWeapon).WeaponFlags.AnimReload)
                {
                    if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "p_load") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload_crouch") || isReloading)
                    {
                        GetAnimTime();
                        if (Main.pAmmo == Main.mAmmo && (animTime >= 0.8f || cancelReload))
                        {
                            ammoList[currWeaponIndex] = Main.mAmmo;
                            currClip = ammoList[currWeaponIndex];
                            isReloading = false;
                        }
                        else if (animTime < 0.8f && Main.pAmmo == Main.mAmmo)
                            RevertAmmo();
                        else
                        {
                            ammoList[currWeaponIndex] = Main.pAmmo;
                            currClip = ammoList[currWeaponIndex];
                        }
                    }

                    else if (currClip > ammoList[currWeaponIndex] && !(WeaponHelper.DBFiringWeapon(Main.PlayerPed) && Main.pAmmo == 0) && !isReloading && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "p_load") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload_crouch"))
                        RevertAmmo();
                }
            }
        }
        private static void GetAnimTime()
        {
            if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload"))
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.wAnim, "reload", out animTime);
            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "p_load"))
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.wAnim, "p_load", out animTime);
            else if (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.wAnim, "reload_crouch"))
                GET_CHAR_ANIM_CURRENT_TIME(Main.PlayerHandle, Main.wAnim, "reload_crouch", out animTime);
        }

        // Sets the current weapon's clip ammo to the value last saved for it
        private static void RevertAmmo()
        {
            int ammoDiff = currClip - ammoList[currWeaponIndex];
            if (ammoDiff != 0)
            {
                SET_CHAR_AMMO(Main.PlayerHandle, currWeapon, (Main.aAmmo + ammoDiff));
                SET_AMMO_IN_CLIP(Main.PlayerHandle, currWeapon, ammoList[currWeaponIndex]);
                currClip = Main.pAmmo;
            }
            if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle))
            {
                SET_CHAR_AMMO(Main.PlayerHandle, currWeapon, (Main.aAmmo));
                SET_AMMO_IN_CLIP(Main.PlayerHandle, currWeapon, ammoList[currWeaponIndex]);
                currClip = Main.pAmmo;
            }
        }
    }
}