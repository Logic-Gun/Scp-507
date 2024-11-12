using Exiled.API.Features;
using UnityEngine;

namespace Scp_507.Components
{
    internal class Scp507Component : MonoBehaviour
    {
        private Player _player;
        public float Cooldown;

        internal Scp507Component Init(Player pl)
        {
            _player = pl;
            Cooldown = 0;

            InvokeRepeating("HUD", 0, 1);
            return this;
        }

        internal void HUD()
        {
            if (Cooldown > 0) Cooldown--;

            _player.ShowHint(Plugin.Instance.Translation.Teleport.Replace("%s%", Cooldown == 0 ? string.Empty : $"({Cooldown})"), 1.5f);
        }
    }
}
