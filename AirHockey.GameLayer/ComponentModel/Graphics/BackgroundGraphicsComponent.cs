namespace AirHockey.GameLayer.ComponentModel.Graphics
{
    using InteractionLayer.Components;
    using Resources;
    using Utility.Classes;

    class BackgroundGraphicsComponent : GraphicsComponent
    {
        private readonly ResourceName _backgroundImage;

        public BackgroundGraphicsComponent(ResourceName backgroundImage, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this._backgroundImage = backgroundImage;
        }

        public override void Draw()
        {
            DrawingManager.DrawImage(this._backgroundImage, 0.0f, 0.0f, DrawingOrigin.TopLeft, 0.0f, 1.0f);
        }
    }
}
