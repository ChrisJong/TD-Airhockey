namespace AirHockey.GameLayer.ComponentModel
{
    using System.Collections.Generic;
    using Audio;
    using DataTransfer;
    using Graphics;
    using GUI;
    using Input;
    using LogicLayer.Collisions;
    using Physics;
    using Utility.Attributes;
    using Utility.Classes;

    /// <summary>
    /// A base class for all game objects. Any object that
    /// is to appear in a View or ViewDialog must be of
    /// this type.
    /// </summary>
    abstract class GameObjectBase : UpdateAndDrawableBase, IMessageHandler
    {
        private readonly List<IMessageHandler> _messageHandlers = new List<IMessageHandler>();
        private UserInterfaceComponent _userInterface = UserInterfaceComponent.Nil;
        private InputComponent _input = InputComponent.Nil;
        private AudioComponent _audio = AudioComponent.Nil;
        private GraphicsComponent _graphics = GraphicsComponent.Nil;
        private PhysicsComponent _physics = PhysicsComponent.Nil;

        /// <summary>
        /// Whether or not the current game object is active in the world.
        /// This would be false if a tower was not on the game field.
        /// </summary>
        [MessageDataMember]
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The user interface component for this game object.
        /// </summary>
        [NeverNull]
        public UserInterfaceComponent UserInterface
        {
            get { return this._userInterface; }
            set { this._userInterface = this.InitialiseComponent(value ?? UserInterfaceComponent.Nil); }
        }

        /// <summary>
        /// The input component for this game object.
        /// </summary>
        [NeverNull]
        public InputComponent Input
        {
            get { return this._input; }
            set { this._input = this.InitialiseComponent(value ?? InputComponent.Nil); }
        }

        /// <summary>
        /// The audio component for this game object.
        /// </summary>
        [NeverNull]
        public AudioComponent Audio
        {
            get { return this._audio; }
            set { this._audio = this.InitialiseComponent(value ?? AudioComponent.Nil); }
        }

        /// <summary>
        /// The graphics component for this game object.
        /// </summary>
        public GraphicsComponent Graphics
        {
            get { return this._graphics; }
            set { this._graphics = this.InitialiseComponent(value ?? GraphicsComponent.Nil); }
        }

        /// <summary>
        /// The physics component for this game object.
        /// </summary>
        public PhysicsComponent Physics
        {
            get { return this._physics; }
            set
            {
                if (this._physics != PhysicsComponent.Nil)
                {
                    Physman.RemovePhysicsObject(this._physics);
                }

                this._physics = this.InitialiseComponent(value ?? PhysicsComponent.Nil);

                if (this._physics != PhysicsComponent.Nil)
                {
                    Physman.AddPhysicsObject(this._physics);
                }
            }
        }

        /// <summary>
        /// The locations that are searched when this component
        /// seeks data.
        /// </summary>
        //[NeverNull]
        public List<IMessageHandler> MessageHandlers
        {
            get { return this._messageHandlers; }
        }

        protected GameObjectBase(params IMessageHandler[] messageHandlers)
        {
            this.MessageHandlers.Add(this);
            this.MessageHandlers.AddRange(messageHandlers);
            this.IsActive = true;
        }

        /// <summary>
        /// Removes the phyics component from Physman.
        /// </summary>
        ~GameObjectBase()
        {
            if (this._physics != PhysicsComponent.Nil)
            {
                Physman.RemovePhysicsObject(this._physics);
            }
        }

        /// <summary>
        /// performs render operations.
        /// </summary>
        public sealed override void Render()
        {
            this.Graphics.Draw();
            this.Audio.Play();
            this.UserInterface.Draw();
        }

        /// <summary>
        /// Performs update operations.
        /// </summary>
        /// <param name="elapsedTime"></param>
        public sealed override void Update(double elapsedTime)
        {
            this.Input.Process();
            this.UserInterface.Update(elapsedTime);
            this.UpdateGameObject(elapsedTime);
            this.Physics.Update(elapsedTime);
            this.Graphics.Update(elapsedTime);
        }

        /// <summary>
        /// Performs updates that do not fall under any component's
        /// jurisdiction.
        /// </summary>
        /// <param name="elapsedTime">The elapsed game time since the last frame.</param>
        public virtual void UpdateGameObject(double elapsedTime)
        {
        }

        /// <summary>
        /// Initialises the component. This should be done before you set
        /// the component to its field.
        /// </summary>
        /// <param name="component">The component to initialise.</param>
        /// <returns>The initialised component (same component).</returns>
        private TComponent InitialiseComponent<TComponent>(TComponent component)
            where TComponent : ComponentBase
        {
            if (component != null && !component.MessageHandlers.Contains(this) && !component.GetType().Name.StartsWith("Nil"))
            {
                component.MessageHandlers.Add(this);
            }

            return component;
        }

        /// <summary>
        /// Handles a message that is passed in.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        /// <param name="parameters">The parameters for that message.</param>
        /// <returns>The result of the message.</returns>
        public virtual object AcceptMessage(string message, params object[] parameters)
        {
            var result = MessageSystemHelper.HandleGetOrSetMessage(this, message, parameters) ??
                   this.Physics.AcceptMessage(message, parameters) ??
                   this.UserInterface.AcceptMessage(message, parameters) ??
                   this.Audio.AcceptMessage(message, parameters) ??
                   this.Input.AcceptMessage(message, parameters) ??
                   this.Graphics.AcceptMessage(message, parameters);

            if (result == null)
            {
                if (MessageSystemHelper.ValidateMessage("Delete", message, new[] { typeof(string) }, parameters))
                {
                    if (parameters[0] as string == "GameObject")
                    {
                        result = this.SendMessage<object>("Delete", this);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Sends a message from the game object to all the
        /// associated message handlers. This will include the
        /// View/DialogView.
        /// </summary>
        /// <typeparam name="TResult">The expected result.</typeparam>
        /// <param name="message">The message to send.</param>
        /// <param name="parameters">The parameters for the message.</param>
        /// <returns>The result.</returns>
        public TResult SendMessage<TResult>(string message, params object[] parameters)
        {
            return MessageSystemHelper.ConvertResult<TResult>(
                this.AcceptMessage(message, parameters) ??
                MessageSystemHelper.IterateThroughMessageHandlers(this.MessageHandlers, message, parameters));
        }

        public override void Release()
        {
            this.Graphics.Release();
            this.Input.Release();
            this.Physics.Release();
            this.Audio.Release();
            this.UserInterface.Release();

            this.Graphics = null;
            this.Input = null;
            this.Physics = null;
            this.Audio = null;
            this.UserInterface = null;

            MessageHandlers.Clear();
        }
    }
}
