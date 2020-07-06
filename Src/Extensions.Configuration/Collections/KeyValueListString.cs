using System;
using GoodToCode.Extensions.Text;
using System.Collections;

namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Serializable Key Value List strongly typed as string
    /// </summary>
    
    public class KeyValueListString : KeyValueList<StringMutable, StringMutable>, IEnumerable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public KeyValueListString() : base() { }       
    }
}
