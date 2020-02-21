namespace AirHockey.GameLayer.Views.GameSummaryViewContent
{
    using ComponentModel;

    class GameSummaryUserInterfaceGameObject : GameObjectBase
    {
        public GameSummaryUserInterfaceGameObject(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new GameSummaryUserInterfaceComponent(messageHandlers);
        }
    }
}
