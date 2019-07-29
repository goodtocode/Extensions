//-----------------------------------------------------------------------
// <copyright file="IEnumerableExtension.cs" company="GoodToCode">
//      Copyright (c) 2017-2020 GoodToCode. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// IEnumerable Extension
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Adds IEnumerable to current IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of IEnumerables</typeparam>
        /// <param name="item">Destination IEnumerable</param>
        /// <param name="itemToAdd">Source IEnumerable</param>
        public static void Add<T>(this IEnumerable<T> item, T itemToAdd)
        {
            item.Concat(new List<T>() { itemToAdd });
        }

        /// <summary>
        /// Adds IEnumerable to current IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of IEnumerables</typeparam>
        /// <param name="item">Destination IEnumerable</param>
        /// <param name="itemsToAdd">Source IEnumerable</param>
        public static void AddRange<T>(this IEnumerable<T> item, IEnumerable<T> itemsToAdd)
        {
            foreach (T itemToAdd in itemsToAdd)
            {
                item.Add(itemToAdd);
            }
        }
        
        /// <summary>
        /// Fills this IEnumerable IEnumerable with another IEnumerable IEnumerable that has types with matching properties.
        /// </summary>
        /// <typeparam name="T">Type of original object.</typeparam>
        /// <param name="item">Destination object to fill</param>
        /// <param name="sourceList">Source object</param>
        public static void FillRange<T>(this IEnumerable<T> item, IEnumerable sourceList) where T : new()
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
