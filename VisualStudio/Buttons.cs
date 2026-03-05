using Il2Cpp;
using Il2CppTLD.IntBackedUnit;
using MelonLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bountiful_Foraging
{
    internal class Buttons
    {
        internal static string saltText;
        private static GameObject saltButtonMeat;
        private static GameObject saltButtonFish;

        internal static string saltItem = "GEAR_CrushedHalite";
        internal static string saltItem2 = "GEAR_Salt";

        internal static FoodItem rawFoodItem;
        internal static GearItem rawGearItem;
        private static string foodFromAnimal;
        private static string foodFromFish;
        private static float foodCondition;
        private static ItemWeight foodWeight;
        private static float foodCalories;
        private static float fishCaloriesPerKG;

        internal static void InitializeBF(ItemDescriptionPage itemDescriptionPage) //Creates buttons in the inventory menu for the salting system
        {
            saltText = Localization.Get("GAMEPLAY_BF_SaltBehaviourLabel");

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            saltButtonMeat = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            if (BFUtils.thingsToDoInstalled == true)
            {
                saltButtonMeat.transform.Translate(0.345f, -0.1f, 0);
            }
            else {
                saltButtonMeat.transform.Translate(0.345f, 0, 0);
            }
            Utils.GetComponentInChildren<UILabel>(saltButtonMeat).text = saltText;

            saltButtonFish = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            saltButtonFish.transform.Translate(0.345f, -0.1f, 0);
            Utils.GetComponentInChildren<UILabel>(saltButtonFish).text = saltText;

            AddAction(saltButtonMeat, new System.Action(OnSaltMeat));
            AddAction(saltButtonFish, new System.Action(OnSaltFish));
        }

        private static void AddAction(GameObject button, System.Action action) //Creates a function for the button
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }

        internal static void SetSaltMeatActive(bool active) //Activates/Deactivates the visibility of the button.
        {
            NGUITools.SetActive(saltButtonMeat, active);
        }
        internal static void SetSaltFishActive(bool active) //Same, but for fish. Probably can be removed and just use the one above.
        {
            NGUITools.SetActive(saltButtonFish, active);
        }
        private static void OnSaltMeat() //Salting behavior for meat. Stores the raw item values and destroys the gear item from the inventory
        {
            var thisFoodItem = Buttons.rawFoodItem;
            var thisGearItem = Buttons.rawGearItem;
            foodFromAnimal = BFUtils.FindAnimal(thisGearItem.name);
            foodCalories = thisFoodItem.m_CaloriesRemaining;
            foodCondition = thisGearItem.CurrentHP;
            foodWeight = thisGearItem.GetItemWeightKG();
 
            //MelonLoader.MelonLogger.Msg("Salting:");
            //MelonLoader.MelonLogger.Msg(meatFromAnimal);
            //MelonLoader.MelonLogger.Msg(meatCalories + " calories.");
            //MelonLoader.MelonLogger.Msg(meatCondition + " condition.");
            //MelonLoader.MelonLogger.Msg(meatWeight + " KG.");
            if (thisFoodItem == null || thisGearItem == null) return;
            if (foodCondition < 2.5f)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BF_SaltBehaviourRotting"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (GameManager.GetInventoryComponent().GearInInventory(saltItem, 1)) 
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_BF_SaltBehaviourProgressBar"), 5f, 0f, 0f,
                                "Play_SearchCorpse", null, false, true, new System.Action<bool, bool, float>(OnSaltMeatFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(saltItem, 1);
                GearItem.Destroy(thisGearItem);

            }
            else if (GameManager.GetInventoryComponent().GearInInventory(saltItem2, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_BF_SaltBehaviourProgressBar"), 5f, 0f, 0f,
                                "Play_SearchCorpse", null, false, true, new System.Action<bool, bool, float>(OnSaltMeatFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(saltItem2, 1);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BF_SaltBehaviourNoSalt"));
                GameAudioManager.PlayGUIError();
            }
        }
        private static void OnSaltMeatFinished(bool success, bool playerCancel, float progress) //Instantiates a salted copy with the values previously stored.
        {
            //MelonLoader.MelonLogger.Msg("Salted:");
            //MelonLoader.MelonLogger.Msg("GEAR_SaltedMeat" + meatFromAnimal);
            GearItem saltedMeat = Addressables.LoadAssetAsync<GameObject>("GEAR_SaltedMeat" + foodFromAnimal).WaitForCompletion().GetComponent<GearItem>();
            saltedMeat.WeightKG = foodWeight;
            saltedMeat.CurrentHP = foodCondition + 0.1f;
            //MelonLoader.MelonLogger.Msg(saltedMeat.CurrentHP + " condition.");
            saltedMeat.m_FoodItem.m_CaloriesRemaining = foodCalories;
            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(saltedMeat, 1);
        }
        //These 2 👇 does the same as the 2 functions above, but for fish. Probably unnecessary but...
        private static void OnSaltFish()
        {
            var thisFoodItem = Buttons.rawFoodItem;
            var thisGearItem = Buttons.rawGearItem;
            foodFromFish = BFUtils.FindFish(thisGearItem.name);
            fishCaloriesPerKG = thisGearItem.m_FoodWeight.m_CaloriesPerKG;
            foodCalories = thisFoodItem.m_CaloriesRemaining;
            foodCondition = thisGearItem.CurrentHP;
            foodWeight = thisGearItem.GetItemWeightKG();

            if (thisFoodItem == null || thisGearItem == null) return;
            if (foodCondition < 0.8f)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BF_SaltBehaviourRotting"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (GameManager.GetInventoryComponent().GearInInventory(saltItem, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_BF_SaltBehaviourProgressBar"), 5f, 0f, 0f,
                                "PLAY_HARVESTINGMEATLSMALL", null, false, true, new System.Action<bool, bool, float>(OnSaltFishFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(saltItem, 1);
                GearItem.Destroy(thisGearItem);

            }
            else if (GameManager.GetInventoryComponent().GearInInventory(saltItem2, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_BF_SaltBehaviourProgressBar"), 5f, 0f, 0f,
                                "PLAY_HARVESTINGMEATLSMALL", null, false, true, new System.Action<bool, bool, float>(OnSaltFishFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(saltItem2, 1);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BF_SaltBehaviourNoSalt"));
                GameAudioManager.PlayGUIError();
            }
        }
        private static void OnSaltFishFinished(bool success, bool playerCancel, float progress)
        {
            GearItem saltedFish = Addressables.LoadAssetAsync<GameObject>("GEAR_SaltedFish" + foodFromFish).WaitForCompletion().GetComponent<GearItem>();
            saltedFish.WeightKG = foodWeight;
            saltedFish.CurrentHP = foodCondition + 5f;
            saltedFish.m_FoodItem.m_CaloriesRemaining = foodCalories;
            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(saltedFish, 1);
        }

    }
}
