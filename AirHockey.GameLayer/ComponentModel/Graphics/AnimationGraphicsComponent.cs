namespace AirHockey.GameLayer.ComponentModel.Graphics
{
    using System.Globalization;
    using DataTransfer;
    using InteractionLayer.Components;
    using Resources;
    using Utility.Classes;
    using Constants;
    using System;

    /// <summary>
    /// Draws an image at the Game Object's location.
    /// </summary>
    class AnimationGraphicsComponent : GraphicsComponent
    {
        private ResourceName _baseResourceName;
        private int _frameCount = 0;
        private int _frameDuration = 0;
        private int _currentFrame = 1;
        private int _inFrame = 1;
        private double _delta;
        private bool _isPaused;

        public delegate void AnimationCompleteHandler();
        public event AnimationCompleteHandler AnimationComplete;


        public bool AnimationPaused
        {
            get { return this._isPaused; }
            set { this._isPaused = value; }
        }


        public int InFrame
        {
            get { return this._inFrame; }
            set 
            {
                if (value < 1) this._inFrame = 1;
                else if (value > this._frameCount) this._inFrame = this._frameCount;
                else this._inFrame = value;
            }
        }


        public int FrameCount
        {
            get { return this._frameCount; }
            set { this._frameCount = value; }
        }

        public int CurrentFrame
        {
            get { return this._currentFrame; }
            set
            {
                if (value < 1) this._currentFrame = 1;
                else if (value > this.FrameCount + 1 ) this._currentFrame = this.FrameCount;
                else this._currentFrame = value;
            }
        }

        public int FrameDuration
        {
            get { return this._frameDuration; }
            set { this._frameDuration = value; }
        }

        public ResourceName Resource
        {
            get { return this._baseResourceName; }
            set { this._baseResourceName = value; }
        }

        /// <summary>
        /// Comprehensive Animation Constructor. For towers mainly.
        /// </summary>
        /// <param name="baseResourceName"></param>
        /// <param name="animationValues">Uses ResourceName, FrameCount, FrameDuration, and Depth properties</param>
        /// <param name="parentNode">GameOjbectBase (the usually tower)</param>
        /// <param name="messageHandlers"></param>
        public AnimationGraphicsComponent(
            ResourceName baseResourceName,
            AnimationValues animationValues,
            GameObjectBase parentNode,
            params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this._baseResourceName = baseResourceName;
            this._frameCount = animationValues.FrameCount;
            this._frameDuration = animationValues.FrameDuration;
            this._depth = animationValues.RenderingDepth;
        }

        /// <summary>
        /// Basic Animation constructor, requiring minimal information.
        /// </summary>
        /// <param name="baseResourceName">Resource up to the folder</param>
        /// <param name="frameCount"></param>
        /// <param name="durationOfFrame">In milliseconds</param>
        /// <param name="parentNode">A GameObjectBase (usually "this")</param>
        /// <param name="messageHandlers"></param>
        public AnimationGraphicsComponent(
            ResourceName baseResourceName,
            int frameCount,
            int durationOfFrame,
            GameObjectBase parentNode,
            params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.ParentNode = parentNode;
            this._baseResourceName = baseResourceName;
            this._frameCount = frameCount;
            this._frameDuration = durationOfFrame;
        }


        /// <summary>
        /// Updates the frame and does looping. Also fires the AnimationComplete event
        /// </summary>
        public override void Update(double delta)
        {
            base.Update(delta);

            if (!this._isPaused)
            {
                this._delta += delta;

                while ((this._delta >= this.FrameDuration) && this.FrameDuration > 1)
                {
                    if (this.CurrentFrame == this.FrameCount - 1)
                    {
                        if (this.AnimationComplete != null) this.AnimationComplete();
                    }

                    this.CurrentFrame++;
                    if (this.CurrentFrame > this.FrameCount)
                        this.CurrentFrame = this.InFrame;

                    this._delta -= this.FrameDuration;
                }
            }
        }

        public override void Draw()
        {
            var resource = new ResourceName(this._baseResourceName);
            resource.Name = resource.Name + "." + this._currentFrame.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');

            if (this.ParentNode.Physics == null)
            {
                DrawingManager.DrawImage(resource,
                    this.RenderPositionOffset,
                    this.RenderScaleOffset,
                    DrawingOrigin.Center,
                    this.RenderRotationOffset,
                    this.DrawDepth,
                    this._alpha);
            }
            else
            {
                DrawingManager.DrawImage(resource,
                    this.ParentNode.Physics.Position + this.RenderPositionOffset,
                    this.ParentNode.Physics.Scale + this.RenderScaleOffset,
                    DrawingOrigin.Center,
                    this.ParentNode.Physics.Rotation + this.RenderRotationOffset,
                    this.DrawDepth,
                    this._alpha);
            }
        }


        public override object AcceptMessage(string message, params object[] parameters)
        {
            var result = base.AcceptMessage(message, parameters);

            if (result == null)
            {
                if (MessageSystemHelper.ValidateMessage("Pause", message, new[] {typeof (string)}, parameters) &&
                    parameters[0] as string == "Animation")
                {
                    this._isPaused = true;
                    result = MessageResult.Nil;
                }
                else if (MessageSystemHelper.ValidateMessage("UnPause", message, new[] {typeof (string)}, parameters) &&
                         parameters[0] as string == "Animation")
                {
                    this._isPaused = false;
                    result = MessageResult.Nil;
                }
            }

            return result;
        }
    }
}
