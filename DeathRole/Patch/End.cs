using HarmonyLib;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
    public static class EndGamePatch {

        public static void Prefix(AmongUsClient __instance) {
            EndGameCommons.ResetGlobalVariable();
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public static class EndGameManagerPatch {
        public static bool Prefix(EndGameManager __instance) {
            EndGameCommons.ResetGlobalVariable();

            return true;
        }
    }

    public static class EndGameCommons {
        public static void ResetGlobalVariable() {
            PlayerUpdatePatch.PlayerIsDead = false;
            MeetingHudPopulateButtonsPatch.SpiritHasVoteds.Clear();
        }
    }
}