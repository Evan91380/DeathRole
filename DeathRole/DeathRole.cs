using System;
using System.Linq;
using System.Net;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Essentials.Options;
using HarmonyLib;
using Reactor;
using UnhollowerBaseLib;

namespace DeathRole{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class DeathRole : BasePlugin {
        public const string Id = "fr.evan.deathrole";
        public static ManualLogSource Logger;

        public Harmony Harmony { get; } = new Harmony(Id);

        // Spirit
        public static CustomNumberOption EnableSpirit = CustomOption.AddNumber("Spirit", 0f, 0f, 100f, 5f);
        public static CustomNumberOption NumberSpirit = CustomOption.AddNumber("Number Spirit", 1f, 1f, 10f, 1f);
        public static CustomToggleOption CanVoteMultipleTime = CustomOption.AddToggle("Can Vote Multiple time", false);

        public override void Load() {
        
            Harmony.PatchAll();
            Logger = Log;
            Logger.LogInfo("DeathRole Mods is ready !");

        }
    }
}
