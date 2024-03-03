using HarmonyLib;
using ICSTogether.HarmonyPatches.Door;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ICSTogether.HarmonyPatches
{
    /*[HarmonyPatch(typeof(PlayerRaycast))]
    [HarmonyPatch("Update")]
    public static class Update
    {
        private static bool cansend = true;
        [HarmonyPostfix]
        public static void Postfix(PlayerRaycast __instance)
        {
            if(!vars.readdata&&Main.lastpacket != "OpenDoor")
            {
                if (__instance.hit.collider.CompareTag("door"))
                {
                    string doorname = __instance.hit.collider.name;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (__instance.hit.collider.GetComponent<Animator>().GetBool("open") && cansend)
                        {
                            Main.sw.Write("OpenDoor");
                            Main.sw.Write(doorname);
                            Main.sw.Flush();
                            Main.sw.Write("AfterOpenDoor");
                            Main.sw.Write(doorname);
                            Main.sw.Flush();
                            cansend = false;
                        }
                        else
                        {

                            if (!cansend)
                            {
                                Main.sw.Write("OpenDoor");
                                Main.sw.Write(doorname);
                                Main.sw.Flush();
                                Main.sw.Write("AfterOpenDoor");
                                Main.sw.Write(doorname);
                                Main.sw.Flush();
                                cansend = true;
                            }
                        }
                    }
                    

                }
            }


        }*/
}

