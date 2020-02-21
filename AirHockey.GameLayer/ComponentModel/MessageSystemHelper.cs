namespace AirHockey.GameLayer.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DataTransfer;

    /// <summary>
    /// Contains a set of functions to make life
    /// easier for message handlers and senders.
    /// </summary>
    internal static class MessageSystemHelper
    {
        /// <summary>
        /// Validates the input from a SendMessage and checks
        /// whether or not it matches an expected signature.
        /// </summary>
        /// <param name="expected">The expected message.</param>
        /// <param name="actual">The actual message.</param>
        /// <param name="expectedParameters">The expected parameter signature.</param>
        /// <param name="parameters">The actual parameter signature.</param>
        /// <returns>Whether or not the input matched the described signature.</returns>
        public static bool ValidateMessage(
            string expected,
            string actual,
            Type[] expectedParameters,
            object[] parameters)
        {
            if (expected != actual || expectedParameters.Length != parameters.Length)
            {
                return false;
            }

            for (var i = 0; i < expectedParameters.Length; i++)
            {
                if (parameters[i] == null && !expectedParameters[i].IsClass)
                {
                    return false;
                }

                if (!expectedParameters[i].IsInstanceOfType(parameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Goes through each message handler in the list
        /// and returns the value of the first one not to
        /// return null.
        /// </summary>
        /// <param name="messageHandlers">The list of message handlers.</param>
        /// <param name="message">The message to process.</param>
        /// <param name="parameters">The parameters for the message.</param>
        /// <returns>The result.</returns>
        public static object IterateThroughMessageHandlers(
            IEnumerable<IMessageHandler> messageHandlers,
            string message,
            params object[] parameters)
        {
            object result = null;

            foreach (var messageHandler in messageHandlers)
            {
                if ((result = messageHandler.AcceptMessage(message, parameters)) != null)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether or not the message is a Get message
        /// (data retrieval).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The message's parameters.</param>
        /// <returns>Whether or not he message is a Get.</returns>
        public static bool IsGetMessage(string message, params object[] parameters)
        {
            return ValidateMessage("Get", message, new[] {typeof (string)}, parameters);
        }

        /// <summary>
        /// Returns whether or not the message is a Set message
        /// (data retrieval).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="valueType">The expected type of the value.</param>
        /// <param name="parameters">The message's parameters.</param>
        /// <returns>Whether or not he message is a Set.</returns>
        public static bool IsSetMessage(string message, Type valueType, params object[] parameters)
        {
            return ValidateMessage("Set", message, new[] {typeof (string), valueType}, parameters);
        }

        /// <summary>
        /// Handles a Get or Set message for a given object.
        /// </summary>
        /// <param name="obj">The object to use as a context for the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The message's parameters.</param>
        /// <returns>The response to the message (or null).</returns>
        public static object HandleGetOrSetMessage(object obj, string message, params object[] parameters)
        {
            var objType = obj.GetType();

            var properties =
                objType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => x.GetCustomAttributes(typeof (MessageDataMemberAttribute), false).Any());

            if (message == "Get" && parameters.Length == 1 && parameters[0] is string)
            {
                var valueName = parameters[0] as string;

                foreach (var property in properties)
                {
                    var attribute =
                        (MessageDataMemberAttribute)
                            property.GetCustomAttributes(typeof (MessageDataMemberAttribute), false)[0];

                    if (((attribute.DataNames.Length == 0 && property.Name == valueName) ||
                         attribute.DataNames.Contains(valueName)) && property.CanRead &&
                        property.GetGetMethod(true).IsPublic)
                    {
                        return property.GetValue(obj, null);
                    }
                }
            }
            else if (message == "Set" && parameters.Length == 2 && parameters[0] is string)
            {
                var valueName = parameters[0] as string;

                foreach (var property in properties)
                {
                    var attribute =
                        (MessageDataMemberAttribute)
                            property.GetCustomAttributes(typeof(MessageDataMemberAttribute), false)[0];

                    if (((attribute.DataNames.Length == 0 && property.Name == valueName)
                         || attribute.DataNames.Contains(valueName))
                        && (property.PropertyType.IsInstanceOfType(parameters[1])
                            || (parameters[1] == null
                                && (property.PropertyType.IsClass || property.PropertyType.FullName.StartsWith("System.Nullable`1"))))
                        && property.CanWrite && property.GetSetMethod(true).IsPublic)
                    {
                        property.SetValue(obj, parameters[1], null);
                        return MessageResult.Nil;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Converts the object result value to the type of TResult.
        /// </summary>
        /// <typeparam name="TResult">The type of the desired result.</typeparam>
        /// <param name="result">The result as an object.</param>
        /// <returns>The result as the desired type.</returns>
        public static TResult ConvertResult<TResult>(object result)
        {
            if (result is TResult)
            {
                return (TResult) result;
            }

            return default(TResult);
        }
    }
}
