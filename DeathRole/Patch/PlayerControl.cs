using HarmonyLib;
using UnityEngine;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class PlayerUpdatePatch {
        public static bool PlayerIsDead = false;

        public static void Postfix(PlayerControl __instance) {
            if (__instance.Data.IsDead && !PlayerIsDead) {
                PlayerIsDead = true;

                if (HelperRole.SpiritList != null && HelperRole.IsSpirit(__instance.PlayerId) && __instance.Data.IsDead) {
                    ImportantTextTask ImportantTasks = new GameObject("SpiritTasks").AddComponent<ImportantTextTask>();
                    ImportantTasks.transform.SetParent(__instance.transform, false);
                    ImportantTasks.Text = "[5b00C2FF]You can vote while being dead![]";
                    __instance.myTasks.Insert(0, ImportantTasks);
                }
            }
        }
    }
}