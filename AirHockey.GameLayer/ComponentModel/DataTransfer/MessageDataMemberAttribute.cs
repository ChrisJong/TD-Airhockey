namespace AirHockey.GameLayer.ComponentModel.DataTransfer
{
    using System;

    /// <summary>
    /// An attribute used to indicate and facilitate generic
    /// access to data for a Get and Set message.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class MessageDataMemberAttribute : Attribute
    {
        /// <summary>
        /// All the names to which this data member responds.
        /// One of these would have to be the first parameter
        /// for a Get or Set message.
        /// </summary>
        public readonly string[] DataNames;

        /// <summary>
        /// Creates a <see cref="MessageDataMemberAttribute"/> based on a
        /// given list of names to which it will respond. If no name is given,
        /// the property name that this attribute is attached to will be used.
        /// </summary>
        /// <param name="names">The names to which this attribute responds.</param>
        public MessageDataMemberAttribute(params string[] names)
        {
            this.DataNames = names ?? new string[0];
        }
    }
}
