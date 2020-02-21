namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;

    class CoreExplodeOneShotGraphics: GraphicsComponent
    {
        private readonly AnimationGraphicsComponent _oneShotGraphic;
        private Player _myPlayer;
        private double _endgameDelay = 2000;

        /// <summary>
        /// Animation component which makes the object delete itself once its animation completes.
        /// </summary>
        /// <param name="animationValues"></param>
        /// <param name="parentNode"></param>
        /// <param name="messageHandlers"></param>

        public CoreExplodeOneShotGraphics(Player player, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            var resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Cores.CoreExplode");
            this._oneShotGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.Core.ExplodeFrameCount,
                AnimationValues.Default.FrameDuration,
                parentNode,
                this.MessageHandlers.ToArray())
                {
                    DrawDepth = RenderingValues.Depth.Core.Damaged
                };
            this._oneShotGraphic.AnimationComplete += this.OnAnimationComplete;
            this._oneShotGraphic.Alpha = 1.0f;
            
            //Append animation time to the delay before game ends
            this._endgameDelay += AnimationValues.Core.ExplodeFrameCount * AnimationValues.Default.FrameDuration;
            //Identify who's explosion this is, so the game count/ends properly
            this._myPlayer = player;
        }

        private void OnAnimationComplete()
        {
            _oneShotGraphic.AnimationPaused = true;
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            _oneShotGraphic.Update(delta);

            _endgameDelay -= delta;
            if (_endgameDelay < 0)
            {
                this.SendMessage<object>("Delete", "GameObject");
                CoreManager.CheckEndGame(_myPlayer);
            }
        }

        public override void Draw()
        {
            this._oneShotGraphic.Draw();
        }
    }
}
