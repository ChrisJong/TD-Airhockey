namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Slingshot
{
    using CommonGraphics;
    using CommonPhysics;
    using ComponentModel;
    using Constants;
    using Utility.Classes;

    class SlingshotTower : TowerObjectBase
    {
        public SlingshotTower(Player player, params IMessageHandler[] messageHandlers)
            : base(player, messageHandlers)
        {
            this.ToggleCooldownMax = TowerValues.SlingshotTower.ToggleCooldown;
            this.RegenCooldownMax = TowerValues.SlingshotTower.RegenCooldown;
            this.RegenRate = TowerValues.SlingshotTower.RegenRate;

            this.Input = new SlingshotTowerInputComponent(player, this, this);
            this.Physics = new TowerPhysicsComponent(AirHockeyValues.SlingshotTower.TowerRadius, this, this);

            var animationValues = new AnimationValues
            {
                PlayerDirectory = "Resources.<skin>.Player" + player + ".Towers.Slingshot",
                BaseDirectory = "Resources.<skin>.Towers.Slingshot",
                ActiveAnchorDepth = RenderingValues.Depth.SlingShot.ActiveAnchor,
                EnergyRingDepth = RenderingValues.Depth.SlingShot.EnergyRing,
                TagIconDepth = RenderingValues.Depth.SlingShot.TagIcon,
                ToggleRingDepth = RenderingValues.Depth.SlingShot.ToggleRing,
                FrameDuration = AnimationValues.Default.FrameDuration,
                TagIconFrameCount = AnimationValues.SlingShot.TagIconFrameCount,
                ToggleRingFrameCount = AnimationValues.SlingShot.ToggleRingFrameCount,
                ActiveAnchorFrameCount = AnimationValues.SlingShot.ActiveAnchorFrameCount,
                ToggleRingFrameDuration = AnimationValues.SlingShot.ToggleRingFrameDuration
            };

            this.Graphics = new SimpleTowerBaseGraphicsComponent(animationValues, this, this);
        }
    }
}
