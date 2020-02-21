namespace AirHockey.GameLayer.Views.AboutViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.GUI;
    using GUI;
    using GUI.Events;
    using Resources;
    using AirHockey.Constants;

    class AboutUserInterfaceComponent : UserInterfaceComponent
    {
        private int _currentPage = 0;

        public AboutUserInterfaceComponent(int currentPage = 0, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this._currentPage = currentPage;

            LabelledButtonControl nextPageButton;
            LabelledButtonControl previousPageButton;

            switch(currentPage){
                case 0:
                    nextPageButton = new LabelledButtonControl(ViewValues.NavButtons.NextPageX, ViewValues.NavButtons.NextPageY,
                        ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
                    {
                        ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonNext"),
                        Font = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonFont"),
                    };
                    nextPageButton.Click += this.NextPageButtonOnClick;
                    this.Controls.Add(nextPageButton);
                break;
                    
                case 3:
                    previousPageButton = new LabelledButtonControl(ViewValues.NavButtons.PrevPageX, ViewValues.NavButtons.PrevPageY,
                        ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
                    {
                        ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonPrev"),
                        Font = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonFont"),
                    };
                    previousPageButton.Click += this.PreviousPageButtonOnClick;
                    this.Controls.Add(previousPageButton);
                break;

                default:
                    previousPageButton = new LabelledButtonControl(ViewValues.NavButtons.PrevPageX, ViewValues.NavButtons.PrevPageY,
                        ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
                    {
                        ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonPrev"),
                        Font = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonFont"),
                    };
                    previousPageButton.Click += this.PreviousPageButtonOnClick;
                    this.Controls.Add(previousPageButton);

                    nextPageButton = new LabelledButtonControl(ViewValues.NavButtons.NextPageX, ViewValues.NavButtons.NextPageY,
                        ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
                    {
                        ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonNext"),
                        Font = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonFont"),
                    };
                    nextPageButton.Click += this.NextPageButtonOnClick;
                    this.Controls.Add(nextPageButton);
                break;
            }

            var mainMenuButton = new LabelledButtonControl(ViewValues.NavButtons.GotoMainX, ViewValues.NavButtons.GotoMainY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonMain"),
                Font = this.SendMessage<ResourceName>("Resource", "Resources.About.ButtonFont"),
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

        private void NextPageButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonBlickEventArgs)
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ButtonPress");
            InteractionLayer.Components.AudioManager.PlaySound(resource);

            switch(this._currentPage){
                case 0:
                    this.SendMessage<object>("GoTo", typeof(AboutViewTwo));
                break;

                case 1:
                    this.SendMessage<object>("GoTo", typeof(AboutViewThree));
                break;

                case 2:
                    this.SendMessage<object>("GoTo", typeof(AboutViewFour));
                break;

                case 3:
                    this.SendMessage<object>("GoTo", typeof(AboutViewOne));
                break;
            }
        }

        private void PreviousPageButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonBlickEventArgs)
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ButtonPress");
            InteractionLayer.Components.AudioManager.PlaySound(resource); 
            
            switch (this._currentPage)
            {
                case 0:
                    this.SendMessage<object>("GoTo", typeof(AboutViewFour));
                    break;

                case 1:
                    this.SendMessage<object>("GoTo", typeof(AboutViewOne));
                    break;

                case 2:
                    this.SendMessage<object>("GoTo", typeof(AboutViewTwo));
                    break;

                case 3:
                    this.SendMessage<object>("GoTo", typeof(AboutViewThree));
                    break;
            }
        }
    }
}
