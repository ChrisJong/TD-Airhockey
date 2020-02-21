namespace AirHockey.InteractionLayer.Components.Audio
{
    using System;
    using Microsoft.Xna.Framework.Audio;
    using Resources;

    /// <summary>
    /// This object provides storage and access to an instance
    /// of a sound. This can be used to prematurely stop sounds
    /// that are currently playing as well as other tasks.
    /// </summary>
    public class AudioInstance
    {
        private readonly SoundEffectInstance _soundEffect;

        /// <summary>
        /// Gets whether or not the sound effect is playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return this._soundEffect.State == SoundState.Playing; }
        }

        /// <summary>
        /// Creates a new instance of a <see cref="AudioInstance"/> using
        /// the given audio resource.
        /// </summary>
        /// <param name="resourceName">The resource name for the sound to be used.</param>
        public AudioInstance(ResourceName resourceName, bool loop)
        {
#if DEBUG
            if (!ResourceManager.ValidateResourceName(UsableResourceType.Audio, resourceName))
            {
                throw new ArgumentException("Invalid resouce name provided for PlaySound.");
            }
#endif
            var resource = ResourceManager.GetResource(UsableResourceType.Audio, resourceName);
            var soundEffect = resource.LoadAudio();
            this._soundEffect = soundEffect.CreateInstance();
            this._soundEffect.IsLooped = loop;
            this._soundEffect.Play();
        }

        /// <summary>
        /// Plays the sound effect.
        /// </summary>
        /// <param name="loop">Whether or not to loop the sound until manually stopped.</param>
        public void Play(bool loop = false)
        {
            //this._soundEffect.IsLooped = loop;
            this._soundEffect.Play();
        }

        /// <summary>
        /// Stops playing the sound effect.
        /// </summary>
        public void Stop()
        {
            this._soundEffect.Stop();
        }
    }
}
