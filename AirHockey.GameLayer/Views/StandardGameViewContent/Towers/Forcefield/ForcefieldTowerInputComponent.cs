namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Forcefield
{
    using CommonInput;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using Utility.Classes;

    class ForcefieldTowerInputComponent : RadialToggleInputComponent
    {
        public ForcefieldTowerInputComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(typeof(ForcefieldTowerProjectile), parentNode, messageHandlers)
        {
            this.TagInputComponent = new TagInputComponent(
                    player == Player.One ? TagType.PlayerOne.ForcefieldTower : TagType.PlayerTwo.ForcefieldTower,
                    parentNode,
                    this.MessageHandlers.ToArray());
        }
    }
}
