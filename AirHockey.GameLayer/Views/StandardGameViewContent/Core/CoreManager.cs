namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Constants;
    using System;
    using AirHockey.GameLayer.GUI;
    using System.Drawing;
    using System.Collections.Generic;


    class CoreManager : GameObjectBase
    {
        public static CoreManager Instance;

        public static int PlayerOneScore;
        public static int PlayerTwoScore;

        public static int PlayerOneTextCount;
        public static int PlayerTwoTextCount;

        public static Dictionary<Player, double> PlayerComboCooldown = new Dictionary<Player, double>();
        public static Dictionary<Player, int> PlayerComboCount= new Dictionary<Player, int>();

        public int PlayerOneCoreCount
        {
            get;
            set;
        }

        public int PlayerTwoCoreCount
        {
            get;
            set;
        }

        public CoreManager(int coresPerPlayer, float coreMaxHP, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            PlayerOneScore = 0;
            PlayerTwoScore = 0;

            PlayerOneTextCount = 0;
            PlayerTwoTextCount = 0;

            PlayerComboCooldown.Add(Player.One, 0);
            PlayerComboCooldown.Add(Player.Two, 0);
            PlayerComboCount.Add(Player.One, 1);
            PlayerComboCount.Add(Player.Two, 1);

            Instance = this;

            this.PlayerOneCoreCount = coresPerPlayer;
            this.PlayerTwoCoreCount = coresPerPlayer;

            float coreGapY = (DrawingManager.GetScreenDimensions().Y - AirHockeyValues.Puck.HorizontalWall * 2) / (coresPerPlayer + 1);

            for (var i = 0; i < coresPerPlayer; i++)
            {
                float yOscillation = (float) (coreGapY / (Math.PI * 50));
                float yPosition = (i + 1) * coreGapY + AirHockeyValues.Core.ObjectRadius + AirHockeyValues.Puck.HorizontalWall - yOscillation * (float) (Math.PI * 25);
                this.SendMessage<object>("Create", "GameObject", typeof(CoreBase), Player.One, coreMaxHP, yPosition, yOscillation);
                this.SendMessage<object>("Create", "GameObject", typeof(CoreBase), Player.Two, coreMaxHP, yPosition, yOscillation);
            }

            var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Game_BGM");
            this.Audio = new ComponentModel.Audio.AmbienceAudioComponent(resource, this);

            this.Graphics = new PlayerScoreTrackerGraphicsComponent(this);
        }

        public static void CheckEndGame(Player player)
        {
            if (player == Player.One)
            {
                Instance.PlayerOneCoreCount--;
            }
            else
            {
                Instance.PlayerTwoCoreCount--;
            }

            // Transition to the GameSummary View Throws An Unable to cast object of type 'AirHockey.GameLayer.Views.Core.Transitions.ViewTransitionParameter' to type 'System.IConvertible'.
			if (Instance.PlayerOneCoreCount <= 0)
            {
                Instance.SendMessage<object>("GoTo", typeof(GameSummaryView), "2", PlayerOneScore, PlayerTwoScore);
                ((ComponentModel.Audio.AmbienceAudioComponent)CoreManager.Instance.Audio).Stop();
            }
            else if (Instance.PlayerTwoCoreCount <= 0)
            {
                Instance.SendMessage<object>("GoTo", typeof(GameSummaryView), "1", PlayerOneScore, PlayerTwoScore);
                ((ComponentModel.Audio.AmbienceAudioComponent)CoreManager.Instance.Audio).Stop();
            }
        }

        public override void Release()
        {
            PlayerComboCooldown.Clear();
            PlayerComboCount.Clear();
            Instance = null;
            base.Release();
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            if (PlayerComboCooldown[Player.One] > 0)
                PlayerComboCooldown[Player.One] -= elapsedTime;
            else
                PlayerComboCount[Player.One] = 1;

            if (PlayerComboCooldown[Player.Two] > 0)
                PlayerComboCooldown[Player.Two] -= elapsedTime;
            else
                PlayerComboCount[Player.Two] = 1;

            base.UpdateGameObject(elapsedTime);
        }


        public static int CoreHitCombo(Player player)
        {
            //Adds to combo if it has been recently hit
            if (PlayerComboCooldown[player] > 0) PlayerComboCount[player]++;
            //resets cooldown
            PlayerComboCooldown[player] = AirHockeyValues.Core.ComboCooldown;

            return PlayerComboCount[player];
        }
    }
}
