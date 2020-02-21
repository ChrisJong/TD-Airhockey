namespace AirHockey.GameLayer.GUI
{
    using System.Drawing;
    using InteractionLayer.Components;
    using LogicLayer.Collisions;
    using Resources;
    using Utility.Attributes;
    using AirHockey.Utility.Classes;

    /// <summary>
    /// A GUI control that displays text on to the screen.
    /// </summary>
    class TextControl : GUIControl
    {
        private string _text = string.Empty;

        /// <summary>
        /// Whether or not to draw the text centre-middle alligned.
        /// </summary>
        public bool CentreTextInBounds
        {
            get;
            set;
        }

        /// <summary>
        /// The colour for the text to be when it is drawn.
        /// </summary>
        public Color Colour
        {
            get;
            set;
        }

        /// <summary>
        /// The depth at which to draw the text. Must be between
        /// 0 and 1 (0 is top).
        /// </summary>
        public float DrawDepth
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// The font to be used when drawing the text.
        /// </summary>
        public ResourceName Font
        {
            get;
            set;
        }

        /// <summary>
        /// The text to be drawn for this text element.
        /// </summary>
        [NeverNull]
        public string Text
        {
            get { return this._text; }
            set { this._text = value ?? string.Empty; }
        }

        /// <summary>
        /// Creates an instance of a <see cref="TextControl"/> with an absolute position.
        /// </summary>
        /// <param name="x">The X position of the button.</param>
        /// <param name="y">The Y position of the button.</param>
        /// <param name="width">The width of the text element.</param>
        /// <param name="height">The height of the text element.</param>
        public TextControl(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            this.Colour = Color.White;
            this.Rotation = 0.0f;
        }

        public TextControl(Vector position, int width, int height)
            : base((int)position.X, (int)position.Y, width, height)
        {
            this.Colour = Color.White;
            this.Rotation = 0.0f;
        }

        public TextControl(int x, int y, int width, int height, float rotation)
            : base(x, y, width, height)
        {
            this.Colour = Color.White;
            this.Rotation = rotation;
        }

        /// <summary>
        /// Draws the text element on to the screen.
        /// </summary>
        public override void Render()
        {
            DrawingManager.DrawText(
                this.Font,
                this.Text,
                new Rectangle(this.X, this.Y, this.Width, this.Height),
                this.CentreTextInBounds,
                this.Colour,
                this.Rotation,
                0.0f);
        }
    }
}
