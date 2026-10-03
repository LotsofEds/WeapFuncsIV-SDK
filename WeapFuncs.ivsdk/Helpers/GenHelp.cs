using IVSDKDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WeapFuncs.ivsdk.Helpers
{
    internal class GenHelp
    {
        public static void WriteBooleanToINI(SettingsFile settings, string name, bool booleShit)
        {
            if (!settings.DoesSectionExists(IVGenericGameStorage.ValidSaveName))
                settings.AddSection(IVGenericGameStorage.ValidSaveName);
            if (!settings.DoesKeyExists(IVGenericGameStorage.ValidSaveName, name))
                settings.AddKeyToSection(IVGenericGameStorage.ValidSaveName, name);
            settings.SetBoolean(IVGenericGameStorage.ValidSaveName, name, booleShit);
        }
        public static void WriteIntToINI(SettingsFile settings, string name, int integerShit)
        {
            if (!settings.DoesSectionExists(IVGenericGameStorage.ValidSaveName))
                settings.AddSection(IVGenericGameStorage.ValidSaveName);
            if (!settings.DoesKeyExists(IVGenericGameStorage.ValidSaveName, name))
                settings.AddKeyToSection(IVGenericGameStorage.ValidSaveName, name);
            settings.SetInteger(IVGenericGameStorage.ValidSaveName, name, integerShit);
        }
        public static Vector3 QuaternionToRotation(float X, float Y, float Z, float W)
        {
            double num = W;
            double num2 = X;
            double num3 = Y;
            double num4 = Z;
            double y = ((double)Z * (double)Y + (double)X * (double)W) * 2.0;
            double num5 = num;
            double num6 = num5 * num5;
            double num7 = num2;
            double num8 = num6 - num7 * num7;
            double num9 = num3;
            double num10 = num8 - num9 * num9;
            double num11 = num4;
            float radians = (float)Math.Atan2(y, num10 + num11 * num11);
            num2 = X;
            num = W;
            num3 = Y;
            num4 = Z;
            double y2 = ((double)Y * (double)X + (double)Z * (double)W) * 2.0;
            double num12 = num2;
            double num13 = num12 * num12;
            double num14 = num;
            double num15 = num13 + num14 * num14;
            double num16 = num3;
            double num17 = num15 - num16 * num16;
            double num18 = num4;
            float radians2 = (float)Math.Atan2(y2, num17 - num18 * num18);
            float radians3 = (float)Math.Asin(((double)Z * (double)X - (double)Y * (double)W) * -2.0);
            return new Vector3(Helper.RadianToDegree(radians), Helper.RadianToDegree(radians3), Helper.RadianToDegree(radians2));
        }
        public static Vector3 DirectionToRotation(Vector3 dir, float roll)
        {
            dir = Vector3.Normalize(dir);
            Vector3 result = default(Vector3);
            result.Z = (float)(Math.Atan2(dir.X, dir.Y) * (-180.0 / Math.PI));
            Vector3 vector = new Vector3(dir.X, dir.Y, 0f);
            Vector3 vector2 = new Vector3(dir.Z, vector.Length(), 0f);
            Vector3 vector3 = Vector3.Normalize(vector2);
            result.X = (float)(Math.Atan2(vector3.X, vector3.Y) * (180.0 / Math.PI));
            result.Y = roll;
            return result;
        }
        // Credits to catsmackaroo for these helpers, couldn't be assed to make my own from scratch.
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        public static float SmoothStep(float start, float end, float amount)
        {
            amount = Clamp(amount, 0, 1);
            amount = amount * amount * (3 - 2 * amount);
            return start + (end - start) * amount;
        }
    }
}
