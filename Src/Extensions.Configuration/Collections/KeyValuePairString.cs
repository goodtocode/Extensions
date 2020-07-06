using GoodToCode.Extensions.Text;

namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Simple serializable KeyValuePair string typed
    /// </summary>
    /// <remarks></remarks>

    public class KeyValuePairString : KeyValuePairSafe<StringMutable, StringMutable>
    {        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public KeyValuePairString() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public KeyValuePairString(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
