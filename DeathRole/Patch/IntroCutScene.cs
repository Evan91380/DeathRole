using HarmonyLib;
using UnityEngine;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public static class IntroCutScenePatch {
        public static void Postfix(IntroCutscene.CoBegin__d __instance) {
            byte localPlayerId = PlayerControl.LocalPlayer.PlayerId;
            bool isImpostor = PlayerControl.LocalPlayer.Data.IsImpostor;

            // Astral
           /* if (HelperRole.IsAstral(localPlayerId)) {
                __instance.__this.Title.Text = "Astral";
                __instance.__this.Title.Color = new Color(0.356f, 0f, 0.760f, 1f);
                __instance.__this.ImpostorText.Text = "You can vote while being dead!";
                __instance.__this.BackgroundBar.material.color = new Color(0.356f, 0f, 0.760f, 1f);
            } */
        }
    }
}