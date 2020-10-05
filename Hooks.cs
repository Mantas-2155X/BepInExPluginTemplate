using HarmonyLib;

using System.Linq;

using System.Reflection;
using System.Reflection.Emit;

using System.Collections;
using System.Collections.Generic;

namespace BepInExPluginTemplate
{
    public static class Hooks
    {
        public static void PatchSpecial(Harmony harmony)
        {
            var iteratorType = typeof(ExampleType).GetNestedType("<ExampleMethod>x__IteratorX", AccessTools.all);
            var iteratorMethod = AccessTools.Method(iteratorType, "MoveNext");
            var transpiler = new HarmonyMethod(typeof(Hooks), nameof(type_method_patch));
            harmony.Patch(iteratorMethod, null, null, transpiler);
        }
        
        // Prefix (normal, IEnumerator) / Postfix (normal)
        [HarmonyPrefix, HarmonyPatch(typeof(type), "method")]
        private static void type_method_patch(type __instance)
        {
            
        }
        
        // Postfix IEnumerator method
        [HarmonyPostfix, HarmonyPatch(typeof(type), "method")]
        private static void type_method_patch(ref object __result)
        {
            __result = new[] { __result, patch() }.GetEnumerator();

            IEnumerator patch()
            {
                yield break;
            }
        }
        
        // Transpiler (normal, MoveNext)
        //[HarmonyTranspiler, HarmonyPatch(typeof(type), "method")]
        public static IEnumerable<CodeInstruction> type_method_patch(IEnumerable<CodeInstruction> instructions)
        {
            var il = instructions.ToList();
            
            var index = il.FindIndex(instruction => instruction.opcode == OpCodes.Call && (instruction.operand as MethodInfo)?.Name == "ExampleMethod");
            if (index <= 0)
            {
                // no index found
                return il;
            }
            
            // do stuff here

            return il;
        }
    }
}