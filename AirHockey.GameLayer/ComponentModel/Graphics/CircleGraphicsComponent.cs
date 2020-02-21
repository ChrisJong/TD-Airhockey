namespace AirHockey.GameLayer.ComponentModel.Graphics
{
    using System.Drawing;
    using InteractionLayer.Components;
    using Utility.Classes;

    /// <summary>
    /// Draws a circle at the game object's position
    /// with the given radius and colour.
    /// </summary>
    class CircleGraphicsComponent : GraphicsComponent
    {
        private readonly Color _colour;
        private readonly float _radius;

        public CircleGraphicsComponent(float radius, Color colour, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.ParentNode = parentNode;
            this._colour = colour;
            this._radius = radius;
        }

        public override void Draw()
        {
            if (this.ParentNode == null) return;

            if (this.ParentNode.IsActive)
            {
                var position = this.ParentNode.Physics.Position;

                // Centre of circle at position
                position.X -= this._radius;
                position.Y -= this._radius;

                DrawingManager.DrawEllipse(position, DrawingOrigin.Center, this._radius, this._colour, 1.0f);
            }
        }
    }
}
