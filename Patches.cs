﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using MoreLockedDoors.Locks;
using Random = System.Random;
using Il2CppNodeCanvas.Tasks.Actions;
using LoadScene = Il2Cpp.LoadScene;
using MoreLockedDoors.Utils;
using GearSpawner;

namespace MoreLockedDoors
{
    internal class Patches : MelonMod
    {
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.Awake))]

        internal class GameManager_Awake
        {
            private static void Postfix()
            {
                LockManager lockManager = new();

                AddMysteryLakeLocks(lockManager);
                AddMountainTownLocks(lockManager);
                AddBrokenRailroadLocks(lockManager);
                AddDesolationPointLocks(lockManager);
                AddCoastalHighwayLocks(lockManager);
                AddCinderHillsCoalMineLocks(lockManager);
                AddPleasantValleyLocks(lockManager);

            }
        }

        public static void AddMysteryLakeLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LakeRegion")
            {
                //lock Carter Hydro Dam staging fence gate
                GameObject damFrontGateObj = GameObject.Find(Utils.Paths.damFrontGate);
                lockManager.InitializeLock(damFrontGateObj, 70, lockManager.metalChainDoorLockedAudio, "", Utils.Items.boltcutters.GetComponent<GearItem>());
            }
        }
        public static void AddMountainTownLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MountainTownRegion")
            {
                //lock Milton Credit Union
                GameObject bankFrontDoorObj = GameObject.Find(Utils.Paths.bankFrontDoor);
                lockManager.InitializeLock(bankFrontDoorObj, 50, lockManager.woodDoorLockedAudio, "", Utils.Items.prybar.GetComponent<GearItem>());

            }
        }
        public static void AddBrokenRailroadLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TracksRegion")
            {
                //lock Hunting Lodge fence gate
                GameObject lodgeGateObj = GameObject.Find(Utils.Paths.huntingLodgeGate);
                lockManager.InitializeLock(lodgeGateObj, 60, lockManager.metalChainDoorLockedAudio, "", Utils.Items.boltcutters.GetComponent<GearItem>());

                //lock Maintenance shed exterior doors
                GameObject maintenanceShedDoorAobj = GameObject.Find(Utils.Paths.maintenanceShedDoorA);
                GameObject maintenanceShedDoorBobj = GameObject.Find(Utils.Paths.maintenanceShedDoorB);
                GameObject maintenanceShedDoorCobj = GameObject.Find(Utils.Paths.maintenanceShedDoorC);

                string[] GUIDs = { "bbb36907-bb53-41ec-a5cb-e6d362580663", "2c22cb32-5b14-4a1c-9fef-3d6a9c33ee5d", "ea186e44-18ac-4117-9c46-348158fb0e25s" };

                GameObject[] doors = { maintenanceShedDoorAobj, maintenanceShedDoorBobj, maintenanceShedDoorCobj };
                var i = 0;
                foreach (var door in doors)
                {
                    lockManager.InitializeLock(door, 80, lockManager.metalDoorLockedAudio, GUIDs[i], Utils.Items.prybar.GetComponent<GearItem>());
                    i++;
                }
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MaintenanceShedA")
            {
                //lock Maintenance shed interior doors

                GameObject maintenanceShedDoorAobj = GameObject.Find(Utils.Paths.maintenanceShedDoorAInterior);
                GameObject maintenanceShedDoorBobj = GameObject.Find(Utils.Paths.maintenanceShedDoorBInterior);
                GameObject maintenanceShedDoorCobj = GameObject.Find(Utils.Paths.maintenanceShedDoorCInterior);

                string[] GUIDs = { "575f6158-c639-4bc0-86db-ba39e5f108ce", "2a871b6f-8d5d-4d2b-812f-adbcf2b3d2dc", "6d63dd98-a94c-4b71-80e3-99aeb3da354b" };

                GameObject[] doors = { maintenanceShedDoorAobj, maintenanceShedDoorBobj, maintenanceShedDoorCobj };
                var i = 0;
                foreach (var door in doors)
                {
                    lockManager.InitializeLock(door, 80, lockManager.metalDoorLockedAudio, GUIDs[i], Utils.Items.prybar.GetComponent<GearItem>());
                    i++;
                }

            }
        }
        public static void AddDesolationPointLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "WhalingStationRegion")
            {

                var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "TransitionContact");

                //Lock Quonset Gas Station Exterior
                GameObject mine5door1 = null;
                GameObject mine5door2 = null;

                foreach (var obj in objects)
                {
                    if (obj.GetComponent<ObjectGuid>().PDID == "d61e4ad1-6e4a-47bf-a5ed-696070a88399")
                    {
                        mine5door1 = obj;
                    }
                    else if (obj.GetComponent<ObjectGuid>().PDID == "1d06bcf8-fc72-4c47-ad3d-ed2f5455d5b2")
                    {
                        mine5door2 = obj;
                    }
                }

                if (mine5door1 != null && mine5door2 != null)
                {
                    lockManager.InitializeLock(mine5door1, 40, lockManager.metalChainDoorLockedAudio, "27abfb4e-d322-4b64-b1a8-d382cee053af", Utils.Items.hacksaw.GetComponent<GearItem>());
                    lockManager.InitializeLock(mine5door2, 40, lockManager.metalChainDoorLockedAudio, "a358f1a0-1af4-4faa-9f00-0b77fa1ff114", Utils.Items.hacksaw.GetComponent<GearItem>());
                }
                else
                {
                    MelonLogger.Msg("Mine 5 exterior door objects not found");
                }

                //Lock Hibernia processing
                GameObject hiberniaFrontDoor = GameObject.Find(Utils.Paths.hiberniaFrontDoor);
                GameObject hiberniaBackDoor = GameObject.Find(Utils.Paths.hiberniaBackDoor);

                lockManager.InitializeLock(hiberniaFrontDoor, 60, lockManager.metalDoorLockedAudio, "5805f636-2b63-4bda-a468-750bef9b0ac6", Utils.Items.prybar.GetComponent<GearItem>());
                lockManager.InitializeLock(hiberniaBackDoor, 60, lockManager.metalDoorLockedAudio, "61299ab5-b6ac-420a-bbdc-4e4787c7b461", Utils.Items.prybar.GetComponent<GearItem>());

            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "WhalingWarehouseA")
            {

                //Lock interior Hibernia doors
                GameObject hiberniaFrontDoor = GameObject.Find(Utils.Paths.hiberniaFrontDoorInt);
                GameObject hiberniaBackDoor = GameObject.Find(Utils.Paths.hiberniaBackDoorInt);

                lockManager.InitializeLock(hiberniaFrontDoor, 60, lockManager.metalDoorLockedAudio, "8497788f-28d1-4a17-baab-f3a6b98100f2", Utils.Items.prybar.GetComponent<GearItem>());
                lockManager.InitializeLock(hiberniaBackDoor, 60, lockManager.metalDoorLockedAudio, "ed87b39e-addb-457a-8abf-81d7bfa4a97e", Utils.Items.prybar.GetComponent<GearItem>());

            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "WhalingMine")
            {

                //Lock Mine 5 interior doors
                GameObject mine5door1Int = GameObject.Find(Utils.Paths.mine5door1Interior);
                GameObject mine5door2Int = GameObject.Find(Utils.Paths.mine5door2Interior);

                lockManager.InitializeLock(mine5door1Int, 40, lockManager.metalChainDoorLockedAudio, "d61e4ad1-6e4a-47bf-a5ed-696070a88399", Utils.Items.hacksaw.GetComponent<GearItem>());
                lockManager.InitializeLock(mine5door2Int, 40, lockManager.metalChainDoorLockedAudio, "1d06bcf8-fc72-4c47-ad3d-ed2f5455d5b2", Utils.Items.hacksaw.GetComponent<GearItem>());

            }
        }
        public static void AddCoastalHighwayLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "CoastalRegion")
            {

                var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "InteriorLoadTrigger");


                //Lock Quonset Gas Station Exterior
                GameObject quonsetFrontDoor = null;
                GameObject quonsetBackDoor = null;

                foreach (var obj in objects)
                {
                    if (obj.GetComponent<ObjectGuid>().PDID == "a40c7e13-86b4-4a04-9a50-16f78bd14d8e")
                    {
                        quonsetFrontDoor = obj;
                    }
                    else if (obj.GetComponent<ObjectGuid>().PDID == "e6910ab2-75ec-4df5-adb2-8f3a4c76ecaa")
                    {
                        quonsetBackDoor = obj;
                    }
                }

                if (quonsetFrontDoor != null && quonsetBackDoor != null)
                {
                    lockManager.InitializeLock(quonsetFrontDoor, 70, lockManager.metalDoorLockedAudio, "8939b09a-3b9b-4b2c-9ced-cde4bc8ad171", Utils.Items.prybar.GetComponent<GearItem>());
                    lockManager.InitializeLock(quonsetBackDoor, 70, lockManager.metalDoorLockedAudio, "9991f9bb-0c1a-48b3-81a8-8ce883ccd389", Utils.Items.prybar.GetComponent<GearItem>());
                }
                else
                {
                    MelonLogger.Msg("Quonset door objects not found");
                }


                //Lock Cinder Hills Coal Mine Exterior CH
                GameObject cinderHillsCoalMineDoor = GameObject.Find(Paths.cinderHillsCoalMineExteriorDoor);
                lockManager.InitializeLock(cinderHillsCoalMineDoor, 100, lockManager.metalChainDoorLockedAudio, "27abfb4e-d322-4b64-b1a8-d382cee053af", Utils.Items.hacksaw.GetComponent<GearItem>());

            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "QuonsetGasStation")
            {
                //Lock quonset interior door
                GameObject quonsetFrontDoor = GameObject.Find(Paths.quonsetGasStationInteriorFrontDoor);
                GameObject quonsetBackDoor = GameObject.Find(Paths.quonsetGasStationInteriorBackDoor);

                lockManager.InitializeLock(quonsetFrontDoor, 70, lockManager.metalDoorLockedAudio, "a40c7e13-86b4-4a04-9a50-16f78bd14d8e", Utils.Items.prybar.GetComponent<GearItem>());
                lockManager.InitializeLock(quonsetBackDoor, 70, lockManager.metalDoorLockedAudio, "e6910ab2-75ec-4df5-adb2-8f3a4c76ecaa", Utils.Items.prybar.GetComponent<GearItem>());
            }
        }
        public static void AddCinderHillsCoalMineLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MineTransitionZone")
            {

                //Lock mine exits to CH and PV
                GameObject cinderHillsCoalMineDoorCH = GameObject.Find(Paths.cinderHillsCoalMineInteriorDoorCH);

                lockManager.InitializeLock(cinderHillsCoalMineDoorCH, 100, lockManager.metalChainDoorLockedAudio, "adb77a85-783e-4fc6-bd50-07bd264efe44", Utils.Items.hacksaw.GetComponent<GearItem>());

            }
        }
        public static void AddPleasantValleyLocks(LockManager lockManager)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RuralRegion")
            {

                var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "InteriorLoadTrigger");

                //Lock Rural Store exterior

                GameObject ruralStoreFrontDoor = null;
                GameObject ruralStoreBackDoor = null;

                foreach (var obj in objects)
                {
                    if (obj.GetComponent<ObjectGuid>().PDID == "e5cb7bfa-5c37-4fef-a57e-7fac7174aaa3")
                    {
                        ruralStoreFrontDoor = obj;
                    }
                    else if (obj.GetComponent<ObjectGuid>().PDID == "30ce35d1-f08b-43e4-abdd-c8f226352afc")
                    {
                        ruralStoreBackDoor = obj;
                    }
                }

                if (ruralStoreFrontDoor != null && ruralStoreBackDoor != null)
                {
                    lockManager.InitializeLock(ruralStoreFrontDoor, 70, lockManager.woodDoorLockedAudio, "c17a1359-75ee-43fa-ace4-b76819fdaa9b", Utils.Items.prybar.GetComponent<GearItem>());
                    lockManager.InitializeLock(ruralStoreBackDoor, 70, lockManager.woodDoorLockedAudio, "138894f7-7fd0-4c97-be36-1f350cb93499", Utils.Items.prybar.GetComponent<GearItem>());
                }
                else
                {
                    MelonLogger.Msg("Rural store door objects not found");
                }

                //Lock Radio Control Hut
                GameObject signalHillDoor = GameObject.Find(Paths.signalHillDoor);
                lockManager.InitializeLock(signalHillDoor, 100, lockManager.metalDoorLockedAudio, "", Utils.Items.prybar.GetComponent<GearItem>());
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RuralStoreA")
            {

                //Lock Rural store interior
                GameObject ruralStoreFrontDoor = GameObject.Find(Utils.Paths.ruralStoreFrontDoorInt);
                GameObject ruralStoreBackDoor = GameObject.Find(Utils.Paths.ruralStoreBackDoorInt);

                lockManager.InitializeLock(ruralStoreFrontDoor, 70, lockManager.woodDoorLockedAudio, "e5cb7bfa-5c37-4fef-a57e-7fac7174aaa3", Utils.Items.prybar.GetComponent<GearItem>());
                lockManager.InitializeLock(ruralStoreBackDoor, 70, lockManager.woodDoorLockedAudio, "30ce35d1-f08b-43e4-abdd-c8f226352afc", Utils.Items.prybar.GetComponent<GearItem>());

            }
        }



        //These patches fix issues with the generic Lock component
        [HarmonyPatch(typeof(LoadScene), nameof(LoadScene.Update))]

        internal class LoadScene_Update
        {
            private static void Postfix(LoadScene __instance)
            {
                Lock lck = __instance.Lock;
                lck.MaybeUnlockDueToCompanionBeingUnlocked();
            }
        }

        [HarmonyPatch(typeof(Lock), nameof(Lock.Awake))]

        internal class Lock_Awake
        {

            //Override the Awake method for the lock component if it is added with this mod. This prevents the Awake method from executing before modifications to the component are complete.
            public static bool Prefix(Lock __instance)
            {

                GameObject obj = __instance.gameObject;
                string[] paths = Utils.Paths.paths;

                if (paths.Any(Utils.Paths.GetObjectPath(obj).Contains))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

    }
}


