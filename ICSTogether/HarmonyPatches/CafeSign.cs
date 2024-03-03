using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ICSTogether.HarmonyPatches.Door
{
    [HarmonyPatch(typeof(DoorSign))]
    [HarmonyPatch("OpenCafe")]
    public static class OpenCafe
    {
        internal static bool isOpenCafePacketSent = false;

        [HarmonyPostfix]
        public static void Postfix()
        {
            if (!isOpenCafePacketSent)
            {
                Main.sw.WriteLine("OpenCafe");
                Main.sw.Flush();
                isOpenCafePacketSent = true;
            }

            // Reset the flag for CloseCafe
            CloseCafe.isCloseCafePacketSent = false;

            return;
        }
    }

    [HarmonyPatch(typeof(DoorSign))]
    [HarmonyPatch("CloseCafe")]
    public static class CloseCafe
    {
        internal static bool isCloseCafePacketSent = false;

        [HarmonyPostfix]
        public static void Postfix()
        {
            if (!isCloseCafePacketSent)
            {
                Main.sw.WriteLine("CloseCafe");
                Main.sw.Flush();
                isCloseCafePacketSent = true;
            }

            OpenCafe.isOpenCafePacketSent = false;

            return;
        }
    }
}
