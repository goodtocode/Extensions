using System;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// DoubleExtension
    /// </summary>    
    public static class  IntegerExtension
    {
        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Integer to convert to decimal.</param>
        /// <returns>Converted value, or default 0.</returns>
        public static decimal ToDecimal(this short item)
        {
            return Convert.ToDecimal(item);
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Integer to convert to decimal.</param>
        /// <returns>Converted value, or default 0.</returns>
        public static decimal ToDecimal(this int item)
        {
            return Convert.ToDecimal(item);
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Integer to convert to guid.</param>
        /// <returns>Converted value, or default 00000000-0000-0000-0000-000000000000</returns>
        public static Guid ToGuid(this int item)
        {
            Guid returnValue;
            try
            {
            var bytes = new byte[16];
                BitConverter.GetBytes(item).CopyTo(bytes, 0);
                returnValue = new Guid(bytes);
            }
            catch
            {
                returnValue = Defaults.Guid;
            }
            return returnValue;
        }

        /// <summary>
        /// Quick converts
        /// </summary>
        /// <param name="item">Integer to convert to decimal.</param>
        /// <returns>Converted value, or default 0.</returns>
        public static decimal ToDecimal(this long item)
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
        public static string ToReadable(this int item, string replaceWith = "")
        {
            var returnValue = item.ToString();
            returnValue = item == Defaults.Integer ? replaceWith : returnValue;
            return returnValue;
        }

        /// <summary>
        /// Forces Negative value, regardless of starting value
        /// </summary>
        /// <returns>Negative value converted, or original value if it is negative.</returns>
        /// <remarks>Important to secure values that should never be positive, i.e. A withdrawal should always subtract from a bank account, never add to.</remarks>
        public static short Negate(this short item)
        {
            return Convert.ToInt16((item >= 0 ? item * -1 : item));
        }

        /// <summary>
        /// Forces Negative value, regardless of starting value
        /// </summary>
        /// <returns>Negative value converted, or original value if it is negative.</returns>
        /// <remarks>Important to secure values that should never be positive, i.e. A withdrawal should always subtract from a bank account, never add to.</remarks>
        public static int Negate(this int item)
        {
            return (item >= 0 ? item * -1 : item);
        }

        /// <summary>
        /// Forces Negative value, regardless of starting value
        /// </summary>
        /// <returns>Negative value converted, or original value if it is negative.</returns>
        /// <remarks>Important to secure values that should never be positive, i.e. A withdrawal should always subtract from a bank account, never add to.</remarks>
        public static long Negate(this long item)
        {
            return (item >= 0 ? item * -1 : item);
        }
    }
}
