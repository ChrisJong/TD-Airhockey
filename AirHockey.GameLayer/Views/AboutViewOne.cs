namespace AirHockey.GameLayer.Views
{
    using AboutViewContent;
    using Core.Base;
    using ComponentModel.Graphics;

    class AboutViewOne : GameViewBase
    {
        public AboutViewOne()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.About.Background1"), this);
            this.AddGameObject(new AboutUserInterfaceGameObject(0, this));
        }
    }
}
