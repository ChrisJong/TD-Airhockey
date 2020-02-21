namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonGraphics
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;
    using AirHockey.Utility.Helpers;
    using AirHockey.InteractionLayer.Components;

    class OneShotAuraGraphicsComponent : RadialForceGraphicsComponent
    {
        private double _effectDuration;
        private GameObjectBase _projectileNode;

        public OneShotAuraGraphicsComponent(AnimationValues animationValues, int inFrame, float rotation, double effectDuration, TowerObjectBase parentTower, GameObjectBase parentProjectileNode, params IMessageHandler[] messageHandlers)
            : base(animationValues, inFrame, rotation, parentTower, messageHandlers)
        {
            _projectileNode = parentProjectileNode;
            this._effectDuration = effectDuration;
            ProjectileGraphic.AnimationComplete += this.OnAnimationComplete;
        }

        private void OnAnimationComplete()
        {
            this.SendMessage<object>("Delete", "GameObject");
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            if (this._effectDuration > 0)
            {
                this._effectDuration -= delta;
            }
            else
            {
                if (_projectileNode != null)
                {
                    _projectileNode.Physics.ClearCollisionShapes();
                }
            }               
        }

        public override void Release()
        {
            this._projectileNode = null;
            base.Release();
        }
    }
}

