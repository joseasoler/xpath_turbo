using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace XPathTurbo.HarmonyPatches
{
	[HarmonyPatch]
	internal static class PatchOperation_ApplyWorker_Patch
	{
		[HarmonyTargetMethods]
		static IEnumerable<MethodInfo> TargetMethods()
		{
			const string MethodName = "ApplyWorker";

			// ToDo XML Extensions support
			var types = new HashSet<Type>
			{
				typeof(PatchOperationAdd),
				typeof(PatchOperationAddModExtension),
				typeof(PatchOperationAttributeAdd),
				typeof(PatchOperationAttributeRemove),
				typeof(PatchOperationAttributeSet),
				typeof(PatchOperationConditional),
				typeof(PatchOperationConditional),
				typeof(PatchOperationInsert),
				typeof(PatchOperationRemove),
				typeof(PatchOperationReplace),
				typeof(PatchOperationSetName),
				typeof(PatchOperationTest),
			};

			foreach (var type in types)
			{
				foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
					         BindingFlags.Instance | BindingFlags.Static))
				{
					if (method.Name.Contains(MethodName))
					{
						yield return method;
					}
				}
			}
		}

		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			return Utils.ModifySelectNodes(instructions.ToList());
		}
	}
}