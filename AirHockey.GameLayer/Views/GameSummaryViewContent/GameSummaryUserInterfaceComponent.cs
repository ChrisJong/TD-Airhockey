namespace AirHockey.GameLayer.Views.GameSummaryViewContent
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using ComponentModel;
    using ComponentModel.GUI;
    using GUI;
    using GUI.Events;
    using InteractionLayer.Components;
    using Resources;
    using AirHockey.Constants;

    class GameSummaryUserInterfaceComponent : UserInterfaceComponent
    {
        private GameSummaryData summaryData;

        private const int GameSummaryTextWidth = 420;
        private const int GameSummaryTextHeight = 120;
        private const int GameSummaryTextX = 750;

        private const string GameStartedFormat = "hh:mm:ss (dd/MM/yy)";
        private bool CreateMainMenuButton = false;

        public GameSummaryUserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var summaryTextFont = this.SendMessage<ResourceName>("Resource", "Resources.GameSummary.SummaryText");
            var ScoreTextFont = this.SendMessage<ResourceName>("Resource", "Resources.GameSummary.ScoreText");
            this.summaryData = this.SendMessage<GameSummaryData>("Get", "SummaryData");
            
            // Game Start
            this.Controls.Add(
                new TextControl(GameSummaryTextX, 191, GameSummaryTextWidth, GameSummaryTextHeight)
                {
                    CentreTextInBounds = true,
                    Font = summaryTextFont,
                    Text = summaryData.GameStartTime.ToString(GameStartedFormat),
                    Colour = Color.White
                });
            // Game Duration.
            this.Controls.Add(
                new TextControl(GameSummaryTextX, 296, GameSummaryTextWidth, GameSummaryTextHeight)
                {
                    CentreTextInBounds = true,
                    Font = summaryTextFont,
                    Text = ((int)summaryData.GameDuration / 1000) + " Seconds",
                    Colour = Color.White
                });


            // Player One Score.
            this.Controls.Add(
                new TextControl(GameSummaryTextX, 530, GameSummaryTextWidth, GameSummaryTextHeight)
                {
                    CentreTextInBounds = true,
                    Font = ScoreTextFont,
                    Text = summaryData.PlayerOneScore.ToString(CultureInfo.InvariantCulture),
                    Colour = Color.HotPink
                });

            // Player Two Score.
            this.Controls.Add(
                new TextControl(GameSummaryTextX, 780, GameSummaryTextWidth, GameSummaryTextHeight)
                {
                    CentreTextInBounds = true,
                    Font = ScoreTextFont,
                    Text = summaryData.PlayerTwoScore.ToString(CultureInfo.InvariantCulture),
                    Colour = Color.Cyan
                });
        }

        public override void Update(double elapsedTime)
        {
            if (!this.CreateMainMenuButton)
            {
                if (this.SendMessage<bool>("Get", "DisplayMainMenu"))
                {
                    this.CreateMainMenu();
                    this.CreateMainMenuButton = true;
                }
            }
            base.Update(elapsedTime);
        }

        private void CreateMainMenu()
        {
            this.summaryData = this.SendMessage<GameSummaryData>("Get", "SummaryData");
            GameDataHelper.SaveGameResults(this.summaryData);

            // Main Menu Button
            var mainMenuButton = new LabelledButtonControl(ViewValues.NavButtons.GotoMainX, ViewValues.NavButtons.GotoMainY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height) //X: 747, y: 850
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.GameSummary.ButtonMain"),
                Text = "",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.MainMenu.ButtonFont"),
                ForeColour = Color.LightGreen
            };
            mainMenuButton.Click += this.MainMenuButtonOnClick;
            this.Controls.Add(mainMenuButton);
        }

        private void MainMenuButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ButtonPress");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
            this.SendMessage<object>("GoTo", typeof(MainMenuView));
        }
    }
}
