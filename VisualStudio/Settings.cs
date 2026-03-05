using ModSettings;

namespace Bountiful_Foraging
{
    internal class Settings : JsonModSettings
    {
        public static Settings instance = new Settings();

        [Section("Animals")]
        [Name("Disable Timberwolf Pelt")]
        [Description("Prevents Timberwolves from having harvestable custom pelt. If disabled, Timberwolves will have regular Wolf pelt instead. Needs game restart. Default = No")]
        public bool noPelt = false;

        [Name("Disable Orca Carcasses")]
        [Description("Prevents orca carcasses from spawning. Needs game restart. Default = No")]
        public bool noOrca = false;

        [Name("Disable Crow Carcasses")]
        [Description("Prevents crow carcasses from spawning. Default = No")]
        public bool noCrow = false;

        [Name("Crow Carcass Spawn Chance")]
        [Description("Tweaks the chance of crow carcasses spawn. Crow carcasses are part of the crow feather spawners. Default 10%")]
        [Slider(1f, 30f, 30)]
        public float crowChance = 10f;

        [Section("Natural Resources")]
        [Name("Disable Hives")]
        [Description("Prevents frozen bee hives from spawning. Needs game restart. Default = No")]
        public bool noHive = false;

        [Name("Disable Nests")]
        [Description("Prevents orca carcasses from spawning. Default = No")]
        public bool noNest = false;

        [Name("Disable Fir Cones")]
        [Description("Prevents fir cones from spawning. Default = No")]
        public bool noCone = false;

        [Name("Fir Cone Spawn Chance")]
        [Description("Tweaks the chance of fir cones spawn. Fir cones are part of the sticks spawners. Default 15%")]
        [Slider(1f, 30f, 30)]
        public float coneChance = 15f;

        [Name("Disable Chunks")]
        [Description("Prevents halite and coal chunks from spawning. Needs game restart. Default = No")]
        public bool noChunk = false;

        [Name("Halite (Small) Spawn Chance")]
        [Description("Tweaks the chance of halite (small) to spawn. Halite pieces are part of the coal spawners. Default 10%")]
        [Slider(0f, 30f, 31)]
        public float saltChance = 10f;

        [Section("Miscellaneous")]
        [Name("The Funny Setting")]
        [Description("Toggles the funny.")]
        public bool activeBear = true;
    }
}
