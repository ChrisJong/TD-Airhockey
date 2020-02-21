namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;

    class OneShotGraphicsComponent : GraphicsComponent
    {
        private readonly AnimationGraphicsComponent _oneShotGraphic;

        /// <summary>
        /// Animation component which makes the object delete itself once its animation completes.
        /// </summary>
        /// <param name="animationValues"></param>
        /// <param name="parentNode"></param>
        /// <param name="messageHandlers"></param>

        public OneShotGraphicsComponent(AnimationValues animationValues, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            var resource = this.SendMessage<ResourceName>("Resource", animationValues.BaseDirectory);
            this._oneShotGraphic = new AnimationGraphicsComponent(resource, animationValues, parentNode, this.MessageHandlers.ToArray());
            this._oneShotGraphic.AnimationComplete += this.OnAnimationComplete;
            this._oneShotGraphic.Alpha = 1.0f;
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            _oneShotGraphic.Update(delta);
        }

        private void OnAnimationComplete()
        {
            this.SendMessage<object>("Delete", "GameObject");
        }

        public override void Draw()
        {
            this._oneShotGraphic.Draw();
        }
    }
}