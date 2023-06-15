using Il2Cpp;
using HarmonyLib;
using UnityEngine;

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
        internal class UpdateButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                Buttons.rawMeatGearItem = gi?.GetComponent<GearItem>();
                Buttons.rawMeatFoodItem = gi?.GetComponent<FoodItem>();
                if (gi != null && (gi.gameObject.name.ToLowerInvariant().Contains("meat") && gi.gameObject.name.ToLowerInvariant().Contains("raw")))
                {
                    Buttons.SetActive(true);
                }
                else
                {
                    Buttons.SetActive(false);
                }
            }
        }
        
        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddCones
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                
                if (__instance != null && __instance.name.Contains("RadialSpawn_sticks") && BFUtils.cones != null)
                {
                    if (Utils.RollChance(30))
                    {
                        __result = BFUtils.cones;
                    }
                    //MelonLoader.MelonLogger.Msg("AddCones Postfix is working.");
                }
            }
        }
        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddCrows
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (__instance != null && BFUtils.crows != null && __instance.m_ObjectToSpawn == BFUtils.feather)
                {
                    if (Utils.RollChance(15))
                    {
                        __result = BFUtils.crows;
                    }
                }
            }
        }
        
    }

}
    
 