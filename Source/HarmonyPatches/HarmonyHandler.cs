using System;

namespace XPathTurbo.HarmonyPatches
{
	/// <summary>
	/// Handles the application of harmony patches.
	/// </summary>
	public static class HarmonyHandler
	{
		/// <summary>
		/// Apply harmony patches to the game.
		/// </summary>
		public static void Initialize()
		{
			try
			{
				var harmonyInstance = new HarmonyLib.Harmony(XPathTurbo.Mod.XPathTurbo.PackageId);
				harmonyInstance.PatchAll();
				Report.Debug("Harmony patching applied.");
			}
			catch (Exception exception)
			{
				Report.Error("Harmony patching failed:");
				Report.Error($"{exception.ToString()}");
			}
		}
	}
}