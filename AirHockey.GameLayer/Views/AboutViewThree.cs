namespace AirHockey.GameLayer.Views
{
    using AboutViewContent;
    using Core.Base;
    using ComponentModel.Graphics;

    class AboutViewThree : GameViewBase
    {
        public AboutViewThree()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.About.Background3"), this);
            this.AddGameObject(new AboutUserInterfaceGameObject(2, this));
        }
    }
}
