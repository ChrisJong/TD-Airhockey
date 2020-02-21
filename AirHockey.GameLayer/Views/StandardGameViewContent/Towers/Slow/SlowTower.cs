namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Slow
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Utility.Classes;

    class SlowTower : TowerObjectBase
    {
        public SlowTower(Player player, params IMessageHandler[] messageHandlers)
            : base(player, messageHandlers)
        {
            this.ToggleCooldownMax = TowerValues.SlowTower.ToggleCooldown;
            this.RegenCooldownMax = TowerValues.SlowTower.RegenCooldown;
            this.RegenRate = TowerValues.SlowTower.RegenRate;


            this.Input = new SlowTowerInputComponent(player, this, this);
            this.Physics = new TowerPhysicsComponent(AirHockeyValues.SlowTower.TowerRadius, this, this);

            var animationValues = new AnimationValues
            {
                PlayerDirectory = "Resources.<skin>.Player" + player + ".Towers.Slow",
                BaseDirectory = "Resources.<skin>.Towers.Slow",
                ActiveAnchorDepth = RenderingValues.Depth.Slow.ActiveAnchor,
                EnergyRingDepth = RenderingValues.Depth.Slow.EnergyRing,
                ProjectileDepth = RenderingValues.Depth.Slow.Projectile,
                TagIconDepth = RenderingValues.Depth.Slow.TagIcon,
                ToggleRingDepth = RenderingValues.Depth.Slow.ToggleRing,
                FrameDuration = AnimationValues.Default.FrameDuration,
                TagIconFrameCount = AnimationValues.Slow.TagIconFrameCount,
                ToggleRingFrameCount = AnimationValues.Slow.ToggleRingFrameCount,
                ActiveAnchorFrameCount = AnimationValues.Slow.ActiveAnchorFrameCount,
                ActivatedEffectFrameCount = AnimationValues.Slow.ActivatedEffectFrameCount,
                ToggleRingFrameDuration = AnimationValues.Slow.ToggleRingFrameDuration
            };

            this.Graphics = new ToggleTowerBaseGraphicsComponent(animationValues, this, this);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            if (this.IsActive || this.TaglessCooldown > 0)
            {
                if (!this.IsOutOfEnergy)
                {
                    if (this.IsActivated) this.Energy -= TowerValues.SlowTower.DrainRate * Power / 100;
                }
            }
        }
    }
}
