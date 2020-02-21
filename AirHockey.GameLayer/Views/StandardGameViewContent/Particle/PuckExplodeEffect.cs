namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;

    class PuckExplodeEffect: SimpleParticleBase
    {
        public PuckExplodeEffect(Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Pucks.PuckExplode",
                FrameCount = AnimationValues.Puck.ExplodeFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Puck.Explode
            };
            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            this.Physics.Velocity *= 0.94f;
        }
    }
}

