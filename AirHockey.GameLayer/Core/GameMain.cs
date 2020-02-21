namespace AirHockey.GameLayer.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Constants;
    using InteractionLayer.Components;
    using Resources;
    using Translators;
    using Utility.Extensions;
    using Views.Core.Base;

    /// <summary>
    /// The GameLayer entry point for the game. This sets up
    /// the game environment, resources and so on. When that
    /// is complete, this will start up the first screen/view.
    /// </summary>
    public class GameMain
    {
        private static GameMain _instance;

        /// <summary>
        /// Stores the current view. This is used when forwarding
        /// Update and Render calls to the currently active view.
        /// </summary>
        internal GameViewBase CurrentView
        {
            get;
            private set;
        }

        public GameMain()
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("Only one instance of GameMain should exist.");
            }

            _instance = this;

            GameLoopManager.OnInitialise += this.InitialiseComponents;
            GameLoopManager.OnUpdate += this.Update;
            GameLoopManager.OnRender += this.Render;
        }

        /// <summary>
        /// Ends the execution of the game.
        /// </summary>
        public static void EndGame()
        {
            GameLoopManager.EndGame();
        }

        /// <summary>
        /// Initialises the global settings for the game
        /// and triggers the initalisation of other
        /// components (if necessary).
        /// </summary>
        private void InitialiseComponents()
        {
            ResourceManager.AppendResources(
                ResourceHelper.GenerateResources(GlobalSettings.ResourceDirectories)
                    .Select(ResourceTranslator.Translate));

            this.CurrentView = GetStartingView();
        }

        /// <summary>
        /// Performs per-frame processing and forwards the Update call to
        /// the current view/window.
        /// </summary>
        /// <param name="elapsedMilliseconds">The number of ms that have passed since the last frame.</param>
        private void Update(double elapsedMilliseconds)
        {
            if (this.CurrentView != null)
            {
                if (this.CurrentView.Transition == null)
                {
                    this.CurrentView.Update(elapsedMilliseconds);
                }
                else
                {
                    var currentTransition = this.CurrentView.Transition;

                    var newView = (GameViewBase) Activator.CreateInstance(currentTransition.DestinationView);
                    newView.Initialise(currentTransition.Parameters);
                    newView.Update(elapsedMilliseconds);

                    this.CurrentView.Release();
                    this.CurrentView = newView;
                }
            }
        }

        /// <summary>
        /// Performs the drawing action for the game and forwards the
        /// Render call to the current view/window.
        /// </summary>
        private void Render()
        {
            if (this.CurrentView != null)
            {
                this.CurrentView.Render();
            }
            DebugManager.DrawDebugText();
        }

        /// <summary>
        /// Retrieves an instace of the initial view to be run when the
        /// game loads. This is determined by a global setting.
        /// </summary>
        /// <returns>An instance of the initial view to be run.</returns>
        private static GameViewBase GetStartingView()
        {
            var typeOfGameView = Assembly.GetExecutingAssembly()
                .GetTypeOfKind(GlobalSettings.StartingView, typeof (GameViewBase));

            if (typeOfGameView == null)
            {
                throw new InvalidOperationException("Invalid Initial View has been set. Game cannot run.");
            }

            return (GameViewBase) Activator.CreateInstance(typeOfGameView);
        }
    }
}
