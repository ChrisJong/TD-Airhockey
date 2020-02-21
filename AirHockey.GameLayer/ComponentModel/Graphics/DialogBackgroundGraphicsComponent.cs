namespace AirHockey.GameLayer.ComponentModel.Graphics
{
    using System.Drawing;
    using InteractionLayer.Components;
    using Resources;
    using Utility.Classes;

    class DialogBackgroundGraphicsComponent : GraphicsComponent
    {
        private readonly ResourceName _backgroundImage;

        public DialogBackgroundGraphicsComponent(ResourceName backgroundImage, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this._backgroundImage = backgroundImage;
        }

        public override void Draw()
        {
            var position = this.SendMessage<Point>("Get", "DialogPosition");
            DrawingManager.DrawImage(this._backgroundImage, position.X, position.Y, DrawingOrigin.TopLeft, 0.0f, 1.0f);
        }
    }
}
