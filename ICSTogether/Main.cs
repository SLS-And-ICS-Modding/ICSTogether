using HarmonyLib;
using ICSTogether.Helpers;
using Lean.Pool;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static MelonLoader.MelonLogger;

namespace ICSTogether
{
    
    public class Main:MelonMod
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
        public static int receivedpackets = 0;
        SaveManager gb = GameObject.FindObjectOfType<SaveManager>();

        GameObject[] dg = GameObject.FindObjectsOfType<GameObject>();
        Vector3 lock1Position = new Vector3(49.5f, -1.9f, 126.6f);
        Vector3 lockkitchenPosition = new Vector3(48.1f, -1.9f, 118.7f);
        Vector3 lock2Position = new Vector3(51.6f, -1.9f, 133.7f);
        Vector3 lock3Position = new Vector3(47.6f, -1.9f, 137.3f);
        Vector3 lock4Position = new Vector3(45.0f, 0.9f, 140.9f);
        Vector3 lock5Position = new Vector3(45.7f, 0.9f, 152.8f);
        Vector3 lock6Position = new Vector3(52.4f, 0.9f, 150.7f);
        private void HandlePacket(string packet)
        {
            receivedpackets++;
            switch (packet)
            {
                case "BuyKitchenLock":
                    {
                        foreach(var loc in dg)
                        {
                            if(loc.transform.position == lockkitchenPosition)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buykitchen", 50);
                        gb._icstore.LoadicStore();
                        break;
                    }
                case "BuyFIRSTLOCK":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lock1Position)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buyfirstlock", 50);
                        gb._icstore.LoadicStore();
                        break;
                    }
                case "BuySECONDLOCK":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lock2Position)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buysecondlock", 50);
                        gb._icstore.LoadicStore();
                        break;
                    }
                case "BuyTHIRDLOCK":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lock3Position)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buythirdlock", 50);
                        gb._icstore.LoadicStore();
                        break;
                    }
                case "BuyFOURTHLOCK":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lock4Position)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buyfourthlock", 50);
                        gb._icstore.LoadicStore();
                        break;
                    }
                case "BuyFIVETHLOCK":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lock5Position)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buyfivethlock", 50);
                        gb._icstore.LoadicStore();
                        break;
                    }
                case "BuySIXTHLOCK":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lock6Position)
                            {
                                GameObject.Destroy(loc);
                            }
                        }
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.35f, 0f);
                        PlayerPrefs.SetInt("buysixthlock", 50);
                        gb._icstore.LoadicStore();

                        break;
                    }
                default:
                    {
                        if (packet.StartsWith("Buy:"))
                        {
                            string[] data = packet.Split(new char[] { ':' });
                            Vector3 pos = ItemSpawnerManager.Instance.orderSpawnPosition1.position;
                            MelonLogger.Msg(pos.ToString());
                            GameObject shit = ParseItemId.Spaghetti(data[2]);
                            GameObject.Instantiate(shit, pos, Quaternion.identity);
                        }
                        
                        break;
                    }
            }
        }
        public override void OnUpdate()
        {
            if(IsClient && tcpClient.Connected && ns.DataAvailable)
            {
                string receivedData = sr.ReadLine();
                
                if (!string.IsNullOrEmpty(receivedData))
                {
                    HandlePacket(receivedData);
                }
            }
        }
        static string ip = "127.0.0.1";
        public override void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.yellow;
            style.fontStyle = FontStyle.Bold;
            GUILayout.Label("ICSTogether - v0.2 pre-alpha",style);
            if(!IsHost && !IsClient && !IsInMenu)
            {
                ip = GUILayout.TextField(ip);
                if(GUILayout.Button("Join", style))
                {
                    tcpClient = new TcpClient(ip,4200);
                    ns = tcpClient.GetStream();
                    IsClient = true;
                    sw = new StreamWriter(ns);
                    sr = new StreamReader(ns);
                    
                }
                if(GUILayout.Button("Create", style))
                {
                    IsHost = true;
                    listener = new TcpListener(4200);
                    listener.Start();
                    listener.BeginAcceptTcpClient(new AsyncCallback(TcpDaemon), null);
                }
            }
            GUILayout.Label($"received {receivedpackets} in total", style);
            if(IsHost && !IsDisconnected)
            {
                if(tcpClient.Connected)
                {
                    GUILayout.Label("2nd client is connected", style);
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
                        if (!string.IsNullOrEmpty(receivedData))
                        {
                            HandlePacket(receivedData);
                        }
                    }
                    else
                    {
                        IsDisconnected = true;
                    }
                }
            }).Start();
        }
        
        
        
    }
}
