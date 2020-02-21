namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Blackhole
{
    using ComponentModel;
    using Constants;
    using CommonGraphics;
    using CommonPhysics;
    using Utility.Classes;

    class BlackholeTower : TowerObjectBase
    {
        public BlackholeTower(Player player, params IMessageHandler[] messageHandlers)
            : base(player, messageHandlers)
        {
            this.ToggleCooldownMax = TowerValues.BlackholeTower.ToggleCooldown;
            this.RegenCooldownMax = TowerValues.BlackholeTower.RegenCooldown;
            this.RegenRate = TowerValues.BlackholeTower.RegenRate;

            this.Input = new BlackholeTowerInputComponent(player, this, this);
            this.Physics = new TowerPhysicsComponent(AirHockeyValues.BlackholeTower.TowerRadius, this, this);

            var animationValues = new AnimationValues
            {
                PlayerDirectory = "Resources.<skin>.Player" + player + ".Towers.BlackHole",
                BaseDirectory = "Resources.<skin>.Towers.BlackHole",
                ActiveAnchorDepth = RenderingValues.Depth.BlackHole.ActiveAnchor,
                EnergyRingDepth = RenderingValues.Depth.BlackHole.EnergyRing,
                ProjectileDepth = RenderingValues.Depth.BlackHole.Projectile,
                TagIconDepth = RenderingValues.Depth.BlackHole.TagIcon,
                ToggleRingDepth = RenderingValues.Depth.BlackHole.ToggleRing,
                FrameDuration = AnimationValues.Default.FrameDuration,
                TagIconFrameCount = AnimationValues.BlackHole.TagIconFrameCount,
                ToggleRingFrameCount = AnimationValues.BlackHole.ToggleRingFrameCount,
                ActiveAnchorFrameCount = AnimationValues.BlackHole.ActiveAnchorFrameCount,
                ActivatedEffectFrameCount = AnimationValues.BlackHole.ActivatedEffectFrameCount,
                ToggleRingFrameDuration = AnimationValues.BlackHole.ToggleRingFrameDuration
            };
            this.Graphics = new ToggleTowerBaseGraphicsComponent(animationValues, this, this);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            if (this.IsActive)
            {
                if (!this.IsOutOfEnergy)
                {
                    if (this.IsActivated) this.Energy -= TowerValues.BlackholeTower.DrainRate * Power / 100;
                }
            }
        }
    }
}
