namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonGraphics
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;
    using AirHockey.Utility.Helpers;
    using AirHockey.InteractionLayer.Components;

    class RadialForceGraphicsComponent : GraphicsComponent
    {
        public AnimationGraphicsComponent ProjectileGraphic
        {
            get;
            set;
        }

        protected TowerObjectBase MyTower
        {
            get;
            set;
        }

        public RadialForceGraphicsComponent(AnimationValues animationValues, int inFrame, float rotation, TowerObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.MyTower = (TowerObjectBase)parentNode;
            var tempScaleX = (MyTower.Power / 100.0f) - MyTower.Physics.Scale.X;
            var tempScaleY = (MyTower.Power / 100.0f) - MyTower.Physics.Scale.Y;

            var resource = this.SendMessage<ResourceName>("Resource", animationValues.BaseDirectory + ".Projectile");
            this.ProjectileGraphic = new AnimationGraphicsComponent(resource, animationValues, parentNode, this.MessageHandlers.ToArray())
            {
                RenderRotationOffset = rotation,
                RenderScaleOffset = new Vector(tempScaleX, tempScaleY),
                InFrame = inFrame,
            };
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            ProjectileGraphic.Update(delta);
        }

        public override void Draw()
        {
            if (this.ParentNode.IsActive || MyTower.ToggleCooldown > 0 || MyTower.TaglessCooldown > 0)
            {
                SetTaglessAlpha(this.ProjectileGraphic);
                this.ProjectileGraphic.Draw();
            }
        }

        public override void Release()
        {
            this.MyTower = null;
            base.Release();
        }
    }
}

