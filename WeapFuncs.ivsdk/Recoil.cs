using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using System;
using static IVSDKDotNet.Native.Natives;

// Credits: catsmackaroo

namespace WeapFuncs.ivsdk
{
    internal class Recoil
    {
        private static bool enableIncrease;
        private static bool recoilDebug;

        private static float RecoilAmplitudeMin;
        private static float RecoilAmplitudeMax;
        public static int RecoilTime;

        private static float AdditionalRecoil;
        private static float CurrentRecoil;
        private static float DecayRate;
        private static float MaximumRecoil;
        private static float CrouchMultiplier;

        private static NativeCamera cam;
        private static float appliedRecoil;
        public static void Init(SettingsFile settings)
        {
            enableIncrease = settings.GetBoolean("RECOIL & BULLETSPREAD", "IncreasingRecoil", false);
            recoilDebug = settings.GetBoolean("RECOIL & BULLETSPREAD", "RecoilDebug", false);
        }

        private static void LoadRecoilConf(int weapon)
        {
            int weapIndex = Main.weaponData.FindIndex(w => w.ID == weapon);
            if (weapIndex >= 0)
            {
                RecoilAmplitudeMin = Main.weaponData[weapIndex].RecoilAmpMin;
                RecoilAmplitudeMax = Main.weaponData[weapIndex].RecoilAmpMax;
                RecoilTime = Main.weaponData[weapIndex].RecoilTime;

                AdditionalRecoil = Main.weaponData[weapIndex].RecoilAdd;
                DecayRate = Main.weaponData[weapIndex].RecoilDecay;
                MaximumRecoil = Main.weaponData[weapIndex].MaxRecoil;
                CrouchMultiplier = Main.weaponData[weapIndex].RecoilCrouch;
            }
        }
        public static void Tick()
        {
            cam = NativeCamera.GetGameCam();
            appliedRecoil = CurrentRecoil;

            if (IS_CHAR_DUCKING(Main.PlayerHandle))
                appliedRecoil *= CrouchMultiplier;

            if (IS_CHAR_SHOOTING(Main.PlayerHandle))
            {
                if (enableIncrease)
                    CurrentRecoil = Math.Min(CurrentRecoil + AdditionalRecoil, MaximumRecoil);

                ApplyRecoil(cam, Main.currWeap, appliedRecoil);
            }

            if (enableIncrease)
                CurrentRecoil = Math.Max(CurrentRecoil - DecayRate * Main.frameTime, 0);

            if (recoilDebug)
                IVGame.ShowSubtitleMessage(Math.Truncate(AdditionalRecoil * Main.frameTime * 100).ToString() + "  " + Math.Truncate(DecayRate * Main.frameTime * 100).ToString() + "  " + Math.Truncate(CurrentRecoil * 1000).ToString() + "  " + (RecoilTime * 100) / (int)(Main.frameTime * 6000));
        }

        private static void ApplyRecoil(NativeCamera cam, int weapon, float appliedRecoil)
        {
            LoadRecoilConf(weapon);
            ApplyCameraShake(cam, RecoilAmplitudeMin, RecoilAmplitudeMax, appliedRecoil, RecoilTime);
        }

        private static void ApplyCameraShake(NativeCamera cam, float amplitude1, float amplitude2, float appliedRecoil, int duration)
        {
            cam.Shake(CameraShakeType.PITCH_UP_DOWN, CameraShakeBehaviour.CONSTANT_PLUS_FADE_IN_OUT, duration,
                GENERATE_RANDOM_FLOAT_IN_RANGE(amplitude1, amplitude2) + appliedRecoil, 0.2f, 0f);

            float randomLeftRightAmplitude = GENERATE_RANDOM_FLOAT_IN_RANGE(-amplitude1, amplitude1);
            float randomIncreasingleftRightRecoil = GENERATE_RANDOM_FLOAT_IN_RANGE(-appliedRecoil, appliedRecoil);

            cam.Shake(CameraShakeType.ROLL_LEFT_RIGHT, CameraShakeBehaviour.CONSTANT_PLUS_FADE_IN_OUT, duration,
                randomLeftRightAmplitude + randomIncreasingleftRightRecoil, 0.2f, 0f);
        }
    }
}
