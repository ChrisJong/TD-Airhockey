namespace AirHockey.GameLayer.ComponentModel.GUI
{
    using System.Collections.Generic;
    using GameLayer.GUI;

    /// <summary>
    /// A common base class for all GUI components. Such
    /// as a component for drawing the GUI for a menu.
    /// </summary>
    abstract class UserInterfaceComponent : ComponentBase
    {
        protected List<GUIControl> _controls = new List<GUIControl>();
        private static UserInterfaceComponent _nil;

        /// <summary>
        /// Creates a new instance of a User Interface Component
        /// and sets up its members.
        /// </summary>
        protected UserInterfaceComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        /// <summary>
        /// A nil value for the UI component.
        /// </summary>
        public static UserInterfaceComponent Nil
        {
            get { return _nil ?? (_nil = new NilUserInterfaceComponent()); }
        }

        /// <summary>
        /// A list of controls that are automatically rendered and
        /// updated.
        /// </summary>
        protected List<GUIControl> Controls
        {
            get { return _controls; }
        }

        /// <summary>
        /// Updates the GUI component.
        /// </summary>
        /// <param name="elapsedTime">The amount of time that has passed.</param>
        public virtual void Update(double elapsedTime)
        {
            //if (this.SendMessage<bool?>("Get", "IsActive") != false)
            {
                this.Controls.ForEach(x => x.Update(elapsedTime));
            }
        }

        /// <summary>
        /// Draws the GUI component.
        /// </summary>
        public virtual void Draw()
        {
            //if (this.SendMessage<bool?>("Get", "IsActive") != false)
            {
                this.Controls.ForEach(x => x.Render());
            }
        }

        public override void Release()
        {
            this.Controls.ForEach(c => c.Release());
            this.Controls.Clear();
            base.Release();
        }
    }
}
