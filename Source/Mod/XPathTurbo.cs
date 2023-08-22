using UnityEngine;
using Verse;
using XPathTurbo.HarmonyPatches;

namespace XPathTurbo.Mod
{
	public class XPathTurbo : Verse.Mod
	{
		public const string PackageId = "joseasoler.xpathturbo";

		/// <summary>
		/// Handles the initialization of every component of this mod.
		/// </summary>
		/// <param name="content">Content pack data of this mod.</param>
		public XPathTurbo(ModContentPack content) : base(content)
		{
			HarmonyHandler.Initialize();
		}

		/// <summary>
		/// Initialization steps that must be taken after the game has finished loading.
		/// </summary>
		private void InitializeWhenLoadingFinished()
		{
			GetSettings<Settings>();
		}

		/// <summary>
		/// Name of the mod in the settings list.
		/// </summary>
		/// <returns>Name of the mod in the settings list.</returns>
		public override string SettingsCategory()
		{
			return Report.Name;
		}

		/// <summary>
		/// Contents of the mod settings window.
		/// </summary>
		/// <param name="inRect">Available area for drawing the settings.</param>
		public override void DoSettingsWindowContents(Rect inRect)
		{
			var listing = new Listing_Standard();
			listing.Begin(inRect);

			listing.CheckboxLabeled("XT_DebugLogLabel".Translate(), ref Settings.Values.DebugLog,
				"PF_DebugLogHover".Translate());
			

			listing.Gap();
			var buttonsRect = listing.GetRect(30.0F);
			var buttonWidth = buttonsRect.width / 5.0F;

			var resetRect = new Rect(buttonsRect.width - buttonWidth, buttonsRect.y, buttonWidth, buttonsRect.height);
			if (Widgets.ButtonText(resetRect, "XT_ResetSettingsLabel".Translate()))
			{
				Settings.Reset();
			}

			listing.End();
			base.DoSettingsWindowContents(inRect);
		}
	}
}