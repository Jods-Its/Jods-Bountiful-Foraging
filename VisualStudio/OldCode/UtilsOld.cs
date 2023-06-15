using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bountiful_Foraging.OldCode
{
    internal class UtilsOld
    {
        /*
        internal static string saltItemName = "GEAR_CrushedHalite";
        public static bool IsRawMeat(GearItem gi)
        {
            if (gi != null && (gi.gameObject.name.ToLowerInvariant().Contains("meat") && gi.gameObject.name.ToLowerInvariant().Contains("raw")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsRawMeat(Panel_Inventory_Examine actions)
        {
            if (actions.m_GearItem.name.ToLowerInvariant().Contains("meat") && actions.m_GearItem.name.ToLowerInvariant().Contains("raw"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void SaltBehaviour(GearItem gi)
        {

        }
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<GearItem>();
        public static T? GetComponentSafe<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetComponentSafe<T>(component.GetGameObject());
        }
        public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
        {
            return gameObject == null ? default : gameObject.GetComponent<T>();
        }
        public static T? GetOrCreateComponent<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetOrCreateComponent<T>(component.GetGameObject());
        }
        public static T? GetOrCreateComponent<T>(this GameObject? gameObject) where T : Component
        {
            if (gameObject == null)
            {
                return default;
            }

            T? result = GetComponentSafe<T>(gameObject);

            if (result == null)
            {
                result = gameObject.AddComponent<T>();
            }

            return result;
        }
        internal static GameObject? GetGameObject(this Component? component)
        {
            try
            {
                return component == null ? default : component.gameObject;
            }
#if DEBUG
            catch (System.Exception exception)
            {
               Implementations.LogError($"Returning null since this could not obtain a Game Object from the component. Stack trace:\n{exception.Message}");
            }
#endif
#if !DEBUG
            catch { }
#endif
            return null;
        }
        */
    }
}
