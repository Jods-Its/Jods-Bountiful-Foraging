using Il2Cpp;
using HarmonyLib;
using UnityEngine;
using MelonLoader;
using UnityEngine.Networking;
using Il2CppTLD.IntBackedUnit;
using System.Runtime;

namespace Bountiful_Foraging
{
    
    internal class Patches
    {
        
        [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
        internal class SaltInitialization
        {
            private static void Postfix(Panel_Inventory __instance)
            {
                Buttons.InitializeBF(__instance.m_ItemDescriptionPage);
            }
        }

        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateSaltButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                Buttons.rawGearItem = gi?.GetComponent<GearItem>();
                Buttons.rawFoodItem = gi?.GetComponent<FoodItem>();
                if (gi != null && (gi.gameObject.name.ToLowerInvariant().Contains("meat") && gi.gameObject.name.ToLowerInvariant().Contains("raw")))
                {
                    Buttons.SetSaltMeatActive(true);
                    Buttons.SetSaltFishActive(false);
                }
                else if ((gi != null && (BFUtils.IsFish(gi.gameObject.name) && gi.gameObject.name.ToLowerInvariant().Contains("raw"))))
                {
                    Buttons.SetSaltMeatActive(false);
                    Buttons.SetSaltFishActive(true);
                }
                else
                {
                    Buttons.SetSaltMeatActive(false);
                    Buttons.SetSaltFishActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(EvolveItem), nameof(EvolveItem.DoEvolution))]
        internal class TransferDataToCuredCounterpart
        {
            private static void Prefix(EvolveItem __instance)
            {
                if (__instance != null && __instance.m_GearItem.name.Contains("Salted"))
                {
                    __instance.m_GearItemToBecome.WeightKG = __instance.m_GearItem.WeightKG;
                    __instance.m_GearItemToBecome.CurrentHP = __instance.m_GearItem.CurrentHP;
                    __instance.m_GearItemToBecome.m_FoodItem.m_CaloriesRemaining = __instance.m_GearItem.m_FoodItem.m_CaloriesRemaining;
                }
            }
        }

        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
        private static class SetFishFoodWeight
        {
            private static void Postfix (GearItem __instance)
            {
                if(__instance != null && __instance.m_FoodWeight != null)
                {
                    string targetedFish = BFUtils.FindFish(__instance.name);
                    if (__instance.name.Contains("Salted") && __instance.name.Contains(targetedFish))
                    {
                        __instance.m_FoodWeight.m_CaloriesPerKG = BFUtils.GetFishCaloriesPerKG(targetedFish);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GearItem), nameof (GearItem.Awake))]
        private static class DoThingsToOrcaCarcasses
        {
            private static void Postfix(GearItem __instance)
            {
                if(__instance != null && __instance.name.Contains("OrcaPanel"))
                {
                    BFUtils.GetOrCreateComponent<BodyHarvestInteraction>(__instance.gameObject);   
                    BFUtils.SetBodyHarvest(__instance.m_BodyHarvest);

                }
                if (__instance != null && __instance.name.Contains("OrcaCarcass") && !__instance.name.Contains("Undefined"))
                {
                    BFUtils.GetOrCreateComponent<Carrion>(__instance.gameObject);
                    BFUtils.GetOrCreateComponent<MapDetail>(__instance.gameObject);
                    BFUtils.SetCarrion(__instance.gameObject.GetComponent<Carrion>());
                    BFUtils.SetMapDetail(__instance.gameObject.GetComponent<MapDetail>());
                }
            }
        }

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.InteractiveObjectsProcess))]
        internal class EnableCarcassBehaviour
        {
            private static void Postfix()
            {
                GameObject? go = GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(3f);

                if (go != null && go.name.Contains("OrcaPanel"))
                {
                    GearItem gi = go.GetComponent<GearItem>();
                    gi.enabled = false;
                }
                else
                {
                    return;
                }
            }
        }

        [HarmonyPatch (typeof(GearItem), nameof(GearItem.Awake))]
        internal class AddRigidBodyToHivesAndNests
        {
            private static void Postfix (GearItem __instance)
            {
                if(__instance != null && (__instance.name.Contains("Hive") /*|| __instance.name.Contains("Nest")*/))
                {
                    BFUtils.GetOrCreateComponent<Rigidbody>(__instance.gameObject);
                    BFUtils.SetRigidBody(__instance.gameObject.GetComponent<Rigidbody>());
                }
            }
        }

        [HarmonyPatch (typeof(StoneItem), nameof(StoneItem.OnCollisionEnter))]
        class HandleStoneCollision
        {
            private static bool Prefix (StoneItem __instance, Collision collisionInfo)
            {
                //MelonLogger.Msg(collisionInfo.collider.name);
                
                if (collisionInfo.collider.name.ToLowerInvariant().Contains("errain") || Time.time - BFUtils.executionTimeStone < 1f) return true;
                if (collisionInfo.collider != null && collisionInfo.collider.name == "box_StoneChecker")
                {
                    BFUtils.executionTimeStone = Time.time;
                    GameObject player = BFUtils.GetPlayer();
                    Rigidbody rb = collisionInfo.collider.GetComponentInParent<Rigidbody>();
                    rb.isKinematic = false;
                    if(player != null)
                    {
                        GameAudioManager.PlaySound("PLAY_VOINSPECTOBJECTIMPORTANT", player);
                    }

                }
                return true;
            }
        }

        [HarmonyPatch (typeof(ArrowItem), nameof(ArrowItem.HandleCollisionWithObject))]
        internal class HandleArrowCollision
        {
            private static bool Prefix(ref GameObject collider)
            {
                if(collider != null && collider.name == "box_StoneChecker")
                {
                    GameObject player = BFUtils.GetPlayer();
                    Rigidbody rb = collider.GetComponentInParent<Rigidbody>();
                    rb.isKinematic = false;
                    if (player != null)
                    {
                        GameAudioManager.PlaySound("PLAY_VOINSPECTOBJECTIMPORTANT", player);
                    }
                }
                return true;
            }
        }

        [HarmonyPatch (typeof(BodyHarvest), nameof(BodyHarvest.Awake))]
        internal class AddTimberwolfPelt
        {
            private static void Postfix(BodyHarvest __instance)
            {
                if(Settings.instance.noPelt == false)
                {
                    if (__instance != null && __instance.name.ToLowerInvariant().Contains("grey"))
                    {
                        __instance.m_HidePrefab = BFUtils.pelt;
                        __instance.m_HideWeightKgPerUnit = ItemWeight.FromKilograms(1);
                    }
                }              
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddCones
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (Settings.instance.noCone == false)
                {
                    if (__instance != null && __instance.name.Contains("RadialSpawn_sticks") && BFUtils.cones != null)
                    {
                        if (Utils.RollChance(Settings.instance.coneChance))
                        {
                            __result = BFUtils.cones;
                        }
                    }
                }                
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddCrows
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if(Settings.instance.noCrow == false)
                {
                    if (__instance != null && BFUtils.crows != null && __instance.m_ObjectToSpawn == BFUtils.feather)
                    {
                        if (Utils.RollChance(Settings.instance.crowChance))
                        {
                            __result = BFUtils.crows;
                        }
                    }
                }           
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddSalt
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (__instance != null && BFUtils.salt != null && __instance.name.Contains("RadialSpawn_coal"))
                {
                    if (Utils.RollChance(Settings.instance.saltChance))
                    {
                        __result = BFUtils.salt;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddNests
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (Settings.instance.noNest == false)
                {
                    if (__instance != null && __instance.name.Contains("RadialSpawn_sticks") && BFUtils.nest != null)
                    {
                        if (Utils.RollChance(0.06f))
                        {
                            __result = BFUtils.nest;
                        }
                    }
                }            
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddSkullStag
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (__instance != null && __instance.name.Contains("RadialSpawn_sticks") && BFUtils.skull1 != null)
                {
                    if (Utils.RollChance(0.05f))
                    {
                        __result = BFUtils.skull1;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddSkullBear
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (__instance != null && __instance.name.Contains("RadialSpawn_sticks") && BFUtils.skull2 != null)
                {
                    if (Utils.RollChance(0.02f))
                    {
                        __result = BFUtils.skull2;
                    }
                }
            }
        }

    }

}
    
 