namespace AirHockey.GameLayer.Views.StandardGameViewContent
{
    using System.Drawing;
    using ComponentModel;
    using ComponentModel.GUI;
    using GUI;
    using GUI.Events;
    using Resources;

    /// <summary>
    /// A user interface component used to test GUI features
    /// that have been added.
    /// </summary>
    class TestingUserInterfaceComponent : UserInterfaceComponent
    {
        public TestingUserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            var position = this.SendMessage<Point>("Get", "DialogPosition");
            var dialogW = this.SendMessage<int>("Get", "DialogWidth");
            
            var button = new ButtonControl(position.X + dialogW - 60, position.Y + 10, 50, 50)
            {
                Image = this.SendMessage<ResourceName>("Resource", "resources.<skin>.CloseButton")
            };

            button.Click += this.button_OnClick;

            this.Controls.Add(button);

            var textElement = new TextControl(position.X + 122, position.Y + 110, 450, 175)
            {
                Text = "As you gaze at the screen, reading a message left behind by a fellow team member, you realize that this message is actually talking about what you are doing right now. In a hasty attempt to break the mystic connection, you try to think of something random - like fish-fingers and custard.",
                Font = this.SendMessage<ResourceName>("Resource", "Resources.Global.Debugging"),
                Colour = Color.White
            };

            this.Controls.Add(textElement);
        }

        void button_OnClick(ButtonControl sender, ButtonClickEventArgs e)
        {
            this.SendMessage<object>("Close");
        }
    }
}
