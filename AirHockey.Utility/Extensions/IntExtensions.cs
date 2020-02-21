namespace AirHockey.Utility.Extensions
{
    using System;

    public static class IntExtensions
    {
        /// <summary>
        /// Converts a float describing an angle in degrees to the
        /// same value in radians. This exists to simplify writting
        /// literals for angles.
        /// </summary>
        /// <param name="valueInDegrees">The float value in degrees.</param>
        /// <returns>The float value in radians.</returns>
        public static float Radians(this int valueInDegrees)
        {
            return valueInDegrees/180.0f*(float) Math.PI;
        }
    }
}
