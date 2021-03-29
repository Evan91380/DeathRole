using HarmonyLib;
using System;
using System.Diagnostics;
using UnityEngine;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetTasks))]
    class TasksPatch {
        /*public static void Postfix(PlayerControl __instance) {
            if (PlayerControl.LocalPlayer != null) {
                if (HelperRole.AstralList != null && HelperRole.IsAstral(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead) {
                    ImportantTextTask ImportantTasks = new GameObject("AstralTasks").AddComponent<ImportantTextTask>();
                    ImportantTasks.transform.SetParent(__instance.transform, false);
                    ImportantTasks.Text = "[5b00C2FF]You can vote while being dead![]";
                    __instance.myTasks.Insert(0, ImportantTasks);
                }
            }
        }*/
    }
}