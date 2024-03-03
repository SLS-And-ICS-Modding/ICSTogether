using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSTogether.HarmonyPatches.Workers
{
    [HarmonyPatch(typeof(WorkersPanel))]
    [HarmonyPatch("BUTTON_CHEFBUY")]
    public static class BUTTON_CHEFBUY
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BUTTON_CHEFBUY");
            Main.sw.Flush();
        }
    }
    [HarmonyPatch(typeof(WorkersPanel))]
    [HarmonyPatch("BUTTON_BODYGUARDBUY")]
    public static class BUTTON_BODYGUARDBUY
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Main.sw.WriteLine("BUTTON_BODYGUARDBUY");
            Main.sw.Flush();
        }
    }
}
