namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;

    class PuckDissolveEffect : SimpleParticleBase
    {
        public PuckDissolveEffect(Vector position, Vector velocity, float motionMultiplier, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Pucks.PuckDissolve",
                FrameCount = AnimationValues.Puck.DissolveFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Puck.Dissolve
            };
            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);
            this.Physics.MotionMultiplier = motionMultiplier;

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Puck_Dissolve");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            this.Physics.Velocity *= 0.92f;
        }
    }
}

