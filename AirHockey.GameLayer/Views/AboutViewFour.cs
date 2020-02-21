namespace AirHockey.GameLayer.Views
{
    using AboutViewContent;
    using Core.Base;
    using ComponentModel.Graphics;

    class AboutViewFour : GameViewBase
    {
        public AboutViewFour()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.About.Background4"), this);
            this.AddGameObject(new AboutUserInterfaceGameObject(3, this));
        }
    }
}
