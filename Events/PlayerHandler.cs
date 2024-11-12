using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Linq;
using UnityEngine;

namespace Scp_507.Events
{
    public static class PlayerHandler
    {
        public static void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.IsNPC || ev.Player.IsHost) return;
            if (ev.Reason != Exiled.API.Enums.SpawnReason.RoundStart) return;
            if (ev.Player.Role.Type != RoleTypeId.Scientist) return;
            if (Player.List.Where(r => !r.IsNPC && !r.IsHost).Any(Plugin.Instance.Config.Scp507.Check)) return;
            if (Random.Range(0, 100) <= Plugin.Instance.Config.Scp507SpawnChance) return;

            Plugin.Instance.Config.Scp507.AddRole(ev.Player);
        }
    }
}
