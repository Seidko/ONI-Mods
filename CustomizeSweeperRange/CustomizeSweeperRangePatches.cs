using HarmonyLib;
using UnityEngine;

namespace CustomizeSweeperRange
{
	public class CustomizeSweeperRangePatches
	{
		[HarmonyPatch(typeof(SolidTransferArmConfig))]
		[HarmonyPatch(nameof(SolidTransferArmConfig.DoPostConfigureComplete))]
		public static class Customize_SweeperRange_SolidTransferArmConfig_DoPostConfigureComplete_Patch
		{
			public static void Postfix(ref GameObject go)
			{
				if (ModOption.Options == null) ModOption.ReadOptions();

				go.AddOrGet<SolidTransferArm>().pickupRange = ModOption.Options.SweeperRange;
			}
		}

		[HarmonyPatch(typeof(SolidTransferArmConfig))]
		[HarmonyPatch("AddVisualizer")]
		public static class Customize_SolidTransferArmConfig_AddVisualizer_Patch
		{
			public static void Postfix(ref GameObject prefab, ref bool movable)
			{
				if (ModOption.Options == null) ModOption.ReadOptions();

				var stationaryChoreRangeVisualizer = prefab.AddOrGet<StationaryChoreRangeVisualizer>();
				stationaryChoreRangeVisualizer.x = -ModOption.Options.SweeperRange;
				stationaryChoreRangeVisualizer.y = -ModOption.Options.SweeperRange;
				stationaryChoreRangeVisualizer.width = ModOption.Options.SweeperRange * 2 + 1;
				stationaryChoreRangeVisualizer.height = ModOption.Options.SweeperRange * 2 + 1;
				stationaryChoreRangeVisualizer.movable = movable;
			}
		}
	}
}
