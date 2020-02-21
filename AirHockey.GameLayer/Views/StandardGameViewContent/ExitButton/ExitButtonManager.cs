namespace AirHockey.GameLayer.Views.StandardGameViewContent.ExitButton
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AirHockey.GameLayer.ComponentModel;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Utility.Classes;


    class ExitButtonManager : GameObjectBase
    {
        public static ExitButtonManager Instance;

        private List<ExitButton> _buttonList = new List<ExitButton>();

        public ExitButtonManager(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            Instance = this;

#if DEBUG
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.One, 50, 50);
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.One, 100, 50);
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.Two, 50, 100);
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.Two, 100, 100);

#else
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.One, 50, 50);
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.One, 50, 1030);
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.Two, 1870, 50);
            this.SendMessage<object>("Create", "GameObject", typeof(ExitButton), Player.Two, 1870, 1030);
#endif
        }

        public void AddExitButton(ExitButton newButton)
        {
            _buttonList.Add(newButton);
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            int playerOneExitCount = 0;
            int playerTwoExitCount = 0;

            foreach (ExitButton e in _buttonList)
            {
                if (e.isPressed == true && e._player == Player.One)
                    playerOneExitCount++;

                if (e.isPressed == true && e._player == Player.Two)
                    playerTwoExitCount++;

            }

            //DebugManager.Write(exitButtonPressedCount.ToString());

            if (playerOneExitCount > 0 && playerTwoExitCount > 0)
            {
                ((ComponentModel.Audio.AmbienceAudioComponent)Core.CoreManager.Instance.Audio).Stop();
                this.SendMessage<object>("GoTo", typeof(MainMenuView));
            }

            base.UpdateGameObject(elapsedTime);
        }


        public override void Release()
        {
            Instance = null;
            base.Release();
        }
    }
}
