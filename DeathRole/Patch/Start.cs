using HarmonyLib;
using UnityEngine;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    class GameEndedPatch {
        public static void Postfix(ShipStatus __instance) {
            PlayerUpdatePatch.PlayerIsDead = false;
        }
    }
}