using System;

namespace AirHockey.LogicLayer.Collisions.CollisionShapes
{
    public class CircleCollisionShape : CollisionShapeBase
    {
        public float Radius
        {
            get { return this.Width / 2; }
            set {
                // Sets width and height to double the radius.
                // Enables seamless usage as a rectangle (also seamless typecasting)
                this.Width = value * 2;
                this.Height = value * 2;
            }
        }

        public override bool CheckCollision(CollisionShapeBase otherShape)
        {
            if (otherShape is CircleCollisionShape)
            {
                return this.CheckCollision((CircleCollisionShape)otherShape);
            }

            if (otherShape is RectangleCollisionShape)
            {
                return this.CheckCollision((RectangleCollisionShape)otherShape);
            }

            // If there are no shapes are supported, it defaults to no collision.
            // Used instead of a throw/catch to avoid error messages when being demo'd live
            // for just physics glitches.
            return false;
        }

        /// <summary>
        /// Circle to Circle Collision.
        /// Optimised not to use square roots.
        /// </summary>
        /// <param name="otherCircle"></param>
        /// <returns></returns>
        public bool CheckCollision(CircleCollisionShape otherCircle)
        {
            // circle circle check
            var dx = Math.Abs(this.AbsolutePosition.X - otherCircle.AbsolutePosition.X);
            var dy = Math.Abs(this.AbsolutePosition.Y - otherCircle.AbsolutePosition.Y);

            var radii = this.Radius + otherCircle.Radius;

            if ((dx * dx) + (dy * dy) < radii * radii)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Circle to Rectangle Collision. //NOTE: This may not be functioning correctly
        /// </summary>
        /// <param name="otherRectangle">The other Rectangle.</param>
        /// <returns></returns>
        public bool CheckCollision(RectangleCollisionShape otherRectangle)
        {
            var w = (float)0.5 * (this.Width + otherRectangle.Width);
            var h = (float)0.5 * (this.Height + otherRectangle.Height);
            var dx = (this.AbsolutePosition.X) - (otherRectangle.AbsolutePosition.X);
            var dy = (this.AbsolutePosition.Y) - (otherRectangle.AbsolutePosition.Y);

            return (Math.Abs(dx) <= w && Math.Abs(dy) <= h);           
        }
    }
}