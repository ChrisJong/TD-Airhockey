namespace AirHockey.GameLayer.Views
{
    using AboutViewContent;
    using Core.Base;
    using ComponentModel.Graphics;

    class AboutViewTwo : GameViewBase
    {
        public AboutViewTwo()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.About.Background2"), this);
            this.AddGameObject(new AboutUserInterfaceGameObject(1, this));
        }
    }
}
