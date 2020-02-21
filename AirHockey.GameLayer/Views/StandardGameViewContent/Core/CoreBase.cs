namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using Utility.Classes;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;


    class CoreBase : GameObjectBase
    {
        public float MaxHealthPoints
        {
            get;
            set;
        }

        public float HealthPoints
        {
            get;
            set;
        }

        public double RegenCooldown
        {
            get;
            set;
        }

        public Player Player
        {
            get;
            set;
        }

        public CoreBase(Player player, float coreHealthPoints, float yPosition, float yOscillation, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.Player = player;
            this.HealthPoints = coreHealthPoints;
            this.MaxHealthPoints = this.HealthPoints;

            this.Graphics = new CoreBaseGraphicsComponent(player, this, this);
            this.Physics = new CoreBasePhysicsComponent(player, yPosition, yOscillation, this, this);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            if (RegenCooldown > 0)
            {
                RegenCooldown -= elapsedTime;
            }
            else
            {
                RegenCooldown = 6000;
                if (this.HealthPoints < this.MaxHealthPoints)
                {
                    this.HealthPoints += ((this.MaxHealthPoints - this.HealthPoints) / 5) + 1;
                    
                    if (this.HealthPoints > this.MaxHealthPoints)
                    {
                        this.HealthPoints = this.MaxHealthPoints;
                    }

                    this.SendMessage<object>("Create", "GameObject", typeof(CoreRegenEffect),
                        this.Physics.Position, this.Physics.Velocity);
                }
            }
        }
    }
}
