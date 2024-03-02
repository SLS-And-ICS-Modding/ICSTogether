using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ICSTogether
{
    
    public class Class1:MelonMod
    {
        static bool IsHost = false;
        static bool IsClient = false;
        static bool IsInMenu => GameObject.FindObjectsOfType<PlayerRaycast>().Length > 0;
        public static float oldmoney = 0;
        public static NetworkStream ns;
        public static TcpClient tcpClient;
        public static StreamWriter sw;
        public static StreamReader sr;
        public static TcpListener listener;
        public static bool IsDisconnected = false;
        public static string receivedpackets = "";
        public override void OnUpdate()
        {
            if(IsClient && tcpClient.Connected && ns.DataAvailable)
            {
                string receivedData = sr.ReadLine();
                MelonLogger.Msg(receivedData);
                if (!string.IsNullOrEmpty(receivedData))
                {
                    switch (receivedData)
                    {
                        case "BuyFIRSTLOCK":
                            {
                                receivedpackets += "\nBuyFIRSTLOCK";
                                break;
                            }
                        case "BuySECONDLOCK":
                            {
                                receivedpackets += "\nBuySECONDLOCK";
                                break;
                            }
                        case "BuyTHIRDLOCK":
                            {
                                receivedpackets += "\nBuyTHIRDLOCK";
                                break;
                            }
                        case "BuyFOURTHLOCK":
                            {
                                receivedpackets += "\nBuyFOURTHLOCK";
                                break;
                            }
                        default:
                            {
                                receivedpackets += "\n" + receivedData;
                                break;
                            }
                    }
                }
            }
        }
        public override void OnGUI()
        {
            GUILayout.Label("ICSTogether - v0.1 pre-alpha");
            if(!IsHost && !IsClient && !IsInMenu)
            {
                if(GUILayout.Button("Join"))
                {
                    tcpClient = new TcpClient("192.168.0.19",4200);
                    ns = tcpClient.GetStream();
                    IsClient = true;
                    sw = new StreamWriter(ns);
                    sr = new StreamReader(ns);
                    
                }
                if(GUILayout.Button("Create"))
                {
                    IsHost = true;
                    listener = new TcpListener(4200);
                    listener.Start();
                    listener.BeginAcceptTcpClient(new AsyncCallback(TcpDaemon), null);
                }
            }
            GUILayout.Label($"{receivedpackets} packets");
            if(IsHost && !IsDisconnected)
            {
                if(tcpClient.Connected)
                {
                    GUILayout.Label("client is connected");
                } else
                {
                    GUILayout.Label("client is disconnected");
                }
            }
        }
        private void TcpDaemon(IAsyncResult result)
        {
            tcpClient = listener.EndAcceptTcpClient(result);
            ns = tcpClient.GetStream();
            sw = new StreamWriter(ns);
            sr = new StreamReader(ns);
            new Thread(() => {
                while(true)
                {
                    if (tcpClient.Connected)
                    {
                        IsDisconnected = false;
                        string receivedData = sr.ReadLine();
                        MelonLogger.Msg(receivedData);
                        if (!string.IsNullOrEmpty(receivedData))
                        {
                            switch (receivedData)
                            {
                                case "BuyFIRSTLOCK":
                                    {
                                        receivedpackets += "\nBuyFIRSTLOCK";
                                        break;
                                    }
                                case "BuySECONDLOCK":
                                    {
                                        receivedpackets += "\nBuySECONDLOCK";
                                        break;
                                    }
                                case "BuyTHIRDLOCK":
                                    {
                                        receivedpackets += "\nBuyTHIRDLOCK";
                                        break;
                                    }
                                case "BuyFOURTHLOCK":
                                    {
                                        receivedpackets += "\nBuyFOURTHLOCK";
                                        break;
                                    }
                                default:
                                    {
                                        receivedpackets += "\n" + receivedData;
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        IsDisconnected = true;
                    }
                }
                    }).Start();
        }
        [HarmonyPatch(typeof(icstore))]
        [HarmonyPatch("BuyFIRSTLOCK")]
        public static class FIRSTLOCKPATCH
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BuyFIRSTLOCK");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(icstore))]
        [HarmonyPatch("BuySECONDLOCK")]
        public static class BuySECONDLOCK
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BuySECONDLOCK");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(icstore))]
        [HarmonyPatch("BuyTHIRDLOCK")]
        public static class BuyTHIRDLOCK
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BuyTHIRDLOCK");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(icstore))]
        [HarmonyPatch("BuyFOURTHLOCK")]
        public static class BuyFOURTHLOCK
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BuyFOURTHLOCK");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(icstore))]
        [HarmonyPatch("BuyFIVETHLOCK")]
        public static class BuyFIVETHLOCK
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BuyFIVETHLOCK");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(WorkersPanel))]
        [HarmonyPatch("BUTTON_CHEFBUY")]
        public static class BUTTON_CHEFBUY
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BUTTON_CHEFBUY");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(WorkersPanel))]
        [HarmonyPatch("BUTTON_BODYGUARDBUY")]
        public static class BUTTON_BODYGUARDBUY
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                sw.WriteLine("BUTTON_BODYGUARDBUY");
                sw.Flush();
            }
        }
        [HarmonyPatch(typeof(BuyButton))]
        [HarmonyPatch("Update")]
        public static class Update
        {
            [HarmonyPrefix]
            public static void Prefix(BuyButton __instance)
            {
                try
                {
                    if (sw != null && tcpClient != null && tcpClient.Connected)
                    {
                        sw.WriteLine($"Buy:{__instance.productName}:{__instance.itemID}:{__instance.starValue}");
                        sw.Flush();
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error($"Exception during data write: {ex.Message}");
                }
            }
        }
    }
}
