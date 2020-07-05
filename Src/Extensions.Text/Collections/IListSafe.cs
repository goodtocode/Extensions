using System.Collections.Generic;

namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Serializer interface
    /// </summary>
    
    public interface IListSafe<ListType> : IEnumerable<ListType>
    {
        /// <summary>
        /// Adds a known type to the collection
        /// </summary>
        void Add(ListType itemToAdd);
        /// <summary>
        /// Adds a range of known types to the collection
        /// </summary>
        void AddRange(IEnumerable<ListType> itemsToAdd);
    }
 }
