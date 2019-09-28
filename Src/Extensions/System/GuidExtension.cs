using System;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// DoubleExtension
    /// </summary>    
    public static class  GuidExtension
    {
        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Integer to convert to guid.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static int ToInteger(this Guid item)
        {
            int returnValue;
            try
            {
            var b = item.ToByteArray();
                var bint = BitConverter.ToInt32(b, 0);
                returnValue = bint;
            }
            catch
            {
                returnValue = Defaults.Integer;
            }
            return returnValue;
        }

        /// <summary>
        /// Replaces Default.{Type} with String.Empty with a readable value
        /// Values to replace are: 01/01/1900, -1, 0.00, 00000000-0000-0000-0000-000000000000
        /// I.e. Replace -1 with "". Replace 0.00 with "Free"
        /// </summary>
        /// <param name="item">Original value to replace</param>
        /// <param name="replaceWith">Value to replace default values with</param>
        /// <returns></returns>
        public static string ToReadable(this Guid item, string replaceWith = "")
        {
            var returnValue = item.ToString();
            returnValue = item == Defaults.Guid ? replaceWith : returnValue;
            return returnValue;
        }
    }
}
