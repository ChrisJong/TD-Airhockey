namespace AirHockey.GameLayer.Views.CreditsViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.Graphics;
    using InteractionLayer.Components;

    /// <summary>
    /// Provides a semi-transparent shadow on the Credits text.
    /// </summary>
    class CreditsTextBackgroundGraphicsComponent : GraphicsComponent
    {
        public CreditsTextBackgroundGraphicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        public override void Draw()
        {
            DrawingManager.DrawRectangle(410, 300, 1100, 560, Color.FromArgb(128, 0, 0, 0), 0.89f);
        }
    }
}
