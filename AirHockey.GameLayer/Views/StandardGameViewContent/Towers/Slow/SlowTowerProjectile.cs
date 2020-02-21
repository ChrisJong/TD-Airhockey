namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers
{
    using ComponentModel;
    using InteractionLayer.Components;
    using Particle;
    using Towers.CommonGraphics;
    using Constants;
    using AirHockey.Utility.Helpers;

    class SlowTowerProjectile : RadialForceProjectileBase
    {
        public SlowTowerProjectile(TowerObjectBase myTower, params IMessageHandler[] messageHandlers)
            : base(myTower, messageHandlers)
        {
            var animationValues = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.Slow",
                FrameCount = AnimationValues.Slow.ProjectileFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.Slow.Projectile
            };

            this.Graphics = new RadialForceGraphicsComponent(animationValues,
                AnimationValues.Slow.ProjectileInFrame,
                (float)RandomisationHelper.Random.NextDouble() * 3.142f, 
                this.MyTower, this);

            this.Physics = new MotionAuraPhysicsComponent(AirHockeyValues.SlowTower.ProjectileRadius * this.MyTower.Power / 100.0f,
                0.05f, 20, 6, this.MyTower, this);

            Resources.ResourceName resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Slow_Activate");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}
