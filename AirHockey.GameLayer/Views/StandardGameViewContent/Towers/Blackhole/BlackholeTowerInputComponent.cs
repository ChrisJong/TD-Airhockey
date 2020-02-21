namespace AirHockey.GameLayer.Views.StandardGameViewContent
{
    using ComponentModel;
    using ComponentModel.Input;
    using Towers.Blackhole;
    using Towers.CommonInput;
    using Utility.Classes;
    using Constants;

    class BlackholeTowerInputComponent : RadialToggleInputComponent
    {
        public BlackholeTowerInputComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(typeof(BlackholeTowerProjectile), parentNode, messageHandlers)
        {
            this.TagInputComponent = new TagInputComponent(
                    player == Player.One ? TagType.PlayerOne.BlackholeTower : TagType.PlayerTwo.BlackholeTower,
                    parentNode,
                    this.MessageHandlers.ToArray());
        }
    }
}
