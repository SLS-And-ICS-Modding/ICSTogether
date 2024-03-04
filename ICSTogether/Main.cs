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
        public static float oldmoney = 0;
        public static NetworkStream ns;
        public static TcpClient tcpClient;
        public static StreamWriter sw;
        public static StreamReader sr;
        public static TcpListener listener;
        public static bool IsDisconnected = false;
        public static int receivedpackets = 0;
        public static string lastpacket = "";
        public static float player2money = 0;
        public static GameObject player;
        SaveManager gb = GameObject.FindObjectOfType<SaveManager>();

        GameObject[] dg = GameObject.FindObjectsOfType<GameObject>();
        Vector3 lock1Position = new Vector3(49.5f, -1.9f, 126.6f);
        Vector3 lockkitchenPosition = new Vector3(48.1f, -1.9f, 118.7f);
        Vector3 lock2Position = new Vector3(51.6f, -1.9f, 133.7f);
        Vector3 lock3Position = new Vector3(47.6f, -1.9f, 137.3f);
        Vector3 lock4Position = new Vector3(45.0f, 0.9f, 140.9f);
        Vector3 lock5Position = new Vector3(45.7f, 0.9f, 152.8f);
        Vector3 lock6Position = new Vector3(52.4f, 0.9f, 150.7f);
        static Vector3 oldpos = new Vector3(0,0,0);
        static Vector3 lastsentpos = new Vector3(0, 0, 0);
        static int calls = 0;
        private void HandlePacket(string packet)
        {
            receivedpackets++;
            lastpacket = packet;
            switch (packet)
            {
                case "BuyKitchenLock":
                    {
                        foreach (var loc in dg)
                        {
                            if (loc.transform.position == lockkitchenPosition)
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
                case "OpenCafe":
                    {
                        DoorSign ds = GameObject.FindObjectOfType<DoorSign>();
                        ds.OpenCafe();
                        break;
                    }
                case "CloseCafe":
                    {
                        DoorSign ds = GameObject.FindObjectOfType<DoorSign>();
                        ds.CloseCafe();
                        break;
                    }
                /*case "OpenDoor":
                    {
                        string name = sr.ReadLine();
                        
                        vars.readdata = true;
                        GameObject door = GameObject.FindGameObjectsWithTag("door").Where(x => x.name == name).First();
                        MelonLogger.Msg("opened door at" + door.transform.position.ToString());
                        if (door.GetComponent<Animator>().GetBool("open"))
                        {
                            door.GetComponent<Animator>().SetBool("open", false);
                            if (door.GetComponent<ItemName>().itemID == "wDoor")
                            {
                                AudioManager.Instance.PlaySound(AudioManager.Instance.w_doorClose, 0.35f, 0.55f);
                            }
                            else
                            {
                                AudioManager.Instance.PlaySound(AudioManager.Instance.doorClose, 0.15f, 0.75f);
                            }
                        }
                        else
                        {
                            door.GetComponent<Animator>().SetBool("open", true);
                            if (door.GetComponent<ItemName>().itemID == "wDoor")
                            {
                                AudioManager.Instance.PlaySound(AudioManager.Instance.w_doorOpen, 0.45f, 0f);
                            }
                            else
                            {
                                AudioManager.Instance.PlaySound(AudioManager.Instance.doorOpen, 0.2f, 0f);
                            }
                        }
                        vars.readdata = false;
                        break;
                    }*/
                case "UpdateMovement":
                    {
                        float x = float.Parse(sr.ReadLine());
                        float y = float.Parse(sr.ReadLine()) + 0.8f;
                        float z = float.Parse(sr.ReadLine());
                        if(calls >= 25)
                        {
                            GameObject.Destroy(player.gameObject);
                            player = null;
                            calls = 0;
                        }
                        if (player == null)
                        {
                            player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        }
                        lastsentpos = new Vector3(x, y, z);
                        player.transform.position = lastsentpos;
                        MelonLogger.Msg("player spawned at " + lastsentpos);
                        MelonLogger.Msg("current local pos " + player.transform.localPosition);
                        MelonLogger.Msg("current pos " + player.transform.position);
                        calls++;
                        break;
                    }
                case "BUTTON_CHEFBUY":
                    {
                        PlayerPrefs.SetInt("buychef", 50);
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.3f, 0f);
                        var wp = GameObject.FindObjectOfType<WorkersPanel>();
                        GameObject backup = wp.gameObject;
                        GameObject.Destroy(wp);
                        var gb = GameObject.Instantiate(backup);
                        break;
                    }
                case "BUTTON_BODYGUARDBUY":
                    {

                        PlayerPrefs.SetInt("buybodyguard", 50);
                        AudioManager.Instance.PlaySound(AudioManager.Instance.cashRegister, 0.3f, 0f);
                        var wp = GameObject.FindObjectOfType<WorkersPanel>();
                        GameObject backup = wp.gameObject;
                        GameObject.Destroy(wp);
                        var gb = GameObject.Instantiate(backup);

                        break;
                    }
                case "MONEY_UPDATE":
                    {
                        player2money = float.Parse(sr.ReadLine());
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
            if(GameObject.FindObjectOfType<PlayerRaycast>() != null)
            {
                Vector3 pos = GameObject.FindObjectOfType<PlayerRaycast>().transform.position;
                if (Vector3.Distance(oldpos, pos) >= 1f)
                {
                    sw.WriteLine("UpdateMovement");
                    sw.WriteLine(pos.x);
                    sw.WriteLine(pos.y);
                    sw.WriteLine(pos.z);
                    sw.Flush();
                    oldpos = pos;

                }
            }
            if(Input.GetKeyDown(KeyCode.Mouse5))
            {
                player = GameObject.FindObjectsOfType<GameObject>().Where(m => m.name.Contains("Capsule")).First(); // refresh pointer i guess???
                player.transform.position = new Vector3(0,0,0);
                MelonLogger.Msg("repainted player at"+player.transform.position);
            }
            if(oldmoney != PlayerPrefs.GetFloat("money"))
            {
                sw.WriteLine("MONEY_UPDATE");
                sw.WriteLine(PlayerPrefs.GetFloat("money"));
                sw.Flush();
                oldmoney = PlayerPrefs.GetFloat("money");
            }
            if (IsClient && tcpClient.Connected && ns.DataAvailable)
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
            style.normal.textColor = Color.red;
            style.fontStyle = FontStyle.Bold;
            style.normal.background = ICSTogether.Helpers.Texture.MakeTex(2, 2, Color.black);
            GUILayout.Label("ICSTogether - v0.1.4 pre-alpha",style);
            
            if (!IsHost && !IsClient)
            {
                ip = GUILayout.TextField(ip, style);
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
            GUILayout.Label($"received {receivedpackets} packets in total", style);
            GUILayout.Label($"last {lastpacket} packet", style);
            if(IsHost||IsClient)
            {
                GUILayout.Label($"Your money: {PlayerPrefs.GetFloat("money")}\nPlayer's 2 money: {player2money}", style);
                GUILayout.Label("Click MOUSE5 if your friend isn't moving on your side", style);
                GUILayout.Label("Last sent pos" + lastsentpos);
            }
        }
        private void TcpDaemon(IAsyncResult result)
        {
            tcpClient = listener.EndAcceptTcpClient(result);
            player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
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
