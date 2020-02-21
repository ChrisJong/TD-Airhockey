namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;

    class CoreBaseGraphicsComponent : GraphicsComponent
    {
        private CoreBase _myCore;
        private AnimationGraphicsComponent _coreGraphic;
        private AnimationGraphicsComponent _backgroundGraphic;

        public CoreBaseGraphicsComponent(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this._myCore = (CoreBase)parentNode;


            //Player Graphic
            var resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Player" + player + ".Core");
            this._coreGraphic = new AnimationGraphicsComponent(resource, 
                AnimationValues.Core.CoreFrameCount,
                AnimationValues.Default.FrameDuration,
                parentNode,
                this.MessageHandlers.ToArray())
            {
                AnimationPaused = true,
                DrawDepth = RenderingValues.Depth.Core.Object
            };

            //Background Graphic
            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Cores.CoreBackground");
            this._backgroundGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.Core.BackgroundFrameCount,
                AnimationValues.Default.FrameDuration,
                parentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = RenderingValues.Depth.Core.Background
            };

        }

        public override void Update(double delta)
        {
            base.Update(delta);
            _coreGraphic.Update(delta);
            _backgroundGraphic.Update(delta);
        }

        public override void Draw()
        {
            if (this._myCore.HealthPoints > 0.0f)
            {
                this._backgroundGraphic.Draw();
                this._coreGraphic.CurrentFrame = (int)(this._myCore.HealthPoints / this._myCore.MaxHealthPoints * AnimationValues.Core.CoreFrameCount);
                this._coreGraphic.Draw();
            }
        }

        public override void Release()
        {
            this._myCore = null;
            base.Release();
        }
    }
}
