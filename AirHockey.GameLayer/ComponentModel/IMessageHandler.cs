namespace AirHockey.GameLayer.ComponentModel
{
    /// <summary>
    /// Defines the members of a Data Context that can
    /// be used by a component to retrieve data.
    /// </summary>
    interface IMessageHandler
    {
        /// <summary>
        /// Handles a message that is passed in.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        /// <param name="parameters">The parameters for that message.</param>
        /// <returns>The result of the message.</returns>
        object AcceptMessage(string message, params object[] parameters);
    }
}
