namespace AirHockey.InteractionLayer.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class contains all the XNA components that need
    /// to be accessed across the entire Layer.
    /// </summary>
    static class InternalComponents
    {
        /// <summary>
        /// Facilitates drawing functionality.
        /// </summary>
        internal static SpriteBatch SpriteBatch
        {
            get;
            private set;
        }

        /// <summary>
        /// Stores a reference to the GraphicsDevice used by the current
        /// game instance.
        /// </summary>
        internal static GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        /// <summary>
        /// Stores a reference to the Content Manager used by the current
        /// game instance.
        /// </summary>
        internal static ContentManager Content
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets up the required values for the operation of this
        /// component abstraction.
        /// </summary>
        /// <param name="game">The current game instance.</param>
        internal static void Initialise(Game game)
        {
            GraphicsDevice = game.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Content = game.Content;
        }
    }
}
