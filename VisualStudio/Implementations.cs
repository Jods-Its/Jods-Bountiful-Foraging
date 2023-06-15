using MelonLoader;

namespace Bountiful_Foraging;
internal sealed class Implementations : MelonMod
{

    public override void OnInitializeMelon()
    {
        //Any initialization code goes here.
        //This method can be deleted if no initialization is required.
        MelonLogger.Msg("Foraging, engage!");
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
