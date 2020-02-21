namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Stasis
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Particle;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Utility.Helpers;

    class StasisTowerProjectile : RadialForceProjectileBase
    {
        public StasisTowerProjectile(TowerObjectBase myTower, params IMessageHandler[] messageHandlers)
            : base(myTower, messageHandlers)
        {
            var animationValues = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.Stasis",
                FrameCount = AnimationValues.Stasis.ProjectileFrameCount,
                FrameDuration = AnimationValues.Stasis.ProjectileFrameDuration,
                RenderingDepth = RenderingValues.Depth.Stasis.Projectile
            };

            this.Graphics = new OneShotAuraGraphicsComponent(animationValues,
                AnimationValues.Stasis.ProjectileInFrame,
                (float)RandomisationHelper.Random.NextDouble() * 3.142f,
                1200,
                this.MyTower, this, this);

            this.Physics = new MotionAuraPhysicsComponent(AirHockeyValues.StasisTower.ProjectileRadius * this.MyTower.Power / 100.0f,
               0, 0, 1, this.MyTower, this);

            Resources.ResourceName resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Stasis_Pulse");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}
