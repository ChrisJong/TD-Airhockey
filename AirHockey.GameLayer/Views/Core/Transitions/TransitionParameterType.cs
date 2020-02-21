namespace AirHockey.GameLayer.Views.Core.Transitions
{
    /// <summary>
    /// An explicit set of possible parameters for a view
    /// to pass to another when transitioning into it.
    /// </summary>
    enum TransitionParameterType
    {
        /// <summary>
        /// Value: Either "1" or "2".
        /// </summary>
        WinningPlayer,
        /// <summary>
        /// Value: Integer.
        /// </summary>
        PlayerOneScore,
        /// <summary>
        /// Value: Integer.
        /// </summary>
        PlayerTwoScore,
        /// <summary>
        /// Value: Double.
        /// </summary>
        GameDuration,
        /// <summary>
        /// Value: Date/Time.
        /// </summary>
        GameStarted
    }
}
