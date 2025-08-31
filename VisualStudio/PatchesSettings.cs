using Il2Cpp;
using Il2CppTLD.Gear;
using HarmonyLib;
using MelonLoader;

namespace Bountiful_Foraging
{
    internal class PatchesSettings
    {
        [HarmonyPatch] // ModComponent patch of gear spawns
        class ManageSpawnsForaging
        {
            public static System.Reflection.MethodBase TargetMethod() 
            {
                var type = AccessTools.TypeByName("ModComponent.Mapper.ZipFileLoader");
                return AccessTools.FirstMethod(type, method => method.Name.Contains("TryHandleTxt"));
            }
            public static bool Prefix(string zipFilePath, string internalPath, ref string text, ref bool __result)
            {
                if (zipFilePath.EndsWith("Bountiful_Foraging.modcomponent"))
                {
                    string fileName = internalPath.Replace("gear-spawns/", "").Replace(".txt", "");

                    if (Settings.instance.noOrca && fileName == "Orcas")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }

                    if (Settings.instance.noChunk && fileName == "Chunks")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }

                    if (Settings.instance.noHive && fileName == "Hives")
                    {
                        MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                        text = "";
                    }
                }

                return true;
            }
        }
    }
}
