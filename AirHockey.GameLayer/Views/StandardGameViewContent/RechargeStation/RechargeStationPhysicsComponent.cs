namespace AirHockey.GameLayer.Views.StandardGameViewContent.RechargeStation
{
    using ComponentModel;
    using ComponentModel.DataTransfer;
    using ComponentModel.Physics;
    using Constants;
    using InteractionLayer.Components;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using Towers.CommonPhysics;

    class RechargeStationPhysicsComponent : PhysicsComponent
    {
        public bool IsCharging
        {
            get;
            set;
        }

        public RechargeStationPhysicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.AddCollisionShape(new RectangleCollisionShape
            {
                Width = 90.0f,
                Height = 150.0f
            });

            this.PhysicsFlag = AirHockeyValues.PhysicsCollisionType.ChargeStation;
            this.CollisionEvents += this.OnCollision;
        }

        public void OnCollision(IPhysicsObject otherObject)
        {
            //if (otherObject is TowerPhysicsComponent)
            if (otherObject.PhysicsFlag == AirHockeyValues.PhysicsCollisionType.Tower)
            {
                var towerPhysicsBody = (TowerPhysicsComponent) otherObject;
                var targetTower = (TowerObjectBase) towerPhysicsBody.ParentNode;

                //Reset cooldowns for ANY active towers sitting on a charge station
                //Switch towers off on there too
                if (!this.IsCharging && (targetTower.IsActive || targetTower.TaglessCooldown > 0))
                {
                    targetTower.IsActivated = false;
                    targetTower.RegenCooldown = targetTower.RegenCooldownMax;
                    targetTower.ToggleCooldown = targetTower.ToggleCooldownMax;

                    if (targetTower.Energy < 100)
                    {
                        this.IsCharging = true;
                        targetTower.Energy += targetTower.RegenRate / 20f;
                        ParentNode.Graphics.Alpha += (1 - ParentNode.Graphics.Alpha) / 5f;
                    }
                }
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            this.IsCharging = false;

            if (ParentNode.Graphics.Alpha > 0.5f)
            {
                ParentNode.Graphics.Alpha += (0.5f - ParentNode.Graphics.Alpha) / 25;
            }
        }
    }
}
