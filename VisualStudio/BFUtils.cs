using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bountiful_Foraging
{
    internal class BFUtils
    {
        public static GameObject cones = Addressables.LoadAssetAsync<GameObject>("GEAR_4FirCone").WaitForCompletion();
        public static GameObject crows = Addressables.LoadAssetAsync<GameObject>("GEAR_CrowCarcass").WaitForCompletion();
        public static GameObject feather = Addressables.LoadAssetAsync<GameObject>("GEAR_CrowFeather").WaitForCompletion();
        public static GameObject salt = Addressables.LoadAssetAsync<GameObject>("GEAR_HaliteSmall").WaitForCompletion();
        public static GameObject skull1 = Addressables.LoadAssetAsync<GameObject>("GEAR_SkullStag").WaitForCompletion();
        public static GameObject skull2 = Addressables.LoadAssetAsync<GameObject>("GEAR_SkullBear").WaitForCompletion();
        public static string FindAnimal (string gearItemName)
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
    }
}
