namespace AirHockey.GameLayer.Views.StandardGameViewContent.ExitButton
{
    using ComponentModel;
    using ComponentModel.DataTransfer;
    using ComponentModel.Graphics;
    using Utility.Classes;
    using Resources;
    using Constants;

    class ExitButton : GameObjectBase
    {
        public bool isPressed = false;
        public Player _player;

        public ExitButton(Player player, float x, float y, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            _player = player;
            this.Graphics = new ExitButtonGraphicsComponent(this, this);

            this.Physics = new ExitButtonPhysicsComponent(this)
            {
                Position = new Vector(x, y)
            };

            this.Input = new ExitButtonInputComponent(this, this);

            ExitButtonManager.Instance.AddExitButton(this);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);
        }
    }
}
