using AirHockey.Utility.Classes;
using AirHockey.GameLayer.ComponentModel.DataTransfer;
using AirHockey.Constants;
namespace AirHockey.GameLayer.ComponentModel.Graphics
{
    /// <summary>
    /// A common base class for all graphics components. Such
    /// as a component for drawing a View's background.
    /// </summary>
    abstract class GraphicsComponent : ComponentBase
    {
        private static GraphicsComponent _nil;

        //Transform values
        protected Vector _renderScaleOffset = new Vector(0, 0);
        protected float _renderRotationOffset = 0f;
        protected Vector _renderPositionOffset = new Vector(0, 0);

        //Rendering Values
        protected float _alpha = 1.0f;
        protected float _depth = 1.0f;

        public Vector RenderScaleOffset
        {
            get { return this._renderScaleOffset; }
            set { this._renderScaleOffset = value; }
        }

        public float RenderRotationOffset
        {
            get { return this._renderRotationOffset; }
            set { this._renderRotationOffset = value % 360; }
        }

        public Vector RenderPositionOffset
        {
            get { return this._renderPositionOffset; }
            set { this._renderPositionOffset = value; }
        }

        public float Alpha
        {
            get { return this._alpha; }
            set 
            {
                if (value < 0) this._alpha = 0;
                else if (value > 1) this._alpha = 1;
                else this._alpha = value;
            }
        }

        /// <summary>
        /// The depth at which to draw the image.
        /// </summary>
        public float DrawDepth
        {
            get { return this._depth; }
            set { this._depth = value; }
        }

        /// <summary>
        /// A nil value for the graphics component.
        /// </summary>
        public static GraphicsComponent Nil
        {
            get { return _nil ?? (_nil = new NilGraphicsComponent()); }
        }


        protected void SetTaglessAlpha(GraphicsComponent graphicsComponent)
        {
            var MyTower = (TowerObjectBase)graphicsComponent.ParentNode;
            graphicsComponent.Alpha = (float)(MyTower.TaglessCooldown / AirHockeyValues.TaglessCooldownMax + 0.4);
        }


        protected GraphicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.ParentNode = parentNode;
        }

        protected GraphicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        /// <summary>
        /// Draws the graphics for this component.
        /// </summary>
        public abstract void Draw();

        public virtual void Update(double delta)
        {
        }
    }
}
