using HarmonyLib;
using UnityEngine;

namespace CustomizeSweeperRange
{
	public class CustomizeSweeperRangePatches
	{
		[HarmonyPatch(typeof(SolidTransferArmConfig))]
		[HarmonyPatch(nameof(SolidTransferArmConfig.DoPostConfigureComplete))]
		public static class CustomizeSweeperRange_SolidTransferArmConfig_DoPostConfigureComplete_Patch
		{
			public static void Postfix(ref GameObject go)
			{
				if (GameOnLoadPatch.Settings == null)
				{
					GameOnLoadPatch.ReadSettings();
				}

				go.AddOrGet<SolidTransferArm>().pickupRange = GameOnLoadPatch.Settings.SweeperRange;
			}
		}

		[HarmonyPatch(typeof(SolidTransferArmConfig))]
		[HarmonyPatch("AddVisualizer")]
		public static class SolidTransferArmConfig_AddVisualizer_Patch
		{
			public static void CustomizeSweeperRange_Postfix(ref GameObject prefab, ref bool movable)
			{
				Debug.Log("execute");
				if (GameOnLoadPatch.Settings == null)
				{
					GameOnLoadPatch.ReadSettings();
				}

				var CustomizeStationaryChoreRangeVisualizer = prefab.AddOrGet<StationaryChoreRangeVisualizer>();
				CustomizeStationaryChoreRangeVisualizer.x = - GameOnLoadPatch.Settings.SweeperRange;
				CustomizeStationaryChoreRangeVisualizer.y = - GameOnLoadPatch.Settings.SweeperRange;
				CustomizeStationaryChoreRangeVisualizer.width = GameOnLoadPatch.Settings.SweeperRange * 2 + 1;
				CustomizeStationaryChoreRangeVisualizer.height = GameOnLoadPatch.Settings.SweeperRange * 2 + 1;
				CustomizeStationaryChoreRangeVisualizer.movable = movable;
			}
		}
	}
}
