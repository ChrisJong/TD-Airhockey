namespace AirHockey.GameLayer.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utility.Attributes;

    /// <summary>
    /// A common base class for all Component Base classes.
    /// </summary>
    abstract class ComponentBase : IMessageHandler
    {
        private readonly List<IMessageHandler> _messageHandlers = new List<IMessageHandler>();
        private GameObjectBase _parentNode;

        /// <summary>
        /// The locations that are searched when this component
        /// seeks data.
        /// </summary>
        [NeverNull]
        public List<IMessageHandler> MessageHandlers
        {
            get { return this._messageHandlers; }
        }

        /// <summary>
        /// Whether or not this Component is a Nil component. There should
        /// be limited use for this but any use should be unified.
        /// </summary>
        public bool IsNil
        {
            get { return this.GetType().Name.StartsWith("Nil"); }
        }

        /// <summary>
        /// Reference to the parent node.
        /// </summary>
        public GameObjectBase ParentNode
        {
            get { return this._parentNode; }
            set { this._parentNode = value; }
        }

        protected ComponentBase(params IMessageHandler[] messageHandlers)
        {
            if (messageHandlers.Length == 0 && !this.IsNil)
            {
                throw new ArgumentException(
                    "Component Base is expecting at least 1 message handler (parent game object) in constructor.");
            }

            this.MessageHandlers.Add(this);
            this.MessageHandlers.AddRange(messageHandlers);
        }

        /// <summary>
        /// Handles a message that is passed in.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        /// <param name="parameters">The parameters for that message.</param>
        /// <returns>The result of the message.</returns>
        public virtual object AcceptMessage(string message, params object[] parameters)
        {
            return MessageSystemHelper.HandleGetOrSetMessage(this, message, parameters);
        }

        /// <summary>
        /// Sends a message from the component to all the
        /// associated message handlers.
        /// </summary>
        /// <typeparam name="TResult">The expected result.</typeparam>
        /// <param name="message">The message to send.</param>
        /// <param name="parameters">The parameters for the message.</param>
        /// <returns>The result.</returns>
        public TResult SendMessage<TResult>(string message, params object[] parameters)
        {
            var result = MessageSystemHelper.ConvertResult<TResult>(
                this.AcceptMessage(message, parameters) ?? // remove this.AcceptMessage since 'this' is a message handler.
                MessageSystemHelper.IterateThroughMessageHandlers(
                    this.MessageHandlers.Where(x => !(x is GameObjectBase)),
                    message,
                    parameters));

            if (Equals(result, default (TResult)))
            {
                result = this.SendMessageToGameObject<TResult>(message, parameters);
            }

            return result;
        }

        /// <summary>
        /// Forwards a message to the game object in the form
        /// of a SendMessage. This is used to allow the message to
        /// reach the View and ViewDialog levels since AcceptMessage
        /// only goes down the hierarchy.
        /// </summary>
        /// <typeparam name="TResult">The expected message result.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters for the message.</param>
        /// <returns>The result of the message.</returns>
        private TResult SendMessageToGameObject<TResult>(string message, object[] parameters)
        {
            var gameObject = (GameObjectBase) this.MessageHandlers.FirstOrDefault(x => x is GameObjectBase);

            return gameObject != null ? gameObject.SendMessage<TResult>(message, parameters) : default (TResult);
        }

        /// <summary>
        /// Releases all references that the current object has.
        /// Child classes need to override this when they refer to parents, objects, etc...
        /// and also call base.Release() to release resources that base classes implement
        /// </summary>
        public virtual void Release()
        {
            MessageHandlers.Clear();
            this.ParentNode = null;
        }
    }
}
