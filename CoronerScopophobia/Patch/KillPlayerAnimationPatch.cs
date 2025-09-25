using System;
using System.Collections;
using HarmonyLib;

namespace CoronerScopophobia.Patch {

    [HarmonyPatch(typeof(ShyGuy.AI.ShyGuyAI))]
    [HarmonyPatch("KillPlayerAnimation")]
    class KillPlayerAnimationPatch {
        public static void Postfix(ShyGuy.AI.ShyGuyAI __instance, ulong player, ref IEnumerator __result) {
            try {
		        Action prefixAction = () => { Console.WriteLine("--> beginning"); };
		        Action postfixAction = () => { HandleShyGuyKill(player); };
		        Action<object> preItemAction = (item) => { Console.WriteLine($"--> before {item}"); };
		        Action<object> postItemAction = (item) => { Console.WriteLine($"--> after {item}"); };
		        Func<object, object> itemAction = (item) =>
		        {
		        	var newItem = item + "+";
		        	Console.WriteLine($"--> item {item} => {newItem}");
		        	return newItem;
		        };
		        var myEnumerator = new SimpleEnumerator()
		        {
		        	enumerator = __result,
		        	prefixAction = prefixAction,
		        	postfixAction = postfixAction,
		        	preItemAction = preItemAction,
		        	postItemAction = postItemAction,
		        	itemAction = itemAction
		        };
		        __result = myEnumerator.GetEnumerator();
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in KillPlayerAnimationPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleShyGuyKill(ulong playerId) {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by Shy Guy! Processing...");
            var player = StartOfRound.Instance.allPlayerScripts[playerId];

            // if (player.isPlayerDead && player.causeOfDeath == CauseOfDeath.Unknown) {
            Plugin.Instance.PluginLogger.LogDebug($"Player {playerId} was killed by Shy Guy! Setting cause of death...");
            Coroner.API.SetCauseOfDeath(player, Plugin.Instance.SHY_GUY);
            // }
        }
    }

    class SimpleEnumerator : IEnumerable
	{
		public IEnumerator enumerator;
		public Action prefixAction, postfixAction;
		public Action<object> preItemAction, postItemAction;
		public Func<object, object> itemAction;
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
		public IEnumerator GetEnumerator()
		{
			prefixAction();
			while (enumerator.MoveNext())
			{
				var item = enumerator.Current;
				preItemAction(item);
				yield return itemAction(item);
				postItemAction(item);
			}
			postfixAction();
		}
	}
}