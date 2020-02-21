namespace AirHockey.Utility.Classes
{
    using System;
    using System.Drawing;
    using AirHockey.Utility.Helpers;

    /// <summary>
    /// A datatype which defines a vector in terms of X and Y.
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// The X Component Of The Vector.
        /// </summary>
        public float X
        {
            get;
            set;
        }

        /// <summary>
        /// The Y Component Of The Vector.
        /// </summary>
        public float Y
        {
            get;
            set;
        }

        /// <summary>
        /// Zero - Returns A New Vector At A Position Of (0,0)
        /// </summary>
        public static Vector Zero
        {
            get
            {
                return new Vector(0, 0);
            }
        }

        public Vector()
        {}

        /// <summary>
        /// Constructs A New Instance.
        /// </summary>
        /// <param name="value">The Value That Will Initialize This Instance.</param>
        public Vector(float value)
        {
            this.X = value;
            this.Y = value;
        }

        /// <summary>
        /// Constructs A New Vector Using Float Values.
        /// </summary>
        /// <param name="x">The X Coordinate Of The New Vector.</param>
        /// <param name="y">The Y Coordinate Of The New Vector.</param>
        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs A New Vector Using Double Values.
        /// </summary>
        /// <param name="x">The X Coordinate Of The New Vector.</param>
        /// <param name="y">The Y Coordinate Of The New Vector.</param>
        public Vector(double x, double y)
            : this((float)x, (float)y)
        {}

        /// <summary>
        /// Constructs A New Vector Using Integer Values.
        /// </summary>
        /// <param name="x">The X Coordinate Of The New Vector.</param>
        /// <param name="y">The Y Coordinate Of The New Vector.</param>
        public Vector(int x, int y)
            : this((float)x, y)
        {}

        /// <summary>
        /// Constructs A New Vector Using Another Given Vector.
        /// </summary>
        /// <param name="v1">The Vector To Copy It's Components From.</param>
        public Vector(Vector v1)
            : this(v1.X, v1.Y)
        {}

        public Vector(Point v1)
            : this(v1.X, v1.Y)
        {}

        /// <summary>
        /// Clone - Copies The Current Vector Onto Another.
        /// </summary>
        public Vector Clone()
        {
            return new Vector(this.X, this.Y);
        }

        /// <summary>
        /// Dot Product - Calculates The Dot Product Of This Vector With Another Specified Vecotr.
        /// </summary>
        /// <param name="v1">The Second Vector.</param>
        public float DotProduct(Vector v1)
        {
            return this.X * v1.X + this.Y * v1.Y;
        }

        /// <summary>
        /// Dot Product - Calculates The Dot Product Of Two Vectors.
        /// </summary>
        /// <param name="v1">The First Vector.</param>
        /// <param name="v2">The Second Vector.</param>
        public static float DotProduct(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        /// <summary>
        /// Cross Product - Returns The Cross Product Of This Vector With Another Specified Vector.
        /// </summary>
        /// <param name="v1">The Second Vector.</param>
        public float CrossProduct(Vector v1)
        {
            return this.X * v1.Y - this.Y * v1.X;
        }

        /// <summary>
        /// Cross Product - Calculates The Cross Product Of Two Vectors.
        /// </summary>
        /// <param name="v1">The First Vector.</param>
        /// <param name="v2">The Second Vector.</param>
        public static float CrossProduct(Vector v1, Vector v2)
        {
            return v1.X * v2.X - v1.Y * v2.Y;
        }

        /// <summary>
        /// Length Property - The Length Of This Vector.
        /// </summary>
        public float Length
        {
            get { return (float) Math.Sqrt(this.LengthSq); }
        }

        /// <summary>
        /// Legnth Squared Property - The Squared Length OF This Vector.
        /// </summary>
        public float LengthSq
        {
            get { return this.X*this.X + this.Y*this.Y; }
        }

        /// <summary>
        /// Distance - Calculates The Distance Between Two Vectors.
        /// </summary>
        /// <param name="v1">The First Vector</param>
        /// <param name="v2">The Second Vector</param>
        public static float Distance(Vector v1, Vector v2)
        {
            return (float)Math.Sqrt(DistanceSq(v1, v2));
        }

        /// <summary>
        /// Distance Squared - Calculates The Distance Squared Between Two Vectors.
        /// </summary>
        /// <param name="v1">The First Vector</param>
        /// <param name="v2">The Second Vector</param>
        public static float DistanceSq(Vector v1, Vector v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return (dx * dx + dy * dy);
        }

        public static Vector AngleToUnitVector(float angle)
        {
            return new Vector(Math.Sin(angle), -Math.Cos(angle)).UnitVector;
        }

        /// <summary>
        /// UnitVector - Converts This Vector Into A Unit Vector. Will return a Zero
        /// vector if the original vector is zero.
        /// </summary>
        public Vector UnitVector
        {
            get { return Math.Abs(this.Length) < 0.0001 ? new Vector() : this/this.Length; }
        }

        /// <summary>
        /// AngleBetween - Returns The Angle In Degrees Between Two Vectors.
        /// </summary>
        /// <param name="v1">The First Vector.</param>
        /// <param name="v2">The Second Vector.</param>
        public static double AngleBetweenInDegree(Vector v1, Vector v2)
        {
            var dx = v2.X - v1.X;
            var dy = v2.Y - v1.Y;

            return Math.Atan2(dy, dx) * 180.0f / Math.PI;
        }

        public static float AngleBetweenInRadian(Vector v1, Vector v2)
        {
            return (float)((int)(AngleBetweenInDegree(v1, v2) - 90) * (Math.PI / 180.0f));
        }

        public static Vector Projection(Vector v1, Vector target)
        {
            return new Vector(target * (v1.DotProduct(target) / target.LengthSq));
        }

        public Vector Projection(Vector target)
        {
            return Projection(this, target);
        }

        public Vector Perpendicular()
        {
            var x = this.X;
            var y = this.Y;

            y = -y;
            return new Vector(y, x);
        }

        /// <summary>
        /// Randomizes the vector by the given value (positive and negatively)
        /// </summary>
        /// <param name="scale"></param>
        public void Randomize(float scale)
        {
            this.X += ((float)RandomisationHelper.Random.NextDouble() * scale * 2) - scale;
            this.Y += ((float)RandomisationHelper.Random.NextDouble() * scale * 2) - scale;
        }

        /// <summary>
        /// Reflects vector off a given normal vector.
        /// </summary>
        /// <param name="normal">Normal to reflect off.</param>
        public void Reflect(Vector normal)
        {
            var dot = DotProduct(this, normal.UnitVector);
            this.X -= ((2f * dot) * normal.X);
            this.Y -= ((2f * dot) * normal.Y);
        }

        /// <summary>
        /// Reverses the direction of the vector.
        /// </summary>
        public void Reverse()
        {
            this.X = -this.X;
            this.Y = -this.Y;
        }

        /// <summary>
        /// Retrieves a <see cref="Vector"/> which is the inverse of
        /// this <see cref="Vector"/>.
        /// </summary>
        /// <returns>The inverse of this <see cref="Vector"/>.</returns>
        public Vector Inverse()
        {
            return new Vector(-this.X, -this.Y);
        }

        /// <summary>
        /// Converts A Specified Vector Into A Point Type.
        /// </summary>
        /// <param name="v1">The Vector To Convert.</param>
        /// <returns>Returns A New Point Of The Given Vector.</returns>
        public static implicit operator Point(Vector v1)
        {
            return new Point((int)v1.X, (int)v1.Y);
        }

        /// <summary>
        /// Converts A Specified Point Into A Vector Type.
        /// </summary>
        /// <param name="p1">The Point To Convert.</param>
        /// <returns>Returns A New Vector Of The Given Point.</returns>
        public static implicit operator Vector(Point p1)
        {
            return new Vector(p1.X, p1.Y);
        }

        /// <summary>
        /// (Operator +) - Adds Two Vectors.
        /// </summary>
        /// <param name="left">The First Vector To Add.</param>
        /// <param name="right">The Second Vector To Add.</param>
        /// <returns>Returns A New Vector Of The Sum Of Two Vectors.</returns>
        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// (Operator -) - Subtracts Two Vectors.
        /// </summary>
        /// <param name="left">The First Vector To Subtract.</param>
        /// <param name="right">The Second Vector To Subtract.</param>
        /// <returns>Returns A New Vector Of The Difference Of Two Vectors.</returns>
        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// (Operator *) - Multiplies (Scales/Integer) A Vector By A Given Value.
        /// </summary>
        /// <param name="left">The Vector To Scale.</param>
        /// <param name="scale"></param>
        /// <returns>Returns A New Scaled Vector.</returns>
        public static Vector operator *(Vector left, int scale)
        {
            return new Vector(left.X * scale, left.Y * scale);
        }

        /// <summary>
        /// Multiplies (Scales/Float) A Vector By A Given Value.
        /// </summary>
        /// <param name="left">The Vector To Scale.</param>
        /// <param name="scale"></param>
        /// <returns>Returns A New Scaled Vector.</returns>
        public static Vector operator *(Vector left, float scale)
        {
            return new Vector(left.X * scale, left.Y * scale);
        }

        /// <summary>
        /// Multiplication between the specified instance by a scale vector.
        /// </summary>
        /// <param name="left">The First Vector To Multiply.</param>
        /// <param name="scale"></param>
        /// <returns>Returns A New Vector Of The Multiplication Of The Two Vectors.</returns>
        public static Vector operator *(Vector left, Vector scale)
        {
            return new Vector(left.X * scale.X, left.Y * scale.Y);
        }

        /// <summary>
        /// Divides (Scales/Float) A Vector By A Given Value.
        /// </summary>
        /// <param name="left">The Vector To Scale</param>
        /// <param name="scale"></param>
        /// <returns>Returns A New Scaled Vector.</returns>
        public static Vector operator /(Vector left, float scale)
        {
            return new Vector(left.X / scale, left.Y / scale);
        }

        /// <summary>
        /// Divide The Vector With Another Vector.
        /// </summary>
        /// <param name="left">The First Vector To Divide</param>
        /// <param name="right">The Second Vector To Divide</param>
        /// <returns>Returns A New Vector For The Division Of The Two Vectors.</returns>
        public static Vector operator /(Vector left, Vector right)
        {
            return new Vector(left.X/right.X, left.Y/right.Y);
        }

        /// <summary>
        /// Compares the specified instances for equality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>True if both instances are equal; false otherwise.</returns>
        public static bool operator ==(Vector left, Vector right)
        {
            return (left == null && right == null)
                   || (left != null && left.Equals(right));
        }

        /// <summary>
        /// Compares the specified instances for inequality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>True if both instances are not equal; false otherwise.</returns>
        public static bool operator !=(Vector left, Vector right)
        {
            return (left == null && right == null)
                   || (left != null && left.Equals(right));
        }

        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
                return false;

            return this.Equals((Vector)obj);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>True if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public bool Equals(Vector other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return this.X == other.X && this.Y == other.Y;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        /// <summary>
        /// Converts This Instance Vector Into A String Format.
        /// </summary>
        /// <returns>Returns A System.String That Represents The Current Vector.</returns>
        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}", this.X, this.Y);
        }
    }
}
