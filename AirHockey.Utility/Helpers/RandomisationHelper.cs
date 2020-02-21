namespace AirHockey.Utility.Helpers
{
    using System;

    public static class RandomisationHelper
    {
        public static readonly Random Random = new Random((int) DateTime.Now.Ticks);
    }
}
