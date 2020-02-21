namespace AirHockey.GameLayer.Views.StandardGameViewContent.ExitButton
{
    using ComponentModel;
    using ComponentModel.DataTransfer;
    using ComponentModel.Physics;
    using InteractionLayer.Components;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using AirHockey.Constants;

    class ExitButtonPhysicsComponent : PhysicsComponent
    {
        public ExitButtonPhysicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {

        }
    }
}
