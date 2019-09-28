using System.Collections.Generic;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Compares based on key and value comparison. 
    /// HashCode is immutable based on Key + Value calculation on first call of GetHashCode().
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyValuePairSafeComparer<TKey, TValue> : EqualityComparer<KeyValuePairSafe<TKey, TValue>> where TKey : new() where TValue : new()
    {
        /// <summary>
        /// Immutable calculated hash code based on (Key.GetHashCode() * 17) + (Value.GetHashCode())
        /// </summary>
        /// <param name="obj">Object to compare, must be of type KeyValuePairSafe</param>
        /// <returns></returns>
        public override int GetHashCode(KeyValuePairSafe<TKey, TValue> obj)
        {
            KeyValuePairSafe<TKey, TValue> item = obj ?? new KeyValuePairSafe<TKey, TValue>();
            return (item.Key.GetHashCode() * 17 + item.Value.GetHashCode());
        }

        /// <summary>
        /// Equality comparison of child key and value combination
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool Equals(KeyValuePairSafe<TKey, TValue> x, KeyValuePairSafe<TKey, TValue> y)
        {
            return (x.ToStringSafe() == y.Key.ToStringSafe() && x.ToStringSafe() == y.Value.ToStringSafe());
        }
    }
}
