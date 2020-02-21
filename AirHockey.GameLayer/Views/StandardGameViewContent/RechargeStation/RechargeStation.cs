namespace AirHockey.GameLayer.Views.StandardGameViewContent.RechargeStation
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Utility.Classes;
    using Resources;
    using Constants;
    using AirHockey.InteractionLayer.Components;

    class RechargeStation : GameObjectBase
    {
        public RechargeStation(float x, float y, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.RechargeStation");
            this.Graphics = new RechargeStationGraphicsComponent(this, this);

            this.Physics = new RechargeStationPhysicsComponent(this, this)
            {
                Position = new Vector(x, y)
            };
        }
    }
}
