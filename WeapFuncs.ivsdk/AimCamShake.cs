using CCL.GTAIV;
using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using System;
using WeapFuncs.ivsdk.Helpers;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class AimCamShake
    {
        //IniShit
        private static bool healthDebuff;

        private static float shakeAmplitudeNormalMin;
        private static float shakeAmplitudeNormalMax;
        private static float shakeAmplitudeLowMin;
        private static float shakeAmplitudeLowMax;

        private static float crouchMultiplier;

        // OtherShit
        private static NativeCamera cam;
        private static uint fTimer;

        private static float shakeAmplitudeMin;
        private static float shakeAmplitudeMax;

        public static void Init(SettingsFile settings)
        {
            healthDebuff = settings.GetBoolean("AIM CAMERA SHAKE", "IncreaseWhenLowHealth", false);

            shakeAmplitudeNormalMin = settings.GetFloat("AIM CAMERA SHAKE", "ShakeMin", 0.02f);
            shakeAmplitudeNormalMax = settings.GetFloat("AIM CAMERA SHAKE", "ShakeMax", 0.03f);
            shakeAmplitudeLowMin = settings.GetFloat("AIM CAMERA SHAKE", "LowHealthShakeMin", 0.1f);
            shakeAmplitudeLowMax = settings.GetFloat("AIM CAMERA SHAKE", "LowHealthShakeMax", 0.12f);

            crouchMultiplier = settings.GetFloat("AIM CAMERA SHAKE", "CrouchMultiplier", 0.5f);
        }

        public static void Tick()
        {
            cam = NativeCamera.GetGameCam();
            if (!IS_CAM_SHAKING() && (WeaponHelper.IsAimingAnimPlaying() || (WeaponHelper.IsReloadAnimPlaying() && WeaponHelper.IsPressingAimButton())))
            {
                if (!IS_CHAR_SHOOTING(Main.PlayerHandle) && (WeaponHelper.IsReloadAnimPlaying() || (!NativeControls.IsGameKeyPressed(0, GameKey.Attack) && !NativeControls.IsGameKeyPressed(2, GameKey.Attack))))
                {
                    if (Main.gTimer >= fTimer + 100)
                    {
                        GET_GAME_TIMER(out fTimer);
                        GET_PLAYER_MAX_HEALTH((int)Main.PlayerIndex, out int maxHealth);
                        GET_CHAR_HEALTH(Main.PlayerHandle, out uint pHealth);
                        if (!healthDebuff)
                            pHealth = (uint)maxHealth;

                        shakeAmplitudeMin = (((maxHealth - (float)pHealth) / 100) * (shakeAmplitudeLowMin - shakeAmplitudeNormalMin)) + shakeAmplitudeNormalMin;
                        shakeAmplitudeMax = (((maxHealth - (float)pHealth) / 100) * (shakeAmplitudeLowMax - shakeAmplitudeNormalMax)) + shakeAmplitudeNormalMax;

                        if (IS_CHAR_DUCKING(Main.PlayerHandle))
                        {
                            shakeAmplitudeMin *= crouchMultiplier;
                            shakeAmplitudeMax *= crouchMultiplier;
                        }
                        ApplyCameraShake(cam, shakeAmplitudeMin, shakeAmplitudeMax, 0, (100 * 100) / (int)(Main.frameTime * 6000));
                    }
                }
                else if (IS_CHAR_SHOOTING(Main.PlayerHandle))
                {
                    GET_GAME_TIMER(out fTimer);

                    fTimer += (uint)Recoil.RecoilTime;
                }
            }
        }
        private static void ApplyCameraShake(NativeCamera cam, float amplitude1, float amplitude2, float appliedRecoil, int duration)
        {
            cam.Shake(CameraShakeType.PITCH_UP_DOWN, CameraShakeBehaviour.CONSTANT_PLUS_FADE_IN_OUT, duration,
                GENERATE_RANDOM_FLOAT_IN_RANGE(amplitude1, amplitude2) + appliedRecoil, 0.1f, 0f);

            float randomLeftRightAmplitude = GENERATE_RANDOM_FLOAT_IN_RANGE(-amplitude1, amplitude1);
            float randomIncreasingleftRightRecoil = GENERATE_RANDOM_FLOAT_IN_RANGE(-appliedRecoil, appliedRecoil);

            cam.Shake(CameraShakeType.YAW_LEFT_RIGHT, CameraShakeBehaviour.CONSTANT_PLUS_FADE_IN_OUT, duration,
                randomLeftRightAmplitude + randomIncreasingleftRightRecoil, 0.1f, 0f);
        }
    }
}
