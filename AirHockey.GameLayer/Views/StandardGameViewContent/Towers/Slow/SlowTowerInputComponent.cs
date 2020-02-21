namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Slow
{
    using CommonInput;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using Utility.Classes;

    class SlowTowerInputComponent : RadialToggleInputComponent
    {
        public SlowTowerInputComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(typeof(SlowTowerProjectile), parentNode, messageHandlers)
        {
            this.TagInputComponent = new TagInputComponent(
                    player == Player.One ? TagType.PlayerOne.SlowTower : TagType.PlayerTwo.SlowTower,
                    parentNode,
                    this.MessageHandlers.ToArray());
        }
    }
}
