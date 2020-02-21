namespace AirHockey.GameLayer.Views.HighscoreViewContent
{
    using System.Collections.Generic;
    using ComponentModel;
    using ComponentModel.DataTransfer;
    using GameSummaryViewContent;

    class HighscoreUserInterfaceGameObject : GameObjectBase
    {
        [MessageDataMember]
        public List<GameSummaryData> GameSummaryHistory
        {
            get;
            set;
        }

        public HighscoreUserInterfaceGameObject(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.GameSummaryHistory = GameDataHelper.LoadGameResults();
            this.UserInterface = new HighscoreUserInterfaceComponent(this.MessageHandlers.ToArray());
        }
    }
}
