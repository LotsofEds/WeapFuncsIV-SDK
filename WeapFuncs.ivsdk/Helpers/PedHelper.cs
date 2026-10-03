using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using IVSDKDotNet;
using CCL;

namespace WeapFuncs.ivsdk
{
    internal class PedHelper
    {
        public static Dictionary<UIntPtr, int> PedHandles { get; private set; } = new Dictionary<UIntPtr, int>();

        public static void GrabAllPeds()
        {
            PedHandles.Clear();

            IVPool pedPool = IVPools.GetPedPool();
            for (int i = 0; i < pedPool.Count; i++)
            {
                UIntPtr ptr = pedPool.Get(i);

                if (ptr != UIntPtr.Zero)
                {
                    int pedHandle = (int)pedPool.GetIndex(ptr);
                    PedHandles[ptr] = pedHandle;
                }
            }
        }
    }
}
