namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonGraphics
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;

    class LinearProjectileGraphicsComponent : GraphicsComponent
    {
        private readonly AnimationGraphicsComponent _projectileGraphic;

        public LinearProjectileGraphicsComponent(AnimationValues animationValues, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            var resource = this.SendMessage<ResourceName>("Resource", animationValues.BaseDirectory + ".Projectile");

            this._projectileGraphic = new AnimationGraphicsComponent(resource, animationValues, parentNode, this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.ProjectileDepth,
                Alpha = this._alpha
            };
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            _projectileGraphic.Update(delta);
        }

        public override void Draw()
        {
            this._projectileGraphic.Alpha = this.Alpha;
            this._projectileGraphic.Draw();
        }
    }
}
