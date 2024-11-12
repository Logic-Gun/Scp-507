using Exiled.API.Features;
using Exiled.CustomRoles.API;
using System;

namespace Scp_507
{
    public class Plugin : Plugin<Config, Translation>
    {
        public override string Name => "Scp-507";
        public override string Author => "Logic_Gun";
        //public override Version RequiredExiledVersion => new(8, 9, 11);
        public override Version Version => new(1, 0, 1);

        public static Plugin Instance;

        public override void OnEnabled()
        {
            Instance = this;

            Instance.Config.Scp507.Register();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance.Config.Scp507.Unregister();

            Instance = null;

            base.OnDisabled();
        }
    }
}
