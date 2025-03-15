using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;

//Credits: Symbiote/AngryAmoeba for the original ScriptHook.Net mod
namespace WeapFuncs.ivsdk
{
    internal class SwitchWeapNoReload
    {
        Keys ini_ReloadKey;
        bool ini_LoseClipAmmo;

        private static List<int> gunList = new List<int>();        // List of held guns
        private static List<int> ammoList = new List<int>();       // List of each gun's last clip ammo
        private static List<int> totalAmmoList = new List<int>();       // List of each gun's last clip ammo
        private static int currWeaponIndex = 0;                    // The gunList index of the current weapon
        private static int currWeapon;                             // The current weapon
        private static int currClip = 0,                           // The current weapon's clip ammo
            extraAmmo = 0,                                         // The current weapon's total ammo, minus the clip
            bulletsFired = 0;                                      // Used to detect legitimate drops in clip ammo
        private static bool isReloading = false,
            bGunIsUsable = false;

        public static void Init()
        {
            gunList.Clear();
            ammoList.Clear();
            totalAmmoList.Clear();
        }
        public static void Tick()
        {
            // Prune guns that are no longer present
            for (int i = gunList.Count - 1; i >= 0; i--)
            {
                if (!HAS_CHAR_GOT_WEAPON(Main.PlayerHandle, gunList[i]))
                {
                    gunList.Remove(gunList[i]);
                    ammoList.Remove(ammoList[i]);
                }
            }

            if (!IS_CHAR_DEAD(Main.PlayerHandle) && Main.currWeap != 56 && Main.currWeap != 46 && Main.wSlot != 8)
            {
                /*if (isReloading)
                {
                    // Cancel reload if player switched weapons before it was complete
                    if (currWeapon != Main.currWeap)
                    {
                        ammoList[currWeaponIndex] = currClip;
                        isReloading = false;
                    }
                    // Check if reload has been completed (the game has moved ammo into the clip)
                    else if (currClip < Main.pAmmo)
                    else if (currClip < Main.pAmmo)
                        isReloading = false;
                }
                bGunIsUsable = true;
                if (currWeapon != Main.currWeap || ((currClip > Main.pAmmo) && bulletsFired == GET_INT_STAT(237)))
                    bGunIsUsable = false;
                else
                    bulletsFired = GET_INT_STAT(237);
                // Usable once clip ammo is no longer zero (which it's set to while switching weapons)

                if (isReloading || !bGunIsUsable)
                    return;
                */
                if (IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && currClip != Main.mAmmo && (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "p_load") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload_crouch") || (WeapFuncs.FiringWeapon(Main.PlayerPed) && Main.pAmmo == 0)))
                {
                    currClip = Main.mAmmo;
                    int ammoDiff = Main.pAmmo - ammoList[currWeaponIndex];
                    if (Main.LoseAmmoInMag && !(WeapFuncs.FiringWeapon(Main.PlayerPed) && Main.pAmmo == 0))
                    {
                        SET_CHAR_AMMO(Main.PlayerHandle, currWeapon, (Main.aAmmo + ammoDiff));
                        SET_AMMO_IN_CLIP(Main.PlayerHandle, currWeapon, 0);
                    }
                }

                if (Main.pAmmo != 0)
                {
                    currWeapon = Main.currWeap;
                    currClip = Main.pAmmo;
                }

                if (currWeapon == Main.currWeap && !IS_CHAR_SITTING_IN_ANY_CAR(Main.PlayerHandle) && (IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "p_load") || IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload_crouch")))
                {
                    currClip = Main.pAmmo;
                }

                if (bulletsFired < GET_INT_STAT(287))
                {
                    if (currWeapon == Main.currWeap && currClip == 1 && Main.pAmmo == 0)
                    {
                        currClip = 0;
                        isReloading = true;
                    }
                    bulletsFired = GET_INT_STAT(287);
                }

                if (!gunList.Contains(Main.currWeap))
                {
                    gunList.Add(currWeapon);
                    ammoList.Add(currClip);
                    totalAmmoList.Add(currClip);
                }
                currWeaponIndex = gunList.IndexOf(currWeapon);

                if (currWeaponIndex < 0)
                    return;

                if (currClip != ammoList[currWeaponIndex])
                {
                    if (isReloading)
                    {
                        ammoList[currWeaponIndex] = currClip;
                        isReloading = false;
                    }

                    else if (currClip > ammoList[currWeaponIndex] && !(WeapFuncs.FiringWeapon(Main.PlayerPed) && Main.pAmmo == 0) && !isReloading && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "p_load") && !IS_CHAR_PLAYING_ANIM(Main.PlayerHandle, Main.WeapAnim, "reload_crouch"))
                        RevertAmmo();

                    else
                        ammoList[currWeaponIndex] = currClip;
                }
            }
        }

        // Sets the current weapon's clip ammo to the value last saved for it
        private static void RevertAmmo()
        {
            int ammoDiff = currClip - ammoList[currWeaponIndex];
            if (ammoDiff != 0)
            {
                SET_CHAR_AMMO(Main.PlayerHandle, currWeapon, (Main.aAmmo + ammoDiff));
                SET_AMMO_IN_CLIP(Main.PlayerHandle, currWeapon, ammoList[currWeaponIndex]);
                //Main.aAmmo += ammoDiff;
                //Main.pAmmo -= ammoDiff;
                currClip = Main.pAmmo;
            }
        }
    }
}