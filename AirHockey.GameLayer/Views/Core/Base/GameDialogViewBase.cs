namespace AirHockey.GameLayer.Views.Core.Base
{
    using System;
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.DataTransfer;
    using Constants;
    using InteractionLayer.Components;
    using Resources;
    using Utility.Attributes;
    using ComponentModel.Graphics;

    /// <summary>
    /// A common base class for all game dialogs/popups.
    /// </summary>
    abstract class GameDialogViewBase : GameObjectContainerBase
    {
        /// <summary>
        /// The X position of the dialog.
        /// </summary>
        [MessageDataMember("DialogX")]
        public int X
        {
            get;
            private set;
        }

        /// <summary>
        /// The Y position of the dialog.
        /// </summary>
        [MessageDataMember("DialogY")]
        public int Y
        {
            get;
            private set;
        }

        /// <summary>
        /// The Width of the dialog.
        /// </summary>
        [MessageDataMember("DialogW", "DialogWidth")]
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// The height of the dialog.
        /// </summary>
        [MessageDataMember("DialogH", "DialogHeight")]
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// The position of the dialog.
        /// </summary>
        [MessageDataMember("DialogPosition")]
        public Point Position
        {
            get
            {
                return new Point(this.X, this.Y);
            }
        }

        /// <summary>
        /// The graphics component to use when drawing the
        /// background.
        /// </summary>
        protected GraphicsComponent Background = GraphicsComponent.Nil;

        /// <summary>
        /// The result optionally returned by the dialog.
        /// </summary>
        public string DialogResult
        {
            get;
            private set;
        }

        /// <summary>
        /// The parent view of this dialog. This is used to
        /// reference the current skin (shared with parent).
        /// </summary>
        [NeverNull]
        public GameViewBase Parent
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of a GameDialogViewBase&lt;&gt;
        /// object.
        /// </summary>
        /// <param name="parent">The view that is the parent of this dialog.</param>
        /// <param name="width">The width of the dialog.</param>
        /// <param name="height">The height of the dialog.</param>
        protected GameDialogViewBase(GameViewBase parent, int width, int height)
        {
            if (parent == null)
            {
                throw new ArgumentException("Parent for Dialog must not be null.");
            }

            this.Parent = parent;

            var screenDimensions = DrawingManager.GetScreenDimensions();

            this.X = (screenDimensions.X/2) - (width/2);
            this.Y = (screenDimensions.Y/2) - (height/2);
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// An implementation of Render that forwards it to the
        /// dialog after the view renders.
        /// </summary>
        public sealed override void Render()
        {
            // Must draw fadding before InitialiseDrawingContext so that it covers the entire screen.
            var screenDimensions = DrawingManager.GetScreenDimensions();

            DebugManager.DrawDebugText();

            DrawingManager.DrawRectangle(
                0,
                0,
                screenDimensions.X,
                screenDimensions.Y,
                Color.FromArgb(128, 0, 0, 0),
                0.01f);

            DrawingManager.InitialiseDrawingContext(
                this.X,
                this.Y,
                this.Width,
                this.Height,
                RenderingValues.Colour.DialogBackground);

            this.Background.Draw();

            this.GameObjects.ForEach(x => x.Render());
        }

        /// <summary>
        /// Creates a resource name object based on the current context
        /// and the given resource name.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <returns>The created ResourceName object.</returns>
        protected ResourceName Resource(string name)
        {
            return new ResourceName(name, this.Parent);
        }

        /// <summary>
        /// An implementation of Update that forwards it to the
        /// dialog if it exists and if not then calls in to the
        /// view's update method.
        /// </summary>
        /// <param name="elapsedTime">The elapsed ms since the last update call.</param>
        public sealed override void Update(double elapsedTime)
        {
            this.GameObjects.ForEach(x => x.Update(elapsedTime));
        }

        /// <summary>
        /// Closes this Dialog.
        /// </summary>
        protected void Close()
        {
            this.DialogResult = this.DialogResult ?? string.Empty;
        }

        /// <summary>
        /// Closes this Dialog with the given dialog result
        /// message.
        /// </summary>
        /// <param name="result">The result of the dialog.</param>
        protected void Close(string result)
        {
            this.DialogResult = result ?? string.Empty;
        }

        /// <summary>
        /// Handles a message that is passed in.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        /// <param name="parameters">The parameters for that message.</param>
        /// <returns>The result of the message.</returns>
        public override object AcceptMessage(string message, params object[] parameters)
        {
            var result = this.Parent.AcceptMessage(message, parameters) ??
                         MessageSystemHelper.HandleGetOrSetMessage(this, message, parameters);

            if (result != null)
            {
                return result;
            }

            if (MessageSystemHelper.ValidateMessage("Close", message, new Type[0], parameters))
            {
                this.Close();
                result = MessageResult.Nil;
            }
            else if (MessageSystemHelper.ValidateMessage("Close", message, new []{typeof(string)}, parameters))
            {
                this.Close((parameters[0] as string) ?? string.Empty);
                result = MessageResult.Nil;
            }

            return result;
        }
    }
}
