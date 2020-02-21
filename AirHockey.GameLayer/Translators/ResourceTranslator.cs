namespace AirHockey.GameLayer.Translators
{
    using InteractionLayer.Components.Resources;
    using Resources;
    using Utility.Translators;

    /// <summary>
    /// Translates from a GameResource (GameLayer) to a UsableResource
    /// (InteractionLayer).
    /// </summary>
    class ResourceTranslator
    {
        /// <summary>
        /// Translates from a GameResource (GameLayer) to a UsableResource
        /// (InteractionLayer).
        /// </summary>
        /// <param name="source">The GameResource to translate from.</param>
        /// <returns>The UsableResource created by the translation.</returns>
        /// Note: This translation is lossy since GameResource does not state
        /// whether or not to unload the resource from memory when it is not
        /// referenced.
        public static UsableResource Translate(GameResource source)
        {
            return new UsableResource(
                source.Name,
                source.FileName,
                EnumTranslator.Translate<GameResourceType, UsableResourceType>(source.Type));
        }
    }
}
