namespace AirHockey.InteractionLayer.Components
{
    using System;
    using Resources;

    /// <summary>
    /// Abstracts all sound and audio functionality for simple and
    /// portable access to playing sound.
    /// </summary>
    public static class AudioManager
    {
        /// <summary>
        /// Plays the given sound effect.
        /// </summary>
        /// <param name="resourceName">The resource name of the sound effect.</param>
        public static void PlaySound(ResourceName resourceName)
        {
            if (!ResourceManager.ValidateResourceName(UsableResourceType.Audio, resourceName))
            {
                throw new ArgumentException("Invalid resouce name provided for PlaySound.");
            }

            var resource = ResourceManager.GetResource(UsableResourceType.Audio, resourceName);
            var soundEffect = resource.LoadAudio();

            soundEffect.Play();
        }
    }
}
