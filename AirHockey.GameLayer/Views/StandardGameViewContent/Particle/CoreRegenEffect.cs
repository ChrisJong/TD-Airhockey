namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;

    class CoreRegenEffect : SimpleParticleBase
    {
        public CoreRegenEffect(Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Cores.CoreRegen",
                FrameCount = AnimationValues.Core.RegenFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Core.Regen
            };
            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Core_Regen");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}

