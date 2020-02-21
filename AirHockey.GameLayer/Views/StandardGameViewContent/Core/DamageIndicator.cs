namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using AirHockey.Utility.Classes;
    using AirHockey.Constants;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;
    using AirHockey.GameLayer.GUI;

    class DamageIndicator : SimpleParticleBase
    {
        public DamageIndicator(Player player, float targetDmg, int combo, bool killingBlow, Vector position, Vector velocity, params IMessageHandler[] messageHandlers)
            : base(position, velocity, messageHandlers)
        {
            this.Graphics = new DamageIndicatorGraphicsComponent(player, targetDmg, combo, killingBlow, this, this);
        }

    }
}

