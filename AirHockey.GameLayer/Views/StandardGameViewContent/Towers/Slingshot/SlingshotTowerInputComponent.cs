namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Slingshot
{
    //NOTE: Subcomponents must be initialised with this.MessageHandlers.ToArray() so that they can access the game object as well.
    using CommonInput;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using InteractionLayer.Components;
    using InteractionLayer.Components.Input;
    using Utility.Classes;

    class SlingshotTowerInputComponent : TowerPullbackInputComponent
    {        
        public SlingshotTowerInputComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.TagInputComponent = new TagInputComponent(
                    player == Player.One ? TagType.PlayerOne.SlingshotTower : TagType.PlayerTwo.SlingshotTower,
                    parentNode, 
                    this.MessageHandlers.ToArray());
        }

        protected override void OnFingerPressed(TouchPoint point)
        {
            if (this.MyTower.IsOutOfEnergy || this.MyTower.ToggleCooldown > 0)
                return;

            if ((this.ParentNode.IsActive || this.MyTower.TaglessCooldown > 0) && this.FingerId == null)
            {
                var distanceSq = (point.Location - this.ParentNode.Physics.Position).LengthSq;

                if (distanceSq >= TowerValues.SlingshotTower.MinStartPullDistanceSq && distanceSq <= TowerValues.SlingshotTower.MaxStartPullDistanceSq)
                {
                    // within the threshold for pull-back.
                    this.FingerId = point.Id;

                    this.MyTower.PullBackPoint = point.Location;
                    this.MyTower.PullBackRotation = this.GetPullBackRotation(point) - ParentNode.Physics.Rotation;

                    var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Tower_Pressed");
                    InteractionLayer.Components.AudioManager.PlaySound(resource);
                }
            }
        }


        protected override void OnFingerReleased(TouchPoint point)
        {
            if (point.Id == this.FingerId)
            {
                var offset = this.MyTower.Physics.Position - this.GetPullBackPoint(point, TowerValues.ForceFieldTower.MaxPullbackLength);
                var power = offset.Length - TowerValues.SlingshotTower.MinStartPullDistance;

                if (power > 0)
                {
                    var projectilePosition = this.MyTower.Physics.Position;
                    var projectileVelocity = (offset * power / 3.0f) + (offset * 12.0f);
                    var projectileRotation = this.GetPullBackRotation(point);

                    this.SendMessage<object>("Create", "GameObject", typeof(SlingshotTowerProjectile), this.MyTower.Player,
                        projectilePosition, projectileVelocity, projectileRotation);

                    this.MyTower.Energy -= TowerValues.SlingshotTower.DrainRate * power / 100;
                    MyTower.ToggleCooldown = MyTower.ToggleCooldownMax;
                }

                this.MyTower.PullBackPoint = null;
                this.FingerId = null;
            }
        }
    }
}
