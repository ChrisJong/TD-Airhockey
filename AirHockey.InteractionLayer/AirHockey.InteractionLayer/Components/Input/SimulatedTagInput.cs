namespace AirHockey.InteractionLayer.Components.Input
{
    /// <summary>
    /// Stores the details for tag input being simulated by
    /// the <see cref="SimulationManager"/>.
    /// </summary>
    class SimulatedTagInput
    {
        /// <summary>
        /// The X position to use for this tag's input.
        /// </summary>
        public int X
        {
            get;
            set;
        }

        /// <summary>
        /// The Y position to use for this tag's input.
        /// </summary>
        public int Y
        {
            get;
            set;
        }

        /// <summary>
        /// The tag value for the tag being simulated.
        /// </summary>
        public int Tag
        {
            get;
            set;
        }
    }
}
