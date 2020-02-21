﻿namespace AirHockey.GameLayer.ComponentModel.Graphics
{
    using InteractionLayer.Components;
    using Resources;
    using Utility.Classes;

    /// <summary>
    /// Draws an image at the Game Object's location.
    /// </summary>
    class ImageGraphicsComponent : GraphicsComponent
    {
        private readonly ResourceName _resourceName;


        public ImageGraphicsComponent(
            ResourceName resourceName,
            bool useGameObjectRotation = true,
            params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this._resourceName = resourceName;
        }

        public ImageGraphicsComponent(
            ResourceName resourceName,
            bool useGameObjectRotation,
            GameObjectBase parentNode,
            params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.ParentNode = parentNode;
            this._resourceName = resourceName;
        }

        public override void Draw()
        {
            if (this.ParentNode == null) return;

            if (this.ParentNode.IsActive)
            {
                 DrawingManager.DrawImage(this._resourceName,
                     this.ParentNode.Physics.Position + this.RenderPositionOffset, 
                     this.ParentNode.Physics.Scale + this.RenderScaleOffset, 
                     DrawingOrigin.Center,
                     this.ParentNode.Physics.Rotation + this.RenderRotationOffset,
                     this.DrawDepth,
                     this.Alpha);
            }
        }
    }
}
