namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using Utility.Classes;
    using AirHockey.GameLayer.GUI;
    using System.Drawing;

    class DamageIndicatorGraphicsComponent : GraphicsComponent
    {
        private float _DisplayDmg = 0;
        private float _TargetDmg;
        private TextControl _DmgText;
        private double _lifetime = AirHockeyValues.Core.DamageIndicatorLifetime;
        private Player _player;
        private bool _isKillingBlow;
        private int _comboCount;

        public DamageIndicatorGraphicsComponent(Player player, float targetDmg, int comboCount, bool killingBlow, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this._comboCount = comboCount;
            this._isKillingBlow = killingBlow;
            _player = player;
            this._TargetDmg = targetDmg;
            var DamageTextFont = this.SendMessage<AirHockey.GameLayer.Resources.ResourceName>("Resource", "Resources.Global.DefaultFont");

            _DmgText = new TextControl(this.ParentNode.Physics.Position, ViewValues.InGameStats.Width, ViewValues.InGameStats.Height)
            {
                Font = DamageTextFont,
                Text = "",
                Colour = Color.White,
                CentreTextInBounds = true
            };

            this.RenderPositionOffset.Y = ViewValues.InGameStats.PlayerOneTrackerY - parentNode.Physics.Position.Y;
        }

        public override void Update(double delta)
        {
            this.RenderPositionOffset.Y -= this.RenderPositionOffset.Y / 10.0f;
            _DmgText.Y = (int)(this.RenderPositionOffset.Y + this.ParentNode.Physics.Position.Y);

            _DmgText.Text = _comboCount.ToString() + " x ";
            _DmgText.Text += ((int)_DisplayDmg).ToString();
            if (_isKillingBlow) _DmgText.Text += "*";
            for (float i = 0.25f; i < _DisplayDmg / 1500; i++) _DmgText.Text += "!";
            _DisplayDmg += (_TargetDmg - _DisplayDmg) / 7;

            if (_DisplayDmg > 1300 || _isKillingBlow == true) _DmgText.Colour = Color.Red;
            else if (_DisplayDmg > 1000) _DmgText.Colour = Color.OrangeRed;
            else if (_DisplayDmg > 800) _DmgText.Colour = Color.Orange;
            else if (_DisplayDmg > 600) _DmgText.Colour = Color.Yellow;
            else _DmgText.Colour = Color.White;


            _lifetime -= delta;
            if (_lifetime < 0)
            {
                if (_player == Player.One) CoreManager.PlayerOneTextCount--;
                if (_player == Player.Two) CoreManager.PlayerTwoTextCount--;

                this.SendMessage<object>("Delete", "GameObject");
            }
            base.Update(delta);
        }

        public override void Draw()
        {
            _DmgText.Render();
        }

        public override void Release()
        {
            _DmgText.Release();
            base.Release();
        }
    }
}
