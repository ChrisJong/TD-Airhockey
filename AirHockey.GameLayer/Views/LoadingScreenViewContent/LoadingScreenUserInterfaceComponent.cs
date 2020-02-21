namespace AirHockey.GameLayer.Views.LoadingScreenViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.GUI;
    using GUI;
    using GUI.Events;
    using Resources;
    using Utility.Extensions;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Constants;

    class LoadingScreenUserInterfaceComponent : UserInterfaceComponent
    {
        private TextControl _currentResourceText;

        public LoadingScreenUserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var defaultFont = this.SendMessage<ResourceName>("Resource", "Resources.Global.DefaultFont");
            this._currentResourceText = new TextControl(150, 150, 1000, 1000)
            {
                CentreTextInBounds = false,
                Text = "LOADING RESOURCE",
                Colour = Color.White,
                Font = defaultFont
            };

            this.Controls.Add(this._currentResourceText);
        }

        public override void Update(double elapsedTime)
        {
            base.Update(elapsedTime);
            if (ResourceManager.LoadingCompleted || AirHockey.Constants.GlobalSettings.PreloadResources == false)
            {
                AirHockey.InteractionLayer.EntryPointWrapper.SetFullScreen(GlobalSettings.FullScreenMode);
                this.SendMessage<object>("GoTo", typeof(MainMenuView));
            }
            else
            {
                this._currentResourceText.Text = ResourceManager.GetCurrentResourceName();
                ResourceManager.PreLoadResource();
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Release()
        {
            this.Controls.ForEach(o => o.Release());

            this.Controls.Clear();
            base.Release();
        }
    }
}
