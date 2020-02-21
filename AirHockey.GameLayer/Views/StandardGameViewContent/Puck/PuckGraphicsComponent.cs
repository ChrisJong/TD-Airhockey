namespace AirHockey.GameLayer.Views.StandardGameViewContent.Puck
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;

    class PuckGraphicsComponent : GraphicsComponent
    {
        private readonly AnimationGraphicsComponent _puckGraphic;
        private readonly AnimationGraphicsComponent _exciteGraphic;

        protected PuckPhysicsComponent MyPuckPhysics
        {
            get;
            set;
        }

        public PuckGraphicsComponent(AnimationValues animationValues, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.MyPuckPhysics = (PuckPhysicsComponent)parentNode.Physics;
 
            var resource = this.SendMessage<ResourceName>("Resource", animationValues.BaseDirectory);
            this._puckGraphic = new AnimationGraphicsComponent(resource,
                animationValues.FrameCount,
                animationValues.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.RenderingDepth
            };

            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Pucks.PuckExcite");
            this._exciteGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.Puck.ExciteFramecount,
                AnimationValues.Default.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = RenderingValues.Depth.Puck.Excite,
                AnimationPaused = true
            };
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            _puckGraphic.Update(delta);
            _exciteGraphic.Update(delta);
        }

        public override void Draw()
        {
            this._puckGraphic.Draw();

            this._exciteGraphic.CurrentFrame = (int)((1-(MyPuckPhysics.ExciteCooldown / MyPuckPhysics.ExciteCooldownMax)) * AnimationValues.Puck.ExciteFramecount);
            this._exciteGraphic.Draw();
        }

        public override void Release()
        {
            this.MyPuckPhysics = null;
            base.Release();
        }
    }
}

