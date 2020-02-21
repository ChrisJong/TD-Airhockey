namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonGraphics
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;

    class ToggleTowerBaseGraphicsComponent : SimpleTowerBaseGraphicsComponent
    {
        protected AnimationGraphicsComponent ActivatedEffectGraphic;

        public ToggleTowerBaseGraphicsComponent(AnimationValues animationValues, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(animationValues, parentNode, messageHandlers)
        {
            var resource = this.SendMessage<ResourceName>("Resource", animationValues.BaseDirectory + ".ActivatedEffect");

            this.ActivatedEffectGraphic = new AnimationGraphicsComponent(resource,
                animationValues.ActivatedEffectFrameCount,
                animationValues.FrameDuration,
                parentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = RenderingValues.Depth.Pulsar.ToggleRing
            };
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            ActivatedEffectGraphic.Update(delta);
        }

        public override void Draw()
        {
            base.Draw();

            if (this.MyTower.IsActivated)
            {
                if (!this.MyTower.IsOutOfEnergy)
                {
                    SetTaglessAlpha(this.ActivatedEffectGraphic);
                    this.ActivatedEffectGraphic.Draw();
                }
            }
        }
    }
}
