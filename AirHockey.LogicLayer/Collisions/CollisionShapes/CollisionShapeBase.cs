using AirHockey.Utility.Classes;

namespace AirHockey.LogicLayer.Collisions.CollisionShapes
{
    /// <summary>
    /// A common base class for all collision shapes. Such
    /// as circles and rectangles (non-axis alligned).
    /// </summary>
    public abstract class CollisionShapeBase
    {
        private Vector _offset = new Vector();
        /// <summary>
        /// The offset/relative position of the collision shape from
        /// the physics object's position.
        /// </summary>
        public Vector Offset
        {
            get { return this._offset; }
            set { this._offset = value; }
        }

        /// <summary>
        /// The offset/relative position of the collision shape from
        /// the physics object's position.
        /// </summary>
        public Vector AbsolutePosition
        {
            get
            {
                return this.Parent.Position + this.Offset;
            }
        }

        /// <summary>
        /// Width of the collision shape.
        /// </summary>
        public float Width
        {
            get;
            set;
        }

        /// <summary>
        /// Height of the collision shape.
        /// </summary>
        public float Height
        {
            get;
            set;
        }

        /// <summary>
        /// Physics object of the collision shape.
        /// </summary>
        public IPhysicsObject Parent
        {
            get;
            set;
        }

        public abstract bool CheckCollision(CollisionShapeBase otherShape);
    }
}