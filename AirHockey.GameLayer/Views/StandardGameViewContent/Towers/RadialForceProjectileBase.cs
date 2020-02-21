namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers
{
    using ComponentModel;
    using Constants;
    using InteractionLayer.Components;
    using CommonGraphics;
    using Particle;

    class RadialForceProjectileBase: GameObjectBase
    {
        protected TowerObjectBase MyTower
        {
            get;
            set;
        }

        public RadialForceProjectileBase(TowerObjectBase myTower, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.MyTower = myTower;
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);
            if (this.MyTower.IsActive)
            {
                this.Physics.Position = this.MyTower.Physics.Position;
            }
            else
            {
                if (this.MyTower.TaglessCooldown <= 0)
                {
                    this.SendMessage<object>("Delete", "GameObject");
                }
            }

            if (this.MyTower != null) //check in case previous if statement deletes tower
            {
                if (this.MyTower.IsActivated == false || this.MyTower.IsOutOfEnergy)
                {
                    this.MyTower.IsActivated = false;

                    var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.TowerEmpty");
                    InteractionLayer.Components.AudioManager.PlaySound(resource);

                    this.SendMessage<object>("Delete", "GameObject");
                }
            }
        }

        public override void Release()
        {
            this.MyTower = null;
            base.Release();
        }
    }
}
