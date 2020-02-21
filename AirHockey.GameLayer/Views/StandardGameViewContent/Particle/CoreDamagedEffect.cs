namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;

    class CoreDamagedEffect : SimpleParticleBase
    {
        public CoreDamagedEffect(Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Cores.CoreDamaged",
                FrameCount = AnimationValues.Core.DamagedFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Core.Damaged
            };
            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Core_Damaged");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}

