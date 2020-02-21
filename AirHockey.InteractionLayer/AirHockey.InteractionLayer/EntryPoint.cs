namespace AirHockey.InteractionLayer
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Color = Microsoft.Xna.Framework.Color;
    using Point = System.Drawing.Point;
    using Microsoft.Surface.Core;
    using Microsoft.Surface;
    using System.Linq;

    /// <summary>
    /// This is the main type for your application.
    /// </summary>
    public class EntryPoint : Game
    {
        #region Fields
        private readonly GraphicsDeviceManager _graphics;

        private bool _applicationLoadCompleteSignalled;

        // Hold on to the game window.
        private static GameWindow _window;
        #endregion

        public static EntryPoint Instance = null;

        /// <summary>
        /// The target receiving all surface input for the application.
        /// </summary>
        protected TouchTarget TouchTarget
        {
            get;
            private set;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntryPoint()
        {
            Instance = this;

            this._graphics = new GraphicsDeviceManager(this);

            //VSync needs to be on to avoid screen tearing.
            this._graphics.SynchronizeWithVerticalRetrace = true; 

            this.Content.RootDirectory = "Content";

        }

        public void SetFullScreen(bool isFullScreen)
        {
            if (this._graphics.IsFullScreen == false && isFullScreen == true)
            {
                this._graphics.ToggleFullScreen();
                //this._graphics.IsFullScreen = isFullScreen;
            }
        }

        #region Initialization

        /// <summary>
        /// Moves and sizes the window to cover the input surface.
        /// </summary>
        private void SetWindowOnSurface()
        {
            Debug.Assert(this.Window != null && this.Window.Handle != IntPtr.Zero,
                "Window initialization must be complete before SetWindowOnSurface is called");
            if (this.Window == null || this.Window.Handle == IntPtr.Zero)
                return;

            // Get the window sized right.
            InitializeWindow(this.Window);
            // Set the graphics device buffers.
            this._graphics.PreferredBackBufferWidth = 1920;  // WindowSize.Width;
            this._graphics.PreferredBackBufferHeight = 1080; //WindowSize.Height;

            this._graphics.ApplyChanges();
            // Make sure the window is in the right location.
            PositionWindow();
        }

        /// <summary>
        /// Initializes the surface input system. This should be called after any window
        /// initialization is done, and should only be called once.
        /// </summary>
        private void InitializeSurfaceInput()
        {
            Debug.Assert(this.Window != null && this.Window.Handle != IntPtr.Zero,
                "Window initialization must be complete before InitializeSurfaceInput is called");
            if (this.Window == null || this.Window.Handle == IntPtr.Zero)
                return;
            Debug.Assert(this.TouchTarget == null,
                "Surface input already initialized");
            if (this.TouchTarget != null)
                return;

            // Create a target for surface input.
            this.TouchTarget = new TouchTarget(this.Window.Handle, EventThreadChoice.OnBackgroundThread);
            this.TouchTarget.EnableInput();
        }

        #endregion

        #region Overridden Game Methods

        /// <summary>
        /// Allows the app to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this.IsMouseVisible = true; // easier for debugging not to "lose" mouse
            this.SetWindowOnSurface();
            this.InitializeSurfaceInput();

            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += this.OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += this.OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += this.OnWindowUnavailable;

            base.Initialize();

            InterfaceLayerHelper.InitialiseComponents(this);

            GameLoopManager.InvokeOnInitialise();
        }

        /// <summary>
        /// LoadContent will be called once per app and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
        }

        /// <summary>
        /// UnloadContent will be called once per app and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            InterfaceLayerHelper.ReleaseComponents(this);
        }

        /// <summary>
        /// Allows the app to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (ApplicationServices.WindowAvailability != WindowAvailability.Unavailable)
            {
                InputManager.StartFrameInput();

                if (ApplicationServices.WindowAvailability == WindowAvailability.Interactive)
                {
                    this.TouchTarget.GetState().ToList().ForEach(
                        x =>
                        {
                            if (x.IsFingerRecognized)
                            {
                                InputManager.RegisterTouchPoint(x.Id, (int) x.X, (int) x.Y);
                            }
                            else if (x.IsTagRecognized)
                            {
                                InputManager.RegisterTagInput(x.Id, (int) x.Tag.Value, (int) x.X, (int) x.Y, x.Orientation);
                            }
                        });

                    // Include mouse state to facilitate simple testing as well as
                    // testing on Win8 (which does not support InputSimulator).
                    SimulationManager.PerformSimulations();
                }

                InputManager.EndFrameInput();

                GameLoopManager.TimeSinceLastFrame = gameTime.ElapsedGameTime.TotalMilliseconds;
                GameLoopManager.InvokeOnUpdate(GameLoopManager.TimeSinceLastFrame);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the app should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!this._applicationLoadCompleteSignalled)
            {
                // Dismiss the loading screen now that we are starting to draw
                ApplicationServices.SignalApplicationLoadComplete();
                this._applicationLoadCompleteSignalled = true;
            }

            InternalComponents.GraphicsDevice.Clear(Color.Black);
            InternalComponents.SpriteBatch.Begin();
            GameLoopManager.InvokeOnRender();
            InternalComponents.SpriteBatch.End();
        }

        #endregion

        #region Application Event Handlers

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            SoundEffect.MasterVolume = 1.0f;
            GameLoopManager.UpdateEnabled = true;
            GameLoopManager.RenderEnabled = true;
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            SoundEffect.MasterVolume = 0.0f;
            GameLoopManager.UpdateEnabled = false;
            GameLoopManager.RenderEnabled = true;
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            SoundEffect.MasterVolume = 0.0f;
            GameLoopManager.UpdateEnabled = false;
            GameLoopManager.RenderEnabled = false;
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Release managed resources.
                var graphicsDispose = this._graphics as IDisposable;

                if (graphicsDispose != null)
                {
                    graphicsDispose.Dispose();
                }

                if (this.TouchTarget != null)
                {
                    this.TouchTarget.Dispose();
                    this.TouchTarget = null;
                }
            }

            // Release unmanaged Resources.

            // Set large objects to null to facilitate garbage collection.

            base.Dispose(disposing);
        }

        #endregion

        #region Functions that were in Program

        /// <summary>
        /// Gets the size of the main window.
        /// </summary>
        internal static Size WindowSize
        {
            get
            {
                return ((Form)Control.FromHandle(_window.Handle)).DesktopBounds.Size;
            }
        }

        /// <summary>
        /// Position and adorn the Window appropriately.
        /// </summary>
        /// <param name="window"></param>
        internal static void InitializeWindow(GameWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            _window = window;

            var form = (Form)Control.FromHandle(_window.Handle);
            form.LocationChanged += OnFormLocationChanged;

            SetWindowStyle();
            SetWindowSize();
        }

        /// <summary>
        /// Respond to changes in the form location, adjust if necessary.
        /// </summary>
        private static void OnFormLocationChanged(object sender, EventArgs e)
        {
            if (SurfaceEnvironment.IsSurfaceEnvironmentAvailable)
            {
                var form = (Form)Control.FromHandle(_window.Handle);
                form.LocationChanged -= OnFormLocationChanged;
                PositionWindow();
                form.LocationChanged += OnFormLocationChanged;
            }
        }

        /// <summary>
        /// Position and size the window to the primary device.
        /// </summary>
        internal static void PositionWindow()
        {
            var left = (InteractiveSurface.PrimarySurfaceDevice != null)
                            ? InteractiveSurface.PrimarySurfaceDevice.WorkingAreaLeft
                            : Screen.PrimaryScreen.WorkingArea.Left;
            var top = (InteractiveSurface.PrimarySurfaceDevice != null)
                            ? InteractiveSurface.PrimarySurfaceDevice.WorkingAreaTop
                            : Screen.PrimaryScreen.WorkingArea.Top;

            var form = (Form)Control.FromHandle(_window.Handle);
            var windowState = form.WindowState;
            form.WindowState = FormWindowState.Normal;
            form.Location = new Point(left, top);
            form.WindowState = windowState;
        }

        /// <summary>
        /// Set the window's style based on the availability of the Surface environment.
        /// </summary>
        private static void SetWindowStyle()
        {
            _window.AllowUserResizing = true;
            var form = (Form)Control.FromHandle(_window.Handle);
            form.FormBorderStyle = (SurfaceEnvironment.IsSurfaceEnvironmentAvailable)
                                    ? FormBorderStyle.None
                                    : FormBorderStyle.Sizable;
        }

        /// <summary>
        /// Size the window to the primary device.
        /// </summary>
        private static void SetWindowSize()
        {
            var width = (InteractiveSurface.PrimarySurfaceDevice != null)
                            ? InteractiveSurface.PrimarySurfaceDevice.WorkingAreaWidth
                            : Screen.PrimaryScreen.WorkingArea.Width;
            var height = (InteractiveSurface.PrimarySurfaceDevice != null)
                            ? InteractiveSurface.PrimarySurfaceDevice.WorkingAreaHeight
                            : Screen.PrimaryScreen.WorkingArea.Height;

            // Just set it to 1080p resolution.
            // Enables runing on non HD resolution workstations. by moving window position around
            width = 1920;
            height = 1080;

            var form = (Form)Control.FromHandle(_window.Handle);

            form.ClientSize = new Size(width, height);
            form.WindowState = (SurfaceEnvironment.IsSurfaceEnvironmentAvailable)
                            ? FormWindowState.Normal
                            : FormWindowState.Maximized;
        }
        #endregion
    }
}
