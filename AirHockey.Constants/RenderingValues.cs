namespace AirHockey.Constants
{
    using System.Drawing;

    /// <summary>
    /// Contains values related to rendering.
    /// </summary>
    public static class RenderingValues
    {
        public static class Depth
        {
            public const float Transitions = 0.001f;
            public const float ExitButton = 0.1f;

            public static class SlingShot
            {
                public const float TagIcon = 0.33f;
                public const float Projectile = 0.30f;
                public const float ToggleRing = 0.31f;
                public const float ActiveAnchor = 0.31f;
                public const float EnergyRing = 0.30f;
            }

            public static class ForceField
            {
                public const float TagIcon = 0.29f;
                public const float Projectile = 0.30f;
                public const float ToggleRing = 0.31f;
                public const float ActiveAnchor = 0.31f;
                public const float EnergyRing = 0.30f;
            }

            public static class BlackHole
            {
                public const float TagIcon = 0.29f;
                public const float Projectile = 0.30f;
                public const float ToggleRing = 0.31f;
                public const float ActiveAnchor = 0.31f;
                public const float EnergyRing = 0.30f;
            }

            public static class Pulsar
            {
                public const float TagIcon = 0.33f;
                public const float Projectile = 0.30f;
                public const float ToggleRing = 0.31f;
                public const float ActiveAnchor = 0.31f;
                public const float EnergyRing = 0.30f;
            }

            public static class Slow
            {
                public const float TagIcon = 0.29f;
                public const float Projectile = 0.30f;
                public const float ToggleRing = 0.31f;
                public const float ActiveAnchor = 0.31f;
                public const float EnergyRing = 0.30f;
            }


            public static class Stasis
            {
                public const float TagIcon = 0.29f;
                public const float Projectile = 0.30f;
                public const float ToggleRing = 0.31f;
                public const float ActiveAnchor = 0.31f;
                public const float EnergyRing = 0.30f;
            }


            public static class RechargeStation
            {
                public const float Background = 0.36f;
            }

            public static class Core
            {
                public const float Object = 0.34f;
                public const float Background = 0.35f;
                public const float Regen = 0.32f;
                public const float Damaged = 0.33f;
            }

            public static class Puck
            {
                public const float Object = 0.34f;
                public const float Dissolve = 0.35f;
                public const float Create = 0.33f;
                public const float Explode = 0.33f;
                public const float Excite = 0.32f;
            }
        }

        public static class Colour
        {
            public static readonly Color ViewBackground = Color.Black;
            public static readonly Color DialogBackground = Color.Transparent;
        }
    }
}
