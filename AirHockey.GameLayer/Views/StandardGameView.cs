namespace AirHockey.GameLayer.Views
{
    using System;
    using System.Globalization;
    using Core.Base;
    using ComponentModel.Graphics;
    using Core.Transitions;
    using StandardGameViewContent.Core;
    using StandardGameViewContent.Puck;
    using StandardGameViewContent.Towers.Blackhole;
    using StandardGameViewContent.Towers.Forcefield;
    using StandardGameViewContent.Towers.Pulsar;
    using StandardGameViewContent.Towers.Slingshot;
    using StandardGameViewContent.RechargeStation;
    using Utility.Classes;
    using AirHockey.GameLayer.Views.StandardGameViewContent.ExitButton;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Slow;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Towers.Stasis;
    using AirHockey.LogicLayer.Collisions;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;

    class StandardGameView : GameViewBase
    {
        private readonly DateTime _startTime = DateTime.Now;

        public StandardGameView()
        {
            this.Background = new BackgroundGraphicsComponent(this.Resource("Resources.<skin>.Background"), this);

            this.AddGameObject(new OneShotFlashTransition(this));
            this.AddGameObject(new SlingshotTower(Player.One, this));
            this.AddGameObject(new PulsarTower(Player.One, this));
            this.AddGameObject(new ForcefieldTower(Player.One, this));
            this.AddGameObject(new BlackholeTower(Player.One, this));
            this.AddGameObject(new SlowTower(Player.One, this));
            this.AddGameObject(new StasisTower(Player.One, this));

            this.AddGameObject(new SlingshotTower(Player.Two, this));
            this.AddGameObject(new PulsarTower(Player.Two, this));
            this.AddGameObject(new ForcefieldTower(Player.Two, this));
            this.AddGameObject(new BlackholeTower(Player.Two, this));
            this.AddGameObject(new SlowTower(Player.Two, this));
            this.AddGameObject(new StasisTower(Player.Two, this));

            this.AddGameObject(new PuckGenerator(this));
            this.AddGameObject(new CoreManager(3, 1, this));

            this.AddGameObject(new RechargeStation(50, 232, this));
            this.AddGameObject(new RechargeStation(50, 846, this));
#if DEBUG
            this.AddGameObject(new RechargeStation(1150, 232, this));
            this.AddGameObject(new RechargeStation(1150, 846, this));
#else
            this.AddGameObject(new RechargeStation(1870, 232, this));
            this.AddGameObject(new RechargeStation(1870, 846, this));
#endif

            this.AddGameObject(new ExitButtonManager(this));
        }

        public override object AcceptMessage(string message, params object[] parameters)
        {
            if (message == "GoTo" && parameters[0] as Type == typeof(GameSummaryView))
            {
                this.GoTo(
                    typeof(GameSummaryView),
                    new ViewTransitionParameter(TransitionParameterType.GameStarted, this._startTime.ToString()),
                    new ViewTransitionParameter(TransitionParameterType.WinningPlayer, parameters[1].ToString()),
                    new ViewTransitionParameter(
                        TransitionParameterType.GameDuration,
                        (DateTime.Now - this._startTime).TotalMilliseconds.ToString(CultureInfo.InvariantCulture)),
                    new ViewTransitionParameter(TransitionParameterType.PlayerOneScore, parameters[2].ToString()),
                    new ViewTransitionParameter(TransitionParameterType.PlayerTwoScore, parameters[3].ToString()));

                return new object();
            }

            return base.AcceptMessage(message, parameters);
        }

        public override void Release()
        {
            Physman.Release();
            base.Release();
        }
    }
}
