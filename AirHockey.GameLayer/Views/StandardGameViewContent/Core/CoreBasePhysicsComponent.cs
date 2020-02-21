namespace AirHockey.GameLayer.Views.StandardGameViewContent.Core
{
    using ComponentModel;
    using ComponentModel.Physics;
    using Constants;
    using InteractionLayer.Components;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using Puck;
    using Utility.Classes;
    using Utility.Helpers;
    using System;
    using AirHockey.GameLayer.Views.StandardGameViewContent.Particle;

    class CoreBasePhysicsComponent : PhysicsComponent
    {
        private CoreBase _myCore;
        private float _yOscillation;
        private double _oscillationPhase;
        private Player _myPlayer;

        public CoreBasePhysicsComponent(Player player, float yPosition, float yOscillation, GameObjectBase coreBase, params IMessageHandler[] messageHandlers)
            : base(coreBase, messageHandlers)
        {
            this.AddCollisionShape(
                new CircleCollisionShape
                {
                    Radius = AirHockeyValues.Core.ObjectRadius
                }
            );

            this._myCore = (CoreBase)coreBase;
            this._yOscillation = yOscillation;
            this.PhysicsFlag = AirHockeyValues.PhysicsCollisionType.Core;
            this.CollisionEvents += this.OnCollision;

            this.Position = player == Player.One
                                ? new Vector(250, yPosition)
                                : new Vector(DrawingManager.GetScreenDimensions().X - 250, yPosition);
            this._myPlayer = player;
        }

        public void OnCollision(IPhysicsObject otherObject)
        {
            if (_myCore == null) return;

            if (otherObject is PuckPhysicsComponent)
            {
                this.SendMessage<object>("Create", "GameObject", typeof(PuckExplodeEffect), otherObject.Position, otherObject.Velocity);
                this.SendMessage<object>("Create", "GameObject", typeof(CoreDamagedEffect), this.Position, new Vector());
                float Damage = 3 + otherObject.Mass                    // Base Damage of 3 HP + puck's mass (1, 1.25, 2, or 3.25)
                    + (_myCore.HealthPoints * 0.10f);                               // + 10% of current HP
                float SpeedDamage = (otherObject.Velocity.LengthSq / AirHockeyValues.Puck.MaxVelocitySq * 10);// + 0 ~ 10 HP Damage depending on velocity
                if (SpeedDamage > 10.5) SpeedDamage = 10 + (float)RandomisationHelper.Random.NextDouble(); // Cap at 10.something;
                Damage += SpeedDamage;
                _myCore.HealthPoints -= Damage; // Damage caps at max of 26.25
                _myCore.RegenCooldown = 12000;

                // For scores to count, it needs to be faster than a minimum speed + 15%
                if (otherObject.Velocity.LengthSq > AirHockeyValues.Puck.StartingVelocitySq * 1.15)
                {
                    int _comboCount = CoreManager.CoreHitCombo(this._myPlayer);

                    float Score = Damage * Damage; // SCORE scales exponentially with damage. Max is 689.0625
                    Score *= 2; // Score multiplies with combo, adding x10 for more juicy numbers
                    if (_myCore.HealthPoints <= 0.0f) Score *= 1.2f;

                    Vector textPosition = new Vector();

                    if (this._myPlayer == Player.One)
                    {
                        textPosition.X = ViewValues.InGameStats.PlayerTwoTrackerX;
                        textPosition.Y = ViewValues.InGameStats.PlayerTwoTrackerY + ViewValues.InGameStats.Height * (CoreManager.PlayerTwoTextCount + 1);
                        this.SendMessage<object>("Create", "GameObject", typeof(DamageIndicator), Player.Two, Score, _comboCount,
                            (_myCore.HealthPoints <= 0), textPosition, new Vector());
                        CoreManager.PlayerTwoTextCount++;

                        CoreManager.PlayerTwoScore += (int)Score * _comboCount;
                    }
                    else
                    {
                        textPosition.X = ViewValues.InGameStats.PlayerOneTrackerX;
                        textPosition.Y = ViewValues.InGameStats.PlayerOneTrackerY + ViewValues.InGameStats.Height * (CoreManager.PlayerOneTextCount + 1);
                        this.SendMessage<object>("Create", "GameObject", typeof(DamageIndicator), Player.One, Score, _comboCount,
                            (_myCore.HealthPoints <= 0), textPosition, new Vector());
                        CoreManager.PlayerOneTextCount++;

                        CoreManager.PlayerOneScore += (int)Score * _comboCount;
                    }
     
                }

                ((PuckPhysicsComponent)otherObject).SendMessage<object>("Delete", "GameObject");

                if (_myCore.HealthPoints <= 0.0f)
                {
                    //Create explosion with the player ID attached to it (for checking end state)
                    this.SendMessage<object>("Create", "GameObject", typeof(CoreExplodeEffect), _myPlayer, this.Position, new Vector());
                    this.SendMessage<object>("Delete", "GameObject");
                }
            }
        }

        public override void Update(double delta)
        {
            this._oscillationPhase += delta / (Math.PI * 200);
            this.Position.Y += (float) Math.Sin(_oscillationPhase) * _yOscillation;
        }

        public override void Release()
        {
            this._myCore= null;
            base.Release();
        }
    }
}
