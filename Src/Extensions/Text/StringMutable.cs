using System;
using System.Runtime.Serialization;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text
{
    /// <summary>
    /// Need string class with parameterless constructor so that base classes can require "GenericType with : new()"
    /// </summary>
    
    public class StringMutable
    {
        private string valueField = Defaults.String;
        
        /// <summary>
        /// Value. Ignored for serialization, to spoof string behavior
        /// </summary>      
        public string Value
        {
            get { return valueField; }
            set { valueField = value.Trim(); }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public StringMutable()
            : base()
        {
        }

        /// <summary>
        ///  Constructor that sets string value
        /// </summary>
        /// <param name="value"></param>
        private StringMutable(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Returns a copy of this System.String object converted to lowercase using the
        ///     casing rules of the invariant culture.
        /// </summary>
        /// <returns>The lowercase equivalent of the current string</returns>
        public String ToLowerInvariant()
        {
            return this.valueField.ToLowerInvariant();
        }

        /// <summary>
        ///     Returns a copy of this System.String object converted to lowercase using the
        ///     casing rules of the invariant culture.
        /// </summary>
        /// <returns>The uppercase equivalent of the current string</returns>
        public String ToUpperInvariant()
        {
            return this.valueField.ToUpperInvariant();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Expose contains through this wrapper class
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(string value)
        {
            return this.ToStringSafe().Contains(value);
        }

        /// <summary>
        /// Accepts mutable GoodToCode.Extensions.Text.StringMutable instead of forcing consumer to cast to immutable System.String class
        /// </summary>
        /// <param name="item">Mutable string to treat as immutable .net String</param>
        public static implicit operator String(StringMutable item)
        {
            if (item == null) { item = Defaults.String; }
            return item.ToString();
        }

        /// <summary>
        /// Accepts (normal) immutable string instead of forcing consumer to cast to mutable string (this) class
        /// </summary>
        /// <param name="item">Mutable string to treat as immutable .net String</param>
        public static implicit operator StringMutable(string item)
        {
            if (item == null) { item = Defaults.String; }
            return new StringMutable(item);
        }

        /// <summary>
        /// Test for data equivalence
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var returnValue = Defaults.Boolean;

            if (System.Object.ReferenceEquals(obj, null))
            {
                returnValue = false;
            } else
            {
                returnValue = this.ToString() == obj.ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// Hash identifier for this record
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked // ignore int overflow
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ this.Value.GetHashCode(); // hash based on string value used in ==, != and equals().
                return hash;
            }
        }

        /// <summary>
        /// Compares the equality of the string contents. 
        /// </summary>
        /// <param name="obj1">First item to compare</param>
        /// <param name="obj2">Second item to compare</param>
        /// <returns>True if contents are equal, false if contents are not equal.</returns>
        public static bool operator ==(StringMutable obj1, StringMutable obj2)
        {
            var returnValue = Defaults.Boolean;

            if (System.Object.ReferenceEquals(obj1, null) || System.Object.ReferenceEquals(obj2, null))
            {
                if (System.Object.ReferenceEquals(obj1, null) && System.Object.ReferenceEquals(obj2, null))
                {
                    returnValue = true;
                }
                returnValue = false;
            } else
            {
                returnValue = obj1.ToString() == obj2.ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// Compares the equality of the string contents. 
        /// </summary>
        /// <param name="obj1">First item to compare</param>
        /// <param name="obj2">Second item to compare</param>
        /// <returns>True if contents are equal, false if contents are not equal.</returns>
        public static bool operator !=(StringMutable obj1, StringMutable obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// Returns type of string
        /// </summary>
        /// <returns></returns>
        public new Type GetType()
        {
            var returnData = Defaults.String;
            return returnData.GetType();
        }

    }
}
