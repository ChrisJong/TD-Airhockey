namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonInput
{
    using System;
    using ComponentModel;
    using Constants;
    using InteractionLayer.Components.Input;

    class RadialToggleInputComponent : TowerPullbackInputComponent
    {
        protected Type ProjectileType;

        /// <summary>
        /// Constructor for Inputs for Toggling Radial Towers.
        /// </summary>
        /// <param name="projectileType">Projectile Class Name (eg: ForceFieldProjectile, BlackHoleProjectile)</param>
        /// <param name="parentNode">the tower</param>
        /// <param name="messageHandlers"></param>
        public RadialToggleInputComponent(Type projectileType, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.ProjectileType = projectileType;
            //TagInputComponent is handled by the subclasses that inherit from this
            //See below for example code
            //=======================================================================
            //this.TagInputComponent = new TagInputComponent(
            //      player == Player.One ? TagType.PlayerOne.ForcefieldTower : TagType.PlayerTwo.ForcefieldTower,
            //      this.MessageHandlers.ToArray());
        }

        protected override void OnFingerPressed(TouchPoint point)
        {
            if (this.MyTower.IsOutOfEnergy || this.MyTower.ToggleCooldown > 0)
                return;

            if (this.MyTower.IsActive || this.MyTower.ToggleCooldown > 0 || this.MyTower.TaglessCooldown > 0)
            {
                if (this.MyTower.IsActivated)
                {
                    var distanceSq = (point.Location - this.MyTower.Physics.Position).LengthSq;
                    var scaledRadius = (AirHockeyValues.ForceFieldTower.ProjectileRadius*this.MyTower.Power/100.0f);

                    if (distanceSq >= TowerValues.SlingshotTower.MinStartPullDistanceSq && distanceSq <= TowerValues.SlingshotTower.MaxStartPullDistanceSq)
                    {
                        this.MyTower.IsActivated = false;
                        MyTower.ToggleCooldown = MyTower.ToggleCooldownMax;

                        var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.TowerDeactivated");
                        InteractionLayer.Components.AudioManager.PlaySound(resource);
                    }
                }
                else if (this.FingerId == null)
                {
                    var distanceSq = (point.Location - this.MyTower.Physics.Position).LengthSq;

                    if (distanceSq >= TowerValues.ForceFieldTower.MinStartPullDistanceSq &&
                        distanceSq <= TowerValues.ForceFieldTower.MaxStartPullDistanceSq)
                    {
                        this.FingerId = point.Id;
                        this.MyTower.PullBackPoint = point.Location;

                        var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Tower_Pressed");
                        InteractionLayer.Components.AudioManager.PlaySound(resource);
                    }
                }
            }
        }

        protected override void OnFingerReleased(TouchPoint point)
        {
            if (point.Id == this.FingerId)
            {
                this.MyTower.IsActivated = true;
                this.MyTower.PullBackPoint = null;
                this.FingerId = null;

                var offset = this.MyTower.Physics.Position - this.GetPullBackPoint(point, TowerValues.ForceFieldTower.MaxPullbackLength);
                var power = offset.Length - TowerValues.ForceFieldTower.MinStartPullDistance;
                if (power < 50) power = 50;

                this.MyTower.Power = power;

                if (this.ProjectileType != null)
                {
                    this.SendMessage<object>("Create", "GameObject", this.ProjectileType, this.MyTower);
                }
            }
        }
    }
}
