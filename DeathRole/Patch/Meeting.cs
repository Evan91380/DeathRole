﻿using HarmonyLib;
using Hazel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using InnerNet;
using DeathRole.Utility;

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

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
        class MeetingUpdatePatch
        {
            static void Prefix(MeetingHud __instance)
            {
                if (HelperRole.IsAstral(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer.Data.IsDead)
                {
                    if (!__instance.DidVote(PlayerControl.LocalPlayer.Data.PlayerId) && __instance.discussionTimer == 0)
                    {
                        //__instance.SkipVoteButton.SetEnabled();
                        __instance.SkipVoteButton.gameObject.SetActive(true);
                    }

                        

                }
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
        class MeetingVotePatch {

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
                    foreach (PlayerVoteArea player in __instance.playerStates)
                    {
                        player.ClearButtons();
                        __instance.SkipVoteButton.gameObject.SetActive(false);
                        if (player.TargetPlayerId == PlayerControl.LocalPlayer.Data.PlayerId){
                        player.didVote = true;
                        player.votedFor = suspectPlayerId;
                        }
                    }

                }
            }

        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CastVote))]
        class CastVoteNormal
        {
            static void Prefix(MeetingHud __instance, [HarmonyArgument(0)] byte srcPlayerId, [HarmonyArgument(1)] sbyte suspectPlayerId)  {
                if (HelperRole.IsAstral(srcPlayerId) && PlayerControlUtils.FromPlayerId(srcPlayerId).Data.IsDead) {

                    foreach (PlayerVoteArea player in __instance.playerStates)
                    {
                        if (player.TargetPlayerId == srcPlayerId)
                        {
                            if (!DeathRole.CanVoteMultipleTime.GetValue() && !AstralHasVoted)
                            {
                                player.didVote = true;
                                player.votedFor = suspectPlayerId;
                                //player.Flag.enabled = true;

                                AstralHasVoted = true;
                            }
                            else if (DeathRole.CanVoteMultipleTime.GetValue())
                            {
                                player.didVote = true;
                                player.votedFor = suspectPlayerId;
                                //player.Flag.enabled = true;
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
                    
                    if (!__instance.isDead && __instance.Parent.state != MeetingHud.VoteStates.Discussion && !MeetingInstance.DidVote(PlayerControl.LocalPlayer.PlayerId) && !AstralHasVoted)
                         __instance.Buttons.SetActive(true);
                }
            }
        }

    }
}