namespace AirHockey.GameLayer.Views.GameSummaryViewContent.Keyboard
{
    using System.Collections.Generic;
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.GUI;
    using GUI;
    using GUI.Events;
    using Resources;
    using Utility.Extensions;
    using InteractionLayer.Components;
    using Constants;
    using Utility.Classes;

    class KeyboardUserInterfaceComponent : UserInterfaceComponent
    {
        public char[] charName = new char[8];
        public string stringName = "";
        private int _currentIndex = 0;
        private Player currentPlayer;
        private bool _isSet = false;

        private int _playerOneStartX = 470;
        private int _playerOneStartY = 245;
        private int _playerOneStringStartX = 600;
        private int _playerOneStringStartY = 405;

        private int _playerTwoStartX = 1398;
        private int _playerTwoStartY = 806;
        private int _playerTwoStringStartX = 1321;
        private int _playerTwoStringStartY = 646;

        private int _buttonWidth = 52;
        private int _buttonHeight = 65;
        private float _buttonAngle = 0.0f;

        private int[] _row1 = new int[11] { 49, 50, 51, 52, 53, 54, 55, 56, 57, 48, 08 }; // 1,2,3,4,5,6,7,8,9,0,Backspace
        private int[] _row2 = new int[11] { 65, 66, 67, 68, 69, 70, 71, 72, 73, 33, 63 }; // A,B,C,D,E,F,G,H,I,Exclamation,Question
        private int[] _row3 = new int[11] { 74, 75, 76, 77, 78, 79, 80, 81, 82, 95, 45 }; // J,K,L,M,N,O,P,Q,R,Underscore,Minus
        private int[] _row4 = new int[11] { 83, 84, 85, 86, 87, 88, 89, 90, 46, 38, 13 }; // S,T,U,V,W,X,Y,Z,Fullstop,Ampersand,Ok(Enter/Carriage Return)

        private TextControl _nameDisplay;

        public KeyboardUserInterfaceComponent(Player player, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            int startX = 0;
            int startY = 0;
            this.currentPlayer = player;
            List<int[]> rowList = new List<int[]>();
            rowList.Add(_row1);
            rowList.Add(_row2);
            rowList.Add(_row3);
            rowList.Add(_row4);

            if (player == Player.One)
            {
                startX = this._playerOneStartX;
                startY = this._playerOneStartY;
                this._buttonAngle = 1.57f;

                var textFont = this.SendMessage<ResourceName>("Resource", "Resources.GameSummary.NameText");
                this._nameDisplay = new TextControl(this._playerOneStringStartX, this._playerOneStringStartY, this._playerOneStringStartY + (this._buttonWidth * 8), this._buttonHeight)
                {
                    Font = textFont,
                    Colour = Color.White,
                    Text = "Enter Your Name",
                    Rotation = this._buttonAngle
                };
                this.Controls.Add(this._nameDisplay);
            }
            else
            {
                startX = this._playerTwoStartX;
                startY = this._playerTwoStartY;
                this._buttonAngle = 4.71f;

                var textFont = this.SendMessage<ResourceName>("Resource", "Resources.GameSummary.NameText");
                this._nameDisplay = new TextControl(this._playerTwoStringStartX, this._playerTwoStringStartY, this._playerTwoStringStartY + (this._buttonWidth * 8), this._buttonHeight)
                {
                    Font = textFont,
                    Colour = Color.White,
                    Text = "Enter Your Name",
                    Rotation = this._buttonAngle
                };
                this.Controls.Add(this._nameDisplay);
            }

            for (int i = 0; i < rowList.Count; i++)
            {
                int newX = 0;

                if(player == Player.One)
                    newX = startX - (this._buttonHeight * i);
                else
                    newX = startX + (this._buttonHeight * i);

                this.CreateKeyButton(player, rowList[i], newX, startY, this._buttonWidth, this._buttonHeight, this._buttonAngle);
            }
        }

        private void CreateKeyButton(Player player, int[] row, int x, int y, int width, int height, float angle)
        {
            for (int i = 0; i < 11; i++)
            {
                string pressedString;
                string unpressedString;
                int newY = 0;

                if(player == Player.One)
                    newY = y + (width * i);
                else
                    newY = y - (width * i);

                switch(row[i]){
                    case 08:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.Backspace";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.Backspace";
                        break;

                    case 33:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.Exclamation";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.Exclamation";
                        break;

                    case 63:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.Question";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.Question";
                        break;

                    case 95:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.Underscore";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.Underscore";
                        break;

                    case 45:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.Minus";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.Minus";
                        break;

                    case 46:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.FullStop";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.FullStop";
                        break;

                    case 38:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.Ampersand";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.Ampersand";
                        break;

                    case 13:
                        pressedString = "Resources.<skin>.Keyboard.Pressed.OK";
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed.OK";
                        break;

                    default:
                        pressedString = "Resources.<skin>.Keyboard.Pressed." + (char)row[i];
                        unpressedString = "Resources.<skin>.Keyboard.Unpressed." + (char)row[i];
                        break;
                }

                ResourceName pressedImage = this.SendMessage<ResourceName>("Resource", pressedString);
                ResourceName unpressedImage = this.SendMessage<ResourceName>("Resource", unpressedString);
                var currentKey = new KeyboardButtonControl(x, newY, width, height)
                {
                    Image = unpressedImage,
                    PressedImage = pressedImage,
                    Angle = angle,
                    KeyID = row[i],
                    KeyCharacter = (char)row[i]
                };
                currentKey.Click += this.OnButtonClick;
                this.Controls.Add(currentKey);
            }
        }

        private void AddCharacter(int keyID)
        {
            if (this._currentIndex < this.charName.Length)
                this.charName.SetValue((char)keyID, _currentIndex++);

            this.DisplayName();
        }

        private void RemoveCharacter()
        {
            if (this._currentIndex < 0)
                this._currentIndex = 0;
            else if (this._currentIndex == 0)
                this.charName.SetValue(null, this._currentIndex);
            else
                this.charName.SetValue(null, --this._currentIndex);

            this.DisplayName();
        }

        private void EndInput()
        {
            if (!this._isSet)
            {
                if (this.currentPlayer == Player.One)
                    this.SendMessage<string>("Set", "PlayerOneName", this.stringName);
                else
                    this.SendMessage<string>("Set", "PlayerTwoName", this.stringName);

                this._isSet = true;
            }
        }

        private void DisplayName()
        {
            this.stringName = "";
            foreach (char c in this.charName)
            {
                if (c == '\0')
                    break;

                this.stringName += c;
            }

            this._nameDisplay.Text = this.stringName;
        }

        private void OnButtonClick(KeyboardButtonControl sender, ButtonClickEventArgs buttonClickEventArgs)
        {
            if (sender.KeyID == 08)
            {
                if(!this._isSet)
                    this.RemoveCharacter();
            }
            else if (sender.KeyID == 13)
            {
                this.EndInput();
            }
            else
            {
                if(!this._isSet)
                    this.AddCharacter(sender.KeyID);
            }
        }

        public override void Release()
        {
            this.Controls.ForEach(x => x.Release());
            this.Controls.Clear();

            base.Release();
        }
    }
}
