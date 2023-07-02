using ModSettings;

namespace Bountiful_Foraging
{
    internal class Settings : JsonModSettings
    {
        public static Settings instance = new Settings();

        [Section("Spawn chances")]
        [Name("Fir Cone")]
        [Description("Tweaks the cance of fir cones spawn. Fir cones are part of the sticks spawners. Default 15%")]
        [Slider(0f, 30f, 301)]
        public float coneChance = 15f;

        [Name("Crow Carcass")]
        [Description("Tweaks the cance of crow carcasses spawn. Crow carcasses are part of the crow feather spawners. Default 10%")]
        [Slider(0f, 30f, 301)]
        public float crowChance = 10f;

        [Name("Halite (Small)")]
        [Description("Tweaks the cance of halite (small) to spawn. Halite pieces are part of the coal spawners. Default 10%")]
        [Slider(0f, 30f, 301)]
        public float saltChance = 10f;
    }
}
