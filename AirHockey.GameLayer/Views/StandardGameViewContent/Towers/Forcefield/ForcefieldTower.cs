namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Forcefield
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Utility.Classes;

    class ForcefieldTower : TowerObjectBase
    {
        public ForcefieldTower(Player player, params IMessageHandler[] messageHandlers)
            : base(player, messageHandlers)
        {
            this.ToggleCooldownMax = TowerValues.ForceFieldTower.ToggleCooldown;
            this.RegenCooldownMax = TowerValues.ForceFieldTower.RegenCooldown;
            this.RegenRate = TowerValues.ForceFieldTower.RegenRate;


            this.Input = new ForcefieldTowerInputComponent(player, this, this);
            this.Physics = new TowerPhysicsComponent(AirHockeyValues.ForceFieldTower.TowerRadius, this, this);

            var animationValues = new AnimationValues
            {
                PlayerDirectory = "Resources.<skin>.Player" + player + ".Towers.ForceField",
                BaseDirectory = "Resources.<skin>.Towers.ForceField",
                ActiveAnchorDepth = RenderingValues.Depth.ForceField.ActiveAnchor,
                EnergyRingDepth = RenderingValues.Depth.ForceField.EnergyRing,
                ProjectileDepth = RenderingValues.Depth.ForceField.Projectile,
                TagIconDepth = RenderingValues.Depth.ForceField.TagIcon,
                ToggleRingDepth = RenderingValues.Depth.ForceField.ToggleRing,
                FrameDuration = AnimationValues.Default.FrameDuration,
                TagIconFrameCount = AnimationValues.ForceField.TagIconFrameCount,
                ToggleRingFrameCount = AnimationValues.ForceField.ToggleRingFrameCount,
                ActiveAnchorFrameCount = AnimationValues.ForceField.ActiveAnchorFrameCount,
                ActivatedEffectFrameCount = AnimationValues.ForceField.ActivatedEffectFrameCount,
                ToggleRingFrameDuration = AnimationValues.ForceField.ToggleRingFrameDuration
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
                    if (this.IsActivated) this.Energy -= TowerValues.ForceFieldTower.DrainRate * Power / 100;
                }
            }
        }
    }
}
