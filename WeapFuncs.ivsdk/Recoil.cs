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
        private static bool enable;
        private static bool enableIncrease;
        private static bool recoilDebug;

        private static float RecoilAmplitudeMin;
        private static float RecoilAmplitudeMax;
        private static float RecoilFrequencyMin;
        private static float RecoilFrequencyMax;
        private static int RecoilTime;

        private static float BaseRecoil;
        private static float AdditionalRecoil;
        private static float CurrentRecoil;
        private static float DecayRate;
        private static float MaximumRecoil;
        private static float CrouchMultiplier;

        private static NativeCamera cam;
        private static float appliedRecoil;
        public static void Init(SettingsFile settings)
        {
            enable = settings.GetBoolean("RECOIL & BULLETSPREAD", "WeaponRecoil", false);
            enableIncrease = settings.GetBoolean("RECOIL & BULLETSPREAD", "IncreasingRecoil", false);
            recoilDebug = settings.GetBoolean("RECOIL & BULLETSPREAD", "RecoilDebug", false);
        }

        private static void LoadRecoilConf(int weapon)
        {
            if (Main.wConfFile.DoesSectionExists(weapon.ToString()))
            {
                RecoilAmplitudeMin = Main.wConfFile.GetFloat(weapon.ToString(), "RecoilAmpMin", 0.0f);
                RecoilAmplitudeMax = Main.wConfFile.GetFloat(weapon.ToString(), "RecoilAmpMax", 0.0f);
                RecoilFrequencyMin = Main.wConfFile.GetFloat(weapon.ToString(), "RecoilFreqMin", 0.0f);
                RecoilFrequencyMax = Main.wConfFile.GetFloat(weapon.ToString(), "RecoilFreqMax", 0.0f);
                RecoilTime = Main.wConfFile.GetInteger(weapon.ToString(), "RecoilTime", 0);

                BaseRecoil = Main.wConfFile.GetFloat(weapon.ToString(), "BaseRecoil", 0.0f);
                AdditionalRecoil = Main.wConfFile.GetFloat(weapon.ToString(), "AdditionalRecoil", 0.0f);
                DecayRate = Main.wConfFile.GetFloat(weapon.ToString(), "DecayRate", 0.0f);
                MaximumRecoil = Main.wConfFile.GetFloat(weapon.ToString(), "MaximumRecoil", 0.0f);
                CrouchMultiplier = Main.wConfFile.GetFloat(weapon.ToString(), "CrouchMultiplier", 0.0f);
            }
        }
        public static void Tick()
        {
            if (!enable)
                return;

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
                CurrentRecoil = Math.Max(CurrentRecoil - DecayRate * Main.frameTime, BaseRecoil);

            if (recoilDebug)
                IVGame.ShowSubtitleMessage(Math.Truncate(AdditionalRecoil * Main.frameTime * 100).ToString() + "  " + Math.Truncate(DecayRate * Main.frameTime * 100).ToString() + "  " + Math.Truncate(CurrentRecoil * 1000).ToString() + "  " + (RecoilTime * 100) / (int)(Main.frameTime * 6000));
        }

        private static void ApplyRecoil(NativeCamera cam, int weapon, float appliedRecoil)
        {
            LoadRecoilConf(weapon);
            ApplyCameraShake(cam, RecoilAmplitudeMin, RecoilAmplitudeMax, RecoilFrequencyMin, RecoilFrequencyMax, appliedRecoil, (RecoilTime * 100) / (int)(Main.frameTime * 6000));
        }

        private static void ApplyCameraShake(NativeCamera cam, float amplitude1, float amplitude2, float frequency1, float frequency2, float appliedRecoil, int duration)
        {
            cam.Shake(CameraShakeType.PITCH_UP_DOWN, CameraShakeBehaviour.CONSTANT_PLUS_FADE_IN_OUT, duration,
                GENERATE_RANDOM_FLOAT_IN_RANGE(amplitude1, amplitude2) + appliedRecoil,
                GENERATE_RANDOM_FLOAT_IN_RANGE(frequency1, frequency2), 0f);

            float randomLeftRightAmplitude = GENERATE_RANDOM_FLOAT_IN_RANGE(-amplitude1, amplitude1);
            float randomIncreasingleftRightRecoil = GENERATE_RANDOM_FLOAT_IN_RANGE(-appliedRecoil, appliedRecoil);

            cam.Shake(CameraShakeType.ROLL_LEFT_RIGHT, CameraShakeBehaviour.CONSTANT_PLUS_FADE_IN_OUT, duration,
                randomLeftRightAmplitude + randomIncreasingleftRightRecoil,
                GENERATE_RANDOM_FLOAT_IN_RANGE(frequency1, frequency2), 0f);
        }
    }
}
