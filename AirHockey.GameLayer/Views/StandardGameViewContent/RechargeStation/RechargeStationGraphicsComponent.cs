namespace AirHockey.GameLayer.Views.StandardGameViewContent.RechargeStation
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using AirHockey.InteractionLayer.Components;

    class RechargeStationGraphicsComponent : GraphicsComponent
    {
        protected AnimationGraphicsComponent BackgroundGraphic;

        protected RechargeStation MyStation
        {
            get;
            set;
        }

        public RechargeStationGraphicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.MyStation = (RechargeStation)parentNode;

            //Recharge Station Background
            var resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.RechargeStation.Fast");
            this.BackgroundGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.RechargeStation.BackgroundFrameCount,
                AnimationValues.Default.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = RenderingValues.Depth.RechargeStation.Background
            };
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            BackgroundGraphic.Update(delta);
        }

        public override void Draw()
        {
            this.BackgroundGraphic.Alpha = this.Alpha;
            this.BackgroundGraphic.Draw();
        }

        public override void Release()
        {
            this.MyStation = null;
            base.Release();
        }
    }
}
