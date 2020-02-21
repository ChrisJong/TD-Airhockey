using System;

namespace AirHockey.LogicLayer.Collisions.CollisionShapes
{
    public class RectangleCollisionShape : CollisionShapeBase
    {
        public override bool CheckCollision(CollisionShapeBase otherShape)
        {
            if (otherShape is RectangleCollisionShape)
            {
                return this.CheckCollision((RectangleCollisionShape)otherShape);
            }

            if (otherShape is CircleCollisionShape)
            {
                return this.CheckCollision((CircleCollisionShape)otherShape);
            }
            //If there are no shapes are supported, it defaults to no collision.
            return false;
        }

        /// <summary>
        /// Rectangle to Rectangle Collision.
        /// Uses Minkowski sums to mathematically check collisions.
        /// </summary>
        /// <param name="otherRectangle">The rectangle.</param>
        /// <returns></returns>
        public bool CheckCollision(RectangleCollisionShape otherRectangle)
        {
            var w = (float)0.5 * (this.Width + otherRectangle.Width);
            var h = (float)0.5 * (this.Height + otherRectangle.Height);
            var dx = (this.AbsolutePosition.X) - (otherRectangle.AbsolutePosition.X);
            var dy = (this.AbsolutePosition.Y) - (otherRectangle.AbsolutePosition.Y);

            return (Math.Abs(dx) <= w && Math.Abs(dy) <= h);
        }

        /// <summary>
        /// Rectangle to Circle collision.
        /// </summary>
        /// <param name="otherCircle">The other circle.</param>
        /// <returns></returns>
        public bool CheckCollision(CircleCollisionShape otherCircle)
        {
            var w = (float)0.5 * (this.Width + otherCircle.Width);
            var h = (float)0.5 * (this.Height + otherCircle.Height);
            var dx = (this.AbsolutePosition.X) - (otherCircle.AbsolutePosition.X);
            var dy = (this.AbsolutePosition.Y) - (otherCircle.AbsolutePosition.Y);

            return (Math.Abs(dx) <= w && Math.Abs(dy) <= h);
        }
    }
}
