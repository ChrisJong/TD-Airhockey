namespace AirHockey.GameLayer.Views.ArmouryViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.Graphics;
    using InteractionLayer.Components;

    /// <summary>
    /// Provides a semi-transparent shadow on the Armoury text.
    /// </summary>
    class ArmouryTextBackgroundGraphicsComponent : GraphicsComponent
    {
        public ArmouryTextBackgroundGraphicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        public override void Draw()
        {
            DrawingManager.DrawRectangle(410, 300, 1100, 560, Color.FromArgb(128, 0, 0, 0), 0.89f);
        }
    }
}
