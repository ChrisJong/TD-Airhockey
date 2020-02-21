namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonPhysics
{
    using ComponentModel;
    using ComponentModel.Physics;
    using LogicLayer.Collisions.CollisionShapes;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Utility.Classes;
    using System;

    /// <summary>
    /// Physics Component for all towers.
    /// Pass in the radius of how big the collision circle is.
    /// </summary>
    class TowerPhysicsComponent : PhysicsComponent
    {
        public bool UseTagRotation = false;
        public float _frictionScale = 0;

        public TowerObjectBase MyTower
        {
            get;
            set;
        }

        private Vector _prevPosition = new Vector();

        public TowerPhysicsComponent(float radius, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.Rotation = 20;
            this.AddCollisionShape(new CircleCollisionShape
            {
                Radius = radius
            });
            this.PhysicsFlag = Constants.AirHockeyValues.PhysicsCollisionType.Tower;
            MyTower = (TowerObjectBase)parentNode;
        }


        public override void Update(double delta)
        {
            if (this._prevPosition.X != this.Position.X && this._prevPosition.Y != this.Position.Y)
            {
                if (MyTower.IsActivated)
                {
                    // up to 10% of a tower's energy is lost when moved while active
                    this._frictionScale += (float)((0.1f - this._frictionScale) / 6.0f);
                    MyTower.Energy -= MyTower.Energy * _frictionScale;
                }
            }

            if (this._frictionScale > 0.01f)
            {
                this._frictionScale -= _frictionScale / 24.0f;
            }
            else
            {
                this._frictionScale = 0;
            }

            this._prevPosition = this.Position;
            base.Update(delta);
        }

        public override void Release()
        {
            this.MyTower = null;
            base.Release();
        }
    }
}
