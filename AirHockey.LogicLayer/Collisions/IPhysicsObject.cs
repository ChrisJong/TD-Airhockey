namespace AirHockey.LogicLayer.Collisions
{
    using System.Collections.Generic;
    using CollisionShapes;
    using Utility.Classes;
    using Constants;

    /// <summary>
    /// Defines a physics object that can be used when
    /// calculating physics interactions.
    /// </summary>
    public interface IPhysicsObject
    {
        /// <summary>
        /// Flag for the IPhysicsObject. See global enums for the flags.
        /// </summary>
        AirHockeyValues.PhysicsCollisionType PhysicsFlag
        {
            get;
            set;
        }

        /// <summary>
        /// Mass of the object. Is used as a scalar for how quickly velocity changes.
        /// Usage: Divide velocity changes by mass. Objects start with a default of 1.
        /// </summary>
        float Mass
        {
            get;
            set;
        }


        /// <summary>
        /// Multiplier used for movements of objects, slow/speed towers change this and not mass, etc..
        /// </summary>
        float MotionMultiplier
        {
            get;
            set;
        }


        /// <summary>
        /// The position of the game object.
        /// </summary>
        Vector Position
        {
            get;
            set;
        }


        /// <summary>
        /// The Rotation of the game object.
        /// </summary>
        float Rotation
        {
            get;
            set;
        }


        /// <summary>
        /// The velocity of the game object.
        /// </summary>
        Vector Velocity
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not this game object collides like a solid object
        /// (such as a wall).
        /// </summary>
        bool IsSolid
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not this game object cannot be moved (ie is static
        /// in the game field).
        /// </summary>
        bool IsStatic
        {
            get;
        }

        /// <summary>
        /// The basic shapes that decribe the collision area for this
        /// game object. This is a list to cater for more complex shapes.
        /// </summary>
        List<CollisionShapeBase> CollisionShapes
        {
            get;
        }

        /// <summary>
        /// Method that is called when collisions are detected between physics objects.
        /// </summary>
        /// <param name="otherObject"></param>
        void TriggerCollisionEvent(IPhysicsObject otherObject);


        event CollisionHandler CollisionEvents;
    }

    //Delegates define the method/function signature of [anything], but in this context
    //it's the event. Think of it as an alias.
    //Define this outside of the class
    public delegate void CollisionHandler(IPhysicsObject other);
}
