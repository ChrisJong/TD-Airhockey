namespace AirHockey.GameLayer.GUI.Events
{
    using System.Drawing;

    /// <summary>
    /// Contains the data pertaining to a button being clicked.
    /// </summary>
    class ButtonClickEventArgs
    {
        /// <summary>
        /// The finger position when the button was released.
        /// </summary>
        public Point FingerPosition
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of a ButtonClickEventArgs object
        /// with the given Finger Position for the data.
        /// </summary>
        /// <param name="fingerPosition">The finger position when the button was released.</param>
        public ButtonClickEventArgs(Point fingerPosition)
        {
            this.FingerPosition = fingerPosition;
        }
    }
}
