namespace AirHockey.GameLayer.Views.StandardGameViewContent
{
    using ComponentModel.Graphics;
    using Resources;
    using ComponentModel;
    using Constants;

    /// <summary>
    /// This is as a class to enable tweening animations for energy drops.
    /// </summary>
    class TowerEnergyAnimationComponent : AnimationGraphicsComponent
    {
        public TowerEnergyAnimationComponent(ResourceName baseResource, AnimationValues anim, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(baseResource, anim.EnergyRingFrameCount, anim.FrameDuration, parentNode, messageHandlers)
        {
            this.AnimationPaused = true;
            this.DrawDepth = anim.EnergyRingDepth;
        }
    }
}
