namespace AirHockey.GameLayer.Views.Core.Transitions
{
    /// <summary>
    /// Stores the parameter information for parameters
    /// passed from one view to another.
    /// </summary>
    class ViewTransitionParameter
    {
        /// <summary>
        /// The name/type of the parameter.
        /// </summary>
        public TransitionParameterType Name
        {
            get;
            set;
        }

        /// <summary>
        /// The value of the parameter.
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        public ViewTransitionParameter(TransitionParameterType name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
