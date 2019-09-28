using System;
using GoodToCode.Extensions.Collections;

namespace GoodToCode.Extensions.Configuration
{
    /// <summary>
    /// Container class for connection strings
    /// </summary>
    
    public class AppSettingSafe : KeyValuePairString
    {
        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlElements
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string AppSettings = "appSettings";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Add = "add";
            /// <summary>
            /// Clear element
            /// </summary>
            public const string Clear = "clear";
            /// <summary>
            /// Remove element
            /// </summary>
            public const string Remove = "remove";
        }

        /// <summary>
        /// Element names
        /// </summary>
        public struct XmlAttributes
        {
            /// <summary>
            /// appSettings element
            /// </summary>
            public const string Key = "key";
            /// <summary>
            /// Add element
            /// </summary>
            public const string Value = "value";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingSafe() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingSafe(KeyValuePairString item) : base(item.Key, item.Value) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public AppSettingSafe(string key, string value) : base(key, value) { }
        
        /// <summary>
        /// Returns key as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Key;
        }
    }
}
