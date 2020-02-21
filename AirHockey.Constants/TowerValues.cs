namespace AirHockey.Constants
{
    public static class TowerValues
    {
        private const float TowerMinStartPullDistance = 36.0f;
        private const float TowerMaxStartPullDistance = 92.0f;

        public static class SlingshotTower
        {
            /// <summary>
            /// The minimum distance threshold for starting a pullback for
            /// a slingshot tower. This is also used when calculating the
            /// amount of power and particles to project.
            /// </summary>
            public const float MinStartPullDistance = TowerMinStartPullDistance;

            /// <summary>
            /// The maximum distance threshold for starting a pullback for
            /// a slingshot tower.
            /// </summary>
            public const float MaxStartPullDistance = TowerMaxStartPullDistance;

            public const float MinStartPullDistanceSq = MinStartPullDistance * MinStartPullDistance;
            public const float MaxStartPullDistanceSq = MaxStartPullDistance * MaxStartPullDistance;

            /// <summary>
            /// The maximum distance from the centre of the tower that
            /// the player can pull back. Will be used as a Cap.
            /// </summary>
            public const float MaxPullbackLength = 100.0f + MinStartPullDistance;
            public const float MaxPullbackLengthSq = MaxPullbackLength * MaxPullbackLength;

            /// <summary>
            /// The multiplier to apply to the Power value to calculate the
            /// number of particles to fire.
            /// </summary>
            public const float PowerToParticlesMultiplier = 0.015f;

            public const float DrainRate = 3.0f;
            public const float ToggleCooldown = 300.0f;
            public const float RegenRate = 3.0f;
            public const float RegenCooldown = 8000.0f;
        }

        public static class ForceFieldTower
        {
            public const float MinStartPullDistance = TowerMinStartPullDistance;
            public const float MaxStartPullDistance = TowerMaxStartPullDistance;
            public const float MinStartPullDistanceSq = MinStartPullDistance * MinStartPullDistance;
            public const float MaxStartPullDistanceSq = MaxStartPullDistance * MaxStartPullDistance;

            public const float MaxPullbackLength = 100.0f + MinStartPullDistance;
            public const float MaxPullbackLengthSq = MaxPullbackLength * MaxPullbackLength;

            public const float DrainRate = 0.04f;
            public const float ToggleCooldown = 1800.0f;
            public const float RegenRate = 3.0f;
            public const float RegenCooldown = 15000.0f;
        }

        public static class BlackholeTower
        {
            public const float MinStartPullDistance = TowerMinStartPullDistance;
            public const float MaxStartPullDistance = TowerMaxStartPullDistance;
            public const float MinStartPullDistanceSq = MinStartPullDistance * MinStartPullDistance;
            public const float MaxStartPullDistanceSq = MaxStartPullDistance * MaxStartPullDistance;

            public const float MaxPullbackLength = 100.0f + MinStartPullDistance;
            public const float MaxPullbackLengthSq = MaxPullbackLength * MaxPullbackLength;

            public const float DrainRate = 0.06f;
            public const float ToggleCooldown = 2200.0f;
            public const float RegenRate = 3.0f;
            public const float RegenCooldown = 15000.0f;
        }

        public static class PulsarTower
        {
            public const float MinStartPullDistance = TowerMinStartPullDistance;
            public const float MaxStartPullDistance = TowerMaxStartPullDistance;
            public const float MinStartPullDistanceSq = MinStartPullDistance * MinStartPullDistance;
            public const float MaxStartPullDistanceSq = MaxStartPullDistance * MaxStartPullDistance;

            public const float MaxPullbackLength = 100.0f + MinStartPullDistance;
            public const float MaxPullbackLengthSq = MaxPullbackLength * MaxPullbackLength;

            public const float DrainRate = 1.5f;
            public const float ToggleCooldown = 3000.0f;
            public const float RegenRate = 1.5f;
            public const float RegenCooldown = 12000.0f;
        }

        public static class SlowTower
        {
            public const float MinStartPullDistance = TowerMinStartPullDistance;
            public const float MaxStartPullDistance = TowerMaxStartPullDistance;
            public const float MinStartPullDistanceSq = MinStartPullDistance * MinStartPullDistance;
            public const float MaxStartPullDistanceSq = MaxStartPullDistance * MaxStartPullDistance;

            public const float MaxPullbackLength = 100.0f + MinStartPullDistance;
            public const float MaxPullbackLengthSq = MaxPullbackLength * MaxPullbackLength;

            public const float DrainRate = 0.03f;
            public const float ToggleCooldown = 4500.0f;
            public const float RegenRate = 2.0f;
            public const float RegenCooldown = 14000.0f;
        }

        public static class StasisTower
        {
            public const float MinStartPullDistance = TowerMinStartPullDistance;
            public const float MaxStartPullDistance = TowerMaxStartPullDistance;
            public const float MinStartPullDistanceSq = MinStartPullDistance * MinStartPullDistance;
            public const float MaxStartPullDistanceSq = MaxStartPullDistance * MaxStartPullDistance;

            public const float MaxPullbackLength = 100.0f + MinStartPullDistance;
            public const float MaxPullbackLengthSq = MaxPullbackLength * MaxPullbackLength;

            public const float DrainRate = 6.0f;
            public const float ToggleCooldown = 5200.0f;
            public const float RegenRate = 4.5f;
            public const float RegenCooldown = 16000.0f;
        }
    }
}
