namespace AirHockey.GameLayer.Views
{
    using Core.Base;
    using ComponentModel.Graphics;
    using ArmouryViewContent;

    /// <summary>
    /// Displays the credits for the developers of the game.
    /// </summary>
    class ArmouryViewThree : GameViewBase
    {
        public ArmouryViewThree()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.Armoury.Background3"), this);
            this.AddGameObject(new ArmouryUserInterfaceGameObject(2, this));
        }
    }
}
