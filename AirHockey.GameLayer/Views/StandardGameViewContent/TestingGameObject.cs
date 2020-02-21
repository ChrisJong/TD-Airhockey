namespace AirHockey.GameLayer.Views.StandardGameViewContent
{
    using ComponentModel;

    class TestingGameObject : GameObjectBase
    {
        public TestingGameObject(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new TestingUserInterfaceComponent(this);
        }
    }
}
