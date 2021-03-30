using HarmonyLib;
using Hazel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using InnerNet;

namespace DeathRole.Patch {

    [HarmonyPatch]
    public static class MeetingHudPopulateButtonsPatch {
        public static bool AstralHasVoted = false;
        
        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Awake))]
        class MeetingServerStartPatch {
            static void Prefix(MeetingHud __instance) {
               if(HelperRole.IsAstral(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead) {
                    
               }
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
        class MeetingEndPatch {

            static void Prefix(MeetingHud __instance, [HarmonyArgument(0)] sbyte suspectIdx) {
               if(HelperRole.IsAstral(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead) {

                    __instance.CmdCastVote(PlayerControl.LocalPlayer.PlayerId, suspectIdx);
               }
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CmdCastVote))]
        class CmdCastVotePatch
        {
            static void Prefix(MeetingHud __instance, [HarmonyArgument(0)] byte srcPlayerId, [HarmonyArgument(1)] sbyte suspectPlayerId)
            {
                if (HelperRole.IsAstral(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead)
                {
                    DeathRole.Logger.LogInfo("CmbCastVote");

                   
                }
            }

        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CastVote))]
        class CastVoteNormal
        {
            static void Prefix(MeetingHud __instance, [HarmonyArgument(0)] byte srcPlayerId, [HarmonyArgument(1)] sbyte suspectPlayerId)  {
                if (HelperRole.IsAstral(srcPlayerId)) {
                    DeathRole.Logger.LogInfo("CastVote");

                    foreach (PlayerVoteArea player in __instance.playerStates)
                    {
                        if (player.TargetPlayerId == srcPlayerId)
                        {
                            if (DeathRole.CanVoteMultipleTime.GetValue() == false && !AstralHasVoted)
                            {
                                DeathRole.Logger.LogInfo("Pas encore voté : " + player.votedFor);
                                player.didVote = true;
                                player.votedFor = suspectPlayerId;
                                player.Flag.enabled = true;
                                DeathRole.Logger.LogInfo("Vote pour : " + player.votedFor);

                                AstralHasVoted = true;
                            }
                            else if (DeathRole.CanVoteMultipleTime.GetValue())
                            {
                                player.didVote = true;
                                player.votedFor = suspectPlayerId;
                                player.Flag.enabled = true;
                                DeathRole.Logger.LogInfo("Vote pours : " + player.votedFor);
                            }

                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayerVoteArea), nameof(PlayerVoteArea.Select))]
        class SelectVoteArea
        {
            static void Prefix(PlayerVoteArea __instance)
            {
                if (HelperRole.IsAstral(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead)  {
                    MeetingHud MeetingInstance = __instance.Parent;

                    foreach (PlayerVoteArea player in MeetingInstance.playerStates) {
                        player.Buttons.SetActive(false);
                    }
                    
                    DeathRole.Logger.LogInfo("select");
                    if (!__instance.isDead && __instance.Parent.state != MeetingHud.VoteStates.Discussion && __instance.didVote != true)
                         __instance.Buttons.SetActive(true);
                }
            }
        }

    }
}