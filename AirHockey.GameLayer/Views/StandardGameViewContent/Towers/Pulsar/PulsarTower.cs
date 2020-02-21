namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Pulsar
{
    using System;
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Utility.Helpers;

    class PulsarTower : TowerObjectBase
    {
        public double BaseCooldown = 750;
        public double CooldownReduction;

        public double ShotCooldown
        {
            get;
            set;
        }

        public PulsarTower(Player player, params IMessageHandler[] messageHandlers)
            : base(player, messageHandlers)
        {
            this.ToggleCooldownMax = TowerValues.PulsarTower.ToggleCooldown;
            this.RegenCooldownMax = TowerValues.PulsarTower.RegenCooldown;
            this.RegenRate = TowerValues.PulsarTower.RegenRate;

            this.Input = new PulsarTowerInputComponent(player, this, this);
            this.Physics = new TowerPhysicsComponent(AirHockeyValues.PulsarTower.TowerRadius, this, this);

            var animationValues = new AnimationValues
            {
                PlayerDirectory = "Resources.<skin>.Player" + player + ".Towers.Pulsar",
                BaseDirectory = "Resources.<skin>.Towers.Pulsar",
                ActiveAnchorDepth = RenderingValues.Depth.Pulsar.ActiveAnchor,
                EnergyRingDepth = RenderingValues.Depth.Pulsar.EnergyRing,
                TagIconDepth = RenderingValues.Depth.Pulsar.TagIcon,
                ToggleRingDepth = RenderingValues.Depth.Pulsar.ToggleRing,
                FrameDuration = AnimationValues.Default.FrameDuration,
                TagIconFrameCount = AnimationValues.Pulsar.TagIconFrameCount,
                ToggleRingFrameCount = AnimationValues.Pulsar.ToggleRingFrameCount,
                ActiveAnchorFrameCount = AnimationValues.Pulsar.ActiveAnchorFrameCount,
                ActivatedEffectFrameCount = AnimationValues.Pulsar.ActivatedEffectFrameCount,
                ToggleRingFrameDuration = AnimationValues.Pulsar.ToggleRingFrameDuration
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
                    if (this.ShotCooldown > 0)
                    {
                        this.ShotCooldown -= elapsedTime;
                    }
                    else if (this.IsActivated)
                    {
                        float randomization = (float)((0.5f - RandomisationHelper.Random.NextDouble()) * (CooldownReduction / BaseCooldown));
                        float projectileRotation = this.Physics.Rotation + randomization / 1.75f;
                        var projectilePosition = this.Physics.Position;
                        var projectileVelocity = Vector.AngleToUnitVector(projectileRotation) * ((this.Power * 32.0f) + (800));

                        Vector tempPosition = new Vector(0, 0);
                        tempPosition += this.Physics.Position;

                        this.SendMessage<object>("Create", "GameObject", typeof(PulsarTowerProjectile), this.Player,
                            projectilePosition, projectileVelocity, projectileRotation);

                        this.Physics.Position = tempPosition;

                        if (CooldownReduction > 0)
                        {
                            CooldownReduction -= CooldownReduction * 0.001 + 50;
                        }
                        else
                        {
                            CooldownReduction = 0;
                        }

                        this.ShotCooldown = BaseCooldown - CooldownReduction;
                        this.Energy -= TowerValues.PulsarTower.DrainRate * (this.Power / 100.0f) + (float)(1 - (CooldownReduction / BaseCooldown));
                        if (this.IsOutOfEnergy) this.IsActivated = false;
                    }
                }
            }
        }
    }
}
