namespace AirHockey.Utility.Extensions
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Contains extensions for the <see cref="Point"/> class.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Returns the distance between two points.
        /// </summary>
        /// <param name="origin">The origin point.</param>
        /// <param name="destination">The destination point.</param>
        /// <returns>The distance.</returns>
        public static float DistanceTo(this Point origin, Point destination)
        {
            var dx = destination.X - origin.X;
            var dy = destination.Y - origin.Y;

            return (float) Math.Sqrt(dx*dx + dy*dy);
        }

        /// <summary>
        /// Returns the un-square-rooted distance between two points.
        /// </summary>
        /// <param name="origin">The origin point.</param>
        /// <param name="destination">The destination point.</param>
        /// <returns>The un-square-rooted distance.</returns>
        public static float DistanceTo2(this Point origin, Point destination)
        {
            var dx = destination.X - origin.X;
            var dy = destination.Y - origin.Y;

            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Gets whether or not a point is inside a rectangular area.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="area">The area.</param>
        /// <returns>Whether or not the point is inside the area.</returns>
        public static bool IsInside(this Point point, Rectangle area)
        {
            return point.X >= area.X && point.X < area.X + area.Width && point.Y >= area.Y &&
                   point.Y < area.Y + area.Height;
        }
    }
}
