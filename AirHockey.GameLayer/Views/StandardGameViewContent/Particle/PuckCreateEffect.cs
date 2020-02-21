namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;

    class PuckCreateEffect: SimpleParticleBase
    {
        public PuckCreateEffect(Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Pucks.PuckCreate",
                FrameCount = AnimationValues.Puck.CreateFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Puck.Create
            };
            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);
        }
    }
}

