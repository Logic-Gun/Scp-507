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

            base.RoleAdded(player);
        }

        protected override void RoleRemoved(Player player)
        {
            if (_components.ContainsKey(player)) GameObject.Destroy(_components[player]);

            base.RoleRemoved(player);
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.DroppingItem += OnDroppingItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.DroppingItem -= OnDroppingItem;

            base.UnsubscribeEvents();
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            Log.Info(1);
            Log.Info(ev.Item.ToString());
            if (ev.Item != null) return;
            if (_components[ev.Player].Cooldown > 0) return;
            if (Physics.Raycast(ev.Player.CameraTransform.position, ev.Player.CameraTransform.forward, out RaycastHit hitInfo))
            {
                List<Player> players = [];

                foreach (Player pl in Player.List.Where(r => r.IsAlive && !r.IsScp && Vector3.Distance(ev.Player.Position, r.Position) <= 5))
                {
                    players.Add(pl);
                }

                _components[ev.Player].Cooldown = 5;
                ev.Player.Position = hitInfo.point + new Vector3(0, 0.5f);
            }
        }

        private IEnumerator<float> Teleporter(Player pl)
        {
            while (!Round.IsEnded)
            {
                pl.Position = Room.List.GetRandomValue().Position;

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
