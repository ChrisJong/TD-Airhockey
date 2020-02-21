namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;
    using System;
    using AirHockey.Utility.Helpers;

    class OneShotFlashTransition: SimpleParticleBase
    {
        public OneShotFlashTransition(params IMessageHandler[] messageHandlers)
            : base(new Vector(0, 0), new Vector(0, 0), messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.Transitions.Flash",
                FrameCount = AnimationValues.Transitions.FlashFrameCount,
                FrameDuration = AnimationValues.Transitions.FlashFrameDuration,
                RenderingDepth = RenderingValues.Depth.Transitions
            };
            this.Graphics = new OneShotGraphicsComponent(animInfo, this, this);

            this.Physics.Scale.X = 2.0f + (float)RandomisationHelper.Random.NextDouble();
            this.Physics.Scale.Y = this.Physics.Scale.X;
            this.Physics.Position.X = 840 + RandomisationHelper.Random.Next(240);
            this.Physics.Position.Y = 420 + RandomisationHelper.Random.Next(240);
            this.Physics.Rotation = (float)(2 * Math.PI * RandomisationHelper.Random.NextDouble());

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.TransitionFlash");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}

