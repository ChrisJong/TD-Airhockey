namespace AirHockey.GameLayer.Views.StandardGameViewContent
{
    using ComponentModel;
    using ComponentModel.Physics;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using Utility.Classes;
    using Constants;
    using System;
    using Puck;

    class RadialForcePhysicsComponent: PhysicsComponent
    {
        private TowerObjectBase _myTower;
        public float _forceMultiplier;

        public RadialForcePhysicsComponent(float radius, float forceMultiplier, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.AddCollisionShape(new CircleCollisionShape
            {
                    Radius = radius
                });
            this.PhysicsFlag = AirHockeyValues.PhysicsCollisionType.Force;
            this.CollisionEvents += this.OnCollision;
            this._forceMultiplier = forceMultiplier;
            this._myTower = (TowerObjectBase)parentNode;
            this.Position = this._myTower.Physics.Position;
        }

        private void OnCollision(IPhysicsObject otherObject)
        {
            if (otherObject is PuckPhysicsComponent)
            {
                var pushMultiplier = this._myTower.Power / 100.0f;

                //The smaller the black hole, the stronger the pull
                if (otherObject.Velocity.LengthSq > 400000.0f - (100000 * pushMultiplier)) return;

                var distance = (this.Position - otherObject.Position).LengthSq / 80;
                var exponentMultiplier = (pushMultiplier * pushMultiplier) + 0.1f;

                var pushVector = new Vector(otherObject.Position.X - this.Position.X, otherObject.Position.Y - this.Position.Y).UnitVector;
                pushVector *= this._forceMultiplier * (1 + exponentMultiplier) / (distance * exponentMultiplier + 70 * exponentMultiplier);
                pushVector /= otherObject.Mass;

                otherObject.Velocity += pushVector ;
            }
        }

        public override void Release()
        {
            this._myTower = null;
            base.Release();
        }

        public override string ToString()
        {
            return "RadialForce Physics Component (f = " + this._forceMultiplier.ToString() + ")";
        }
    }
}
