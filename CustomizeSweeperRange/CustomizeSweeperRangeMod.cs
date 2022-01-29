using HarmonyLib;
using KMod;
using Newtonsoft.Json;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using PeterHan.PLib.Database;

namespace CustomizeSweeperRange
{
    public class CustomizeSweeperRangeMod: UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(RangeOptions));
            new PLocalization().Register();
        }
    }

    [HarmonyPatch(typeof(Game), "Load")]
    public static class GameOnLoadPatch
    {
        public static RangeOptions Settings { get; private set; }

        public static void Prefix()
        {
            ReadSettings();
        }
        public static void ReadSettings()
        {

            Settings = POptions.ReadSettings<RangeOptions>();
            if (Settings == null)
            {
                Settings = new RangeOptions();
            }

        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [ModInfo("https://github.com/Seidko/ONI-Mods", "preview.png")]
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
}
