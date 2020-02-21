namespace AirHockey.GameLayer.ComponentModel.Audio
{
    using InteractionLayer.Components.Audio;
    using Resources;

    /// <summary>
    /// A generic component that loops the given sound for as long as
    /// the game object is active.
    /// </summary>
    class AmbienceAudioComponent : AudioComponent
    {
        private readonly AudioInstance _audio;

        public AmbienceAudioComponent(ResourceName resourceName, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this._audio = new AudioInstance(resourceName, true);
        }

        public override void Play()
        {
            if (this.SendMessage<bool>("Get", "IsActive"))
            {
                if (!this._audio.IsPlaying)
                {
                    this._audio.Play(true);
                }
            }
            else
            {
                if (this._audio.IsPlaying)
                {
                    this._audio.Stop();
                }
            }
        }

        public void Stop()
        {
            this._audio.Stop();
        }
    }
}
