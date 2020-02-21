namespace AirHockey.Constants
{
    // Declared as non-static becuase the subclasses can be instantiated for parameter passing
    public class AnimationValues
    {
        public string BaseDirectory = "";
        public string PlayerDirectory = "";
        public int FrameDuration = Default.FrameDuration;
        public int FrameCount = Default.FrameCount;
        public float RenderingDepth = 1.0f;

        public int TagIconFrameCount = 1;
        public int TagIconFrameDuration = 1000 / 15;

        public int ProjectileFrameCount = 1;
        public int ProjectileInFrame = 1;

        public int ToggleRingFrameCount = 1;
        public int ToggleRingFrameDuration = 1000 / 15;

        public int ActiveRingFrameCount = 1;

        public int ActiveAnchorFrameCount = 1;
        public int TrailFrameCount = 1;

        public int ActivatedEffectFrameCount = 10;
        public int EnergyRingFrameCount = 100;

        public float TagIconDepth = 0.33f;
        public float ProjectileDepth = 0.30f;
        public float ToggleRingDepth = 0.31f;
        public float ActiveAnchorDepth = 0.31f;
        public float EnergyRingDepth = 0.30f;

        public class Default
        {
            public const int FrameDuration = 1000 / 30;
            public const int FrameCount = 1;
            public const int ToggleRingActivatedFrameCount = 19;
            public const int ToggleRingActivatedInFrame = 16;
            public const int ToggleRingCooldownFrameCount = 90;
            public const int ExitButtonIdle = 15;
        }

        public class Transitions
        {
            public const int FlashFrameCount = 21;
            public const int FlashFrameDuration = 1000 / 30;
        }

        public class RechargeStation
        {
            public const int BackgroundFrameCount = 30;
        }

        public class Puck
        {
            public const int FrameCount = 10;
            public const int DissolveFrameCount = 30;
            public const int CreateFrameCount = 30;
            public const int ExplodeFrameCount = 45;
            public const int ExciteFramecount = 19;
        }

        public class Core
        {
            public const int CoreFrameCount = 50;
            public const int RegenFrameCount = 40;
            public const int DamagedFrameCount = 40;
            public const int ExplodeFrameCount = 60;
            public const int BackgroundFrameCount = 30;
        }

        public class EnergyRing
        {
            public const int TowerEnergyFrameCount = 100;
            public const int TowerEnergyEmptyFramecount = 56;
            public const int TowerRegenFrameCount = 30;
        }

        public class SlingShot
        {
            public const int TagIconFrameCount = 10;
            public const int ProjectileFrameCount = 8;
            public const int ProjectileFrameDuration = 1000 / 30;

            public const int ToggleRingFrameCount = 45;
            public const int ToggleRingFrameDuration = 1000 / 15;

            public const int ActiveRingFrameCount = 10;

            public const int ActiveAnchorFrameCount = 20;
            public const int TrailFrameCount = 11;

            public const int ActivatedEffectFrameCount = 10;
        }

        public class BlackHole
        {
            public const int TagIconFrameCount = 10;
            public const int ProjectileFrameCount = 166;
            public const int ProjectileFrameDuration = 1000 / 30;

            public const int ProjectileInFrame = 17;

            public const int ToggleRingFrameCount = 45;
            public const int ToggleRingFrameDuration = 1000 / 15;

            public const int ActiveRingFrameCount = 10;

            public const int ActiveAnchorFrameCount = 20;
            public const int TrailFrameCount = 11;

            public const int ActivatedEffectFrameCount = 30;
        }

        public class ForceField
        {
            public const int TagIconFrameCount = 10;
            public const int ProjectileFrameCount = 235;
            public const int ProjectileFrameDuration = 1000 / 30;

            public const int ProjectileInFrame = 36;

            public const int ToggleRingFrameCount = 45;
            public const int ToggleRingFrameDuration = 1000 / 15;

            public const int ActiveRingFrameCount = 10;

            public const int ActiveAnchorFrameCount = 20;
            public const int TrailFrameCount = 11;

            public const int ActivatedEffectFrameCount = 30;
        }

        public class Slow
        {
            public const int TagIconFrameCount = 10;
            public const int ProjectileFrameCount = 125;
            public const int ProjectileFrameDuration = 1000 / 30;

            public const int ProjectileInFrame = 30;

            public const int ToggleRingFrameCount = 45;
            public const int ToggleRingFrameDuration = 1000 / 15;

            public const int ActiveRingFrameCount = 10;

            public const int ActiveAnchorFrameCount = 20;
            public const int TrailFrameCount = 11;

            public const int ActivatedEffectFrameCount = 30;
        }


        public class Stasis
        {
            public const int TagIconFrameCount = 10;
            public const int ProjectileFrameCount = 60;
            public const int ProjectileFrameDuration = 1000 / 30;

            public const int ProjectileInFrame = 1;

            public const int ToggleRingFrameCount = 45;
            public const int ToggleRingFrameDuration = 1000 / 15;

            public const int ActiveRingFrameCount = 10;

            public const int ActiveAnchorFrameCount = 20;
            public const int TrailFrameCount = 11;

            public const int ActivatedEffectFrameCount = 30;
        }


        public class Pulsar
        {
            public const int TagIconFrameCount = 10;
            public const int ProjectileFrameCount = 12;
            public const int ProjectileFrameDuration = 1000 / 30;

            public const int ToggleRingFrameCount = 45;
            public const int ToggleRingFrameDuration = 1000 / 15;

            public const int ActiveRingFrameCount = 10;

            public const int ActiveAnchorFrameCount = 20;
            public const int TrailFrameCount = 10;

            public const int ActivatedEffectFrameCount = 30;
        }
    }
}