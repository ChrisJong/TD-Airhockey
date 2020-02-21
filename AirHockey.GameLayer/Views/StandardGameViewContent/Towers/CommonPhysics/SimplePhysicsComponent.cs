namespace AirHockey.GameLayer.Views.StandardGameViewContent.Particle
{
    using ComponentModel;
    using ComponentModel.Physics;

    class SimplePhysicsComponent : PhysicsComponent
    {
        /// <summary>
        /// Physics Component with minimal behaviour such as linear motion.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="messageHandlers"></param>
        public SimplePhysicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
        }
    }

}
