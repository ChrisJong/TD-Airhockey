namespace AirHockey.GameLayer.Views.GameSummaryViewContent.Keyboard
{
    using GameLayer.ComponentModel;
    using Utility.Classes;
    using InteractionLayer.Components;
    using ComponentModel.DataTransfer;

    class KeyboardManager : GameObjectBase
    {
        public KeyboardManager(Player player, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.UserInterface = new KeyboardUserInterfaceComponent(player, messageHandlers);
        }
    }
}
