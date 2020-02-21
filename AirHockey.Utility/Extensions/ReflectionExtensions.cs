namespace AirHockey.Utility.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        /// <summary>
        /// Retrieves the first type by the given name that is a child
        /// of the given type.
        /// </summary>
        /// <param name="assembly">The relevant assembly to search.</param>
        /// <param name="shortName">The short name of the class (AKA Name).</param>
        /// <param name="parentType">The type of the parent.</param>
        /// <returns>The first matching type or null.</returns>
        public static Type GetTypeOfKind(this Assembly assembly, string shortName, Type parentType)
        {
            return assembly.GetTypes().FirstOrDefault(x => x.Name == shortName && parentType.IsAssignableFrom(x));
        }
    }
}
