namespace AirHockey.InteractionLayer.Components
{
    /// <summary>
    /// Provides access to game loop events without having
    /// to interact with the EntryPoint class.
    /// </summary>
    public static class GameLoopManager
    {
        public delegate void OnInitialiseHandler();
        public delegate void OnUpdateHandler(double elapsedMilliseconds);
        public delegate void OnRenderHandler();

        /// <summary>
        /// This event is called when the game is Initialised.
        /// </summary>
        public static event OnInitialiseHandler OnInitialise;

        /// <summary>
        /// This event is called whenever an Update operation
        /// is valid.
        /// </summary>
        public static event OnUpdateHandler OnUpdate;

        /// <summary>
        /// This event is called whenever a Draw operation is
        /// valid.
        /// </summary>
        public static event OnRenderHandler OnRender;

        /// <summary>
        /// Whether or not to perform Update operations. This
        /// may be set to false when the user is unable to
        /// interact with the game.
        /// </summary>
        public static bool UpdateEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not to perform Draw operations. This
        /// may be set to false when the user cannot see the
        /// game.
        /// </summary>
        public static bool RenderEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// The time that has passed since the last frame was
        /// processed.
        /// </summary>
        public static double TimeSinceLastFrame
        {
            get;
            internal set;
        }

        static GameLoopManager()
        {
            UpdateEnabled = true;
            RenderEnabled = true;
        }

        /// <summary>
        /// Runs the OnInitialise event.
        /// </summary>
        internal static void InvokeOnInitialise()
        {
            if (OnInitialise != null)
            {
                OnInitialise.Invoke();
            }
        }

        /// <summary>
        /// Runs the OnUpdate event.
        /// </summary>
        /// <param name="elapsedMilliseconds">The elapsed ms since the last valid call.</param>
        internal static void InvokeOnUpdate(double elapsedMilliseconds)
        {
            if (UpdateEnabled && OnUpdate != null)
            {
                OnUpdate.Invoke(elapsedMilliseconds);
            }
        }

        /// <summary>
        /// Runs the OnDraw event.
        /// </summary>
        internal static void InvokeOnRender()
        {
            if (RenderEnabled && OnRender != null)
            {
                OnRender.Invoke();
            }
        }

        /// <summary>
        /// Ends the execution of the game.
        /// </summary>
        public static void EndGame()
        {
            EntryPoint.Instance.Exit();
        }
    }
}
