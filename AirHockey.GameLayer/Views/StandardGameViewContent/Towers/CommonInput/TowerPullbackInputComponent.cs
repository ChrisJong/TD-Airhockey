namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonInput
{
    //NOTE: Subcomponents must be initialised with this.MessageHandlers.ToArray() so that they can access the game object as well.
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using InteractionLayer.Components;
    using InteractionLayer.Components.Input;
    using Utility.Classes;

    abstract class TowerPullbackInputComponent : InputComponent
    {
        private TagInputComponent _tagInputComponent;
        private TowerObjectBase _myTower;
        private float _maxInputLength = TowerValues.SlingshotTower.MaxPullbackLength;

        public float MaxInputLength
        {
            get { return this._maxInputLength; }
            set { this._maxInputLength = value; }
        }

        public TagInputComponent TagInputComponent
        {
            get { return this._tagInputComponent; }
            set { this._tagInputComponent = value; }
        }

        public int? FingerId
        {
            get;
            set;
        }

        public TowerObjectBase MyTower
        {
            get { return this._myTower; }
            set { this._myTower = value; }
        }

        protected TowerPullbackInputComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.ParentNode = parentNode;
            this._myTower = (TowerObjectBase)parentNode;

            InputManager.OnFingerPressed += this.OnFingerPressed;
            InputManager.OnFingerMoved += this.OnFingerMoved;
            InputManager.OnFingerReleased += this.OnFingerReleased;
        }


        protected abstract void OnFingerPressed(TouchPoint point);
        protected abstract void OnFingerReleased(TouchPoint point);

        protected void OnFingerMoved(TouchPoint originPoint, TouchPoint currentPoint)
        {
            if (currentPoint.Id == this.FingerId)
            {
                this._myTower.PullBackPoint = this.GetPullBackPoint(currentPoint, this.MaxInputLength);
                this._myTower.PullBackRotation = this.GetPullBackRotation(currentPoint) - ParentNode.Physics.Rotation;
            }
        }

        
        public override void Process()
        {
            // do not update tower position if it is being interacted with by a finger
            if (this.FingerId == null)
            {
                this._tagInputComponent.Process();
            }
        }

        /// <summary>
        /// Calculates the PullBackPoint based on the finger point
        /// and the max pullback length cap.
        /// </summary>
        /// <param name="point">The finger point.</param>
        /// <param name="maxLength">The maximum pullback length.</param>
        /// <returns>The resultant pull back length.</returns>
        protected Point GetPullBackPoint(TouchPoint point, float maxLength)
        {
            var position = this.MyTower.Physics.Position;
            var offset = point.Location - this.MyTower.Physics.Position;
            var distanceSq = offset.LengthSq;

            if (distanceSq > maxLength * maxLength)
            {
                offset = offset.UnitVector * maxLength;
            }
            return position + offset;
        }


        protected float GetPullBackRotation(TouchPoint point)
        {
            return Vector.AngleBetweenInRadian(this._myTower.Physics.Position, point.Location);
        }


        public override void Release()
        {
            this._myTower = null;
            InputManager.OnFingerPressed -= this.OnFingerPressed;
            InputManager.OnFingerMoved -= this.OnFingerMoved;
            InputManager.OnFingerReleased -= this.OnFingerReleased;
            base.Release();
        }
    }
}
