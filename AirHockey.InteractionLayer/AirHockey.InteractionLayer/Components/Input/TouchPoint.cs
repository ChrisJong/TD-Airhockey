namespace AirHockey.InteractionLayer.Components.Input
{
    using System.Drawing;

    /// <summary>
    /// Represents a location at which Touch input was
    /// detected.
    /// </summary>
    public class TouchPoint
    {
        /// <summary>
        /// The ID of the touch detected.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// The point at which the touch was detected.
        /// </summary>
        public Point Location
        {
            get;
            set;
        }

        public TouchPoint()
        {
            this.Location = new Point();
        }
    }
}
