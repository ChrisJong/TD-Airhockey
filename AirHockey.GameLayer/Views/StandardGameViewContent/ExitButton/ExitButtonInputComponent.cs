namespace AirHockey.GameLayer.Views.StandardGameViewContent.ExitButton
{
    using Towers.CommonInput;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using InteractionLayer.Components.Input;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;

    class ExitButtonInputComponent : InputComponent
    {
        private ExitButton _myButton;

        public ExitButtonInputComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            InputManager.OnFingerPressed += this.OnFingerPressed;
            InputManager.OnFingerMoved += this.OnFingerMoved;
            InputManager.OnFingerReleased += this.OnFingerReleased;

            _myButton = (ExitButton)parentNode;
        }

        public bool CheckBounds(float x, float y, TouchPoint point)
        {
            var left = x - 50;      // left
            var right = x + 50;     // right
            var top = y - 50;       // top
            var bottom = y + 50;    // bottoom

            if ((point.Location.X < right && point.Location.X > left) && (point.Location.Y > top && point.Location.Y < bottom))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void OnFingerPressed(TouchPoint point)
        {
            if (CheckBounds(this.ParentNode.Physics.Position.X, this.ParentNode.Physics.Position.Y, point) == true)
            {
                if (_myButton.isPressed == false)
                {
                    _myButton.isPressed = true;
                }
            }
        }

        protected void OnFingerReleased(TouchPoint point)
        {
            if (this._myButton == null)
                return;

            _myButton.isPressed = false;

        }

        protected void OnFingerMoved(TouchPoint originPoint, TouchPoint currentPoint)
        {
            if (CheckBounds(this.ParentNode.Physics.Position.X, this.ParentNode.Physics.Position.Y, currentPoint) == false)
            {
                _myButton.isPressed = false;
            }
        }

        public override void Process()
        {
            //Do nothing
        }

        public override void Release()
        {
            this._myButton = null;
            InputManager.OnFingerPressed -= this.OnFingerPressed;
            InputManager.OnFingerMoved -= this.OnFingerMoved;
            InputManager.OnFingerReleased -= this.OnFingerReleased;
            base.Release();
        }
    }
}
