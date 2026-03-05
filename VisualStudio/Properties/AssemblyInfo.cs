using Bountiful_Foraging;
using MelonLoader;
using System.Reflection;


[assembly: AssemblyTitle(BuildInfo.ModName)]
[assembly: AssemblyCopyright($"Created by ModAuthor")]

[assembly: AssemblyVersion(BuildInfo.ModVersion)]
[assembly: AssemblyFileVersion(BuildInfo.ModVersion)]
[assembly: MelonInfo(typeof(Implementations), BuildInfo.ModName, BuildInfo.ModVersion, BuildInfo.ModAuthor)]

[assembly: MelonGame("Hinterland", "TheLongDark")]
[assembly: MelonPriority(800)]

internal static class BuildInfo
{
	internal const string ModName = "Bountiful Foraging";
	internal const string ModAuthor = "Jods_Its";
	internal const string ModVersion = "1.2.0";
}