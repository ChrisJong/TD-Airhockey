namespace AirHockey.GameLayer.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using ComponentModel.Graphics;
    using ComponentModel.DataTransfer;
    using Core.Base;
    using Core.Transitions;
    using GameSummaryViewContent;
    using Utility.Classes;
    using AirHockey.GameLayer.Views.GameSummaryViewContent.Keyboard;

    /// <summary>
    /// A view that displayes the game's statistics. This is displayed
    /// after a game completes.
    /// </summary>
    class GameSummaryView : GameViewBase
    {
        /// <summary>
        /// The game's summary data.
        /// </summary>
        [MessageDataMember]
        public GameSummaryData SummaryData
        {
            get;
            set;
        }

        /// <summary>
        /// The game summary history to be displayed as "recent games".
        /// </summary>
        [MessageDataMember]
        public ReadOnlyCollection<GameSummaryData> GameSummaryHistory
        {
            get;
            private set;
        }

        [MessageDataMember]
        public string PlayerOneName
        {
            get;
            set;
        }

        [MessageDataMember]
        public string PlayerTwoName
        {
            get;
            set;
        }

        public bool PlayerOneReady
        {
            get;
            set;
        }

        public bool PlayerTwoReady
        {
            get;
            set;
        }

        public bool DisplayMainMenu
        {
            get;
            set;
        }

        public GameSummaryView()
        {
            this.SummaryData = new GameSummaryData();
        }

        /// <summary>
        /// Reads the game's summary data from the Transition Parameters and loads it into
        /// the game summary object.
        /// </summary>
        /// <param name="parameters">The transition parameters that are being read.</param>
        public override void Initialise(List<ViewTransitionParameter> parameters)
        {
            this.SummaryData.GameStartTime = 
                Convert.ToDateTime(parameters.First(x => x.Name == TransitionParameterType.GameStarted).Value);
            this.SummaryData.WinningPlayer =
                (Player)Convert.ToInt32(parameters.First(x => x.Name == TransitionParameterType.WinningPlayer).Value);
            this.SummaryData.GameDuration =
                Convert.ToDouble(parameters.First(x => x.Name == TransitionParameterType.GameDuration).Value);
            this.SummaryData.PlayerOneScore =
                Convert.ToInt32(parameters.First(x => x.Name == TransitionParameterType.PlayerOneScore).Value);
            this.SummaryData.PlayerTwoScore =
                Convert.ToInt32(parameters.First(x => x.Name == TransitionParameterType.PlayerTwoScore).Value);
            this.SummaryData.PlayerOneName = this.PlayerOneName;
            this.SummaryData.PlayerTwoName = this.PlayerTwoName;

            //GameDataHelper.SaveGameResults(this.SummaryData);

            this.AddGameObject(new AirHockey.GameLayer.Views.StandardGameViewContent.Particle.OneShotFlashTransition(this));

            if (this.SummaryData.WinningPlayer == Player.One)
            {
                this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.GameSummary.WinnerRed"), this);
            }
            else
            {
                this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.GameSummary.WinnerBlue"), this);
            }

            this.AddGameObject(new GameSummaryUserInterfaceGameObject(this));

            this.AddGameObject(new KeyboardManager(Player.One, this));
            this.AddGameObject(new KeyboardManager(Player.Two, this));
        }

        public override object AcceptMessage(string message, params object[] parameters)
        {
            if (message == "Get")
            {
                if (parameters[0].ToString() == "SummaryData")
                    return this.SummaryData;

                if (parameters[0].ToString() == "PlayerOneName")
                    return this.PlayerOneName;
                else if (parameters[0].ToString() == "PlayerTwoName")
                    return this.PlayerTwoName;

                if (parameters[0].ToString() == "DisplayMainMenu")
                    return this.DisplayMainMenu;
            }

            if (message == "Set")
            {
                if (parameters[0].ToString() == "PlayerOneName")
                {
                    this.PlayerOneReady = true;
                    this.PlayerOneName = parameters[1].ToString();
                    this.SummaryData.PlayerOneName = this.PlayerOneName;

                    if (this.PlayerTwoReady)
                        this.DisplayMainMenu = true;

                }
                else if (parameters[0].ToString() == "PlayerTwoName")
                {
                    this.PlayerTwoReady = true;
                    this.PlayerTwoName = parameters[1].ToString();
                    this.SummaryData.PlayerTwoName = this.PlayerTwoName;

                    if(this.PlayerOneReady)
                        this.DisplayMainMenu = true;
                }
            }

            return base.AcceptMessage(message, parameters);
        }
    }
}
