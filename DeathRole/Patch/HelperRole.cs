using System.Collections.Generic;
using UnityEngine;

namespace DeathRole.Patch {
    public static class HelperRole {
        public static List<PlayerControl> SpiritList = new List<PlayerControl>();

        public static bool IsSpirit(byte playerId) {
            bool IsSpirit = false;

            if (SpiritList != null)
                for (int i = 0; i < SpiritList.Count; i++)
                    if (playerId == SpiritList[i].PlayerId)
                        IsSpirit = true;

            return IsSpirit;
        }

        public static void ClearRoles() {
            if (SpiritList != null && SpiritList.Count > 0)
                SpiritList.Clear();
        }
    }
}