using System;
using System.Text;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text
{
    /// <summary>
    /// Produces random strings of mixed characters and integers
    /// </summary>
    
    public class RandomString
    {
        /// <summary>
        /// Generates a random string of the given length
        /// </summary>
        /// <param name="length">Size of the string</param>
        /// <returns>Random string</returns>
        public static string Next(int length = 10)
        {
            var returnValue = Defaults.String;
            var builder = new StringBuilder();
            var randomClass = new Random();
            char character = '\0';

            // Build the string
            for (var Count = 0; Count <= length - 1; Count++)
            {
                character = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomClass.NextDouble() + 65)));
                builder.Append(character);
            }
            returnValue = builder.ToString();

            return returnValue;
        }
    }
}
