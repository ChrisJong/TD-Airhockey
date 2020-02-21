namespace AirHockey.GameLayer.Views.StandardGameViewContent.Puck
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using InteractionLayer.Components;
    using Resources;
    using Utility.Classes;
    using System;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;
    using AirHockey.Utility.Helpers;


    class Puck : GameObjectBase
    {
        public Puck(float x, float y, params IMessageHandler[] messageHandlers)
            : this(new Vector(x, y), messageHandlers)
        {
        }

        public Puck(Vector position, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            int i = RandomisationHelper.Random.Next(4);
            var animationValues = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Pucks.Puck" + i.ToString(),
                FrameCount = AnimationValues.Puck.FrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Puck.Object

            };

            i /= 2; //exponentiate mass
            this.Physics = new PuckPhysicsComponent(this, this)
            {
                Position = position,
                Mass = (float)(1 + i * i) //Masses are: 1, 1.25, 2, 3.25 
            };

            this.Graphics = new PuckGraphicsComponent(animationValues, this, this);

            this.SendMessage<object>("Create", "GameObject", typeof(PuckCreateEffect), position, new Vector());
        }
    }
}
