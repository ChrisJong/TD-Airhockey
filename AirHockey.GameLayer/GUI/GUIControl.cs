namespace AirHockey.GameLayer.GUI
{
    using LogicLayer.Collisions;
    using Utility.Classes;

    /// <summary>
    /// A common base class for all controls such as Button
    /// and InputBox.
    /// </summary>
    abstract class GUIControl : UpdateAndDrawableBase
    {
        /// <summary>
        /// The X position of the button. Value may be slightly
        /// off if this XOR the relative to object is not updated.
        /// </summary>
        public int X
        {
            get;
            set;
        }

        /// <summary>
        /// The Y position of the button. Value may be slightly
        /// off if this XOR the relative to object is not updated.
        /// </summary>
        public int Y
        {
            get;
            set;
        }

        /// <summary>
        /// The X position which is relative to the RelativeTo
        /// physics object.
        /// </summary>
        public int RelativeX
        {
            get;
            set;
        }

        /// <summary>
        /// The Y position which is relative to the RelativeTo
        /// physics object.
        /// </summary>
        public int RelativeY
        {
            get;
            set;
        }

        /// <summary>
        /// The Width of the button.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// The Height of the button.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// What this object's X and Y are relative to.
        /// </summary>
        public IPhysicsObject RelativeTo
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an instance of a <see cref="ButtonControl"/> with an absolute position.
        /// </summary>
        /// <param name="x">The X position of the button.</param>
        /// <param name="y">The Y position of the button.</param>
        /// <param name="width">The Width of the button.</param>
        /// <param name="height">The Height of the button.</param>
        protected GUIControl(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Updates the relative positioning of the GUI control.
        /// </summary>
        /// <param name="elapsedTime">The time that has elapsed since the last frame.</param>
        public override void Update(double elapsedTime)
        {
            if (this.RelativeTo != null)
            {
                this.X = (int) (this.RelativeTo.Position.X + 0.5f) + this.RelativeX;
                this.Y = (int) (this.RelativeTo.Position.Y + 0.5f) + this.RelativeY;
            }
        }

        public override string ToString()
        {
            return "GUI Control (X:" + this.X.ToString() + " Y:" + this.Y.ToString() + ")";
        }

        public override void Release()
        {
            //base.Release() calls stop here. Any further and exceptiosn happen.
        }
    }
}
