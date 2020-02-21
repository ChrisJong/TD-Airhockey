namespace AirHockey.GameLayer.Resources
{
    using Utility.Attributes;
    using InteractionLayerResourceName = InteractionLayer.Components.Resources.ResourceName;

    /// <summary>
    /// This class encapsulates the name of a resource and allows
    /// for clarification of when a resource name is expected.
    /// </summary>
    class ResourceName
    {
        private readonly IResourceContext _context;

        /// <summary>
        /// Contains a list of possible resource names to test agains
        /// when retrieving the resource this resource name refers to.
        /// </summary>
        [NeverNull]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a <see cref="ResourceName"/> object based on the resource name
        /// and the context in which it is used.
        /// </summary>
        /// <param name="resourceName">The name of the resource including aliases (such as "&lt;skin&gt;").</param>
        /// <param name="context"></param>
        public ResourceName(string resourceName, IResourceContext context)
        {
            this.Name = resourceName;
            this._context = context;
        }

        /// <summary>
        /// Creates an instance of a resource name based on a current
        /// one's values.
        /// </summary>
        /// <param name="resourceName">The other <see cref="ResourceName"/>.</param>
        public ResourceName(ResourceName resourceName)
        {
            this.Name = resourceName.Name;
            this._context = resourceName._context;
        }

        /// <summary>
        /// Inplicitly converts a GameLayer resource name to an InteractionLayer
        /// resource name. Saves the need for a translator and the useage
        /// should be obvious.
        /// </summary>
        /// <param name="resourceName">The GameLayer resource name.</param>
        /// <returns>The InteractionLayer resource name.</returns>
        public static implicit operator InteractionLayerResourceName(ResourceName resourceName)
        {
            return new InteractionLayerResourceName(resourceName.Name.Replace("<skin>", resourceName._context.Skin));
        }
    }
}
