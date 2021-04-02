using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using UnhollowerBaseLib;

namespace DeathRole.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch {
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameData.PlayerInfo> infected) {
            List<PlayerControl> playersList = PlayerControl.AllPlayerControls.ToArray().ToList();
            //List<PlayerControl> crewmateList = playersList.FindAll(x => !x.Data.IsImpostor).ToArray().ToList();
            HelperRole.ClearRoles();


            int randomtkt = new Random().Next(0, 100);

            if (playersList != null && playersList.Count > 0 && DeathRole.EnableSpirit.GetValue() >= randomtkt) {
                MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SetSpirit, SendOption.None, -1);
                List<byte> playerSelected = new List<byte>();

                for (int i = 0; i < DeathRole.NumberSpirit.GetValue(); i++) {
                    if (playersList != null && playersList.Count > 0) {
                        Random random = new Random();
                        PlayerControl selectedPlayer = playersList[random.Next(0, playersList.Count)];
                        HelperRole.SpiritList.Add(selectedPlayer);
                        playersList.Remove(selectedPlayer);
                        playerSelected.Add(selectedPlayer.PlayerId);
                        DeathRole.Logger.LogInfo($"Player:  {selectedPlayer.nameText.Text}");
                    }
                }

                messageWriter.WriteBytesAndSize(playerSelected.ToArray());
                AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
            }
        }
    }
}