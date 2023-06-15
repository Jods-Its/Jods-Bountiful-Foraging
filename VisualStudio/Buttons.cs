using Il2Cpp;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bountiful_Foraging
{
    internal class Buttons
    {
        internal static string saltText;
        private static GameObject saltButton;
        internal static FoodItem rawMeatFoodItem;
        internal static GearItem rawMeatGearItem;
        internal static string saltItem = "GEAR_CrushedHalite";
        private static string meatFromAnimal;
        private static float meatCondition;
        private static float meatWeight;
        private static float meatCalories;

        internal static void InitializeBF(ItemDescriptionPage itemDescriptionPage)
        {
            saltText = Localization.Get("GAMEPLAY_BF_SaltBehaviourLabel");

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            saltButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            saltButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(saltButton).text = saltText;

            AddAction(saltButton, new System.Action(OnSalt));
        }

        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }

        internal static void SetActive(bool active)
        {
            NGUITools.SetActive(saltButton, active);
        }

        private static void OnSalt()
        {
            var thisFoodItem = Buttons.rawMeatFoodItem;
            var thisGearItem = Buttons.rawMeatGearItem;
            meatFromAnimal = BFUtils.FindAnimal(thisGearItem.name);
            meatCalories = thisFoodItem.m_CaloriesRemaining;
            meatCondition = thisGearItem.CurrentHP;
            meatWeight = thisGearItem.GetItemWeightKG();
 
            //MelonLoader.MelonLogger.Msg("Salting:");
            //MelonLoader.MelonLogger.Msg(meatFromAnimal);
            //MelonLoader.MelonLogger.Msg(meatCalories + " calories.");
            //MelonLoader.MelonLogger.Msg(meatCondition + " condition.");
            //MelonLoader.MelonLogger.Msg(meatWeight + " KG.");
            if (thisFoodItem == null || thisGearItem == null) return;
            if (meatCondition < 2.5f)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BF_SaltBehaviourRotting"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (GameManager.GetInventoryComponent().GearInInventory(saltItem, 1)) 
            {
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_BF_SaltBehaviourProgressBar"), 5f, 0f, 0f,
                                "Play_SearchCorpse", null, false, true, new System.Action<bool, bool, float>(OnSaltFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(saltItem, 1);
                GearItem.Destroy(thisGearItem);

            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BF_SaltBehaviourNoSalt"));
                GameAudioManager.PlayGUIError();
            }
        }
        private static void OnSaltFinished(bool success, bool playerCancel, float progress)
        {
            //MelonLoader.MelonLogger.Msg("Salted:");
            //MelonLoader.MelonLogger.Msg("GEAR_SaltedMeat" + meatFromAnimal);
            GearItem saltedMeat = Addressables.LoadAssetAsync<GameObject>("GEAR_SaltedMeat" + meatFromAnimal).WaitForCompletion().GetComponent<GearItem>();
            saltedMeat.WeightKG = meatWeight;
            saltedMeat.CurrentHP = meatCondition + 5f;
            saltedMeat.m_FoodItem.m_CaloriesRemaining = meatCalories;
            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(saltedMeat, 1);
        }

    }
}
