using BepInEx;

using HarmonyLib;
using BepInEx.Logging;
using Coroner;

namespace CoronerMimics
{
    public static class PluginInfo
    {
        public const string PLUGIN_ID = "coroner.mimics";
        public const string PLUGIN_NAME = "Coroner - Mimics";
        public const string PLUGIN_VERSION = "1.0.0";
        public const string PLUGIN_GUID = "com.elitemastereric.coroner.mimics";
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public ManualLogSource PluginLogger;

        public AdvancedCauseOfDeath MIMIC;

        private void Awake()
        {
            Instance = this;

            PluginLogger = Logger;

            // Apply Harmony patches (if any exist)
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            // Plugin startup logic
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loaded!");

            MIMIC = AdvancedCauseOfDeath.Build("DeathEnemyMimic");
        }
    }
}
