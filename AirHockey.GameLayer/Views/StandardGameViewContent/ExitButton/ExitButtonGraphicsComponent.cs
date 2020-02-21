namespace AirHockey.GameLayer.Views.StandardGameViewContent.ExitButton
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using AirHockey.InteractionLayer.Components;

    class ExitButtonGraphicsComponent : GraphicsComponent
    {
        protected AnimationGraphicsComponent IdleGraphic;

        protected ExitButton MyExitButton
        {
            get;
            set;
        }

        public ExitButtonGraphicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.MyExitButton = (ExitButton)parentNode;

            //Recharge Station Background
            var resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.ExitButton.Idle");
            this.IdleGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.Default.ExitButtonIdle,
                AnimationValues.Default.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = RenderingValues.Depth.ExitButton
            };
            if (MyExitButton._player == Utility.Classes.Player.Two)
            {
                IdleGraphic.RenderRotationOffset = (float)System.Math.PI;
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            IdleGraphic.Update(delta);
        }

        public override void Draw()
        {
            this.IdleGraphic.Draw();
        }

        public override void Release()
        {
            this.MyExitButton = null;
            base.Release();
        }
    }
}
