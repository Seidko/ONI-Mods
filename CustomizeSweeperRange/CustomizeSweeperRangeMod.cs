using HarmonyLib;
using KMod;
using Newtonsoft.Json;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using PeterHan.PLib.Database;

namespace CustomizeSweeperRange
{
    public static class ModOption
    {
        public static RangeOptions Options { get; set; }

        public static void ReadOptions()
        {
            Options = POptions.ReadSettings<RangeOptions>();
            if (Options == null)
            {
                Options = new RangeOptions();
            }
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [ModInfo("https://github.com/Seidko/ONI-Mods", "preview.png")]
    [RestartRequired]
    public class RangeOptions
    {
        [Option("CustomizeSweeperRange.STRINGS.OPTIONS.SweeperRange.NAME", "CustomizeSweeperRange.STRINGS.OPTIONS.SweeperRange.DESC")]
        [Limit(1, 100)]
        [JsonProperty]
        public int SweeperRange { get; set; }

        public RangeOptions()
        {
            SweeperRange = 4;
        }
    }

    public class CustomizeSweeperRangeMod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(RangeOptions));
            new PLocalization().Register();
            ModOption.ReadOptions();
        }
    }
}
