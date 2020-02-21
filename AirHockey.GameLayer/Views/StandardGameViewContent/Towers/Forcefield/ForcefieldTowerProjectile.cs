namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers
{
    using ComponentModel;
    using InteractionLayer.Components;
    using Particle;
    using Towers.CommonGraphics;
    using Constants;
    using AirHockey.Utility.Helpers;

    class ForcefieldTowerProjectile : RadialForceProjectileBase
    {
        public ForcefieldTowerProjectile(TowerObjectBase myTower, params IMessageHandler[] messageHandlers)
            : base(myTower, messageHandlers)
        {
            var animationValues = new AnimationValues
            {
                BaseDirectory = "Resources.<skin>.Towers.ForceField",
                FrameCount = AnimationValues.ForceField.ProjectileFrameCount,
                FrameDuration = AnimationValues.Default.FrameDuration,
                RenderingDepth = RenderingValues.Depth.ForceField.Projectile

            };
            this.Graphics = new RadialForceGraphicsComponent(animationValues,
                AnimationValues.ForceField.ProjectileInFrame,
                (float)RandomisationHelper.Random.NextDouble() * 3.142f, 
                this.MyTower, this);

            this.Physics = new RadialForcePhysicsComponent(AirHockeyValues.ForceFieldTower.ProjectileRadius * this.MyTower.Power / 100.0f,
                this.MyTower.Power * 20 + 20, this.MyTower, this);

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.ForceField_Activated");
            InteractionLayer.Components.AudioManager.PlaySound(resource);
        }
    }
}
