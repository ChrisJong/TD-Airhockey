namespace AirHockey.GameLayer.Views.HighscoreViewContent
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using ComponentModel;
    using ComponentModel.GUI;
    using GameSummaryViewContent;
    using GUI;
    using GUI.Events;
    using Resources;
    using Constants;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;

    class HighscoreUserInterfaceComponent : UserInterfaceComponent
    {
        private const int GameSummaryMaxRowCount = 10;

        private const int GameHistoryX = 125;
        private const int GameHistoryY = 200;
        private const int GameHistoryRowOffset = 62;
        private const int GameHistoryFieldOffset = 300;

        private const string GameCompletedFormat = "hh:mm:ss (dd/MM/yy)";

        public HighscoreUserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var mainMenuButton = new LabelledButtonControl(ViewValues.NavButtons.GotoMainX, ViewValues.NavButtons.GotoMainY,
                ViewValues.NavButtons.Width, ViewValues.NavButtons.Height)
            {
                ButtonImage = this.SendMessage<ResourceName>("Resource", "Resources.Highscore.ButtonMain"),
                Font = this.SendMessage<ResourceName>("Resource", "Resources.Highscore.ButtonFont")
            };
            mainMenuButton.Click += this.MainMenuButtonOnClick;
            this.Controls.Add(mainMenuButton);

            IList<GameSummaryData> gameSummaryHistory = this.SendMessage<List<GameSummaryData>>("Get", "GameSummaryHistory");
            var gameHistoryFont = this.SendMessage<ResourceName>("Resource", "Resources.Highscore.RowText");

            if (gameSummaryHistory.Count == 0)
            {
                this.Controls.Add(new TextControl(556, 98, 800, 90)
                {
                    CentreTextInBounds = true,
                    Text = "(No game history file found)",
                    Colour = Color.Gray,
                    Font = gameHistoryFont
                });
            }
            else
            {
                if (gameSummaryHistory.Count > GameSummaryMaxRowCount)
                {
                    gameSummaryHistory =
                        gameSummaryHistory.Skip(gameSummaryHistory.Count - GameSummaryMaxRowCount)
                                          .OrderBy(x => x.GameDuration)
                                          .ToList();
                }

                //this.CreateSummaryHistoryHeading(gameHistoryFont);
                for (var i = 0; i < gameSummaryHistory.Count; i++)
                {
                    this.CreateSummaryHistoryRow(gameHistoryFont, gameSummaryHistory[i], i+1);
                }
            }
        }

        private void CreateSummaryHistoryRow(ResourceName font, GameSummaryData data, int index)
        {

            //Winner
            var winningImage = this.SendMessage<ResourceName>("Resource", "Resources.Highscore.star");
            if (data.WinningPlayer == Player.One)
            {
                this.Controls.Add(
                    new ButtonControl(
                        GameHistoryX,
                        GameHistoryY + GameHistoryRowOffset * index,
                        52,
                        62)
                    {
                        Image = winningImage
                    });
            }
            else
            {
                this.Controls.Add(
                    new ButtonControl(
                        GameHistoryX + 274,
                        GameHistoryY + GameHistoryRowOffset * index,
                        52,
                        62)
                    {
                        Image = winningImage
                    });
            }

            // PlayerOne Name
            this.Controls.Add(
                new TextControl(
                    GameHistoryX + 52,
                    GameHistoryY + GameHistoryRowOffset * index,
                    GameHistoryFieldOffset,
                    GameHistoryRowOffset)
                {
                    Font = font,
                    Text = data.PlayerOneName,
                    Colour = Color.White
                });

            // PlayerTwo Name
            this.Controls.Add(
                new TextControl(
                    GameHistoryX + GameHistoryFieldOffset + 52,
                    GameHistoryY + GameHistoryRowOffset * index,
                    GameHistoryFieldOffset,
                    GameHistoryRowOffset)
                {
                    Font = font,
                    Text = data.PlayerTwoName,
                    Colour = Color.White
                });

            //RED Score
            this.Controls.Add(
                new TextControl(
                    GameHistoryX + GameHistoryFieldOffset * 2,
                    GameHistoryY + GameHistoryRowOffset * index,
                    GameHistoryFieldOffset,
                    GameHistoryRowOffset)
                {
                    Font = font,
                    Text = data.PlayerOneScore.ToString(CultureInfo.InvariantCulture),
                    Colour = Color.White
                });

            //Blue Score
            this.Controls.Add(
                new TextControl(
                    GameHistoryX + GameHistoryFieldOffset * 3,
                    GameHistoryY + GameHistoryRowOffset * index,
                    GameHistoryFieldOffset,
                    GameHistoryRowOffset)
                {
                    Font = font,
                    Text = data.PlayerTwoScore.ToString(CultureInfo.InvariantCulture),
                    Colour = Color.White
                });

            //Game Start
            this.Controls.Add(
                new TextControl(
                    GameHistoryX + GameHistoryFieldOffset * 4,
                    GameHistoryY + GameHistoryRowOffset * index,
                    GameHistoryFieldOffset,
                    GameHistoryRowOffset)
                {
                    Font = font,
                    Text = data.GameStartTime.ToString(GameCompletedFormat),
                    Colour = Color.White
                });

            //Duration
            this.Controls.Add(
                new TextControl(
                    GameHistoryX + GameHistoryFieldOffset * 5,
                    GameHistoryY + GameHistoryRowOffset * index,
                    GameHistoryFieldOffset,
                    GameHistoryRowOffset)
                {
                    Font = font,
                    Text = data.GameDuration.ToString(CultureInfo.InvariantCulture),
                    Colour = Color.White
                });
        }

        private void MainMenuButtonOnClick(ButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ButtonPress");
            InteractionLayer.Components.AudioManager.PlaySound(resource);

            this.SendMessage<object>("GoTo", typeof(MainMenuView));
        }
    }
}
