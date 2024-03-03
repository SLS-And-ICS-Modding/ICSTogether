using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ICSTogether;
namespace ICSTogether.HarmonyPatches.Misc
{
    [HarmonyPatch(typeof(BuyButton))]
    [HarmonyPatch("Buy")]
    public static class Buy
    {
        [HarmonyPrefix]
        public static void Prefix(BuyButton __instance)
        {
            try
            {
                if (Main.sw != null && Main.tcpClient != null && Main.tcpClient.Connected)
                {
                    Main.sw.WriteLine($"Buy:{__instance.productName}:{__instance.itemID}");
                    Main.sw.Flush();
                   
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Exception during data write: {ex.Message}");
            }
        }
    }
}
