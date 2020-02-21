namespace AirHockey.GameLayer.Views
{
    using ComponentModel.Graphics;
    using Core.Base;
    using MainMenuViewContent;

    class MainMenuView : GameViewBase
    {
        public MainMenuView()
        {
            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.MainMenu.Background"), this);
            this.AddGameObject(new MainMenuUserInterfaceGameObject(this));
        }

        public override void Release()
        {
            this.Background.Release(); //Since it inherits from componentbase, the messagehandler gets clear()'d.
            this.Background = null;

            base.Release(); //Base class handles game object clearing hit F12 to see it..
        }
    }
}
