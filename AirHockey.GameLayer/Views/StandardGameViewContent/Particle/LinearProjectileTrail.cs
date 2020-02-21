namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;

    class LinearProjectileTrail : SimpleParticleBase
    {
        public LinearProjectileTrail(Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity/60, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.SlingShot.Trail",
                FrameCount = AnimationValues.SlingShot.TrailFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.SlingShot.Projectile
            };

            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);
        }
    }
}

