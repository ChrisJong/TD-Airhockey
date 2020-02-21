namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;

    class CoreExplodeEffect : SimpleParticleBase
    {
        public CoreExplodeEffect(Player player, Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Cores.CoreExplode",
                FrameCount = AnimationValues.Core.ExplodeFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Core.Damaged
            };
            this.Graphics = new CoreExplodeOneShotGraphics(player, this, this);

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Core_Explode");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}

