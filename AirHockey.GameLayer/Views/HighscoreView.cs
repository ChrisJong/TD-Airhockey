namespace AirHockey.GameLayer.Views
{
    using Core.Base;
    using HighscoreViewContent;
    using ComponentModel.Graphics;

    /// <summary>
    /// Displays the highscore values in order of shortest to longest
    /// game duration.
    /// </summary>
    class HighscoreView : GameViewBase
    {
        public HighscoreView()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.Highscore.Background"), this);
            this.AddGameObject(new HighscoreUserInterfaceGameObject(this));
        }
    }
}
