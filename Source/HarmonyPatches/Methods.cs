using System;
using System.Reflection;
using System.Xml;
using HarmonyLib;
using Verse;

namespace XPathTurbo.HarmonyPatches
{
	[StaticConstructorOnStartup]
	public static class Methods
	{
		public static readonly MethodInfo OriginalSelectNodes =
			AccessTools.Method(typeof(XmlDocument), nameof(XmlDocument.SelectNodes),
				new[] { typeof(string) });

		public static readonly MethodInfo NewSelectNodes = AccessTools.Method(typeof(Wmhelp.XPath2.XmlNodeExtensions),
			nameof(Wmhelp.XPath2.XmlNodeExtensions.XPath2SelectNodes), new[] { typeof(XmlNode), typeof(string) });


		static Methods()
		{
			foreach (var field in typeof(Methods).GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				if (field.FieldType != typeof(MethodInfo))
				{
					Report.Error($"Transpiler methods: Field {field.Name} must be a MethodInfo instance.");
				}
				else if (field.GetValue((object)null) == null)
				{
					Report.Error($"Transpiler methods: Field {field.Name} must have a value.");
				}
			}
		}
	}
}