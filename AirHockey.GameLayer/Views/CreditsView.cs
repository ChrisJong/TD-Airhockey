namespace AirHockey.GameLayer.Views
{
    using Core.Base;
    using ComponentModel.Graphics;
    using CreditsViewContent;

    /// <summary>
    /// Displays the credits for the developers of the game.
    /// </summary>
    class CreditsView : GameViewBase
    {
        public CreditsView()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.Credits.Background"), this);
            this.AddGameObject(new CreditsUserInterfaceGameObject(this));
        }
    }
}
