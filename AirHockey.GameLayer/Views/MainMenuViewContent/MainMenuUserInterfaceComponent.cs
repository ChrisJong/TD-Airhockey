namespace AirHockey.GameLayer.Views.MainMenuViewContent
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

    class MainMenuUserInterfaceComponent : UserInterfaceComponent
    {
        private bool _leftReady;
        private bool _rightReady;

        private ToggleButtonControl leftReadyButton;
        private ToggleButtonControl rightReadyButton;
        private LabelledButtonControl aboutButton;
        private LabelledButtonControl creditsButton;
        private LabelledButtonControl highscoreButton;
        private LabelledButtonControl armouryButton;
        private LabelledButtonControl exitButton;

        public MainMenuUserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            //Player 1's OK button
            leftReadyButton = new ToggleButtonControl(ViewValues.MainMenuButtons.PlayerOneReadyX, ViewValues.MainMenuButtons.PlayerOneReadyY,
                ViewValues.MainMenuButtons.ReadyWidth, ViewValues.MainMenuButtons.ReadyWidth)
            {
                InactiveImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonReady"),
                ActiveImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonOK"),
                Angle = 90.Radians()
            };

            //Player 2's OK button
            rightReadyButton = new ToggleButtonControl(ViewValues.MainMenuButtons.PlayerTwoReadyX, ViewValues.MainMenuButtons.PlayerTwoReadyY,
                ViewValues.MainMenuButtons.ReadyWidth, ViewValues.MainMenuButtons.ReadyHeight)
            {
                InactiveImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonReady"),
                ActiveImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonOK"),
                Angle = -90.Radians()
            };


            // About Button
            aboutButton = new LabelledButtonControl(ViewValues.MainMenuButtons.AboutX, ViewValues.MainMenuButtons.AboutY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonAbout"),
                Text = "",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonFont"),
                ForeColour = Color.LightGreen
            };

            // Credits, which is the section about the dev team
            creditsButton = new LabelledButtonControl(ViewValues.MainMenuButtons.CreditsX, ViewValues.MainMenuButtons.CreditsY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonCredits"),
                Text = "",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonFont"),
                ForeColour = Color.LightGreen
            };

            // High Scores
            highscoreButton = new LabelledButtonControl(ViewValues.MainMenuButtons.HighScoreX, ViewValues.MainMenuButtons.HighScoreY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonHiscore"),
                Text = "",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonFont"),
                ForeColour = Color.LightGreen
            };

            // Armoury
            armouryButton = new LabelledButtonControl(ViewValues.MainMenuButtons.ArmouryX, ViewValues.MainMenuButtons.ArmouryY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonArmoury"),
                Text = "",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonFont"),
                ForeColour = Color.LightGreen
            };

            // Exit Game
            exitButton = new LabelledButtonControl(ViewValues.MainMenuButtons.ExitX, ViewValues.MainMenuButtons.ExitY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonExit"),
                Text = "",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonFont"),
                ForeColour = Color.LightGreen
            };

            //// Adding Button Events
            leftReadyButton.Click += this.LeftReadyButtonOnClick;
            rightReadyButton.Click += this.RightReadyButtonOnClick;
            aboutButton.Click += this.AboutButtonOnClick;
            creditsButton.Click += this.CreditsButtonOnClick;
            highscoreButton.Click += this.HighscoreButtonOnClick;
            armouryButton.Click += this.ArmouryButtonOnClick;
            exitButton.Click += this.ExitGameButtonOnClick;

            this.Controls.Add(leftReadyButton);
            this.Controls.Add(rightReadyButton);
            this.Controls.Add(aboutButton);
            this.Controls.Add(creditsButton);
            this.Controls.Add(highscoreButton);
            this.Controls.Add(armouryButton);
            this.Controls.Add(exitButton);
        }

        private void RightReadyButtonOnClick(ToggleButtonControl sender, ButtonClickEventArgs args)
        {
            this.ButtonPressAudio();
            this._rightReady = sender.IsActive;
            this.CheckBothPlayersReady();
        }

        private void LeftReadyButtonOnClick(ToggleButtonControl sender, ButtonClickEventArgs args)
        {
            this.ButtonPressAudio();
            this._leftReady = sender.IsActive;
            this.CheckBothPlayersReady();
        }

        private void CreditsButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            this.ButtonPressAudio();
            this.SendMessage<object>("GoTo", typeof(CreditsView));
        }

        private void AboutButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            this.ButtonPressAudio();
            this.SendMessage<object>("GoTo", typeof(AboutViewOne));
        }

        private void HighscoreButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            this.ButtonPressAudio();
            this.SendMessage<object>("GoTo", typeof(HighscoreView));
        }

        private void ArmouryButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            this.ButtonPressAudio();
            this.SendMessage<object>("GoTo", typeof(ArmouryViewOne));
        }

        private void ExitGameButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            this.ButtonPressAudio();
            this.SendMessage<object>("EndGame");
        }

        private void ButtonPressAudio()
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ButtonPress");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }

        private void CheckBothPlayersReady()
        {
            if (this._leftReady && this._rightReady)
            {
                this.SendMessage<object>("GoTo", typeof (StandardGameView));
            }
        }

        public override void Release()
        {
            this.Controls.ForEach(o => o.Release());

            this.Controls.Clear();
            base.Release();
        }
    }
}
