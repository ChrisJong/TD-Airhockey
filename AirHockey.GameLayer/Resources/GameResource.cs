namespace AirHockey.GameLayer.Resources
{
    using System;
    using Utility.Attributes;

    /// <summary>
    /// Represents the information for a resource. This is not the actual resource.
    /// </summary>
    class GameResource
    {
        /// <summary>
        /// The name/identifier that is used when picking a
        /// resource to be used. This should be unique.
        /// </summary>
        [NeverNull]
        public readonly string Name;

        /// <summary>
        /// The name of the file to which the resource points.
        /// </summary>
        [NeverNull]
        public readonly string FileName;

        /// <summary>
        /// The type of the resource. This is mostly for internal
        /// information and initial validation when loading the
        /// resource being pointed to.
        /// </summary>
        public readonly GameResourceType Type;

        public GameResource(string name, string fileName, GameResourceType type)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Name and FileName for new resources must not be null.");
            }

            this.Name = name;
            this.FileName = fileName;
            this.Type = type;
        }
    }
}
