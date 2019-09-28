using System;
using System.Collections.Generic;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Collections
{
    /// <summary>
    /// Contains an enumerable list of types
    /// </summary>
    
    public class ListSafe<ListType> : List<ListType>, IListSafe<ListType> where ListType : class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ListSafe() : base() { }
        
        /// <summary>
        /// Index overload
        /// </summary>
        /// <param name="key">Item to find</param>
        /// <returns>Item that matches key</returns>
        public ListType this[ListType key]
        {
            get { return base[base.IndexOf(base.Find(x => x.ToString() == key.ToString()))]; }
            set { base[base.IndexOf(base.Find(x => x.ToString() == key.ToString()))] = value; }
        }

        /// <summary>
        /// Gets an item that matches key
        /// </summary>
        /// <param name="key">Item to find</param>
        /// <returns>Item that matches key</returns>
        public ListType GetValue(ListType key)
        {
            return base.Find(x => x == key);
        }

        /// <summary>
        /// Normalizes and Adds a new member to the list
        /// </summary>
        /// <param name="newItem">Item to add</param>
        public new void Add(ListType newItem)
        {
            if (GetValue(newItem).ToStringSafe() != Defaults.String)
            {
                base.RemoveAt(FindIndex(newItem));
            }
            base.Add(newItem);
        }

        /// <summary>
        /// Remove a member from the list
        /// </summary>
        /// <param name="itemToRemove">Item to be removed</param>
        public new void Remove(ListType itemToRemove)
        {
            if (this.GetValue(itemToRemove).ToStringSafe() != Defaults.String)
            {
                base.RemoveAt(FindIndex(itemToRemove));
            }
        }

        /// <summary>
        /// Finds the index
        /// </summary>
        /// <param name="key">Key of item to find</param>
        /// <returns>Index of item matches passed item</returns>
        public int FindIndex(ListType key)
        {
            var returnValue = Defaults.Integer;

            for (var count = 0; count < this.Count; count++)
            {
                if (this[count] == key)
                {
                    returnValue = count;
                    break;
                }
            }

            return returnValue;
        }
    }    
}
