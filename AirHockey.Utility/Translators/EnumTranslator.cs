namespace AirHockey.Utility.Translators
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides a translate function for translating between
    /// two different types of enums.
    /// </summary>
    public static class EnumTranslator
    {
        /// <summary>
        /// Translates between 2 Enums of different types. First it tries to
        /// match using the Enum Value Name, then it uses the index of the
        /// source value and returns the same indexed value in the destination enum.
        /// </summary>
        /// <typeparam name="TSource">The enum to translate from.</typeparam>
        /// <typeparam name="TDest">The enum to translate to.</typeparam>
        /// <param name="source">The value of the source enum.</param>
        /// <returns>The value of the destination enum.</returns>
        public static TDest Translate<TSource, TDest>(TSource source)
            where TSource : struct
            where TDest : struct
        {
            if (!typeof (TSource).IsEnum || !typeof (TDest).IsEnum)
            {
                throw new ArgumentException("TSource and TDest for EnumTranslator.Translate must be enumerations.");
            }

            TDest result;
            var indexOfSource = typeof (TSource).GetEnumNames().ToList().IndexOf(source.ToString());

            if (Enum.TryParse(source.ToString(), true, out result))
            {
                return result;
            }

            if (typeof(TDest).GetEnumValues().Length > indexOfSource)
            {
                return (TDest) Enum.Parse(typeof (TDest), typeof (TDest).GetEnumNames()[indexOfSource]);
            }

            throw new InvalidOperationException("Translation between TSource and TDest in EnumTranslator is not possible.");
        }
    }
}
