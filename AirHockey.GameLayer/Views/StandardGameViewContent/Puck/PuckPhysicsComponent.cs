namespace AirHockey.GameLayer.Views.StandardGameViewContent.Puck
{
    using ComponentModel;
    using ComponentModel.Physics;
    using Constants;
    using InteractionLayer.Components;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using Utility.Classes;
    using System;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;
    using AirHockey.Utility.Helpers;

    class PuckPhysicsComponent : PhysicsComponent
    {
        private double _lifetime = 15000;
        private float _deltaRotation;
        
        public double ExciteCooldown
        {
            get;
            set;
        }

        public double ExciteCooldownMax
        {
            get;
            set;
        }

        public PuckPhysicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.AddCollisionShape(
                new CircleCollisionShape
                {
                    Radius = AirHockeyValues.Puck.ObjectRadius
                }
            );
            this.PhysicsFlag = AirHockeyValues.PhysicsCollisionType.SimpleBody;
            this.MotionMultiplier = 0f;
            this.Mass = AirHockeyValues.Puck.ObjectMass;
            this.CollisionEvents += this.OnCollision;
            this.Velocity.Randomize(AirHockeyValues.Puck.StartingVelocity);

            this.ExciteCooldownMax = 500;
            this.ExciteCooldown = this.ExciteCooldownMax;
        }

        private void OnCollision(IPhysicsObject otherObject)
        {
            this.ExciteCooldown = this.ExciteCooldownMax;
            Physman.CapVelocity(this, AirHockeyValues.Puck.MaxVelocitySq * 1.2f, 0.2f);
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            if (this.Velocity.LengthSq > AirHockeyValues.Puck.StartingVelocitySq * 0.66)
            {
                Physman.EaseVelocity(this, AirHockeyValues.Puck.StartingVelocitySq, (float)Math.Pow(this.Mass, 3f) * 0.0001f);
            }
            Physman.CapVelocity(this, AirHockeyValues.Puck.MaxVelocitySq, 0.05f);
            Physman.StabiliseMotionMultiplier(this, 0.02f);

            if (this.ExciteCooldown > 0)
                ExciteCooldown -= delta;

            this.RandomizeRotation();
            this.WallReflections();

            if (this._lifetime <= 0)
            {
                this.SendMessage<object>("Create", "GameObject", typeof(PuckDissolveEffect), this.Position, this.Velocity * 0.8f, this.MotionMultiplier);
                this.SendMessage<object>("Delete", "GameObject");
            }
            else
            {
                this._lifetime -= delta;
            }
        }

        /// <summary>
        /// Randomizes rotation and delta rotation
        /// </summary>
        private void RandomizeRotation()
        {
            this._deltaRotation += (float)(RandomisationHelper.Random.NextDouble() - 0.5) / 200;
            if (Math.Abs(this._deltaRotation) > 1) this._deltaRotation *= 1 / this._deltaRotation;
            this.Rotation += this._deltaRotation * this.MotionMultiplier;
        }

        /// <summary>
        /// Reflects pucks off the wall boundaries.
        /// </summary>
        private void WallReflections()
        {
            // reflection/capping code for pucks with Veritcal Walls
            if (this.Position.X > DrawingManager.GetScreenDimensions().X - (AirHockeyValues.Puck.VeritcalWall + AirHockeyValues.Puck.ObjectRadius))
            {
                this.Velocity.Reflect(new Vector(-1, 0));
                this.Position.X = DrawingManager.GetScreenDimensions().X - (AirHockeyValues.Puck.VeritcalWall + AirHockeyValues.Puck.ObjectRadius);
                this.PlayBounceAudio();
            }

            if (this.Position.X < (AirHockeyValues.Puck.VeritcalWall + AirHockeyValues.Puck.ObjectRadius))
            {
                this.Velocity.Reflect(new Vector(1, 0));
                this.Position.X = AirHockeyValues.Puck.VeritcalWall + AirHockeyValues.Puck.ObjectRadius;
                this.PlayBounceAudio();
            }


            // reflection/capping code for pucks with Horizontal Walls
            if (this.Position.Y > DrawingManager.GetScreenDimensions().Y - (AirHockeyValues.Puck.HorizontalWall + AirHockeyValues.Puck.ObjectRadius))
            {
                this.Velocity.Reflect(new Vector(0, -1));
                this.Position.Y = DrawingManager.GetScreenDimensions().Y - (AirHockeyValues.Puck.HorizontalWall + AirHockeyValues.Puck.ObjectRadius);
                this.PlayBounceAudio();
            }

            if (this.Position.Y < (AirHockeyValues.Puck.HorizontalWall + AirHockeyValues.Puck.ObjectRadius))
            {
                this.Velocity.Reflect(new Vector(0, 1));
                this.Position.Y = AirHockeyValues.Puck.HorizontalWall + AirHockeyValues.Puck.ObjectRadius;
                this.PlayBounceAudio();
            }
        }


        private void PlayBounceAudio()
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Puck_Bounce");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }

        public override string ToString()
        {
            return "Puck Physics Component";
        }
    }
}
