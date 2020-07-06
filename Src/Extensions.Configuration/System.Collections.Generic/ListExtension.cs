using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// List Extension
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Returns first item in a list, or empty constructed class
        /// </summary>
        /// <typeparam name="T">Type of the generic list</typeparam>
        /// <param name="item">List to get first item</param>
        /// <returns>First item, or new() constructed item</returns>
        public static T FirstOrDefaultSafe<T>(this List<T> item) where T : new()
        {
            return (item != null && item.FirstOrDefault() != null) ? item.FirstOrDefault() : new T();
        }

        /// <summary>
        /// Returns last item in a list, or empty constructed class
        /// </summary>
        /// <typeparam name="T">Type of the generic list</typeparam>
        /// <param name="item">List to get first item</param>
        /// <param name="defaultValue">Will return defaultValue if no items in collection</param>
        /// <returns>First item, or new() constructed item</returns>
        public static T FirstOrDefaultSafe<T>(this List<T> item, T defaultValue)
        {
            return (item != null && item.FirstOrDefault() != null) ? item.FirstOrDefault() : defaultValue;
        }

        /// <summary>
        /// Returns last item in a list, or empty constructed class
        /// </summary>
        /// <typeparam name="T">Type of the generic list</typeparam>
        /// <param name="item">List to get first item</param>
        /// <returns>First item, or new() constructed item</returns>
        public static T LastOrDefaultSafe<T>(this List<T> item) where T : new()
        {
            return (item != null && item.LastOrDefault() != null) ? item.LastOrDefault() : new T();
        }

        /// <summary>
        /// Returns first found item in a list, or empty constructed class.
        /// Exception-safe.
        /// </summary>
        /// <typeparam name="T">Type of generic list.</typeparam>
        /// <param name="item">Item to search.</param>
        /// <param name="index">Index position to search</param>
        /// <returns>Found item or constructed equivalent.</returns>
        public static T Item<T>(this List<T> item, int index) where T : new()
        {
            return item[index].CastSafe<T>();
        }

        /// <summary>
        /// Exception safe Find()
        /// </summary>
        /// <typeparam name="T">Generic type of list</typeparam>
        /// <param name="item">Item to search.</param>
        /// <param name="query">Predicate query to search for data</param>
        /// <returns>Found item in list based on predicate</returns>
        public static T FindSafe<T>(this List<T> item, Predicate<T> query) where T : new()
        {
            return item.Find(query).CastSafe<T>();
        }
        
        /// <summary>
        /// Adds list to current list
        /// </summary>
        /// <typeparam name="T">Type of lists</typeparam>
        /// <param name="item">Destination list</param>
        /// <param name="itemsToAdd">Source list</param>
        public static void AddRange<T>(this List<T> item, List<T> itemsToAdd)
        {
            foreach (T itemToAdd in itemsToAdd)
            {
                item.Add(itemToAdd);
            }
        }

        /// <summary>
        /// Returns type of Generic.List
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="_">Item to determine type</param>
        /// <returns>Type of generic list</returns>
        public static Type GetListType<T>(this List<T> _)
        {
            return typeof(T);
        }

        /// <summary>
        /// Returns type of IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="_">Item to determine type</param>
        /// <returns>Type of generic list</returns>>
        public static Type GetEnumerableType<T>(this IEnumerable<T> _)
        {
            return typeof(T);
        }

        /// <summary>
        /// Fills this IEnumerable list with another IEnumerable list that has types with matching properties.
        /// </summary>
        /// <typeparam name="T">Type of original object.</typeparam>
        /// <param name="item">Destination object to fill</param>
        /// <param name="sourceList">Source object</param>
        public static void FillRange<T>(this List<T> item, IEnumerable sourceList) where T : new()
        {
            var newItem = new T();

            foreach (var sourceItem in sourceList)
            {
                newItem = new T();
                newItem.Fill(sourceItem);
                item.Add(newItem);
            }            
        }
    }
}
