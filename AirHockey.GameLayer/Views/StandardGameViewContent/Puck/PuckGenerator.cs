namespace AirHockey.GameLayer.Views.StandardGameViewContent.Puck
{
    using ComponentModel;
    using ComponentModel.DataTransfer;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;
    using System;
    using AirHockey.Utility.Helpers;

    class PuckGenerator : GameObjectBase
    {
        private double _generateCooldown; // miliseconds
        private float _AdditionalPucks;

        [MessageDataMember]
        public double TimeToInstaniate
        {
            get { return this._generateCooldown; }
            set { this._generateCooldown = value; }
        }

        public PuckGenerator(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            GeneratePucks(3);
            this.TimeToInstaniate = 6000;
            this._AdditionalPucks = 0;
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            if (this._generateCooldown > 0)
            {
                this._generateCooldown -= elapsedTime;
            }
            else
            {
                GeneratePucks(RandomisationHelper.Random.Next(4 + ((int)Math.Floor(this._AdditionalPucks))) + 1);
                this._generateCooldown = 3000 + RandomisationHelper.Random.Next(4000);

                var resource = this.SendMessage<Resources.ResourceName>("Resource", "Resources.<skin>.Audio.Puck_Generate");
                InteractionLayer.Components.AudioManager.PlaySound(resource);

                if (this._AdditionalPucks < 4) this._AdditionalPucks += 0.08f;
            }
        }

        public void GeneratePuck()
        {
            var v = new Vector(DrawingManager.GetScreenDimensions().X / 2, DrawingManager.GetScreenDimensions().Y / 2);
            v.Randomize(DrawingManager.GetScreenDimensions().Y / 4);
            this.SendMessage<object>("Create", "GameObject", typeof(Puck), v.X, v.Y);
        }

        public void GeneratePucks(int instances)
        {
            for (int i = 0; i < instances; i++)
            {
                GeneratePuck();
            }
        }
    }
}
