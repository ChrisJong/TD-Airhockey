namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;
    using AirHockey.GameLayer.GUI;
    using System.Drawing;

    class PlayerScoreTrackerGraphicsComponent : GraphicsComponent
    {
        private float PlayerOneDisplayDamage = 0;
        private float PlayerTwoDisplayDamage = 0;

        private TextControl PlayerOneDmgText;
        private TextControl PlayerTwoDmgText;


        public PlayerScoreTrackerGraphicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var DamageTextFont = this.SendMessage<AirHockey.GameLayer.Resources.ResourceName>("Resource", "Resources.Global.DefaultFont");

            PlayerOneDmgText = new TextControl(ViewValues.InGameStats.PlayerOneTrackerX, ViewValues.InGameStats.PlayerOneTrackerY,
                ViewValues.InGameStats.Width, ViewValues.InGameStats.Height)
            {
                Font = DamageTextFont,
                Text = "",
                Colour = Color.HotPink,
                CentreTextInBounds = true
            };

            PlayerTwoDmgText = new TextControl(ViewValues.InGameStats.PlayerTwoTrackerX, ViewValues.InGameStats.PlayerTwoTrackerY,
                ViewValues.InGameStats.Width, ViewValues.InGameStats.Height)
            {
                Font = DamageTextFont,
                Text = "",
                Colour = Color.Cyan,
                CentreTextInBounds = true
            };
        }

        public override void Update(double delta)
        {
            if (PlayerOneDisplayDamage < CoreManager.PlayerOneScore)
            {
                PlayerOneDisplayDamage += (CoreManager.PlayerOneScore - PlayerOneDisplayDamage) / 30 + 0.2f;
            }

            if (PlayerTwoDisplayDamage < CoreManager.PlayerTwoScore)
            {
                PlayerTwoDisplayDamage += (CoreManager.PlayerTwoScore - PlayerTwoDisplayDamage) / 30 + 0.2f;
            }

            PlayerOneDmgText.Text = ((int)PlayerOneDisplayDamage).ToString();
            PlayerTwoDmgText.Text = ((int)PlayerTwoDisplayDamage).ToString();
            base.Update(delta);
        }

        public override void Draw()
        {
            PlayerOneDmgText.Render();
            PlayerTwoDmgText.Render();
        }

        public override void Release()
        {
            PlayerOneDmgText.Release();
            PlayerTwoDmgText.Release();
            base.Release();
        }
    }
}
