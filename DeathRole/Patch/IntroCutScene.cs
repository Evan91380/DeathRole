using HarmonyLib;
using UnityEngine;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public static class IntroCutScenePatch {
        public static void Postfix(IntroCutscene.CoBegin__d __instance) {
            byte localPlayerId = PlayerControl.LocalPlayer.PlayerId;
            bool isImpostor = PlayerControl.LocalPlayer.Data.IsImpostor;

        }
    }
}