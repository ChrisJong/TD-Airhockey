namespace AirHockey.Utility.Classes
{
    using System;

    /// <summary>
    /// A common base class for all types that support an Update
    /// and Draw operation.
    /// </summary>
    public abstract class UpdateAndDrawableBase
    {
        /// <summary>
        /// Performs the per-frame draw operation on this
        /// instance.
        /// </summary>
        public virtual void Render()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs the per-frame update operation on this
        /// instance.
        /// </summary>
        /// <param name="elapsedTime">The elapsed game time.</param>
        public virtual void Update(double elapsedTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Releases all references that the current object has.
        /// Child classes need to override this when they refer to parents, objects, etc...
        /// and also call base.Release() to release resources that base classes implement.
        /// All updateable and renderable objects may end up needing this.
        /// </summary>
        public virtual void Release()
        {
            throw new NotImplementedException();
        }
    }
}
