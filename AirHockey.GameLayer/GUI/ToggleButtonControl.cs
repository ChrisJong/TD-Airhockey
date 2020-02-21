namespace AirHockey.GameLayer.GUI
{
    using ComponentModel.DataTransfer;
    using Events;
    using LogicLayer.Collisions;
    using Resources;

    class ToggleButtonControl : GUIControl
    {
        private ButtonControl _internalButtonControl;
        private ResourceName _inactiveImage;
        private ResourceName _disabledInactiveImage;
        private ResourceName _activeImage;
        private ResourceName _disabledActiveImage;
        private bool _isActive;

        public delegate void ClickHandler(ToggleButtonControl sender, ButtonClickEventArgs args);
        public event ClickHandler Click;

        /// <summary>
        /// The angle at which to draw the button's images.
        /// </summary>
        public float Angle
        {
            get { return this._internalButtonControl.Angle; }
            set { this._internalButtonControl.Angle = value; }
        }

        [MessageDataMember]
        public bool Disabled
        {
            get { return this._internalButtonControl.Disabled; }
            set { this._internalButtonControl.Disabled = value; }
        }

        [MessageDataMember]
        public bool IsActive
        {
            get { return this._isActive; }
            set
            {
                this._isActive = value;

                if (value)
                {
                    this._internalButtonControl.Image = this.ActiveImage;
                    this._internalButtonControl.DisabledImage = this.DisabledActiveImage;
                }
                else
                {
                    this._internalButtonControl.Image = this.InactiveImage;
                    this._internalButtonControl.DisabledImage = this.DisabledInactiveImage;
                }
            }
        }

        public ResourceName InactiveImage
        {
            get { return this._inactiveImage; }
            set
            {
                this._inactiveImage = value;

                if (!this.IsActive)
                {
                    this._internalButtonControl.Image = value;
                }
            }
        }

        public ResourceName ActiveImage
        {
            get { return this._activeImage; }
            set
            {
                this._activeImage = value;

                if (this.IsActive)
                {
                    this._activeImage = value;
                }
            }
        }

        public ResourceName DisabledInactiveImage
        {
            get { return this._disabledInactiveImage; }
            set
            {
                this._disabledInactiveImage = value;

                if (!this.IsActive)
                {
                    this._internalButtonControl.DisabledImage = value;
                }
            }
        }

        public ResourceName DisabledActiveImage
        {
            get { return this._disabledActiveImage; }
            set
            {
                this._disabledActiveImage = value;

                if (this.IsActive)
                {
                    this._internalButtonControl.DisabledImage = value;
                }
            }
        }

        public ToggleButtonControl(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            this._internalButtonControl = new ButtonControl(x, y, width, height);
            this._internalButtonControl.Click += this.OnInternalButtonClick;
        }

        public override void Render()
        {
            if (this._internalButtonControl == null) return;
            this._internalButtonControl.Render();
        }

        public override void Update(double elapsedTime)
        {
            this._internalButtonControl.Update(elapsedTime);
        }

        private void OnInternalButtonClick(ButtonControl sender, ButtonClickEventArgs args)
        {
            this.IsActive = !this.IsActive;
            if (this.Click != null) this.Click.Invoke(this, args);
        }

        public override void Release()
        {
            this._internalButtonControl.Release();
            this._internalButtonControl = null;
            this._inactiveImage = null;
            this._disabledInactiveImage= null;
            this._activeImage = null;
            this._disabledActiveImage = null;
            this.Click = null;
            base.Release();
        }
    }
}
