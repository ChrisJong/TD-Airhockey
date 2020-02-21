namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Pulsar
{
    using CommonInput;
    using ComponentModel;
    using ComponentModel.Input;
    using Constants;
    using InteractionLayer.Components.Input;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;

    class PulsarTowerInputComponent : TowerPullbackInputComponent
    {
        private PulsarTower _myPulsarTower;

        public PulsarTowerInputComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.TagInputComponent = new TagInputComponent(
                    player == Player.One ? TagType.PlayerOne.PulsarTower : TagType.PlayerTwo.PulsarTower,
                    parentNode,
                    this.MessageHandlers.ToArray());
            _myPulsarTower = (PulsarTower)ParentNode;
        }

        protected override void  OnFingerPressed(TouchPoint point)
        {
            if (this.MyTower.IsOutOfEnergy || this.MyTower.ToggleCooldown > 0)
                return;

            if (this.MyTower.IsActive || this.MyTower.TaglessCooldown > 0)
            {
                var distanceSq = (point.Location - this.MyTower.Physics.Position).LengthSq;

                if (this.MyTower.IsActivated)
                {
                    if (distanceSq >= TowerValues.PulsarTower.MinStartPullDistanceSq &&
                        distanceSq <= TowerValues.PulsarTower.MaxStartPullDistanceSq)
                        this.MyTower.IsActivated = false;
                        MyTower.ToggleCooldown = MyTower.ToggleCooldownMax;
                }
                else if (this.FingerId == null)
                {
                    if (distanceSq >= TowerValues.PulsarTower.MinStartPullDistanceSq &&
                        distanceSq <= TowerValues.PulsarTower.MaxStartPullDistanceSq)
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
                var offset = this.MyTower.Physics.Position - this.GetPullBackPoint(point, TowerValues.ForceFieldTower.MaxPullbackLength);
                var power = offset.Length - TowerValues.PulsarTower.MinStartPullDistance;

                if (power > 0)
                {
                    this._myPulsarTower.ShotCooldown = this._myPulsarTower.BaseCooldown;
                    this._myPulsarTower.CooldownReduction = this._myPulsarTower.BaseCooldown - 50;
                    this.MyTower.Power = power;
                    this.MyTower.IsActivated = true;
                }

                this.MyTower.PullBackPoint = null;
                this.FingerId = null;
            }
        }

        public override void Release()
        {
            this._myPulsarTower = null;
            base.Release();
        }
    }
}
