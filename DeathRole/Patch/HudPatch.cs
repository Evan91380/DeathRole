using HarmonyLib;
using UnityEngine;

namespace DeathRole.Patch {

    public static class HudPatch {
        public static void UpdateMeetingHUD(MeetingHud __instance) {
            foreach (PlayerVoteArea player in __instance.playerStates) {
                if (PlayerControl.AllPlayerControls != null && PlayerControl.AllPlayerControls.Count > 1) {
                    if (PlayerControl.LocalPlayer != null) {                    
                        foreach (var playerControl in PlayerControl.AllPlayerControls) {
                            if (HelperRole.IsAstral(playerControl.PlayerId) && playerControl.Data.IsDead && PlayerControl.LocalPlayer.Data.IsDead) {
                                string playerName = playerControl.Data.PlayerName;

                                if (playerName == player.NameText.Text)
                                    player.NameText.Color = new Color(0.356f, 0f, 0.760f, 1f);
                            }
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudUpdatePatch {
        public static void Postfix(HudManager __instance) {
            if (MeetingHud.Instance != null)
                HudPatch.UpdateMeetingHUD(MeetingHud.Instance);

            if (PlayerControl.LocalPlayer != null) {
                if (PlayerControl.AllPlayerControls != null && PlayerControl.AllPlayerControls.Count > 1) {
                    foreach (var playerControl in PlayerControl.AllPlayerControls) {
                        if (HelperRole.IsAstral(playerControl.PlayerId) && playerControl.Data.IsDead && PlayerControl.LocalPlayer.Data.IsDead) {
                            playerControl.nameText.Color = new Color(0.749f, 0f, 0.839f, 1f);
                        }
                    }
                }
            }
        }
    }
}