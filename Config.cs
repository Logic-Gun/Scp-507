using Exiled.API.Interfaces;
using Scp_507.CustomRoles;
using System.ComponentModel;

namespace Scp_507
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Scp507: Spawn chance")]
        public byte Scp507SpawnChance { get; set; } = 75;

        [Description("Scp507 Teleporter: Minimum time (seconds)")]
        public uint Scp507TeleporterMin { get; set; } = 2;

        [Description("Scp507 Teleporter: Maximum time (seconds)")]
        public uint Scp507TeleporterMax { get; set; } = 135;

        [Description("Scp507: Distance to nearest players.")]
        public float Scp507DistanceNearestPlayers { get; set; } = 5;

        [Description("Custom Roles: Settings")]
        public Scp507 Scp507 { get; set; } = new();
    }
}
