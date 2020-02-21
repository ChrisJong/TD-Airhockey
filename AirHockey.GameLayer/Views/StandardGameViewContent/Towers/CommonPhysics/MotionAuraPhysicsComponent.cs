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

    class MotionAuraPhysicsComponent: PhysicsComponent
    {
        private TowerObjectBase _myTower;
        private float _radiusSq;
        private float _radialEffect;
        private float _linearEffect;

        public MotionAuraPhysicsComponent(float radius, float motionMultiplierTarget, float radialEffect, float linearEffect, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.AddCollisionShape(new CircleCollisionShape
            {
                    Radius = radius
            });
            this._radiusSq = radius * radius;
            this.PhysicsFlag = AirHockeyValues.PhysicsCollisionType.Force;
            this.CollisionEvents += this.OnCollision;
            this.MotionMultiplier = motionMultiplierTarget;
            this._myTower = (TowerObjectBase)parentNode;
            this.Position = this._myTower.Physics.Position;
            this._radialEffect = radialEffect;
            this._linearEffect = linearEffect;
        }

        private void OnCollision(IPhysicsObject otherObject)
        {
            if (otherObject is PuckPhysicsComponent)
            {
                var towerMultiplier = this._myTower.Power / 100.0f;

                float distanceMultiplier = ((this.Position - otherObject.Position).LengthSq / _radiusSq) + 0.4f;
                distanceMultiplier = distanceMultiplier * distanceMultiplier;
                otherObject.MotionMultiplier += (MotionMultiplier - otherObject.MotionMultiplier) / ((_radialEffect * distanceMultiplier) + _linearEffect + otherObject.Mass);
            }
        }

        public override void Release()
        {
            this._myTower = null;
            base.Release();
        }

        public override string ToString()
        {
            return "MotionPhysics Component (f = " + this.MotionMultiplier.ToString() + ")";
        }
    }
}
