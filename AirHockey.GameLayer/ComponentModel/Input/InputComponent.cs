namespace AirHockey.GameLayer.ComponentModel.Input
{
    /// <summary>
    /// A common base class for all Input components. Such
    /// as Player Input and AI Input.
    /// </summary>
    abstract class InputComponent : ComponentBase
    {
        private static InputComponent _nil;

        /// <summary>
        /// A nil value for the Input component.
        /// </summary>
        public static InputComponent Nil
        {
            get { return _nil ?? (_nil = new NilInputComponent()); }
        }

        protected InputComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.ParentNode = parentNode;
        }

        protected InputComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        /// <summary>
        /// Processes input for the associated game
        /// object.
        /// </summary>
        public abstract void Process();
    }
}
