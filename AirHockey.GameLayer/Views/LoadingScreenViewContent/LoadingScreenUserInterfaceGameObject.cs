namespace AirHockey.GameLayer.Views.LoadingScreenViewContent
{
    using ComponentModel;

    class LoadingScreenUserInterfaceGameObject : GameObjectBase
    {
        public LoadingScreenUserInterfaceGameObject(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new LoadingScreenUserInterfaceComponent(messageHandlers);
        }
    }
}
