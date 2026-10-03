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
    internal class ObjectHelper
    {
        public static Dictionary<UIntPtr, int> ObjHandles { get; private set; } = new Dictionary<UIntPtr, int>();

        public static void GrabAllObjs()
        {
            ObjHandles.Clear();

            IVPool objPool = IVPools.GetObjectPool();
            for (int i = 0; i < objPool.Count; i++)
            {
                UIntPtr ptr = objPool.Get(i);

                if (ptr != UIntPtr.Zero)
                {
                    int objHandle = (int)objPool.GetIndex(ptr);
                    ObjHandles[ptr] = objHandle;
                }
            }
        }
    }
}
