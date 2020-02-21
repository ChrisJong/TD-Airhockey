namespace AirHockey.GameLayer.Views.AboutViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.Graphics;
    using InteractionLayer.Components;

    class AboutTextBackgroundGraphicsComponent : GraphicsComponent
    {
        public AboutTextBackgroundGraphicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        public override void Draw()
        {
            DrawingManager.DrawRectangle(410, 300, 1100, 530, Color.FromArgb(128, 0, 0, 0), 0.89f);
        }
    }
}
