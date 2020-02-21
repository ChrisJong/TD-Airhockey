namespace AirHockey.Utility.Extensions
{
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="me">The current Dictionary object.</param>
        /// <returns></returns>
        public static List<TKey> GetKeysList<TKey, TValue>(this Dictionary<TKey, TValue> me)
        {
            var result = new List<TKey>();

            foreach (var pair in me)
            {
                result.Add(pair.Key);
            }

            return result;
        }
    }
}
