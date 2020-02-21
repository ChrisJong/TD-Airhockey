namespace AirHockey.GameLayer.Resources
{
    /// <summary>
    /// Defines a resource context that can be used when
    /// creating a resource name in the GameLayer.
    /// </summary>
    interface IResourceContext
    {
        /// <summary>
        /// The Skin that is used by the current resource context.
        /// </summary>
        string Skin
        {
            get;
        }
    }
}
