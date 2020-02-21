namespace AirHockey.GameLayer.Views.GameSummaryViewContent
{
    using System;
    using Utility.Classes;

    /// <summary>
    /// Contains all the information to be diplayed in the
    /// game summary window (excluding previous game statistics).
    /// </summary>
    class GameSummaryData
    {
        // Notes: Game Summary View has the following information expressed in some way
        // - PlayerOneName
        // - PlayerTwoName
        // - PlayerOneScore
        // - PlayerTwoScore
        // - GameStartTime
        // - GameDuration
        // - WinningPlayer

        /// <summary>
        /// Red player's name - an 8 character (max) string 
        /// </summary>
        public string PlayerOneName
        {
            get;
            set;
        }

        /// <summary>
        /// Blue player's name - an 8 character (max) string
        /// </summary>
        public string PlayerTwoName
        {
            get;
            set;
        }

        /// <summary>
        /// The score of the first player.
        /// </summary>
        public int PlayerOneScore
        {
            get;
            set;
        }

        /// <summary>
        /// The number of goals scored by player two.
        /// </summary>
        public int PlayerTwoScore
        {
            get;
            set;
        }

        /// <summary>
        /// The date and time at which the game was completed.
        /// </summary>
        public DateTime GameStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// For how many milliseconds the game continued.
        /// </summary>
        public double GameDuration
        {
            get;
            set;
        }


        /// <summary>
        /// Which player won the last game.
        /// </summary>
        public Player WinningPlayer
        {
            get;
            set;
        }
    }
}
