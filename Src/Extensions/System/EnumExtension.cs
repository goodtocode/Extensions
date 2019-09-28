using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Enumeration extensions for [Flags] decorated enumeration
    ///   Note: [Flags] enumeration values must be bitwise friendly (1, 2, 4, 8, 16, 32, etc.) 
    ///     None must be 0, and excluded from bitwise operations 
    ///     (None = 0, FirstOption = 1, SecondOption = 2, ThirdOption = 4, etc.)
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts enum to Dictionary
        /// </summary>
        /// <param name="item">enumeration to change</param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary(this Enum item)
        {
            return Enum.GetValues(item.GetType()).Cast<int>().ToDictionary(x => x, x => Enum.GetName(item.GetType(), x));
        }

        /// <summary>
        /// Adds to the list
        /// </summary>
        /// <typeparam name="T">Enumeration type</typeparam>
        /// <param name="item">Enumeration item</param>
        /// <param name="newItem">Item to add</param>
        /// <returns></returns>
        public static T Add<T>(this Enum item, T newItem)
        {
            Type myType = item.GetType();
            object result = item;
            ValueParser itemParsed = new ValueParser(newItem, myType);

            if (itemParsed.Signed is long)
            {
                result = Convert.ToInt64(item) | (long)itemParsed.Signed;
            } 
            else if (itemParsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(item) | (ulong)itemParsed.Unsigned;
            }

            return (T)Enum.Parse(myType, result.ToString());
        }

        /// <summary>
        /// Removes an item from the enumeration
        /// </summary>
        /// <typeparam name="T">Enumeration type</typeparam>
        /// <param name="item">enumeration item</param>
        /// <param name="itemToRemove">item to remove from enumeration</param>
        /// <returns>Removed result</returns>
        public static T Remove<T>(this Enum item, T itemToRemove)
        {
            Type myType = item.GetType();
            object result = item;
            ValueParser itemParsed = new ValueParser(itemToRemove, myType);

            if (itemParsed.Signed is long)
            {
                result = Convert.ToInt64(item) & ~(long)itemParsed.Signed;
            } 
            else if (itemParsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(item) & ~(ulong)itemParsed.Unsigned;
            }

            return (T)Enum.Parse(myType, result.ToString());
        }

        /// <summary>
        /// Checks if contains a value
        /// </summary>
        /// <typeparam name="T">Enumeration type</typeparam>
        /// <param name="item">enumeration item</param>
        /// <param name="itemToCheck">item to find</param>
        /// <returns>True if enumeration contains the item and is of generic type</returns>
        public static bool Contains<T>(this Enum item, T itemToCheck)
        {
            Type myType = item.GetType();
            bool returnValue = Defaults.Boolean;
            ValueParser itemParsed = new ValueParser(itemToCheck, myType);

            if (itemParsed.Signed is long)
            {
                returnValue = (Convert.ToInt64(item) &
                    (long)itemParsed.Signed) == (long)itemParsed.Signed;
            } 
            else if (itemParsed.Unsigned is ulong)
            {
                returnValue = (Convert.ToUInt64(item) & (ulong)itemParsed.Unsigned) == (ulong)itemParsed.Unsigned;
            } 
            else
            {
                returnValue = false;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Handles unsigned and signed long
        /// </summary>
        private class ValueParser
        {
            private static readonly Type unsignedLongType = typeof(ulong);
            private static readonly Type signedLongType = typeof(long);

            /// <summary>
            /// Signed value
            /// </summary>
            public long? Signed { get; set; }

            /// <summary>
            /// Unsigned value
            /// </summary>
            public ulong? Unsigned { get; set; }

            /// <summary>
            /// Parses signed vs. unsigned
            /// </summary>
            /// <param name="value">value to parse</param>
            /// <param name="myType">type of item to parse</param>
            public ValueParser(object value, Type myType)
            {
                if (!myType.GetTypeInfo().IsEnum)
                {
                    throw new ArgumentException("Value provided is not an enumeration.");
                }
                Type compare = Enum.GetUnderlyingType(myType);
                if (compare.Equals(ValueParser.signedLongType) || compare.Equals(ValueParser.unsignedLongType))
                {
                    Unsigned = Convert.ToUInt64(value);
                }
                else
                {
                    Signed = Convert.ToInt64(value);
                }
            }
        }
    }
}

