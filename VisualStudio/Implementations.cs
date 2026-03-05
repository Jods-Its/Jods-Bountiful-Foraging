using MelonLoader;
using Bountiful_Foraging;
using Il2Cpp;
using UnityEngine;
using System.Reflection;

namespace Bountiful_Foraging;
internal sealed class Implementations : MelonMod
{
    private static List<string> cookableGear = new List<string>();
    private static bool loadedCookingTex;
    private static Material vanillaLiquidMaterial;

    public static AssetBundle bundleBFIcons;
    public override void OnInitializeMelon()
    {
        BFUtils.LaunchBF();
        //LoadEmbeddedAssetBundle();
        //BFUtils.BFIconsInitialize();
        CheckMods();
    }
    public override void OnSceneWasLoaded(int buildIndex, string sceneName) // "The Funny Setting". I'm not proud of this, but I'm also evil so...
    {
        if (sceneName.ToLowerInvariant().Contains("cave") && (sceneName != "DamCaveTransitionZone" || sceneName != "MountainTownCaveB") && (Settings.instance.activeBear == true))
        {
            if (Utils.RollChance(5f))
            {
                uConsole.RunCommandSilent("spawn_bear");
            }           
        }
    }
    public override void OnSceneWasInitialized(int level, string name) //Cooking textures handling 
    {
        if (ModComponent.Public.IsLoaded())
        {
            if (!loadedCookingTex) // adding pot cooking textures
            {
                cookableGear.Add("BirdEggRaw");
                cookableGear.Add("UncookedOmelette");
                cookableGear.Add("UncookedForagerOmelette");
                cookableGear.Add("UncookedSpanishOmelette");
                cookableGear.Add("UncookedForagerStew");
                Material potMat;
                GameObject potGear;

                for (int i = 0; i < cookableGear.Count; i++)
                {
                    potGear = GearItem.LoadGearItemPrefab("GEAR_" + cookableGear[i]).gameObject;

                    if (potGear == null) continue;
                    Texture? tex = potGear.transform.Find("POTtexture")?.GetComponent<MeshRenderer>()?.material?.mainTexture;

                    if (tex == null)
                    {
                        MelonLogger.Msg(System.ConsoleColor.Red, "Jods, you forgor 💀");
                        return;
                    }

                    potMat = InstantiateLiquidMaterial();
                    potMat.name = ("CKN_" + cookableGear[i] + "_MAT");

                    potMat.mainTexture = tex;
                    potMat.SetTexture("_Main_texture2", tex);

                    potGear.GetComponent<Cookable>().m_CookingPotMaterialsList = new Material[1] { potMat };
                }

                loadedCookingTex = true;
            }
        }

    }
    public static Material InstantiateLiquidMaterial()
    {
        if (!vanillaLiquidMaterial)
        {
            vanillaLiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);

            vanillaLiquidMaterial.name = "Liquid";
        }
        return new Material(vanillaLiquidMaterial);
    }
    public static void LoadEmbeddedAssetBundle()
    {
        MemoryStream memoryStream1;

        System.IO.Stream stream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bountiful_Foraging.Bundles.bficons");

        memoryStream1 = new MemoryStream((int)stream1.Length);
        stream1.CopyTo(memoryStream1);

        bundleBFIcons = AssetBundle.LoadFromMemory(memoryStream1.ToArray());

        bundleBFIcons.hideFlags = HideFlags.HideAndDontSave;
        GameObject.DontDestroyOnLoad(bundleBFIcons);
    }
    public void CheckMods()
    {
        for (int i = 0; i < RegisteredMelons.Count; i++)
        {
            string modName = RegisteredMelons[i].Info.Name;
            if (modName == "ThingsToDo")
            {
                BFUtils.thingsToDoInstalled = true;
                MelonLogger.Msg(System.ConsoleColor.Cyan, "ThingsToDo compatibility patch applied.");
            }
            
        }
    }
    internal static void Log(string message, params object[] parameters)
    {
        MelonLogger.Msg($"{message}", parameters);
    }
    internal static void LogWarning(string message, params object[] parameters)
    {
        MelonLogger.Warning($"{message}", parameters);
    }
    internal static void LogError(string message, params object[] parameters)
    {
        MelonLogger.Error($"{message}", parameters);
    }
 
}

