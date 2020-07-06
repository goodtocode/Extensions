namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Serializer interface
    /// </summary>
    
    public interface IKeyValuePair<TKey, TValue>
    {
        /// <summary>
        /// Key
        /// </summary>
        TKey Key { get; set; }
        /// <summary>
        /// Value of key
        /// </summary>
        TValue Value { get; set; }
    }
 }
