namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Stasis
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Utility.Classes;

    class StasisTower : TowerObjectBase
    {
        public double ShotCooldown
        {
            get;
            set;
        }

        public StasisTower(Player player, params IMessageHandler[] messageHandlers)
            : base(player, messageHandlers)
        {
            this.ToggleCooldownMax = TowerValues.StasisTower.ToggleCooldown;
            this.RegenCooldownMax = TowerValues.StasisTower.RegenCooldown;
            this.RegenRate = TowerValues.StasisTower.RegenRate;


            this.Input = new StasisTowerInputComponent(player, this, this);
            this.Physics = new TowerPhysicsComponent(AirHockeyValues.StasisTower.TowerRadius, this, this);

            var animationValues = new AnimationValues
            {
                PlayerDirectory = "Resources.<skin>.Player" + player + ".Towers.Stasis",
                BaseDirectory = "Resources.<skin>.Towers.Stasis",
                ActiveAnchorDepth = RenderingValues.Depth.Stasis.ActiveAnchor,
                EnergyRingDepth = RenderingValues.Depth.Stasis.EnergyRing,
                ProjectileDepth = RenderingValues.Depth.Stasis.Projectile,
                TagIconDepth = RenderingValues.Depth.Stasis.TagIcon,
                ToggleRingDepth = RenderingValues.Depth.Stasis.ToggleRing,
                FrameDuration = AnimationValues.Default.FrameDuration,
                TagIconFrameCount = AnimationValues.Stasis.TagIconFrameCount,
                ToggleRingFrameCount = AnimationValues.Stasis.ToggleRingFrameCount,
                ActiveAnchorFrameCount = AnimationValues.Stasis.ActiveAnchorFrameCount,
                ActivatedEffectFrameCount = AnimationValues.Stasis.ActivatedEffectFrameCount,
                ToggleRingFrameDuration = AnimationValues.Stasis.ToggleRingFrameDuration
            };

            this.Graphics = new ToggleTowerBaseGraphicsComponent(animationValues, this, this);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            if (this.IsActive || this.TaglessCooldown > 0)
            {
                if (this.ShotCooldown > 0)
                {
                    this.ShotCooldown -= elapsedTime;
                }

                if (!this.IsOutOfEnergy)
                {
                    if (this.IsActivated && this.ShotCooldown <= 0)
                    {
                        this.ShotCooldown = AnimationValues.Stasis.ProjectileFrameCount * AnimationValues.Stasis.ProjectileFrameDuration * 1.25;
                        this.Energy -= TowerValues.StasisTower.DrainRate * Power / 100;
                        this.SendMessage<object>("Create", "GameObject", typeof(StasisTowerProjectile), this);
                    }
                }
            }
        }
    }
}
