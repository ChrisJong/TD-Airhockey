namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Stasis
{
    using CommonInput;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using Utility.Classes;

    class StasisTowerInputComponent : RadialToggleInputComponent
    {
        public StasisTowerInputComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(null, parentNode, messageHandlers)
        {
            this.TagInputComponent = new TagInputComponent(
                    player == Player.One ? TagType.PlayerOne.StasisTower : TagType.PlayerTwo.StasisTower,
                    parentNode,
                    this.MessageHandlers.ToArray());
        }
    }
}
