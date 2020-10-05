using BepInEx;
using HarmonyLib;

using UnityEngine;

namespace BepInExPluginTemplate
{
    [BepInPlugin(nameof(BepInExPluginTemplate), nameof(BepInExPluginTemplate), VERSION)]
    public class BepInExPluginTemplate : BaseUnityPlugin
    {/*caret*/
        public const string VERSION = "1.0.0";

        private void Awake()
        {
            var harmony = new Harmony("BepInExPluginTemplate");
            harmony.PatchAll(typeof(Hooks));
            
            Hooks.PatchSpecial(harmony);
        }

        private void Update()
        {
            
        }
    }
}