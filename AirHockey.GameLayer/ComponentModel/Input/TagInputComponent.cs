namespace AirHockey.GameLayer.ComponentModel.Input
{
    using InteractionLayer.Components;
    using Utility.Classes;

    /// <summary>
    /// A component that provides input to a Game Object
    /// based on Tag positions and angle.
    /// </summary>
    class TagInputComponent : InputComponent
    {
        private readonly int _tagIndex;

        public TagInputComponent(int tagIndex, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this._tagIndex = tagIndex;
        }

        public override void Process()
        {
            if (ParentNode == null) return;
                
            var point = InputManager.GetTagPoint(this._tagIndex, true);

            if (point != null)
            {
                ParentNode.Physics.Position = point.Location;
                ParentNode.Physics.Rotation = point.Angle;
                ParentNode.IsActive = true;
            }
            else
            {
                ParentNode.IsActive = false;
            }
        }
    }
}
