namespace AirHockey.InteractionLayer.Components.Input
{
    using System.Drawing;

    /// <summary>
    /// Represents a location at which Tag input was
    /// detected.
    /// </summary>
    public class TagPoint
    {
        /// <summary>
        /// The ID of the tag detected.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// The type/mapped value of the tag.
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// The point at which the tag was detected.
        /// </summary>
        public Point Location
        {
            get;
            set;
        }

        /// <summary>
        /// The angle of the detected tag.
        /// </summary>
        public float Angle
        {
            get;
            set;
        }

        public TagPoint()
        {
            this.Location = new Point();
        }
    }
}
