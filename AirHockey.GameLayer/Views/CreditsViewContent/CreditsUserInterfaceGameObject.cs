namespace AirHockey.GameLayer.Views.CreditsViewContent
{
    using ComponentModel;

    class CreditsUserInterfaceGameObject : GameObjectBase
    {
        public CreditsUserInterfaceGameObject(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new CreditsUserInterfaceComponent(messageHandlers);
            this.Graphics = new CreditsTextBackgroundGraphicsComponent(messageHandlers);
        }
    }
}
