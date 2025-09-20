using BepInEx;

using HarmonyLib;
using BepInEx.Logging;
using Coroner;
using CoronerSirenHead.Patch;

namespace CoronerSirenHead
{
    public static class PluginInfo
    {
        public const string PLUGIN_ID = "coroner.sirenhead";
        public const string PLUGIN_NAME = "Coroner - Siren Head";
        public const string PLUGIN_VERSION = "1.0.0";
        public const string PLUGIN_GUID = "Turkeysteaks.coroner.sirenhead";
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public ManualLogSource PluginLogger;
        public string KEY = "DeathEnemySirenHead";
        public AdvancedCauseOfDeath SIREN_HEAD;

        private void Awake()
        {
            Instance = this;

            PluginLogger = Logger;

            // Apply Harmony patches (if any exist)
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            harmony.PatchAll(typeof(SirenHeadEatPlayerPatch));

            // Plugin startup logic
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loaded!");
            if(!API.IsRegistered(KEY))
            {
                SIREN_HEAD = API.Register(KEY);
            }
        }
    }
}
