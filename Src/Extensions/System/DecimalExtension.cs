using System;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extension to decimal class
    /// </summary>
    public static class  DecimalExtension
    {
        /// <summary>
        /// FormatCurrency With Comma
        /// </summary>
        public const string FormatCurrencyWithComma = "C";

        /// <summary>
        /// FormatPercent With Comma
        /// </summary>
        public const string FormatPercentWithComma = "P";

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Decimal to convert to double</param>
        /// <returns>Double of the passed decimal, or 0.</returns>
        public static double ToDouble(this decimal item)
        {
            return Decimal.ToDouble(item);
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Decimal to convert to integer.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static short ToShort(this decimal item)
        {
            return Decimal.ToInt16(item);
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Decimal to convert to integer.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static int ToInt(this decimal item)
        {            
            return Decimal.ToInt32(item);
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Decimal to convert to integer.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static long ToLong(this decimal item)
        {
            return Decimal.ToInt64(item);
        }

        /// <summary>
        /// Replaces Default.{Type} with String.Empty with a readable value
        /// Values to replace are: 01/01/1900, -1, 0.00, 00000000-0000-0000-0000-000000000000
        /// I.e. Replace -1 with "". Replace 0.00 with "Free"
        /// </summary>
        /// <param name="item">Original value to replace</param>
        /// <param name="replaceWith">Value to replace default values with</param>
        /// <returns></returns>
        public static string ToReadable(this decimal item, string replaceWith = "")
        {
            var returnValue = item.ToString();
            returnValue = item == 0m ? replaceWith : returnValue;
            return returnValue;
        }
    }
}
