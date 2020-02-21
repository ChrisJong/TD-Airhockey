namespace AirHockey.GameLayer.Views.ArmouryViewContent
{
    using ComponentModel;

    class ArmouryUserInterfaceGameObject : GameObjectBase
    {
        public ArmouryUserInterfaceGameObject(int currentPageNo, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new ArmouryUserInterfaceComponent(currentPageNo, messageHandlers);
            this.Graphics = new ArmouryTextBackgroundGraphicsComponent(messageHandlers);
        }
    }
}
