namespace AirHockey.GameLayer.Views.GameSummaryViewContent
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Utility.Classes;

    /// <summary>
    /// Provides functions for loading and saving game summary data.
    /// </summary>
    static class GameDataHelper
    {
        /// <summary>
        /// Appends a game's summary data to the ScoreHistory file.
        /// </summary>
        /// <param name="gameResults">The summary data to be saved.</param>
        public static void SaveGameResults(GameSummaryData gameResults)
        {
            using (var stream = new StreamWriter("ScoreHistory.csv", true))
            {
                stream.WriteLine(
                    gameResults.GameStartTime.ToString() + ","
                    + gameResults.PlayerOneName + ","
                    + gameResults.PlayerTwoName + ","
                    + gameResults.PlayerOneScore + ","
                    + gameResults.PlayerTwoScore + ","
                    + gameResults.GameDuration + ","
                    + (int)gameResults.WinningPlayer);
            }
        }

        /// <summary>
        /// Loads all the recorded game summary data for every game played
        /// as saved in the ScoreHistory file.
        /// </summary>
        /// <returns>A collection of the game summaries that have been retrieved.</returns>
        public static List<GameSummaryData> LoadGameResults()
        {
            var result = new List<GameSummaryData>();

            try
            {
                using (var stream = new StreamReader("ScoreHistory.csv"))
                {
                    string line;

                    while (!string.IsNullOrEmpty(line = stream.ReadLine()))
                    {
                        var lineParts = line.Split(',');

                        result.Add(new GameSummaryData
                        {
                            GameStartTime = DateTime.Parse(lineParts[0]),
                            PlayerOneName = Convert.ToString(lineParts[1]),
                            PlayerTwoName = Convert.ToString(lineParts[2]),
                            PlayerOneScore = Convert.ToInt32(lineParts[3]),
                            PlayerTwoScore = Convert.ToInt32(lineParts[4]),
                            GameDuration = Convert.ToDouble(lineParts[5]),
                            WinningPlayer = (Player)Convert.ToInt32(lineParts[6]),
                        });
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }

            return result;
        }
    }
}
