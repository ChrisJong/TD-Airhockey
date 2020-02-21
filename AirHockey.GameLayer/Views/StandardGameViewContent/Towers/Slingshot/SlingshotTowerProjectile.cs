namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Slingshot
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Particle;
    using Utility.Classes;

    class SlingshotTowerProjectile : GameObjectBase
    {
        public SlingshotTowerProjectile(Player player, Vector position, Vector velocity, float rotation, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var animationValues = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.Slingshot",
                FrameCount = AnimationValues.SlingShot.ProjectileFrameCount,
                FrameDuration = AnimationValues.SlingShot.ProjectileFrameDuration,
                RenderingDepth = RenderingValues.Depth.SlingShot.Projectile
            };

            this.Graphics = new LinearProjectileGraphicsComponent(animationValues, this, this);

            this.Physics = new LinearProjectilePhyscisComponent(player,
                AirHockeyValues.SlingshotTower.ProjectileRadius,
                AirHockeyValues.SlingshotTower.ProjectileMass,
                this, this)
                {
                    Rotation = rotation,
                    Position = position,
                    Velocity = velocity,
                    //ParticleType = typeof(LinearProjectileTrail)
                };

            Resources.ResourceName resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Slingshot_Shoot");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}
