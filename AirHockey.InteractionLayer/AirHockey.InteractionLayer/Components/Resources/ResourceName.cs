namespace AirHockey.InteractionLayer.Components.Resources
{
    using Utility.Attributes;

    /// <summary>
    /// This class encapsulates the name of a resource and allows
    /// for clarification of when a resource name is expected.
    /// </summary>
    /// Note: This should ONLY be present in parameters for
    /// InterfaceLayer functions.
    public class ResourceName
    {
        /// <summary>
        /// Contains a list of possible resource names to test agains
        /// when retrieving the resource this resource name refers to.
        /// </summary>
        [NeverNull]
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a resource name object based on the resource name
        /// and the context in which it is used.
        /// </summary>
        /// <param name="actualResourceName">The resource name by which the resources list will be searched. "&lt;skin&gt;" are not processed at this point.</param>
        public ResourceName(string actualResourceName)
        {
            this.Name = actualResourceName;
        }
    }
}
