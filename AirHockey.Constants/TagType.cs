namespace AirHockey.Constants
{
    public static class TagType
    {
        public static class PlayerOne
        {
            public const int SlingshotTower = 0;
            public const int ForcefieldTower = 1;
            public const int BlackholeTower = 2;
            public const int PulsarTower = 3;
            public const int SlowTower = 4;
            public const int StasisTower = 5;
        }

        public static class PlayerTwo
        {
            private const int OffsetFromPlayerOne = 100;
            public const int SlingshotTower = PlayerOne.SlingshotTower + OffsetFromPlayerOne;
            public const int ForcefieldTower = PlayerOne.ForcefieldTower + OffsetFromPlayerOne;
            public const int BlackholeTower = PlayerOne.BlackholeTower + OffsetFromPlayerOne;
            public const int PulsarTower = PlayerOne.PulsarTower + OffsetFromPlayerOne;
            public const int SlowTower = PlayerOne.SlowTower + OffsetFromPlayerOne;
            public const int StasisTower = PlayerOne.StasisTower + OffsetFromPlayerOne;
        }
    }
}
