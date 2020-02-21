namespace AirHockey.GameLayer.ComponentModel
{
    using System.Drawing;
    using DataTransfer;
    using Utility.Classes;
    using AirHockey.InteractionLayer.Components;
using AirHockey.Constants;

    class TowerObjectBase : GameObjectBase
    {
        private float _energy;
        public float PreviousEnergy //Energy from the previous frame
        {
            get;
            set; 
        }

        /// <summary>
        /// The point at which the current finger is pulling back
        /// the slingshot. This will be null if there is no active
        /// finger.
        /// </summary>
        [MessageDataMember]
        public Point? PullBackPoint
        {
            get;
            set;
        }

        [MessageDataMember]
        public float PullBackRotation
        {
            get;
            set;
        }
        
        /// <summary>
        /// Time in milliseconds before tower can be activated again
        /// </summary>
        public double ToggleCooldown
        {
            get;
            set;
        }

        /// <summary>
        /// MAX time for cooldown - this is set in the child classes
        /// </summary>
        public double ToggleCooldownMax
        {
            get;
            set;
        }

        //These two are implemented for situations where the pixelsense is left alone for a while in confereces etc
        /// <summary>
        /// Time in milliseconds before tower regenerates one unit of energy
        /// </summary>
        public double RegenCooldown
        {
            get;
            set;
        }

        /// <summary>
        /// MAX time for regeneration - this is set in the child classes
        /// </summary>
        public double RegenCooldownMax
        {
            get;
            set;
        }

        public double RecentRegen
        {
            get;
            set;
        }

        /// <summary>
        /// Amount of regeration per tick - this is set in the child classes
        /// </summary>
        public float RegenRate
        {
            get;
            set;
        }

        /// <summary>
        /// Amount of time before a tower without a tag turns inactive.
        /// Caters for pixelsense's problems with flickering towers and hard-to-read tags
        /// </summary>
        public double TaglessCooldown
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the slingshot tower is currently pulled back.
        /// </summary>
        [MessageDataMember]
        public bool IsPulledBack
        {
            get { return this.PullBackPoint != null; }
        }

        [MessageDataMember]
        public TowerObjectBase Instance
        {
            get { return this; }
        }

        [MessageDataMember]
        public bool IsActivated
        {
            get;
            set;
        }

        [MessageDataMember]
        public bool IsOutOfEnergy
        {
            get { return (this.Energy <= 0); }
        }

        [MessageDataMember]
        public float Energy
        {
            get { return _energy; }
            set 
            { 
                if (value < 0)
                    this._energy = 0;
                else if (value > 100)
                    this._energy = 100;
                else 
                    this._energy = value;
            }
        }

        [MessageDataMember]
        public float Power
        {
            get;
            set;
        }

        [MessageDataMember]
        public Player Player
        {
            get;
            set;
        }

        public TowerObjectBase(Player player, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.Player = player;
            this.Energy = 100.0f;
            this.Power = 0.0f;
            this.IsActivated = false;
        }

        public override void UpdateGameObject(double elapsedTime)
        {
            base.UpdateGameObject(elapsedTime);

            // Cooldown before activation is allowed
            if (this.ToggleCooldown > 0)
            {
                this.ToggleCooldown -= elapsedTime;
            }

            // Tagless modes for towers
            if (this.IsActive == false)
            {
                if (this.TaglessCooldown > 0)
                {
                    this.TaglessCooldown -= elapsedTime;
                }
                else
                {
                    if (IsActivated == true)
                    {
                        this.ToggleCooldown = this.ToggleCooldownMax;
                        this.TaglessCooldown = AirHockeyValues.TaglessCooldownMax;
                    }
                    this.IsActivated = false;
                }
            }
            else
            {
                this.TaglessCooldown = AirHockeyValues.TaglessCooldownMax;
            }


            // Passive Tower Regeneration
            if (Energy < 100)
            {
                if (this.RegenCooldown > 0)
                {
                    this.RegenCooldown -= elapsedTime;
                    if (this.RegenCooldown <= 0)
                    {
                        this.Energy += this.RegenRate;

                    }
                }
            }
            else
            {
                this.RegenCooldown = this.RegenCooldownMax;
            }

            if (this.PreviousEnergy != this.Energy) // if _energy levels change in any way, reset regen cooldown
            {
                //DebugManager.Write("Regen Reset: " + Energy + " [from " + _prevEnergy + "]");
                this.RegenCooldown = this.RegenCooldownMax;

                if (this.PreviousEnergy < this.Energy)
                {
                    this.RecentRegen = 500;
                }
            }
            else
            {
                this.RecentRegen -= elapsedTime;
            }

            PreviousEnergy = Energy;
        }
    }
}
