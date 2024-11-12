using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using Scp_507.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scp_507.CustomRoles
{
    public class Scp507 : CustomRole
    {
        public override uint Id { get; set; } = 507;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
        public override int MaxHealth { get; set; } = 350;
        public override string Name { get; set; } = "SCP-507";
        public override string Description { get; set; } = string.Empty;
        public override string CustomInfo { get; set; } = "SCP-507";

        private Dictionary<Player, Scp507Component> _components = [];

        protected override void RoleAdded(Player player)
        {
            Scp507Component component = player.GameObject.AddComponent<Scp507Component>().Init(player);

            if (!_components.ContainsKey(player)) _components.Add(player, component);
            else _components[player] = component;

            Timing.RunCoroutine(Teleporter(player));

            base.RoleAdded(player);
        }

        protected override void RoleRemoved(Player player)
        {
            if (_components.ContainsKey(player)) GameObject.Destroy(_components[player]);

            base.RoleRemoved(player);
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.TogglingNoClip += OnTogglingNoClip;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.TogglingNoClip -= OnTogglingNoClip;

            base.UnsubscribeEvents();
        }

        private void OnTogglingNoClip(TogglingNoClipEventArgs ev)
        {
            if (!_components.ContainsKey(ev.Player) || _components[ev.Player].Cooldown > 0) return;
            if (Physics.Raycast(ev.Player.CameraTransform.position, ev.Player.CameraTransform.forward, out RaycastHit hitInfo))
            {
                List<Player> players = [ev.Player];

                foreach (Player pl in Player.List.Where(r => r.IsAlive && !r.IsScp && Vector3.Distance(ev.Player.Position, r.Position) <= Plugin.Instance.Config.Scp507DistanceNearestPlayers))
                {
                    players.Add(pl);
                }

                Vector3 newPosition = hitInfo.point + new Vector3(0, 1.2f);

                foreach (var pl in players)
                {
                    pl.EnableEffect(Exiled.API.Enums.EffectType.Flashed, 1.5f);
                    pl.Position = newPosition;
                }

                _components[ev.Player].Cooldown = 25;
            }
        }

        private IEnumerator<float> Teleporter(Player pl)
        {
            List<Player> players = [];
            Vector3 newPosition = Vector3.zero;

            while (!Round.IsEnded && Check(pl))
            {
                players = [pl];

                foreach (Player ply in Player.List.Where(r => r.IsAlive && !r.IsScp && Vector3.Distance(pl.Position, r.Position) <= Plugin.Instance.Config.Scp507DistanceNearestPlayers))
                {
                    players.Add(pl);
                }
                Room room = Room.List.Where(r => !Plugin.StupidRooms.Contains(r.Type)).GetRandomValue();
                newPosition = room.Position + new Vector3(0, 1.2f);

                Log.Debug(room.Type);

                foreach (var ply in players)
                {
                    ply.EnableEffect(Exiled.API.Enums.EffectType.Flashed, 1.5f);
                    ply.Position = newPosition;
                }

                yield return Timing.WaitForSeconds(
                    Random.Range(
                        Plugin.Instance.Config.Scp507TeleporterMin, 
                        Plugin.Instance.Config.Scp507TeleporterMax
                    )
                );
            }

            yield break;
        }
    }
}
