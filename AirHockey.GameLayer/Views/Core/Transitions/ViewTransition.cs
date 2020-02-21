namespace AirHockey.GameLayer.Views.Core.Transitions
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Utility.Attributes;

    /// <summary>
    /// Describes a transition from one view to another.
    /// </summary>
    class ViewTransition
    {
        private Type _destinationView = typeof(MainMenuView);
        private readonly List<ViewTransitionParameter> _parameters = new List<ViewTransitionParameter>();

        /// <summary>
        /// The view to replace the view that requests this transition.
        /// </summary>
        //[NeverNull]
        public Type DestinationView
        {
            get { return this._destinationView; }
            set
            {
                if (value != null && typeof (GameViewBase).IsAssignableFrom(value))
                {
                    this._destinationView = value;
                }
                else
                {
                    InteractionLayer.Components.DebugManager.Write("Null value assigned ot a transition destination");
                   // throw new ArgumentException("Destination View for View Transition is not valid.");
                }
            }
        }

        /// <summary>
        /// The priority of the view transition.
        /// </summary>
        public ViewTransitionPriority Priority
        {
            get;
            set;
        }

        /// <summary>
        /// The parameters to be passed from the source view to
        /// the destination view.
        /// </summary>
        [NeverNull]
        public List<ViewTransitionParameter> Parameters
        {
            get { return this._parameters; }
        }

        public void Release()
        {
            this._destinationView = null;
        }
    }
}
