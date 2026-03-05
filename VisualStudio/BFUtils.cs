using Il2Cpp;
using Il2CppTLD.IntBackedUnit;
using MelonLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bountiful_Foraging
{
    internal static class BFUtils
    {
        public static bool thingsToDoInstalled = false;

        public static GameObject cones = Addressables.LoadAssetAsync<GameObject>("GEAR_4FirCone").WaitForCompletion();
        public static GameObject crows = Addressables.LoadAssetAsync<GameObject>("GEAR_CrowCarcass").WaitForCompletion();
        public static GameObject feather = Addressables.LoadAssetAsync<GameObject>("GEAR_CrowFeather").WaitForCompletion();
        public static GameObject salt = Addressables.LoadAssetAsync<GameObject>("GEAR_HaliteSmall").WaitForCompletion();
        public static GameObject skull1 = Addressables.LoadAssetAsync<GameObject>("GEAR_SkullStag").WaitForCompletion();
        public static GameObject skull2 = Addressables.LoadAssetAsync<GameObject>("GEAR_SkullBear").WaitForCompletion();
        public static GameObject quarter = Addressables.LoadAssetAsync<GameObject>("GEAR_OrcaQuarter").WaitForCompletion();
        public static GameObject pelt = Addressables.LoadAssetAsync<GameObject>("GEAR_GreyWolfPelt").WaitForCompletion();
        public static GameObject crowFlock = Addressables.LoadAssetAsync<GameObject>("WILDLIFE_CrowFlockForCorpse").WaitForCompletion();
        public static GameObject nest = Addressables.LoadAssetAsync<GameObject>("GEAR_UndefinedNest").WaitForCompletion();
        public static Texture2D orcaIcon;

        public static LocalizedString orcaLoc = new LocalizedString();  
        

        public static float executionTimeStone = Time.time;
        public static string FindAnimal (string gearItemName) //Animal finder for salting system
        {
            string[] animal = {"Bear", "Bird", "Deer", "Moose", "Orca", "Rabbit", "Wolf", "Ptarmigan"};
            string sheFuckedUp = "Venison";
            int index = -1;
            for (int i = 0; i <animal.Length; i++)
            {
                if (gearItemName.Contains(animal[i]))
                {
                    index = i;
                    break;
                }
            }
            if (animal[index] == "Deer")
            {
                return sheFuckedUp;
            }
            return animal[index];
        }
        public static string FindFish(string gearItemName) //Same, but for fish
        {
            string[] fish = { "Salmon", "Bass", "White", "Trout", "Red", "Rock", "Burbot", "Goldeye" };
            int index = -1;
            for (int i = 0; i < fish.Length; i++)
            {
                if (gearItemName.Contains(fish[i]))
                {
                    index = i;
                    break;
                }
            }
            if (fish[index] == "Bass")
            {
                if (gearItemName.Contains("SmallMouth"))
                {
                    return "BassSmall";
                }
                else
                {
                    return "BassLarge";
                }
            }
            return fish[index];
        }
        public static bool IsFish(string gearItemName) //Helps patches fix issue with salting button on fish
        {
            string[] fish = { "Salmon", "Trout", "Bass", "WhiteFish", "Irish", "Burbot", "Goldeye", "Rockfish" };
            for (int i = 0; i < fish.Length; i++)
            {
                if (gearItemName.Contains(fish[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public static float GetFishCaloriesPerKG(string targetedFish) //Attempt at fixing anti-fish
        {
            string[] fish = { "Salmon", "Bass", "White", "Trout", "Red", "Rock", "Burbot", "Goldeye" };
            int index = -1;
            float[] calories = { 300, 300, 250, 250, 300, 300, 325, 300};
            
            for (int i = 0; i < fish.Length; i++)
            {
                if (fish[i] == targetedFish)
                {
                    index = i;
                    break;
                }
            }

            return calories[index];
        }
        public static void SetBodyHarvest(BodyHarvest __bodyHarvest) //BodyHarvest component for orca carcasses helper
        {
            if(__bodyHarvest != null)
            {
                orcaLoc.m_LocalizationID = Localization.Get("GAMEPLAY_OrcaCarcassRavaged");
                __bodyHarvest.m_LocalizedDisplayName = orcaLoc;
                __bodyHarvest.m_FrozenDisplayNameId = Localization.Get("GAMEPLAY_OrcaCarcassFrozen");
                __bodyHarvest.m_CanQuarter = true;
                __bodyHarvest.m_QuarterAudio = "PLAY_HARVESTINGMEATLARGE";
                __bodyHarvest.m_QuarterObjectPrefab = BFUtils.quarter;
                __bodyHarvest.m_QuarterBagMeatCapacity = ItemWeight.FromKilograms(8);
                __bodyHarvest.m_AllowDecay = false;
                __bodyHarvest.m_StartFrozen = true;
                __bodyHarvest.m_QuarterDurationMinutes = 300;

            }

        }
        public static void SetCarrion(Carrion __carrion) //Crows for orca carcasses
        {
            if(__carrion != null)
            {
                __carrion.m_FlyDistanceAboveTarget = 20;
                __carrion.m_LodDistance = 200;
                __carrion.m_LoopAudio = "PLAY_CROWCAWDISTANT";
                __carrion.m_LoopAudioID = 0;
                __carrion.m_MaxFlockSize = 7;
                __carrion.m_MinFlockSize = 5;
                __carrion.m_Prefab = BFUtils.crowFlock;
                __carrion.m_ShouldDisperse = true;
            }
        }
        public static void SetMapDetail(MapDetail __mapDetail) //MapDetail component for orca carcasses
        {
            if(__mapDetail != null)
            {
                __mapDetail.m_IconType = MapIcon.MapIconType.DetailEntry;
                __mapDetail.m_LocID = Localization.Get("GAMEPLAY_OrcaCarcass");
                __mapDetail.m_SpriteName = "ico_knife";
            }
        }
        public static void SetRigidBody(Rigidbody __rigidBody) //RigidBody component for hives
        {
            if(__rigidBody != null)
            {
                __rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                __rigidBody.isKinematic = true;
            }

        }
        //Generic stuff 👇
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
            catch (System.Exception exception)
            {
                MelonLoader.MelonLogger.Msg($"Returning null since this could not obtain a Game Object from the component. Stack trace:\n{exception.Message}");
            }
            return null;
        }
        public static GameObject GetPlayer()
        {
            return GameManager.GetPlayerObject();
        }
        public static void LaunchBF()
        {
            MelonLogger.Msg(System.ConsoleColor.Yellow, "Salting meat...");
            MelonLogger.Msg(System.ConsoleColor.Yellow, "Finding orcas...");
            MelonLogger.Msg(System.ConsoleColor.Yellow, "Placing fir cones...");
            MelonLogger.Msg(System.ConsoleColor.Green, "Bountiful Foraging v1.2.0 loaded!");
            Settings.instance.AddToModSettings("Bountiful Foraging");
        }
        public static void BFIconsInitialize()
        {
            orcaIcon = Implementations.bundleBFIcons.LoadAsset<Texture2D>("icoMap_OrcaCarcass");
            orcaIcon.hideFlags = HideFlags.HideAndDontSave;
            GameManager.DontDestroyOnLoad(orcaIcon);
        }
    }
}
