namespace AirHockey.Constants
{
    /// <summary>
    /// Contains constants directly related to the
    /// Air Hockey element of the game. Such as the
    /// Puck radius.
    /// </summary>
    public static class AirHockeyValues
    {
        public const double TaglessCooldownMax = 2500;

        public enum PhysicsCollisionType
        {
            NonCollidable,
            Force,
            SimpleBody,
            Core,
            Tower,
            ChargeStation,
            Particle,
        }

        public static class Puck
        {
            public const int AnimationFrameCount = 10;
            public const int AnimationFrameDuration = 33;
            public const int VeritcalWall = 100;
            public const int HorizontalWall = 52;
            public const float ObjectRadius = 25.0f;
            public const float ObjectMass = 1.0f;
            public const float MaxVelocity = 500;
            public const float MaxVelocitySq = MaxVelocity * MaxVelocity;
            public const float StartingVelocity = 90;
            public const float StartingVelocitySq = StartingVelocity * StartingVelocity;
        }

        public static class RechargeStation
        {
            public const float ObjectWidth = 10.0f;
            public const float ObjectHeight = 150.0f;
        }

        public static class Core
        {
            public const float ObjectRadius = 52.0f;
            public const double ComboCooldown = 2000;
            public const double DamageIndicatorLifetime = 3000;
        }

        public static class SlingshotTower
        {
            public const float TowerRadius = 1.0f;
            public const float ProjectileRadius = 70.0f;
            public const float ProjectileMass = 0.2f;
            public const float ProjectileStability = 0.90f;
        }

        public static class PulsarTower
        {
            public const float TowerRadius = 1.0f;
            public const float ProjectileRadius = 51.0f;
            public const float ProjectileMass = 0.1f;
            public const float ProjectileStability = 0.98f;
        }

        public static class ForceFieldTower
        {
            public const float TowerRadius = 1.0f;
            public const float ProjectileRadius = 208.0f;
        }

        public static class BlackholeTower
        {
            public const float TowerRadius = 1.0f;
            public const float ProjectileRadius = 208.0f;
        }

        public static class SlowTower
        {
            public const float TowerRadius = 1.0f;
            public const float ProjectileRadius = 262.0f;
        }

        public static class StasisTower
        {
            public const float TowerRadius = 1.0f;
            public const float ProjectileRadius = 158.0f;
        }
    }
}
