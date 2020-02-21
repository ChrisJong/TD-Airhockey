namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Pulsar
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Particle;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;

    class PulsarTowerProjectile : GameObjectBase
    {
        public PulsarTowerProjectile(Player player, Vector position, Vector velocity, float rotation, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var animInfo = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.Pulsar",
                FrameCount = AnimationValues.Pulsar.ProjectileFrameCount,
                FrameDuration = AnimationValues.Pulsar.ProjectileFrameDuration,
                RenderingDepth = RenderingValues.Depth.Pulsar.Projectile
            };

            this.Graphics = new LinearProjectileGraphicsComponent(animInfo, this, this);

            this.Physics = new LinearProjectilePhyscisComponent(player,
                AirHockeyValues.PulsarTower.ProjectileRadius,
                AirHockeyValues.PulsarTower.ProjectileMass,
                this, this)
                {
                    Rotation = rotation,
                    Position = position,
                    Velocity = velocity,
                    //ParticleType = typeof(LinearProjectileTrail)
                };

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Pulsar_Shot");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}
