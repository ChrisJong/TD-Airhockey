namespace AirHockey.GameLayer.GUI
{
    using System;
    using System.Drawing;
    using Events;
    using InteractionLayer.Components;
    using InteractionLayer.Components.Input;
    using LogicLayer.Collisions;
    using Resources;
    using Utility.Classes;
    using Utility.Extensions;

    /// <summary>
    /// Controls the input and display of a button.
    /// </summary>
    class ButtonControl : GUIControl
    {
        public delegate void ButtonClickHandler(ButtonControl sender, ButtonClickEventArgs e);

        private ResourceName _image;
        private ResourceName _pressedImage;
        private ResourceName _disabledImage;

        private bool _buttonDown;
        private int _fingerId;

        /// <summary>
        /// This event is triggered when a user presses and releases
        /// a finger on this button.
        /// </summary>
        public event ButtonClickHandler Click;

        /// <summary>
        /// Whether or not the button can be interracted with.
        /// </summary>
        public bool Disabled
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

        /// <summary>
        /// The angle at which to draw the image.
        /// </summary>
        public float Angle
        {
            get;
            set;
        }

        /// <summary>
        /// The image to be used in the button's default state.
        /// </summary>
        public ResourceName Image
        {
            get { return this._image; }
            set
            {
                if (value == null)
                {
                    this._image = null;
                    return;
                }

                var imageDimensions = DrawingManager.GetImageDimensions(value);

                if (imageDimensions.X != this.Width || imageDimensions.Y != this.Height)
                {
                    throw new ArgumentException("The given image's dimensions do not match the button's dimensions.");
                }

                this._image = value;
            }
        }

        /// <summary>
        /// Optional. The image to be used in the button's pressed/down
        /// state.
        /// </summary>
        public ResourceName PressedImage
        {
            get { return this._pressedImage; }
            set
            {
                if (value == null)
                {
                    this._pressedImage = null;
                    return;
                }

                var imageDimensions = DrawingManager.GetImageDimensions(value);

                if (imageDimensions.X != this.Width || imageDimensions.Y != this.Height)
                {
                    throw new ArgumentException("The given image's dimensions do not match the button's dimensions.");
                }

                this._pressedImage = value;
            }
        }

        /// <summary>
        /// Optional. The image to be used when the button is disabled.
        /// </summary>
        public ResourceName DisabledImage
        {
            get { return this._disabledImage; }
            set
            {
                if (value == null)
                {
                    this._disabledImage = null;
                    return;
                }

                var imageDimensions = DrawingManager.GetImageDimensions(value);

                if (imageDimensions.X != this.Width || imageDimensions.Y != this.Height)
                {
                    throw new ArgumentException("The given image's dimensions do not match the button's dimensions.");
                }

                this._disabledImage = value;
            }
        }

        /// <summary>
        /// Creates an instance of a <see cref="ButtonControl"/> with an absolute position.
        /// </summary>
        /// <param name="x">The X position of the button.</param>
        /// <param name="y">The Y position of the button.</param>
        /// <param name="width">The Width of the button.</param>
        /// <param name="height">The Height of the button.</param>
        public ButtonControl(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            InputManager.OnFingerPressed += this.OnFingerPressed;
            InputManager.OnFingerReleased += this.OnFingerReleased;
        }

        void OnFingerReleased(TouchPoint point)
        {
            if (this._buttonDown && point.Id == this._fingerId)
            {
                this._buttonDown = false;
                this._fingerId = 0;

                if (point.Location.IsInside(new Rectangle(this.X, this.Y, this.Width, this.Height)) &&
                    this.Click != null)
                {
                    this.Click.Invoke(this, new ButtonClickEventArgs(point.Location));
                }
            }
        }

        void OnFingerPressed(TouchPoint point)
        {
            if (!this._buttonDown && !this.Disabled &&
                point.Location.IsInside(new Rectangle(this.X, this.Y, this.Width, this.Height)))
            {
                this._buttonDown = true;
                this._fingerId = point.Id;
            }
        }

        /// <summary>
        /// Draws the button on to the screen.
        /// </summary>
        public override void Render()
        {
            var finalX = this.X + this.Width/2;
            var finalY = this.Y + this.Height/2;

            if (this.Disabled && this.DisabledImage != null)
            {
                DrawingManager.DrawImage(this.DisabledImage, finalX, finalY, DrawingOrigin.Center, this.Angle, this.DrawDepth);
            }
            else if (this._buttonDown && !this.Disabled && this.PressedImage != null)
            {
                DrawingManager.DrawImage(this.PressedImage, finalX, finalY, DrawingOrigin.Center, this.Angle, this.DrawDepth);
            }
            else if (this.Image != null)
            {
                DrawingManager.DrawImage(this.Image, finalX, finalY, DrawingOrigin.Center, this.Angle, this.DrawDepth);
            }
        }

        public override void Release()
        {
            InputManager.OnFingerPressed -= this.OnFingerPressed;
            InputManager.OnFingerReleased -= this.OnFingerReleased;

            this._image = null;
            this._pressedImage = null;
            this._disabledImage = null;

            Click = null;
            base.Release();
        }
    }
}
