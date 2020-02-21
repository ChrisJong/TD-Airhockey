namespace AirHockey.GameLayer.Views.AboutViewContent
{
    using ComponentModel;

    class AboutUserInterfaceGameObject : GameObjectBase
    {
        public AboutUserInterfaceGameObject(int currentPageNo, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new AboutUserInterfaceComponent(currentPageNo, messageHandlers);
            this.Graphics = new AboutTextBackgroundGraphicsComponent(messageHandlers);
        }
    }
}
