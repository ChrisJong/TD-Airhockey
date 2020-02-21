namespace AirHockey.LogicLayer.Collisions
{
    using System.Collections.Generic;
    using CollisionShapes;
    using Utility.Classes;
    using InteractionLayer.Components;
    using Constants;
    using System;

    /// <summary>
    /// A class that manages physics logic, mainly focused around
    /// collisions.
    /// </summary>
    public static class Physman
    {
        // ReSharper disable once InconsistentNaming
        private static readonly List<IPhysicsObject> _forces = new List<IPhysicsObject>();
        private static readonly List<IPhysicsObject> _simpleBodies = new List<IPhysicsObject>();
        private static readonly List<IPhysicsObject> _cores = new List<IPhysicsObject>();
        private static readonly List<IPhysicsObject> _towers = new List<IPhysicsObject>();
        private static readonly List<IPhysicsObject> _chargeStations = new List<IPhysicsObject>();
        private static readonly List<IPhysicsObject> _particles = new List<IPhysicsObject>();

        /// <summary>
        /// A collection of Force physics objects that can interact with one
        /// another.
        /// </summary>
        public static List<IPhysicsObject> Forces
        {
            get { return _forces; }
        }

        /// <summary>
        /// A collection of Simple Body physics objects that can interact with one
        /// another.
        /// </summary>
        public static List<IPhysicsObject> SimpleBodies
        {
            get { return _simpleBodies; }
        }

        /// <summary>
        /// A collection of Core physics objects that can interact with one
        /// another.
        /// </summary>
        public static List<IPhysicsObject> Cores
        {
            get { return _cores; }
        }

        /// <summary>
        /// A collection of Tower physics objects that can interact with one
        /// another.
        /// </summary>
        public static List<IPhysicsObject> Towers
        {
            get { return _towers; }
        }

        /// <summary>
        /// A collection of Charge Station physics objects that can interact with one
        /// another.
        /// </summary>
        public static List<IPhysicsObject> ChargeStations
        {
            get { return _chargeStations; }
        }

        /// <summary>
        /// A collection of Particles physics objects that can interact with one
        /// another.
        /// </summary>
        public static List<IPhysicsObject> Particles
        {
            get { return _particles; }
        }

        /// <summary>
        /// Safely adds a physics object to the collection.
        /// </summary>
        /// <param name="physicsObject">The physics object to add.</param>
        public static void AddPhysicsObject(IPhysicsObject physicsObject)
        {
            switch (physicsObject.PhysicsFlag)
            {
                case AirHockeyValues.PhysicsCollisionType.Force:
                    Forces.Add(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.SimpleBody:
                    SimpleBodies.Add(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.Tower:
                    Towers.Add(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.Core:
                    Cores.Add(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.ChargeStation:
                    ChargeStations.Add(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.Particle:
                    Particles.Add(physicsObject);
                    break;
            }
        }

        /// <summary>
        /// Savely removes a physics object from the collection.
        /// </summary>
        /// <param name="physicsObject">The physics object to remove.</param>
        public static void RemovePhysicsObject(IPhysicsObject physicsObject)
        {
            switch (physicsObject.PhysicsFlag)
            {
                case AirHockeyValues.PhysicsCollisionType.Force:
                    Forces.Remove(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.SimpleBody:
                    SimpleBodies.Remove(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.Tower:
                    Towers.Remove(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.Core:
                    Cores.Remove(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.ChargeStation:
                    ChargeStations.Remove(physicsObject);
                    break;
                case AirHockeyValues.PhysicsCollisionType.Particle:
                    Particles.Remove(physicsObject);
                    break;
            }
        }

        public static void Update(double delta)
        {
            ListCollisionCheck(Forces, SimpleBodies);
            ListCollisionCheck(Cores, SimpleBodies);
            ListCollisionCheck(Towers, ChargeStations);
        }


        /// <summary>
        /// Checks two shape lists against each other.
        /// </summary>
        /// <param name="ShapeListA"></param>
        /// <param name="ShapeListB"></param>
        /// <returns>True if ANY shapes collide with each other. Else false.</returns>
        public static bool ShapeListCollisionCheck(List<CollisionShapeBase> ShapeListA, List<CollisionShapeBase> ShapeListB)
        {
            foreach (var shapeA in ShapeListA)
            {
                foreach (var shapeB in ShapeListB)
                {
                    if (shapeA.CheckCollision(shapeB))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks two IPhysicsObject lists for collisions and fires collision events.
        /// </summary>
        /// <param name="ListA">First List</param>
        /// <param name="ListB">Second List</param>
        public static void ListCollisionCheck(List<IPhysicsObject> ListA, List<IPhysicsObject> ListB)
        {
            // problem occurs when modifying a collection whilst it's iterating it using a foreach.
            // instead i made a copy of the list to check for collisions. nothing to do with object removal.
            // the listB will always have to be simple bodies, or anything that gets deleted instantly.
            // -- need a cleaner solution, right now i don't know if they are still kept in memory.
            var copyB = new List<IPhysicsObject>(ListB);
            var copyA = new List<IPhysicsObject>(ListA);

            foreach (var objA in copyA)
            {
                if (objA.CollisionShapes.Count == 0) continue;

                foreach (var objB in copyB)
                {
                    if (objB.CollisionShapes.Count == 0) continue;

                    if (ShapeListCollisionCheck(objA.CollisionShapes, objB.CollisionShapes))
                    {
                        if (objA.IsSolid && objB.IsSolid)
                        {
                            SolidCollision(objA, objB);
                        }
                        else
                        {
                            //Invoke collision methods on both objects, passing each other in.
                            objA.TriggerCollisionEvent(objB);
                            objB.TriggerCollisionEvent(objA);

                            if (ListB.Count == 0 || ListA.Count == 0)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            copyB.Clear();
        }

        /// <summary>
        /// Solid to Solid collision algorithm. Reflects objects off each other with standard axis aligned reflections
        /// </summary>
        /// <param name="solidA"></param>
        /// <param name="solidB"></param>
        public static void SolidCollision(IPhysicsObject solidA, IPhysicsObject solidB)
        {
            ZeroMassCheck(solidA);
            ZeroMassCheck(solidB);

            if (solidA.IsStatic && solidB.IsStatic) return;
            
            if (solidA.Mass > 0 && solidB.Mass > 0)
            {
                //Transfer velocity from A to B, modified by mass as a ratio.
                var massRatio = solidA.Mass / solidB.Mass;
                var tempVelocity = solidB.Velocity;
                solidB.Velocity += solidA.Velocity * massRatio;

                //Transfer velocity from B to A, modified by mass as a ratio.
                massRatio = solidB.Mass / solidA.Mass;
                solidA.Velocity += tempVelocity * massRatio;
            }
            else
            {
                //Since physman cannot determine width and height.
                //Responsibility will be given to objects to deal with this siutation themselves.
                solidA.TriggerCollisionEvent(solidB);
                solidB.TriggerCollisionEvent(solidA);
            }
        }

        /// <summary>
        /// Projectile to Object Collision. Use typically for projectile to puck collisions.
        /// </summary>
        /// <param name="projectile">Projectile, usually a tower</param>
        /// <param name="target">Target, usually a puck</param>
        public static void ProjectileCollision(IPhysicsObject projectile, IPhysicsObject target)
        {
            ZeroMassCheck(projectile);
            ZeroMassCheck(target);
            target.Velocity += projectile.Velocity * (projectile.Mass / target.Mass);
        }

        /// <summary>
        /// Checks and zeroes velocity of objects who's mass is zero and is moving for some reason.
        /// Zero massed objects should not be moving.
        /// </summary>
        /// <param name="physicsObject"></param>
        public static void ZeroMassCheck(IPhysicsObject physicsObject)
        {
            if (physicsObject.IsStatic)
            {
                physicsObject.Velocity.X = 0;
                physicsObject.Velocity.Y = 0;
            }
        }
        
        /// <summary>
        /// Caps the velocity of a physics object to a given value, with animation easing.
        /// </summary>
        /// <param name="physicsObject">Physics object to cap</param>
        /// <param name="cap">Cap value (squared)</param>
        /// <param name="easing">Ease value between 0 and 1. (0.01 is slow, 0.1 is fast, 0.5 is basically instant.)</param>
        public static void CapVelocity(IPhysicsObject physicsObject, float capSq, float easing)
        {
            if (easing > 1) easing = 1;
            if (easing < 0) easing = 0;

            var lengthSq = physicsObject.Velocity.LengthSq;

            if (lengthSq > capSq)
            {
                EaseVelocity(physicsObject, capSq, easing);
            }
        }

        /// <summary>
        /// Eases the velocity to the targeted scale
        /// </summary>
        /// <param name="physicsObject">Physics object to ease</param>
        /// <param name="cap">Target velocity squared (to save on computing)</param>
        /// <param name="easing">Ease value between 0 and 1. (0.01 is slow, 0.1 is fast, 0.5 is basically instant.)</param>
        public static void EaseVelocity(IPhysicsObject physicsObject, float targetVelocitySq, float easing)
        {
            if (easing > 1) easing = 1;
            if (easing < 0) easing = 0;

            var lengthSq = physicsObject.Velocity.LengthSq;
            var newLengthSq = lengthSq + ((targetVelocitySq - lengthSq) * easing);

            var ratio =  newLengthSq / lengthSq;
            physicsObject.Velocity *= ratio;
        }

        /// <summary>
        /// Returns a physics object's local motion multiplier to 0 over time.
        /// </summary>
        /// <param name="physicsObject">Physics object to affect</param>
        /// <param name="easing">Ease value between 0 and 1. (0.01 is slow, 0.1 is fast, 0.5 is basically instant.)</param>
        public static void StabiliseMotionMultiplier(IPhysicsObject physicsObject, float easing)
        {
            if (easing > 1) easing = 1;
            if (easing < 0) easing = 0;

            physicsObject.MotionMultiplier += (1 - physicsObject.MotionMultiplier) * easing;
            if (physicsObject.MotionMultiplier <= 0.001)
            {
                physicsObject.MotionMultiplier = 0;
            }
        }


        public static void Release()
        {
            Forces.Clear();
            Towers.Clear();
            Cores.Clear();
            SimpleBodies.Clear();
            ChargeStations.Clear();
            Particles.Clear();
        }
    }
}
