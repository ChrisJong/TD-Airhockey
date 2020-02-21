namespace AirHockey.Utility.Attributes
{
    using System;

    /// <summary>
    /// Used to specify that a property can never have
    /// a value of null. This is especially useful for
    /// string properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NeverNullAttribute : Attribute
    {
    }
}
