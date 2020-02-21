namespace AirHockey.GameLayer.ComponentModel.Audio
{
    using InteractionLayer.Components.Audio;
    using Resources;

    class OneShotAudioComponent : AudioComponent
    {
        private readonly AudioInstance _audioInstance;

        public OneShotAudioComponent(ResourceName resource, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this._audioInstance = new AudioInstance(resource, false);
        }

        public override void Play()
        {
            this._audioInstance.Play(false);
        }

        public void Stop()
        {
            this._audioInstance.Stop();
        }
    }
}
