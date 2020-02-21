namespace AirHockey.GameLayer.ComponentModel.Physics
{
    using System.Collections.Generic;
    using DataTransfer;
    using LogicLayer.Collisions;
    using LogicLayer.Collisions.CollisionShapes;
    using Utility.Attributes;
    using Utility.Classes;
    using Constants;

    /// <summary>
    /// A common base class for all physics components. This can
    /// also be used directly since basic physics interactions are
    /// handled by the stored data.
    /// </summary>
    class PhysicsComponent : ComponentBase, IPhysicsObject
    {
        private static PhysicsComponent _nil;
        private readonly List<CollisionShapeBase> _collisionShapes = new List<CollisionShapeBase>();
        private Vector _position = new Vector(0, 0);
        private float _rotation = 0f;
        private Vector _scale = new Vector(1, 1);
        private Vector _velocity = new Vector(0, 0);
        private float _motionMultiplier = 1;
        private AirHockeyValues.PhysicsCollisionType _objectType = AirHockeyValues.PhysicsCollisionType.NonCollidable;

        public PhysicsComponent Instance 
        {
            get { return this; }
        }

        //Collision event handler.
        public event CollisionHandler CollisionEvents;

        /// <summary>
        /// Flag of object. By default is 0 - NonCollidable
        /// </summary>
        public AirHockeyValues.PhysicsCollisionType PhysicsFlag
        {
            get { return this._objectType; }
            set { this._objectType = value; }
        }

        /// <summary>
        /// Mass of the object. A zero value results in a static object.
        /// </summary>
        public float Mass
        {
            get;
            set;
        }

        /// <summary>
        /// Multiplier used for movements of objects, slow/speed towers change this and not mass, etc..
        /// </summary>
        public float MotionMultiplier
        {
            get { return this._motionMultiplier; }
            set { this._motionMultiplier = value; }
        }


        /// <summary>
        /// A nil value for the Physics component.
        /// </summary>
        public static PhysicsComponent Nil
        {
            get { return _nil ?? (_nil = new NilPhysicsComponent()); }
        }

        /// <summary>
        /// The current velocity for the game object.
        /// </summary>
        [MessageDataMember]
        [NeverNull]
        public Vector Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        [MessageDataMember]
        [NeverNull]
        public float Rotation
        {
            get { return this._rotation; }
            set { this._rotation = value; }
        }

        [MessageDataMember]
        [NeverNull]
        public Vector Scale
        {
            get { return this._scale; }
            set { this._scale = value; }
        }

        /// <summary>
        /// The current velocity for the game object.
        /// </summary>
        [MessageDataMember]
        [NeverNull]
        public Vector Velocity
        {
            get { return this._velocity; }
            set { this._velocity = value ?? new Vector(); }
        }



        /// <summary>
        /// Whether or not this game object collides like a solid object
        /// (such as a wall).
        /// </summary>
        [MessageDataMember]
        public bool IsSolid
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not this game object cannot be moved (ie is static
        /// in the game field).
        /// </summary>
        [MessageDataMember]
        public bool IsStatic
        {
            get { return (this.Mass == 0); }
        }

        /// <summary>
        /// The basic shapes that decribe the collision area for this
        /// game object.
        /// </summary>
        public List<CollisionShapeBase> CollisionShapes
        {
            get { return this._collisionShapes; }
        }

        protected PhysicsComponent(GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
            this.ParentNode = parentNode;
        }

        protected PhysicsComponent(params IMessageHandler[] messageHandlers)
            : base(messageHandlers)
        {
        }

        /// <summary>
        /// Updates a physics component's state based on its set values.
        /// </summary>
        /// <param name="delta">The time that has passed.</param>
        public virtual void Update(double delta)
        {
            if (this.ParentNode == null) return;
            if (this.ParentNode.IsActive)
            {
                this.Position.X += (float) (this.Velocity.X*delta/1000) * this.MotionMultiplier;
                this.Position.Y += (float) (this.Velocity.Y*delta/1000) * this.MotionMultiplier;
            }
        }

        /// <summary>
        /// Method called when collisions are detected between physics objects/components.
        /// </summary>
        /// <param name="otherObject"></param>
        public void TriggerCollisionEvent(IPhysicsObject otherObject)
        {
            if (this.CollisionEvents != null)
            {
                this.CollisionEvents(otherObject);
            }
        }

        public void AddCollisionShape(CollisionShapeBase temp)
        {
            if (temp == null)
                return;

            this._collisionShapes.Add(temp);
            temp.Parent = this;
        }

        public void ClearCollisionShapes()
        {
            _collisionShapes.Clear();
        }
    }
}
