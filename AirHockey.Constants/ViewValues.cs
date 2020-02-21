namespace AirHockey.Constants
{
    public static class ViewValues
    {
        /// <summary>
        /// These values are for About and Armoury views, where there are navigation buttons at the bottom
        /// For the Credits, About, Game Summary, and High Score views, only main menu values are used here
        /// </summary>
        public static class NavButtons
        {
            public const int Width = 425;
            public const int Height = 106;

#if DEBUG
            public const int GotoMainX = 0;
            public const int GotoMainY = 0;

            public const int NextPageX = 0;
            public const int NextPageY = 110;

            public const int PrevPageX = 0;
            public const int PrevPageY = 220;

#else

            public const int GotoMainX = 747;
            public const int GotoMainY = 922;

            public const int NextPageX = 1224;
            public const int NextPageY = 922;
            
            public const int PrevPageX = 270;
            public const int PrevPageY = 922;
#endif
        }

        /// <summary>
        /// These values are used for the main menu screen.
        /// </summary>
        public static class MainMenuButtons
        {
            public const int ReadyWidth = 213;
            public const int ReadyHeight = 213;

            public const int Width = 425;
            public const int Height = 106;

#if DEBUG
            public const int PlayerOneReadyX = 162;
            public const int PlayerOneReadyY = 433;

            public const int PlayerTwoReadyX = 162;
            public const int PlayerTwoReadyY = 333;
            
            public const int AboutX = 0;
            public const int AboutY = 110;
            
            public const int HighScoreX = 0;
            public const int HighScoreY = 640;

            public const int CreditsX = 0;
            public const int CreditsY = 220;

            public const int ArmouryX = 0;
            public const int ArmouryY = 750;

            public const int ExitX = 225;
            public const int ExitY = 0;

#else

            public const int PlayerOneReadyX = 162;
            public const int PlayerOneReadyY = 433;

            public const int PlayerTwoReadyX = 1545;
            public const int PlayerTwoReadyY = 433;

            //2 by 2 by 1 struct
            public const int AboutX = 509;
            public const int AboutY = 666;

            public const int CreditsX = 986;
            public const int CreditsY = 666;

            public const int HighScoreX = 509;
            public const int HighScoreY = 798;

            public const int ArmouryX = 986;
            public const int ArmouryY = 798;

            public const int ExitX = 747;
            public const int ExitY = 930;
            //

            /* 3 by 2 struct
            public const int AboutX = 270;
            public const int AboutY = 666;

            public const int CreditsX = 747;
            public const int CreditsY = 666;

            public const int HighScoreX = 1224;
            public const int HighScoreY = 666;

            public const int ArmouryX = 508;
            public const int ArmouryY = 798;

            public const int ExitX = 987;
            public const int ExitY = 798;
             */
#endif
        }

        public static class InGameStats
        {
            public const int Width = 250;
            public const int Height = 52;

            public const int PlayerOneTrackerX = 642;
            public const int PlayerOneTrackerY = 4;

            public const int PlayerTwoTrackerX = 1030;
            public const int PlayerTwoTrackerY = 4;
        }
    }
}
