namespace AirHockey.GameLayer.Views.Core.Base
{
    using System;
    using System.Collections.Generic;
    using ComponentModel;
    using Constants;
    using GameLayer.Core;
    using InteractionLayer.Components;
    using Resources;
    using Transitions;
    using Utility.Attributes;
    using ComponentModel.Graphics;
    using System.Linq;
    using LogicLayer.Collisions;

    /// <summary>
    /// A common base class for all game views/screens.
    /// </summary>
    abstract class GameViewBase : GameObjectContainerBase, IResourceContext
    {
        #region Events

        public delegate void OnLeavingHandler(ref bool cancel);
        public delegate void OnDialogReturnedHandler(GameDialogViewBase dialog);

        /// <summary>
        /// Triggered when a View is about to be transitioned out.
        /// The View must be in a drawable state after this is called
        /// for use in Transitions.
        /// </summary>
        public event OnLeavingHandler OnLeaving;
        /// <summary>
        /// Triggered when a DialogView was closed in the last frame.
        /// Here should be the logic of handling that dialog's result.
        /// </summary>
        public event OnDialogReturnedHandler OnDialogReturned;

        #endregion

        private string _skin = GlobalSettings.DefaultSkin;

        /// <summary>
        /// The graphics component to use when drawing the
        /// background.
        /// </summary>
        protected GraphicsComponent Background = GraphicsComponent.Nil;

        /// <summary>
        /// The currently active dialog. If one exists (not null),
        /// this overrides the view's update (but both are drawn).
        /// </summary>
        private GameDialogViewBase _dialog;

        /// <summary>
        /// The name of the skin to be used when selecting
        /// resources for this view.
        /// </summary>
        /// Note: If skin name cannot be matched to a valid
        /// skin, the default/standard skin is used.
        [NeverNull]
        public string Skin
        {
            get { return this._skin; }
            set { this._skin = value ?? GlobalSettings.DefaultSkin; }
        }

        /// <summary>
        /// Stores the pending transition for this view.
        /// This value is null if no transition is pending.
        /// </summary>
        public ViewTransition Transition
        {
            get;
            set;
        }
        
        /// <summary>
        /// Called when a view is transitioned into. This contains
        /// all the parameters passed from the previous view.
        /// Note: this will not be called on the first view to be loaded.
        /// </summary>
        /// <param name="parameters">The information this view needs to operate.</param>
        public virtual void Initialise(List<ViewTransitionParameter> parameters)
        {
        }

        /// <summary>
        /// An implementation of Render that forwards it to the
        /// dialog after the view renders.
        /// </summary>
        public sealed override void Render()
        {
            DrawingManager.InitialiseDrawingContext(RenderingValues.Colour.ViewBackground);

            this.Background.Draw();

            this.GameObjects.ForEach(x => x.Render());

            if (this._dialog != null)
            {
                this._dialog.Render();
            }
        }

        /// <summary>
        /// An implementation of Update that forwards it to the
        /// dialog if it exists and if not then calls in to the
        /// view's update method.
        /// </summary>
        /// <param name="elapsedTime">The elapsed ms since the last update call.</param>
        public sealed override void Update(double elapsedTime)
        {
            if (this._dialog != null)
            {
                if (this._dialog.DialogResult == null)
                {
                    this._dialog.Update(elapsedTime);
                }
                else
                {
                    if (this.OnDialogReturned != null) this.OnDialogReturned.Invoke(this._dialog);
                    this._dialog = null;
                }
            }
            else
            {
                Physman.Update(elapsedTime);
                this.GameObjects.ForEach(x => x.Update(elapsedTime));
            }
        }

        /// <summary>
        /// Creates a resource name object based on the current context
        /// and the given resource name.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <returns>The created ResourceName object.</returns>
        protected ResourceName Resource(string name)
        {
            return new ResourceName(name, this);
        }

        /// <summary>
        /// Sets up the transition from this view to another.
        /// </summary>
        /// <param name="destinationView">The destination of the transition.</param>
        /// <param name="parameters">The parameters for the new view to use.</param>
        protected void GoTo(Type destinationView, params ViewTransitionParameter[] parameters)
        {
            this.Transition = new ViewTransition {DestinationView = destinationView};
            this.Transition.Parameters.AddRange(parameters);

            var cancel = false;
            if (this.OnLeaving != null)
            {
                this.OnLeaving.Invoke(ref cancel);
                throw new Exception("Leaving for something");
            }

            if (cancel)
            {
                this.Transition = null;
            }
        }

        /// <summary>
        /// Shows a new instance of the given dialog type.
        /// </summary>
        /// <param name="dialog">The dialog to be shown.</param>
        protected void ShowDialog(GameDialogViewBase dialog)
        {
            if (dialog != null && dialog.DialogResult == null)
            {
                this._dialog = dialog;
            }
            else
            {
                throw new ArgumentException("Invalid dialog given to ShowDialog.");
            }
        }

        /// <summary>
        /// Handles a message that is passed in.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        /// <param name="parameters">The parameters for that message.</param>
        /// <returns>The result of the message.</returns>
        public override object AcceptMessage(string message, params object[] parameters)
        {
            if (MessageSystemHelper.ValidateMessage("EndGame", message, new Type[0], parameters))
            {
                GameMain.EndGame();
                return MessageResult.Nil;
            }

            if (MessageSystemHelper.ValidateMessage("Resource", message, new[] { typeof(string) }, parameters))
            {
                return this.Resource(parameters[0] as string);
            }

            if (MessageSystemHelper.ValidateMessage("Delete", message, new[] { typeof(GameObjectBase) }, parameters))
            {
                var objectToDelete = parameters[0] as GameObjectBase;

                this.GameObjects.Remove(objectToDelete);
                Physman.RemovePhysicsObject(objectToDelete.Physics);
                objectToDelete.Release();

                return new object();
            }

            if (MessageSystemHelper.ValidateMessage("GoTo", message, new[] {typeof (Type)}, parameters))
            {
                this.GoTo((Type) parameters[0]);
                return new object();
            }

            if (MessageSystemHelper.ValidateMessage(
                "GoTo",
                message,
                new[] {typeof (Type), typeof (ViewTransitionParameter[])},
                parameters))
            {
                this.GoTo((Type) parameters[0], (ViewTransitionParameter[]) parameters[1]);
                return new object();
            }

            if (message == "Create" && parameters.Length > 1
                && parameters[0] as string == "GameObject"
                && typeof(GameObjectBase).IsAssignableFrom(parameters[1] as Type))
            {
                this.AddGameObject(
                    (GameObjectBase)
                        ((Type) parameters[1]).GetConstructors()[0].Invoke(
                            parameters.Skip(2).Concat(new object[] {new IMessageHandler[] {this}}).ToArray()));

                return MessageResult.Nil;
            }

            return null;
        }

        public override void Release()
        {
            this.OnLeaving = null;
            this.OnDialogReturned = null;

            // base class releases Game Objects List, and removes the 
            // references to parent by clearing the messagehandlers list
            base.Release(); 
        }
    }
}
