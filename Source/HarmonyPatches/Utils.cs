using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace XPathTurbo.HarmonyPatches
{
	public static class Utils
	{
		public static List<CodeInstruction> ModifySelectNodes(List<CodeInstruction> instructions)
		{
			var codeInstructionList = new List<CodeInstruction>();
			foreach (var instruction in instructions)
			{
				// For some reason this does not work... investigate.
				// if (instruction.operand as MethodInfo == Methods.OriginalSelectNodes)
				if (instruction.operand is MethodInfo methodOperand && instruction.opcode == OpCodes.Callvirt &&
				    methodOperand.ToString().Contains("SelectNodes"))
				{
					codeInstructionList.Add(new CodeInstruction(OpCodes.Call, (object)Methods.NewSelectNodes));
				}
				else
				{
					codeInstructionList.Add(instruction);
				}
			}

			return codeInstructionList;
		}
	}
}