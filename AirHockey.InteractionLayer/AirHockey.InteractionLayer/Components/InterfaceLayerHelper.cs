namespace AirHockey.InteractionLayer.Components
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provides functions that are called by EntryPoint to initialise
    /// abstraction components. EntryPoint should never be changed
    /// beyond adding more function calls to this class.
    /// </summary>
    static class InterfaceLayerHelper
    {
        /// <summary>
        /// Initialises all the abstractions in the interface layer.
        /// </summary>
        /// <param name="game">The current game instance.</param>
        internal static void InitialiseComponents(Game game)
        {
            InternalComponents.Initialise(game);
            CreateGameMain();
        }

        /// <summary>
        /// Releases all the manually created content that is not
        /// simply garbage collected.
        /// </summary>
        /// <param name="game">The current game instance.</param>
        internal static void ReleaseComponents(Game game)
        {
            ResourceManager.Release();
        }

        /// <summary>
        /// Loads the GameLayer via the DLL file (since we can't have
        /// 2 projects inter-referencing each other.
        /// </summary>
        private static void CreateGameMain()
        {
            Activator.CreateInstance(
                Assembly.LoadFile(Path.GetFullPath("AirHockey.GameLayer.dll"))
                    .GetTypes()
                    .First(x => x.Name == "GameMain"));
        }
    }
}
