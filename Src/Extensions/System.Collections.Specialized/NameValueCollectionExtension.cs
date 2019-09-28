using System;
using System.Collections.Specialized;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends System.Type
    /// </summary>
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// Converts to flattened array of string, string
        /// </summary>
        /// <param name="item">NameValueCollection to convert to string[count, 2] array</param>
        /// <returns>True if this connection can be opened</returns>
        public static string[,] ToArraySafe(this NameValueCollection item)
        {
            var itemToConvert = item ?? new NameValueCollection();
            string[,] returnValue = new string[itemToConvert.Count, 2];

            for (var count = 0; count < itemToConvert.Count; count++)
            {
                returnValue[count, 0] = itemToConvert.Keys[count];
                returnValue[count, 1] = itemToConvert[count];
            }

            return returnValue;
        }
    }
}
