namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Blackhole
{
    using ComponentModel;
    using Constants;
    using InteractionLayer.Components;
    using CommonGraphics;
    using Particle;
    using AirHockey.Utility.Helpers;

    class BlackholeTowerProjectile : RadialForceProjectileBase
    {
        public BlackholeTowerProjectile(TowerObjectBase myTower, params IMessageHandler[] messageHandlers)
            : base(myTower, messageHandlers)
        {
            var animationValues = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.BlackHole",
                FrameCount = AnimationValues.BlackHole.ProjectileFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.BlackHole.Projectile
            };

            this.Graphics = new RadialForceGraphicsComponent(animationValues, 
                AnimationValues.BlackHole.ProjectileInFrame,
                (float)RandomisationHelper.Random.NextDouble() * 3.142f,
                this.MyTower, this);

            this.Physics = new RadialForcePhysicsComponent(AirHockeyValues.BlackholeTower.ProjectileRadius * this.MyTower.Power / 100.0f,
                -this.MyTower.Power * 20 - 50, this.MyTower, this);

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.BlackHole_Activated");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}
