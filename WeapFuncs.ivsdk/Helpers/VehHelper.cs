using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using CCL;
using IVSDKDotNet.Enums;
using static IVSDKDotNet.Native.Natives;

namespace WeapFuncs.ivsdk
{
    internal class VehHelper
    {
        public static Dictionary<UIntPtr, int> VehHandles { get; private set; } = new Dictionary<UIntPtr, int>();

        public static void GrabAllVehs()
        {
            VehHandles.Clear();

            IVPool vehPool = IVPools.GetVehiclePool();
            for (int i = 0; i < vehPool.Count; i++)
            {
                UIntPtr ptr = vehPool.Get(i);

                if (ptr != UIntPtr.Zero)
                {
                    int vehHandle = (int)vehPool.GetIndex(ptr);
                    VehHandles[ptr] = vehHandle;
                }
            }
        }
    }
}
