namespace AirHockey.GameLayer.Views.CreditsViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.GUI;
    using GUI;
    using GUI.Events;
    using Resources;
    using AirHockey.Constants;

    /// <summary>
    /// Displayes the text and other User Interface elements for the
    /// Credits view.
    /// </summary>
    class CreditsUserInterfaceComponent : UserInterfaceComponent
    {
        public CreditsUserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var mainMenuButton = new LabelledButtonControl(ViewValues.NavButtons.GotoMainX, ViewValues.NavButtons.GotoMainY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.Credits.ButtonMain"),
                Font = this.SendMessage<ResourceName>("Resource", "Resources.Credits.ButtonFont")
            };
            mainMenuButton.Click += this.MainMenuButtonOnClick;
            this.Controls.Add(mainMenuButton);
        }

        private void MainMenuButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ButtonPress");
            InteractionLayer.Components.AudioManager.PlaySound(resource);

            this.SendMessage<object>("GoTo", typeof (MainMenuView));
        }
    }
}
