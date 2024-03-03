using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSTogether.HarmonyPatches.Rooms
{
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuyKitchenLock")]
    public static class BuyKitchenLock
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuyKitchenLock");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuyFIRSTLOCK")]
    public static class FIRSTLOCKPATCH
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuyFIRSTLOCK");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuySECONDLOCK")]
    public static class BuySECONDLOCK
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuySECONDLOCK");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuyTHIRDLOCK")]
    public static class BuyTHIRDLOCK
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuyTHIRDLOCK");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuyFOURTHLOCK")]
    public static class BuyFOURTHLOCK
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuyFOURTHLOCK");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuyFIVETHLOCK")]
    public static class BuyFIVETHLOCK
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuyFIVETHLOCK");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(icstore))]
    [HarmonyPatch("BuySIXTHLOCK")]
    public static class BuySIXTHLOCK
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BuySIXTHLOCK");
            Main.sw.Flush();
        }
    }
}
