namespace AirHockey.GameLayer.GUI
{
    using System.Drawing;
    using LogicLayer.Collisions;
    using Resources;

    /// <summary>
    /// A button with a label over it.
    /// </summary>
    class LabelledButtonControl : GUIControl
    {
        protected ButtonControl InternalButtonControl;
        protected TextControl InternalTextControl;

        /// <summary>
        /// The image use for when the button is disabled.
        /// </summary>
        public ResourceName ButtonDisabledImage
        {
            get { return this.InternalButtonControl.DisabledImage; }
            set { this.InternalButtonControl.DisabledImage = value; }
        }

        /// <summary>
        /// The primary image for the button.
        /// </summary>
        public ResourceName ButtonImage
        {
            get { return this.InternalButtonControl.Image; }
            set { this.InternalButtonControl.Image = value; }
        }

        /// <summary>
        /// The image used for when the button is pressed down.
        /// </summary>
        public ResourceName ButtonPressedImage
        {
            get { return this.InternalButtonControl.PressedImage; }
            set { this.InternalButtonControl.PressedImage = value; }
        }

        /// <summary>
        /// Whether or not the control is disabled.
        /// </summary>
        public bool Disabled
        {
            get { return this.InternalButtonControl.Disabled; }
            set { this.InternalButtonControl.Disabled = value; }
        }

        /// <summary>
        /// The font for the label.
        /// </summary>
        public ResourceName Font
        {
            get { return this.InternalTextControl.Font; }
            set { this.InternalTextControl.Font = value; }
        }

        /// <summary>
        /// The colour used for the label's text.
        /// </summary>
        public Color ForeColour
        {
            get { return this.InternalTextControl.Colour; }
            set { this.InternalTextControl.Colour = value; }
        }

        /// <summary>
        /// The text for the label.
        /// </summary>
        public string Text
        {
            get { return this.InternalTextControl.Text; }
            set { this.InternalTextControl.Text = value; }
        }

        /// <summary>
        /// Triggered when the button is clicked.
        /// </summary>
        public event ButtonControl.ButtonClickHandler Click
        {
            add { this.InternalButtonControl.Click += value; }
            remove { this.InternalButtonControl.Click -= value; }
        }

        /// <summary>
        /// Creates a new instance of a <see cref="LabelledButtonControl"/> with
        /// an absolute position.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="width">The width of the control.</param>
        /// <param name="height">The height of the control.</param>
        public LabelledButtonControl(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            this.InternalButtonControl = new ButtonControl(x, y, width, height)
            {
                DrawDepth = 0.0012f
            };
            this.InternalTextControl = new TextControl(x, y, width, height)
            {
                CentreTextInBounds = true,
                DrawDepth = 0.0011f
            };
        }

        public override void Render()
        {
            this.InternalButtonControl.Render();
            this.InternalTextControl.Render();
        }

        public override void Update(double elapsedTime)
        {
            this.InternalButtonControl.Update(elapsedTime);
            this.InternalTextControl.Update(elapsedTime);
        }

        public override string ToString()
        {
            return this.ButtonImage.Name.ToString() + " --> " + base.ToString();
        }

        public override void Release()
        {
            this.InternalButtonControl.Release();
            this.InternalTextControl.Release();

            this.InternalButtonControl = null;
            this.InternalTextControl = null;
            base.Release();
        }
    }
}
