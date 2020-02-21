namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;
    using System;
    using AirHockey.Utility.Helpers;

    class SimpleParticleBase : GameObjectBase
    {
        public SimpleParticleBase(Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.Physics = new SimplePhysicsComponent(this, this)
            {
                Position = position,
                Velocity = velocity,
                Rotation = (float)RandomisationHelper.Random.NextDouble() * 3.142f
            };
        }
    }
}

