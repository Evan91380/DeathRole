using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using DeathRole.Utility;
using System.Linq;
using UnityEngine;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch {

        public static bool Prefix([HarmonyArgument(0)] byte CallId, [HarmonyArgument(1)] MessageReader reader) {
            if (CallId == (byte) CustomRPC.SetSpirit) {
                HelperRole.SpiritList.Clear();
                List<byte> selectedPlayers = reader.ReadBytesAndSize().ToList();

                for (int i = 0; i < selectedPlayers.Count; i++) {
                    HelperRole.SpiritList.Add(PlayerControlUtils.FromPlayerId(selectedPlayers[i]));
                }

                return false;
            }

            return true;
        }
    }
}