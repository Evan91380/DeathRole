using System.Collections.Generic;
using UnityEngine;

namespace DeathRole.Patch {
    public static class HelperRole {
        public static List<PlayerControl> AstralList = new List<PlayerControl>();

        public static bool IsAstral(byte playerId) {
            bool IsAstral = false;

            if (AstralList != null)
                for (int i = 0; i < AstralList.Count; i++)
                    if (playerId == AstralList[i].PlayerId)
                        IsAstral = true;

            return IsAstral;
        }

        public static void ClearRoles() {
            if (AstralList != null && AstralList.Count > 0)
                AstralList.Clear();
        }
    }
}