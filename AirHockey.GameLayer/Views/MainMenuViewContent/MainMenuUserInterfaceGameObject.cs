namespace AirHockey.GameLayer.Views.MainMenuViewContent
{
    using ComponentModel;

    class MainMenuUserInterfaceGameObject : GameObjectBase
    {
        public MainMenuUserInterfaceGameObject(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new MainMenuUserInterfaceComponent(messageHandlers);
        }
    }
}
