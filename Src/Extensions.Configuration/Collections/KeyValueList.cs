using System;
using System.Collections.Generic;
using GoodToCode.Extensions;
using System.Linq;
using System.Collections;

namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Serializable Key Value List strongly typed
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks></remarks>
    
    public class KeyValueList<TKey, TValue> : List<KeyValuePairSafe<TKey, TValue>>, IFormattable where TKey : new() where TValue : new()
    {
        /// <summary>
        /// Item last selected from list
        /// </summary>
        public string SelectedItem { get; set; } = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public KeyValueList() : base() { }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="key">Key of item to add</param>
        /// <param name="value">Value of item to add</param>
        /// <remarks></remarks>
        public virtual void Add(TKey key, TValue value)
        {
            Remove(key);
            Add(new KeyValuePairSafe<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="key"></param>
        /// <remarks></remarks>
        public virtual void Remove(TKey key)
        {
            var index = base.IndexOf(base.Find(x => x.Key.ToStringSafe() == key.ToStringSafe()));
            if (index > -1)
            { 
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Null safe and self-normalizing indexer
        /// </summary>
        /// <param name="key">Item to get/set based on key index match</param>
        /// <returns>Get returns item from list, or not found will return new instantiation. Set will add/update match by key.</returns>
        public KeyValuePairSafe<TKey, TValue> this[TKey key]
        {
            get
            {
                KeyValuePairSafe<TKey, TValue> returnValue
                    = base.Find(x => x.Key.ToStringSafe() == key.ToStringSafe()).CastSafe<KeyValuePairSafe<TKey, TValue>>();
                return returnValue;
            }
            set
            {
                Add(value);
            }
        }

        /// <summary>
        /// Adds another member to the list
        /// </summary>
        /// <param name="key">Key to search</param>
        /// <remarks>Returns value if found, else the default value for the type</remarks>
        public TValue GetValue(TKey key)
        {
            return this[key].Value;
        }

        /// <summary>
        /// Returns General format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString("G");
        }

        /// <summary>
        /// Formats data according to requesting format
        /// </summary>
        /// <param name="format">G: Key Value/r/n, CD: Key,Value/r/n, PD: Key|Value/r/n, QS: ?Key1=Value18Key2=Value2</param>
        /// <param name="formatProvider">ICustomFormatter compatible class</param>
        /// <returns>Key and/or Value formatted</returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            var returnValue = base.ToString();
            if (formatProvider != null)
            {
                ICustomFormatter fmt = formatProvider.GetFormat(this.GetType()) as ICustomFormatter;
                if (fmt != null) { return fmt.Format(format, this, formatProvider); }
            }
            switch (format)
            {
                case "QS":
                    returnValue = String.Join("&", this.Select(x => x.ToString("EQ"))).RemoveFirst("&").AddFirst("?");
                    break;
                case "CD":
                    returnValue = String.Join(",", this.Select(x => x.ToString("G")));
                    break;
                case "PD":
                    returnValue = String.Join("|", this.Select(x => x.ToString("G")));
                    break;
                case "G":
                    returnValue = String.Join(Environment.NewLine, this.Select(x => x.ToString("G")));
                    break;
                default:
                    returnValue = String.Join(Environment.NewLine, this.Select(x => x.ToString("G")));
                    break;
            }
            return returnValue;
        }
    }
}
