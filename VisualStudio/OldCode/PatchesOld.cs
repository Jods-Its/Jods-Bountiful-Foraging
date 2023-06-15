using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bountiful_Foraging.OldCode
{
    internal class PatchesOld
    {
        /*
[HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.CanExamine), new Type[] { typeof(GearItem) })]
public class AddActionsToMeat 
{
    private static bool Prefix(GearItem gi, ref bool __result)
    {
        if (Utils.IsRawMeat(gi) == true) 
        {
            __result = true;
            return false;
        }
        return true;
    }
}
[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Enable), new Type[] { typeof(bool), typeof(ComingFromScreenCategory) })]
public class ReapirToSalt
{
    private static void Prefix(Panel_Inventory_Examine __instance, bool enable)
    {
        if (Utils.IsRawMeat(__instance) == true)
        {
            Buttons.SetButtonLocalizationKey(__instance.m_Button_Repair, "GAMEPLAY_SaltBehaviour_Label");
            Buttons.SetButtonSprite(__instance.m_Button_Repair, "ico_salt");
            Buttons.SetRepairButtonLabel(__instance, "GAMEPLAY_SaltBehaviour_Label");
        }
        else
        {
            Buttons.SetButtonLocalizationKey(__instance.m_Button_Repair, "GAMEPLAY_Repair");
            Buttons.SetButtonSprite(__instance.m_Button_Repair, "ico_repair");
            Buttons.SetRepairButtonLabel(__instance, "GAMEPLAY_Repair");
        }
    }
}

[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
public class SetButtons
{
    private static void Postfix(Panel_Inventory_Examine __instance)
    {
        if (Utils.IsRawMeat(__instance) == true)
        {
            Vector3 position = Buttons.GetBottomPosition(
                                 __instance.m_Button_Repair
                                    );
            __instance.m_Button_Repair.gameObject.SetActive(true);
        }
    }
}

[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateButtonLegend))]
internal class UpdateButtons
{
    private static void Postfix(Panel_Inventory_Examine __instance)
    {
        if (Utils.IsRawMeat(__instance) == true && Buttons.IsSelected(__instance.m_Button_Repair))
        {
            __instance.m_ButtonLegendContainer.UpdateButton("Continue", "GAMEPLAY_SaltBehaviour_Label", true, 1, true);
        }
    }
}

[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnRepair))]
internal class Patch4
{
    private static bool Prefix(Panel_Inventory_Examine __instance)
    {
        if (Utils.IsRawMeat(__instance) == true)
        {
            Utils.SaltBehaviour(__instance.m_GearItem);
        }
        return true;
    }
}
*/
    }

}
