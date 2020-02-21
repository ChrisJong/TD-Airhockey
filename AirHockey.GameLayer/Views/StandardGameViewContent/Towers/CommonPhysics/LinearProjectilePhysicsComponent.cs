namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonPhysics
{
    using System;
    using ComponentModel;
    using ComponentModel.Physics;
    using Constants;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using Utility.Classes;
    using Puck;

    /// <summary>
    /// A physics object that handles the custom logic
    /// for a particle trail particle's physics.
    /// </summary>
    class LinearProjectilePhyscisComponent : PhysicsComponent
    {
        private static int _projectileDelay;
        private readonly Player _player;
        private readonly float _maxMass;

        public Type ParticleType
        {
            get;
            set;
        }

        public LinearProjectilePhyscisComponent(Player player, float radius, float mass, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.CollisionEvents += this.OnCollision;

            this.AddCollisionShape(
                new CircleCollisionShape
                {
                    Radius = radius
                });

            this.PhysicsFlag = AirHockeyValues.PhysicsCollisionType.Force;
            this.Mass = mass;
            this._maxMass = mass;
            this._player = player;
        }

        private void OnCollision(IPhysicsObject otherObject)
        {
            if (otherObject is PuckPhysicsComponent)
            {
                Physman.ProjectileCollision(this, otherObject);
                this.Velocity *= 0.98f;
                this.Mass *= 0.98f;

                if (_projectileDelay <= 0)
                {
                    if (this.ParticleType != null)
                    {
                        _projectileDelay = 1;

                        var midPoint = new Vector((otherObject.Position.X + this.Position.X) / 2, (otherObject.Position.Y + this.Position.Y) / 2);
                        this.SendMessage<object>("Create", "GameObject", this.ParticleType, this.Position, this.Velocity / 50);
                    }
                }
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            if (this.ParentNode.IsActive)
            {
                this.Velocity *= 0.93f;
                this.Mass *= 0.93f;
                this.ParentNode.Graphics.Alpha = this.Mass / this._maxMass * 2;

                if (this.Mass <= 0.001)
                {
                    this.SendMessage<object>("Delete", "GameObject");
                }
            }

            if (_projectileDelay > 0) _projectileDelay--;
        }
    }
}
