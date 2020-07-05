using System;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Double Extension
    /// </summary>    
    public static class  DoubleExtension
    {
        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Double to convert to decimal.</param>
        /// <returns>Converted value, or default 0.</returns>
        public static decimal ToDecimal(this double item)
        {
            return Convert.ToDecimal(item);
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Double to convert to decimal.</param>
        /// <returns>Converted value, or default 0.</returns>
        public static decimal ToDecimal(this double? item)
        {
            return Convert.ToDecimal(item);
        }

        /// <summary>
        /// Replaces Default.{Type} with String.Empty with a readable value
        /// Values to replace are: 01/01/1900, -1, 0.00, 00000000-0000-0000-0000-000000000000
        /// I.e. Replace -1 with "". Replace 0.00 with "Free"
        /// </summary>
        /// <param name="item">Original value to replace</param>
        /// <param name="replaceWith">Value to replace default values with</param>
        /// <returns></returns>
        public static string ToReadable(this double item, string replaceWith = "")
        {
            var returnValue = item.ToString();
            returnValue = item == 0.0 ? replaceWith : returnValue;
            return returnValue;
        }
    }    
}
