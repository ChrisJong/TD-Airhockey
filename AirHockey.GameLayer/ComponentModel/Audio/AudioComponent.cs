namespace AirHockey.GameLayer.ComponentModel.Audio
{
    /// <summary>
    /// A common base class for all audio components. Such
    /// as a component for playing level music.
    /// </summary>
    abstract class AudioComponent : ComponentBase
    {
        private static AudioComponent _nil;

        /// <summary>
        /// A nil value for the audio component.
        /// </summary>
        public static AudioComponent Nil
        {
            get { return _nil ?? (_nil = new NilAudioComponent()); }
        }

        protected AudioComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        /// <summary>
        /// Plays the audio implemented in this component.
        /// </summary>
        public abstract void Play();
    }
}
