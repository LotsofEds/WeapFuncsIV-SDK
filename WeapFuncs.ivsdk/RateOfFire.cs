using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using static IVSDKDotNet.Native.Natives;
using CCL;
using IVSDKDotNet.Enums;
using System.Threading;
using System.Runtime;

namespace WeapFuncs.ivsdk
{
    internal class RateOfFire
    {
        // Dont try to make the reload work with this, dumbass me
        static string WeapAnim = "";
        static string BFAnim = "";
        static float FireRate;
        static float DbFireRate;
        static float BFFireRate;

        public static float PistolFire;
        public static float PistolFireDb;
        public static float PistolFireCover;

        public static float SilencedFire;
        public static float SilencedFireDb;
        public static float SilencedFireCover;

        public static float DeagleFire;
        public static float DeagleFireDb;
        public static float DeagleFireCover;

        public static float PumpShotFire;
        public static float PumpShotFireDb;
        public static float PumpShotFireCover;

        public static float CombatShotFire;
        public static float CombatShotFireDb;
        public static float CombatShotFireCover;

        public static float UziFire;
        public static float UziFireDb;
        public static float UziFireCover;

        public static float MP5Fire;
        public static float MP5FireDb;
        public static float MP5FireCover;

        public static float AK47Fire;
        public static float AK47FireDb;
        public static float AK47FireCover;

        public static float M4Fire;
        public static float M4FireDb;
        public static float M4FireCover;

        public static float SnipFire;
        public static float SnipFireDb;
        public static float SnipFireCover;

        public static float PsgFire;
        public static float PsgFireDb;
        public static float PsgFireCover;

        public static float RpgFire;
        public static float RpgFireDb;
        public static float RpgFireCover;

        public static float FThrowerFire;
        public static float FThrowerFireDb;
        public static float FThrowerFireCover;

        public static float AutoPFire;
        public static float AutoPFireDb;
        public static float AutoPFireCover;

        public static float SawnOffFire;
        public static float SawnOffFireDb;
        public static float SawnOffFireCover;

        public static float AssaultShotFire;
        public static float AssaultShotFireDb;
        public static float AssaultShotFireCover;

        public static float GrndLaunchFire;
        public static float GrndLaunchFireDb;
        public static float GrndLaunchFireCover;

        public static float Pistol44Fire;
        public static float Pistol44FireDb;
        public static float Pistol44FireCover;

        public static float AA12Fire;
        public static float AA12FireDb;
        public static float AA12FireCover;

        public static float AA12ExpFire;
        public static float AA12ExpFireDb;
        public static float AA12ExpFireCover;

        public static float P90Fire;
        public static float P90FireDb;
        public static float P90FireCover;

        public static float GoldUziFire;
        public static float GoldUziFireDb;
        public static float GoldUziFireCover;

        public static float M249Fire;
        public static float M249FireDb;
        public static float M249FireCover;

        public static float AdvSnipFire;
        public static float AdvSnipFireDb;
        public static float AdvSnipFireCover;

        public static float Episodic22Fire;
        public static float Episodic22FireDb;
        public static float Episodic22FireCover;

        public static float Episodic23Fire;
        public static float Episodic23FireDb;
        public static float Episodic23FireCover;

        public static float Episodic24Fire;
        public static float Episodic24FireDb;
        public static float Episodic24FireCover;

        public static float Episodic3Fire;
        public static float Episodic3FireDb;
        public static float Episodic3FireCover;

        public static float Addon1Fire;
        public static float Addon1FireDb;
        public static float Addon1FireCover;

        public static float Addon2Fire;
        public static float Addon2FireDb;
        public static float Addon2FireCover;

        public static float Addon3Fire;
        public static float Addon3FireDb;
        public static float Addon3FireCover;

        public static float Addon4Fire;
        public static float Addon4FireDb;
        public static float Addon4FireCover;

        public static float Addon5Fire;
        public static float Addon5FireDb;
        public static float Addon5FireCover;

        public static float Addon6Fire;
        public static float Addon6FireDb;
        public static float Addon6FireCover;

        public static float Addon7Fire;
        public static float Addon7FireDb;
        public static float Addon7FireCover;

        public static float Addon8Fire;
        public static float Addon8FireDb;
        public static float Addon8FireCover;

        public static float Addon9Fire;
        public static float Addon9FireDb;
        public static float Addon9FireCover;

        public static float Addon10Fire;
        public static float Addon10FireDb;
        public static float Addon10FireCover;

        public static float Addon11Fire;
        public static float Addon11FireDb;
        public static float Addon11FireCover;

        public static float Addon12Fire;
        public static float Addon12FireDb;
        public static float Addon12FireCover;

        public static float Addon13Fire;
        public static float Addon13FireDb;
        public static float Addon13FireCover;

        public static float Addon14Fire;
        public static float Addon14FireDb;
        public static float Addon14FireCover;

        public static float Addon15Fire;
        public static float Addon15FireDb;
        public static float Addon15FireCover;

        public static float Addon16Fire;
        public static float Addon16FireDb;
        public static float Addon16FireCover;

        public static float Addon17Fire;
        public static float Addon17FireDb;
        public static float Addon17FireCover;

        public static float Addon18Fire;
        public static float Addon18FireDb;
        public static float Addon18FireCover;

        public static float Addon19Fire;
        public static float Addon19FireDb;
        public static float Addon19FireCover;

        public static float Addon20Fire;
        public static float Addon20FireDb;
        public static float Addon20FireCover;

        public static float Unused0Fire;
        public static float Unused0FireDb;
        public static float Unused0FireCover;

        public static float BatFire;
        public static float BatFireDb;
        public static float BatFireCover;

        public static float PoolcueFire;
        public static float PoolcueFireDb;
        public static float PoolcueFireCover;

        public static float KnifeFire;
        public static float KnifeFireDb;
        public static float KnifeFireCover;

        public static float GrenadeFire;
        public static float GrenadeFireDb;
        public static float GrenadeFireCover;

        public static float MolotovFire;
        public static float MolotovFireDb;
        public static float MolotovFireCover;

        public static float RocketFire;
        public static float RocketFireDb;
        public static float RocketFireCover;

        public static float Episodic4Fire;
        public static float Episodic4FireDb;
        public static float Episodic4FireCover;

        public static float Episodic5Fire;
        public static float Episodic5FireDb;
        public static float Episodic5FireCover;

        public static float Episodic8Fire;
        public static float Episodic8FireDb;
        public static float Episodic8FireCover;

        public static float Episodic16Fire;
        public static float Episodic16FireDb;
        public static float Episodic16FireCover;

        public static float Episodic17Fire;
        public static float Episodic17FireDb;
        public static float Episodic17FireCover;

        public static float Episodic18Fire;
        public static float Episodic18FireDb;
        public static float Episodic18FireCover;

        public static float Episodic19Fire;
        public static float Episodic19FireDb;
        public static float Episodic19FireCover;

        public static float Episodic20Fire;
        public static float Episodic20FireDb;
        public static float Episodic20FireCover;

        public static float Episodic21Fire;
        public static float Episodic21FireDb;
        public static float Episodic21FireCover;

        public static float CameraFire;
        public static float CameraFireDb;
        public static float CameraFireCover;

        public static float ObjectFire;
        public static float ObjectFireDb;
        public static float ObjectFireCover;
        public static void Tick()
        {
            foreach (var ped in PedHelper.PedHandles)
            {
                int pedHandle = ped.Value;
                if (IS_CHAR_DEAD(pedHandle))
                    continue;
                if (!IS_CHAR_SHOOTING(pedHandle))
                    continue;

                //Main.TheWeaponHandler.Process();
                GET_CURRENT_CHAR_WEAPON(pedHandle, out int currWeap);
                uint slot = IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponSlot;
                bool twoHanded = (IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.TreatAsTwoHandedInCover || IVWeaponInfo.GetWeaponInfo((uint)currWeap).WeaponFlags.TwoHanded);

                switch (currWeap)
                {
                    case 1:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 2:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 3:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 4:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 5:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 6:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 7:
                        WeapAnim = Main.PistolAnim;
                        FireRate = PistolFire;
                        DbFireRate = PistolFireDb;
                        BFFireRate = PistolFireCover;
                        break;
                    case 8:
                        WeapAnim = Main.Unused0Anim;
                        FireRate = Unused0Fire;
                        DbFireRate = Unused0FireDb;
                        BFFireRate = Unused0FireCover;
                        break;
                    case 9:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 10:
                        WeapAnim = Main.PumpShotAnim;
                        FireRate = PumpShotFire;
                        DbFireRate = PumpShotFireDb;
                        BFFireRate = PumpShotFireCover;
                        break;
                    case 11:
                        WeapAnim = Main.CombatShotAnim;
                        FireRate = CombatShotFire;
                        DbFireRate = CombatShotFireDb;
                        BFFireRate = CombatShotFireCover;
                        break;
                    case 12:
                        WeapAnim = Main.UziAnim;
                        FireRate = UziFire;
                        DbFireRate = UziFireDb;
                        BFFireRate = UziFireCover;
                        break;
                    case 13:
                        WeapAnim = Main.MP5Anim;
                        FireRate = MP5Fire;
                        DbFireRate = MP5FireDb;
                        BFFireRate = MP5FireCover;
                        break;
                    case 14:
                        WeapAnim = Main.AK47Anim;
                        FireRate = AK47Fire;
                        DbFireRate = AK47FireDb;
                        BFFireRate = AK47FireCover;
                        break;
                    case 15:
                        WeapAnim = Main.M4Anim;
                        FireRate = M4Fire;
                        DbFireRate = M4FireDb;
                        BFFireRate = M4FireCover;
                        break;
                    case 16:
                        WeapAnim = Main.PsgAnim;
                        FireRate = PsgFire;
                        DbFireRate = PsgFireDb;
                        BFFireRate = PsgFireCover;
                        break;
                    case 17:
                        WeapAnim = Main.SnipAnim;
                        FireRate = SnipFire;
                        DbFireRate = SnipFireDb;
                        BFFireRate = SnipFireCover;
                        break;
                    case 18:
                        WeapAnim = Main.RpgAnim;
                        FireRate = RpgFire;
                        DbFireRate = RpgFireDb;
                        BFFireRate = RpgFireCover;
                        break;
                    case 19:
                        WeapAnim = Main.FThrowerAnim;
                        FireRate = FThrowerFire;
                        DbFireRate = FThrowerFireDb;
                        BFFireRate = FThrowerFireCover;
                        break;
                    case 20:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 21:
                        WeapAnim = Main.GrndLaunchAnim;
                        FireRate = GrndLaunchFire;
                        DbFireRate = GrndLaunchFireDb;
                        BFFireRate = GrndLaunchFireCover;
                        break;
                    case 22:
                        WeapAnim = Main.AssaultShotAnim;
                        FireRate = AssaultShotFire;
                        DbFireRate = AssaultShotFireDb;
                        BFFireRate = AssaultShotFireCover;
                        break;
                    case 23:
                        WeapAnim = Main.Episodic3Anim;
                        FireRate = Episodic3Fire;
                        DbFireRate = Episodic3FireDb;
                        BFFireRate = Episodic3FireCover;
                        break;
                    case 24:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 25:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 26:
                        WeapAnim = Main.SawnOffAnim;
                        FireRate = SawnOffFire;
                        DbFireRate = SawnOffFireDb;
                        BFFireRate = SawnOffFireCover;
                        break;
                    case 27:
                        WeapAnim = Main.AutoPAnim;
                        FireRate = AutoPFire;
                        DbFireRate = AutoPFireDb;
                        BFFireRate = AutoPFireCover;
                        break;
                    case 28:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 29:
                        WeapAnim = Main.Pistol44Anim;
                        FireRate = Pistol44Fire;
                        DbFireRate = Pistol44FireDb;
                        BFFireRate = Pistol44FireCover;
                        break;
                    case 30:
                        WeapAnim = Main.AA12ExpAnim;
                        FireRate = AA12ExpFire;
                        DbFireRate = AA12ExpFireDb;
                        BFFireRate = AA12ExpFireCover;
                        break;
                    case 31:
                        WeapAnim = Main.AA12Anim;
                        FireRate = AA12Fire;
                        DbFireRate = AA12FireDb;
                        BFFireRate = AA12FireCover;
                        break;
                    case 32:
                        WeapAnim = Main.P90Anim;
                        FireRate = P90Fire;
                        DbFireRate = P90FireDb;
                        BFFireRate = P90FireCover;
                        break;
                    case 33:
                        WeapAnim = Main.GoldUziAnim;
                        FireRate = GoldUziFire;
                        DbFireRate = GoldUziFireDb;
                        BFFireRate = GoldUziFireCover;
                        break;
                    case 34:
                        WeapAnim = Main.M249Anim;
                        FireRate = M249Fire;
                        DbFireRate = M249FireDb;
                        BFFireRate = M249FireCover;
                        break;
                    case 35:
                        WeapAnim = Main.AdvSnipAnim;
                        FireRate = AdvSnipFire;
                        DbFireRate = AdvSnipFireDb;
                        BFFireRate = AdvSnipFireCover;
                        break;
                    case 36:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 37:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 38:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 39:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 40:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 41:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 42:
                        WeapAnim = Main.Episodic22Anim;
                        FireRate = Episodic22Fire;
                        DbFireRate = Episodic22FireDb;
                        BFFireRate = Episodic22FireCover;
                        break;
                    case 43:
                        WeapAnim = Main.Episodic23Anim;
                        FireRate = Episodic23Fire;
                        DbFireRate = Episodic23FireDb;
                        BFFireRate = Episodic23FireCover;
                        break;
                    case 44:
                        WeapAnim = Main.Episodic24Anim;
                        FireRate = Episodic24Fire;
                        DbFireRate = Episodic24FireDb;
                        BFFireRate = Episodic24FireCover;
                        break;
                    case 45:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 46:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 47:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 48:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 49:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 50:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 51:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 52:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 53:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 54:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 55:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 56:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 57:
                        WeapAnim = Main.DeagleAnim;
                        FireRate = DeagleFire;
                        DbFireRate = DeagleFireDb;
                        BFFireRate = DeagleFireCover;
                        break;
                    case 58:
                        WeapAnim = Main.Addon1Anim;
                        FireRate = Addon1Fire;
                        DbFireRate = Addon1FireDb;
                        BFFireRate = Addon1FireCover;
                        break;
                    case 59:
                        WeapAnim = Main.Addon2Anim;
                        FireRate = Addon2Fire;
                        DbFireRate = Addon2FireDb;
                        BFFireRate = Addon2FireCover;
                        break;
                    case 60:
                        WeapAnim = Main.Addon3Anim;
                        FireRate = Addon3Fire;
                        DbFireRate = Addon3FireDb;
                        BFFireRate = Addon3FireCover;
                        break;
                    case 61:
                        WeapAnim = Main.Addon4Anim;
                        FireRate = Addon4Fire;
                        DbFireRate = Addon4FireDb;
                        BFFireRate = Addon4FireCover;
                        break;
                    case 62:
                        WeapAnim = Main.Addon5Anim;
                        FireRate = Addon5Fire;
                        DbFireRate = Addon5FireDb;
                        BFFireRate = Addon5FireCover;
                        break;
                    case 63:
                        WeapAnim = Main.Addon6Anim;
                        FireRate = Addon6Fire;
                        DbFireRate = Addon6FireDb;
                        BFFireRate = Addon6FireCover;
                        break;
                    case 64:
                        WeapAnim = Main.Addon7Anim;
                        FireRate = Addon7Fire;
                        DbFireRate = Addon7FireDb;
                        BFFireRate = Addon7FireCover;
                        break;
                    case 65:
                        WeapAnim = Main.Addon8Anim;
                        FireRate = Addon8Fire;
                        DbFireRate = Addon8FireDb;
                        BFFireRate = Addon8FireCover;
                        break;
                    case 66:
                        WeapAnim = Main.Addon9Anim;
                        FireRate = Addon9Fire;
                        DbFireRate = Addon9FireDb;
                        BFFireRate = Addon9FireCover;
                        break;
                    case 67:
                        WeapAnim = Main.Addon10Anim;
                        FireRate = Addon10Fire;
                        DbFireRate = Addon10FireDb;
                        BFFireRate = Addon10FireCover;
                        break;
                    case 68:
                        WeapAnim = Main.Addon11Anim;
                        FireRate = Addon11Fire;
                        DbFireRate = Addon11FireDb;
                        BFFireRate = Addon11FireCover;
                        break;
                    case 69:
                        WeapAnim = Main.Addon12Anim;
                        FireRate = Addon12Fire;
                        DbFireRate = Addon12FireDb;
                        BFFireRate = Addon12FireCover;
                        break;
                    case 70:
                        WeapAnim = Main.Addon13Anim;
                        FireRate = Addon13Fire;
                        DbFireRate = Addon13FireDb;
                        BFFireRate = Addon13FireCover;
                        break;
                    case 71:
                        WeapAnim = Main.Addon14Anim;
                        FireRate = Addon14Fire;
                        DbFireRate = Addon14FireDb;
                        BFFireRate = Addon14FireCover;
                        break;
                    case 72:
                        WeapAnim = Main.Addon15Anim;
                        FireRate = Addon15Fire;
                        DbFireRate = Addon15FireDb;
                        BFFireRate = Addon15FireCover;
                        break;
                    case 73:
                        WeapAnim = Main.Addon16Anim;
                        FireRate = Addon16Fire;
                        DbFireRate = Addon16FireDb;
                        BFFireRate = Addon16FireCover;
                        break;
                    case 74:
                        WeapAnim = Main.Addon17Anim;
                        FireRate = Addon17Fire;
                        DbFireRate = Addon17FireDb;
                        BFFireRate = Addon17FireCover;
                        break;
                    case 75:
                        WeapAnim = Main.Addon18Anim;
                        FireRate = Addon18Fire;
                        DbFireRate = Addon18FireDb;
                        BFFireRate = Addon18FireCover;
                        break;
                    case 76:
                        WeapAnim = Main.Addon19Anim;
                        FireRate = Addon19Fire;
                        DbFireRate = Addon19FireDb;
                        BFFireRate = Addon19FireCover;
                        break;
                    case 77:
                        WeapAnim = Main.Addon20Anim;
                        FireRate = Addon20Fire;
                        DbFireRate = Addon20FireDb;
                        BFFireRate = Addon20FireCover;
                        break;
                }
                switch (slot)
                {
                    case 2:
                        if (twoHanded)
                            BFAnim = "ak47_blindfire";
                        else
                            BFAnim = "pistol_blindfire";
                        break;
                    case 3:
                        BFAnim = "shotgun_blindfire";
                        break;
                    case 4:
                        if (twoHanded)
                            BFAnim = "ak47_blindfire";
                        else
                            BFAnim = "uzi_blindfire";
                        break;
                    case 5:
                        BFAnim = "ak47_blindfire";
                        break;
                    case 6:
                        BFAnim = "rifle_blindfire";
                        break;
                    case 7:
                        BFAnim = "rocket_blindfire";
                        break;
                }

                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_crouch", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_alt", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_crouch_alt", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_up", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "fire_down", (FireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "dbfire", (DbFireRate));
                SET_CHAR_ANIM_SPEED(pedHandle, WeapAnim, "dbfire_l", (DbFireRate));
                if (pedHandle != Main.PlayerHandle)
                {
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_high_corner", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_centre", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_l_low_corner", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_high_corner", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_centre", BFAnim, (BFFireRate));
                    SET_CHAR_ANIM_SPEED(pedHandle, "cover_r_low_corner", BFAnim, (BFFireRate));
                }
            }
        }
        public static void LoadSettings(SettingsFile settings)
        {
            PistolFire = settings.GetFloat("GLOCK", "NormalROF", 1.0f);
            PistolFireDb = settings.GetFloat("GLOCK", "DrivebyROF", 1.0f);
            PistolFireCover = settings.GetFloat("GLOCK", "InCoverROF", 1.0f);

            SilencedFire = settings.GetFloat("SILENCED", "NormalROF", 1.0f);
            SilencedFireDb = settings.GetFloat("SILENCED", "DrivebyROF", 1.0f);
            SilencedFireCover = settings.GetFloat("SILENCED", "InCoverROF", 1.0f);

            DeagleFire = settings.GetFloat("DEAGLE", "NormalROF", 1.0f);
            DeagleFireDb = settings.GetFloat("DEAGLE", "DrivebyROF", 1.0f);
            DeagleFireCover = settings.GetFloat("DEAGLE", "InCoverROF", 1.0f);

            PumpShotFire = settings.GetFloat("PUMP SHOTGUN", "NormalROF", 1.0f);
            PumpShotFireCover = settings.GetFloat("PUMP SHOTGUN", "InCoverROF", 1.0f);

            CombatShotFire = settings.GetFloat("SEMIAUTO SHOTGUN", "NormalROF", 1.0f);
            CombatShotFireCover = settings.GetFloat("SEMIAUTO SHOTGUN", "InCoverROF", 1.0f);

            UziFire = settings.GetFloat("MICRO UZI", "NormalROF", 1.0f);
            UziFireDb = settings.GetFloat("MICRO UZI", "DrivebyROF", 1.0f);
            UziFireCover = settings.GetFloat("MICRO UZI", "InCoverROF", 1.0f);

            MP5Fire = settings.GetFloat("MP5", "NormalROF", 1.0f);
            MP5FireDb = settings.GetFloat("MP5", "DrivebyROF", 1.0f);
            MP5FireCover = settings.GetFloat("MP5", "InCoverROF", 1.0f);

            AK47Fire = settings.GetFloat("AK47", "NormalROF", 1.0f);
            AK47FireDb = settings.GetFloat("AK47", "DrivebyROF", 1.0f);
            AK47FireCover = settings.GetFloat("AK47", "InCoverROF", 1.0f);

            M4Fire = settings.GetFloat("M4", "NormalROF", 1.0f);
            M4FireDb = settings.GetFloat("M4", "DrivebyROF", 1.0f);
            M4FireCover = settings.GetFloat("M4", "InCoverROF", 1.0f);

            SnipFire = settings.GetFloat("BOLTACTION SNIPER", "NormalROF", 1.0f);
            SnipFireDb = settings.GetFloat("BOLTACTION SNIPER", "DrivebyROF", 1.0f);
            SnipFireCover = settings.GetFloat("BOLTACTION SNIPER", "InCoverROF", 1.0f);

            PsgFire = settings.GetFloat("SEMIAUTO SNIPER", "NormalROF", 1.0f);
            PsgFireDb = settings.GetFloat("SEMIAUTO SNIPER", "DrivebyROF", 1.0f);
            PsgFireCover = settings.GetFloat("SEMIAUTO SNIPER", "InCoverROF", 1.0f);

            RpgFire = settings.GetFloat("RPG", "NormalROF", 1.0f);
            RpgFireCover = settings.GetFloat("RPG", "InCoverROF", 1.0f);

            FThrowerFire = settings.GetFloat("FTHROWER", "NormalROF", 1.0f);
            FThrowerFireDb = settings.GetFloat("FTHROWER", "DrivebyROF", 1.0f);
            FThrowerFireCover = settings.GetFloat("FTHROWER", "InCoverROF", 1.0f);

            AutoPFire = settings.GetFloat("FULLAUTO PISTOL", "NormalROF", 1.0f);
            AutoPFireDb = settings.GetFloat("FULLAUTO PISTOL", "DrivebyROF", 1.0f);
            AutoPFireCover = settings.GetFloat("FULLAUTO PISTOL", "InCoverROF", 1.0f);

            SawnOffFire = settings.GetFloat("SAWNOFF SHOTGUN", "NormalROF", 1.0f);
            SawnOffFireDb = settings.GetFloat("SAWNOFF SHOTGUN", "DrivebyROF", 1.0f);
            SawnOffFireCover = settings.GetFloat("SAWNOFF SHOTGUN", "InCoverROF", 1.0f);

            AssaultShotFire = settings.GetFloat("ASSAULT SHOTGUN", "NormalROF", 1.0f);
            AssaultShotFireDb = settings.GetFloat("ASSAULT SHOTGUN", "DrivebyROF", 1.0f);
            AssaultShotFireCover = settings.GetFloat("ASSAULT SHOTGUN", "InCoverROF", 1.0f);

            GrndLaunchFire = settings.GetFloat("GRENADE LAUNCHER", "NormalROF", 1.0f);
            GrndLaunchFireCover = settings.GetFloat("GRENADE LAUNCHER", "InCoverROF", 1.0f);

            Pistol44Fire = settings.GetFloat("PISTOL 44", "NormalROF", 1.0f);
            Pistol44FireDb = settings.GetFloat("PISTOL 44", "DrivebyROF", 1.0f);
            Pistol44FireCover = settings.GetFloat("PISTOL 44", "InCoverROF", 1.0f);

            AA12Fire = settings.GetFloat("AA12", "NormalROF", 1.0f);
            AA12FireCover = settings.GetFloat("AA12", "InCoverROF", 1.0f);

            AA12ExpFire = settings.GetFloat("AA12 EXP", "NormalROF", 1.0f);
            AA12ExpFireCover = settings.GetFloat("AA12 EXP", "InCoverROF", 1.0f);

            P90Fire = settings.GetFloat("P90", "NormalROF", 1.0f);
            P90FireDb = settings.GetFloat("P90", "DrivebyROF", 1.0f);
            P90FireCover = settings.GetFloat("P90", "InCoverROF", 1.0f);

            GoldUziFire = settings.GetFloat("GOLD UZI", "NormalROF", 1.0f);
            GoldUziFireDb = settings.GetFloat("GOLD UZI", "DrivebyROF", 1.0f);
            GoldUziFireCover = settings.GetFloat("GOLD UZI", "InCoverROF", 1.0f);

            M249Fire = settings.GetFloat("M249", "NormalROF", 1.0f);
            M249FireDb = settings.GetFloat("M249", "DrivebyROF", 1.0f);
            M249FireCover = settings.GetFloat("M249", "InCoverROF", 1.0f);

            AdvSnipFire = settings.GetFloat("ADV SNIPER", "NormalROF", 1.0f);
            AdvSnipFireDb = settings.GetFloat("ADV SNIPER", "DrivebyROF", 1.0f);
            AdvSnipFireCover = settings.GetFloat("ADV SNIPER", "InCoverROF", 1.0f);

            Episodic22Fire = settings.GetFloat("EPISODIC 22", "NormalROF", 1.0f);
            Episodic22FireDb = settings.GetFloat("EPISODIC 22", "DrivebyROF", 1.0f);
            Episodic22FireCover = settings.GetFloat("EPISODIC 22", "InCoverROF", 1.0f);

            Episodic23Fire = settings.GetFloat("EPISODIC 23", "NormalROF", 1.0f);
            Episodic23FireDb = settings.GetFloat("EPISODIC 23", "DrivebyROF", 1.0f);
            Episodic23FireCover = settings.GetFloat("EPISODIC 23", "InCoverROF", 1.0f);

            Episodic24Fire = settings.GetFloat("EPISODIC 24", "NormalROF", 1.0f);
            Episodic24FireDb = settings.GetFloat("EPISODIC 24", "DrivebyROF", 1.0f);
            Episodic24FireCover = settings.GetFloat("EPISODIC 24", "InCoverROF", 1.0f);

            Episodic3Fire = settings.GetFloat("EPISODIC 3", "NormalROF", 1.0f);
            Episodic3FireDb = settings.GetFloat("EPISODIC 3", "DrivebyROF", 1.0f);
            Episodic3FireCover = settings.GetFloat("EPISODIC 3", "InCoverROF", 1.0f);

            Addon1Fire = settings.GetFloat("ADDONWEAPON 1", "NormalROF", 1.0f);
            Addon1FireDb = settings.GetFloat("ADDONWEAPON 1", "DrivebyROF", 1.0f);
            Addon1FireCover = settings.GetFloat("ADDONWEAPON 1", "InCoverROF", 1.0f);

            Addon2Fire = settings.GetFloat("ADDONWEAPON 2", "NormalROF", 1.0f);
            Addon2FireDb = settings.GetFloat("ADDONWEAPON 2", "DrivebyROF", 1.0f);
            Addon2FireCover = settings.GetFloat("ADDONWEAPON 2", "InCoverROF", 1.0f);

            Addon3Fire = settings.GetFloat("ADDONWEAPON 3", "NormalROF", 1.0f);
            Addon3FireDb = settings.GetFloat("ADDONWEAPON 3", "DrivebyROF", 1.0f);
            Addon3FireCover = settings.GetFloat("ADDONWEAPON 3", "InCoverROF", 1.0f);

            Addon4Fire = settings.GetFloat("ADDONWEAPON 4", "NormalROF", 1.0f);
            Addon4FireDb = settings.GetFloat("ADDONWEAPON 4", "DrivebyROF", 1.0f);
            Addon4FireCover = settings.GetFloat("ADDONWEAPON 4", "InCoverROF", 1.0f);

            Addon5Fire = settings.GetFloat("ADDONWEAPON 5", "NormalROF", 1.0f);
            Addon5FireDb = settings.GetFloat("ADDONWEAPON 5", "DrivebyROF", 1.0f);
            Addon5FireCover = settings.GetFloat("ADDONWEAPON 5", "InCoverROF", 1.0f);

            Addon6Fire = settings.GetFloat("ADDONWEAPON 6", "NormalROF", 1.0f);
            Addon6FireDb = settings.GetFloat("ADDONWEAPON 6", "DrivebyROF", 1.0f);
            Addon6FireCover = settings.GetFloat("ADDONWEAPON 6", "InCoverROF", 1.0f);

            Addon7Fire = settings.GetFloat("ADDONWEAPON 7", "NormalROF", 1.0f);
            Addon7FireDb = settings.GetFloat("ADDONWEAPON 7", "DrivebyROF", 1.0f);
            Addon7FireCover = settings.GetFloat("ADDONWEAPON 7", "InCoverROF", 1.0f);

            Addon8Fire = settings.GetFloat("ADDONWEAPON 8", "NormalROF", 1.0f);
            Addon8FireDb = settings.GetFloat("ADDONWEAPON 8", "DrivebyROF", 1.0f);
            Addon8FireCover = settings.GetFloat("ADDONWEAPON 8", "InCoverROF", 1.0f);

            Addon9Fire = settings.GetFloat("ADDONWEAPON 9", "NormalROF", 1.0f);
            Addon9FireDb = settings.GetFloat("ADDONWEAPON 9", "DrivebyROF", 1.0f);
            Addon9FireCover = settings.GetFloat("ADDONWEAPON 9", "InCoverROF", 1.0f);

            Addon10Fire = settings.GetFloat("ADDONWEAPON 10", "NormalROF", 1.0f);
            Addon10FireDb = settings.GetFloat("ADDONWEAPON 10", "DrivebyROF", 1.0f);
            Addon10FireCover = settings.GetFloat("ADDONWEAPON 10", "InCoverROF", 1.0f);

            Addon11Fire = settings.GetFloat("ADDONWEAPON 11", "NormalROF", 1.0f);
            Addon11FireDb = settings.GetFloat("ADDONWEAPON 11", "DrivebyROF", 1.0f);
            Addon11FireCover = settings.GetFloat("ADDONWEAPON 11", "InCoverROF", 1.0f);

            Addon12Fire = settings.GetFloat("ADDONWEAPON 12", "NormalROF", 1.0f);
            Addon12FireDb = settings.GetFloat("ADDONWEAPON 12", "DrivebyROF", 1.0f);
            Addon12FireCover = settings.GetFloat("ADDONWEAPON 12", "InCoverROF", 1.0f);

            Addon13Fire = settings.GetFloat("ADDONWEAPON 13", "NormalROF", 1.0f);
            Addon13FireDb = settings.GetFloat("ADDONWEAPON 13", "DrivebyROF", 1.0f);
            Addon13FireCover = settings.GetFloat("ADDONWEAPON 13", "InCoverROF", 1.0f);

            Addon14Fire = settings.GetFloat("ADDONWEAPON 14", "NormalROF", 1.0f);
            Addon14FireDb = settings.GetFloat("ADDONWEAPON 14", "DrivebyROF", 1.0f);
            Addon14FireCover = settings.GetFloat("ADDONWEAPON 14", "InCoverROF", 1.0f);

            Addon15Fire = settings.GetFloat("ADDONWEAPON 15", "NormalROF", 1.0f);
            Addon15FireDb = settings.GetFloat("ADDONWEAPON 15", "DrivebyROF", 1.0f);
            Addon15FireCover = settings.GetFloat("ADDONWEAPON 15", "InCoverROF", 1.0f);

            Addon16Fire = settings.GetFloat("ADDONWEAPON 16", "NormalROF", 1.0f);
            Addon16FireDb = settings.GetFloat("ADDONWEAPON 16", "DrivebyROF", 1.0f);
            Addon16FireCover = settings.GetFloat("ADDONWEAPON 16", "InCoverROF", 1.0f);

            Addon17Fire = settings.GetFloat("ADDONWEAPON 17", "NormalROF", 1.0f);
            Addon17FireDb = settings.GetFloat("ADDONWEAPON 17", "DrivebyROF", 1.0f);
            Addon17FireCover = settings.GetFloat("ADDONWEAPON 17", "InCoverROF", 1.0f);

            Addon18Fire = settings.GetFloat("ADDONWEAPON 18", "NormalROF", 1.0f);
            Addon18FireDb = settings.GetFloat("ADDONWEAPON 18", "DrivebyROF", 1.0f);
            Addon18FireCover = settings.GetFloat("ADDONWEAPON 18", "InCoverROF", 1.0f);

            Addon19Fire = settings.GetFloat("ADDONWEAPON 19", "NormalROF", 1.0f);
            Addon19FireDb = settings.GetFloat("ADDONWEAPON 19", "DrivebyROF", 1.0f);
            Addon19FireCover = settings.GetFloat("ADDONWEAPON 19", "InCoverROF", 1.0f);

            Addon20Fire = settings.GetFloat("ADDONWEAPON 20", "NormalROF", 1.0f);
            Addon20FireDb = settings.GetFloat("ADDONWEAPON 20", "DrivebyROF", 1.0f);
            Addon20FireCover = settings.GetFloat("ADDONWEAPON 20", "InCoverROF", 1.0f);

            Unused0Fire = settings.GetFloat("UNUSED0", "NormalROF", 1.0f);
            Unused0FireDb = settings.GetFloat("UNUSED0", "DrivebyROF", 1.0f);
            Unused0FireCover = settings.GetFloat("UNUSED0", "InCoverROF", 1.0f);

            Episodic4Fire = settings.GetFloat("EPISODIC 4", "NormalROF", 1.0f);
            Episodic4FireDb = settings.GetFloat("EPISODIC 4", "DrivebyROF", 1.0f);
            Episodic4FireCover = settings.GetFloat("EPISODIC 4", "InCoverROF", 1.0f);

            Episodic5Fire = settings.GetFloat("EPISODIC 5", "NormalROF", 1.0f);
            Episodic5FireDb = settings.GetFloat("EPISODIC 5", "DrivebyROF", 1.0f);
            Episodic5FireCover = settings.GetFloat("EPISODIC 5", "InCoverROF", 1.0f);

            Episodic8Fire = settings.GetFloat("EPISODIC 8", "NormalROF", 1.0f);
            Episodic8FireDb = settings.GetFloat("EPISODIC 8", "DrivebyROF", 1.0f);
            Episodic8FireCover = settings.GetFloat("EPISODIC 8", "InCoverROF", 1.0f);

            Episodic16Fire = settings.GetFloat("EPISODIC 16", "NormalROF", 1.0f);
            Episodic16FireDb = settings.GetFloat("EPISODIC 16", "DrivebyROF", 1.0f);
            Episodic16FireCover = settings.GetFloat("EPISODIC 16", "InCoverROF", 1.0f);

            Episodic17Fire = settings.GetFloat("EPISODIC 17", "NormalROF", 1.0f);
            Episodic17FireDb = settings.GetFloat("EPISODIC 17", "DrivebyROF", 1.0f);
            Episodic17FireCover = settings.GetFloat("EPISODIC 17", "InCoverROF", 1.0f);

            Episodic18Fire = settings.GetFloat("EPISODIC 18", "NormalROF", 1.0f);
            Episodic18FireDb = settings.GetFloat("EPISODIC 18", "DrivebyROF", 1.0f);
            Episodic18FireCover = settings.GetFloat("EPISODIC 18", "InCoverROF", 1.0f);

            Episodic19Fire = settings.GetFloat("EPISODIC 19", "NormalROF", 1.0f);
            Episodic19FireDb = settings.GetFloat("EPISODIC 19", "DrivebyROF", 1.0f);
            Episodic19FireCover = settings.GetFloat("EPISODIC 19", "InCoverROF", 1.0f);

            Episodic20Fire = settings.GetFloat("EPISODIC 20", "NormalROF", 1.0f);
            Episodic20FireDb = settings.GetFloat("EPISODIC 20", "DrivebyROF", 1.0f);
            Episodic20FireCover = settings.GetFloat("EPISODIC 20", "InCoverROF", 1.0f);

            Episodic21Fire = settings.GetFloat("EPISODIC 21", "NormalROF", 1.0f);
            Episodic21FireDb = settings.GetFloat("EPISODIC 21", "DrivebyROF", 1.0f);
            Episodic21FireCover = settings.GetFloat("EPISODIC 21", "InCoverROF", 1.0f);

            CameraFire = settings.GetFloat("CAMERA", "NormalROF", 1.0f);
            CameraFireDb = settings.GetFloat("CAMERA", "DrivebyROF", 1.0f);
            CameraFireCover = settings.GetFloat("CAMERA", "InCoverROF", 1.0f);

            ObjectFire = settings.GetFloat("OBJECT", "NormalROF", 1.0f);
            ObjectFireDb = settings.GetFloat("OBJECT", "DrivebyROF", 1.0f);
            ObjectFireCover = settings.GetFloat("OBJECT", "InCoverROF", 1.0f);

            BatFire = settings.GetFloat("BASEBALL BAT", "NormalROF", 1.0f);
            BatFireDb = settings.GetFloat("BASEBALL BAT", "DrivebyROF", 1.0f);
            BatFireCover = settings.GetFloat("BASEBALL BAT", "InCoverROF", 1.0f);

            PoolcueFire = settings.GetFloat("POOLCUE", "NormalROF", 1.0f);
            PoolcueFireDb = settings.GetFloat("POOLCUE", "DrivebyROF", 1.0f);
            PoolcueFireCover = settings.GetFloat("POOLCUE", "InCoverROF", 1.0f);

            KnifeFire = settings.GetFloat("KNIFE", "NormalROF", 1.0f);
            KnifeFireDb = settings.GetFloat("KNIFE", "DrivebyROF", 1.0f);
            KnifeFireCover = settings.GetFloat("KNIFE", "InCoverROF", 1.0f);

            GrenadeFire = settings.GetFloat("GRENADE", "NormalROF", 1.0f);
            GrenadeFireDb = settings.GetFloat("GRENADE", "DrivebyROF", 1.0f);
            GrenadeFireCover = settings.GetFloat("GRENADE", "InCoverROF", 1.0f);

            MolotovFire = settings.GetFloat("MOLOTOV", "NormalROF", 1.0f);
            MolotovFireDb = settings.GetFloat("MOLOTOV", "DrivebyROF", 1.0f);
            MolotovFireCover = settings.GetFloat("MOLOTOV", "InCoverROF", 1.0f);

            RocketFire = settings.GetFloat("ROCKET", "NormalROF", 1.0f);
            RocketFireDb = settings.GetFloat("ROCKET", "DrivebyROF", 1.0f);
            RocketFireCover = settings.GetFloat("ROCKET", "InCoverROF", 1.0f);
        }
    }
}