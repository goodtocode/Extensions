//-----------------------------------------------------------------------
// <copyright file="IQuerableExtension.cs" company="GoodToCode">
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
using System.Linq;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// IQueryableExtension
    /// </summary>
    public static class IQueryableExtension
    {
        /// <summary>
        /// Finds first item in IQueryable, else returns new() constructed item
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="item">Item to search</param>
        /// <returns>First item in IQueryable, else returns new() constructed item</returns>
        public static T FirstOrDefaultSafe<T>(this IQueryable<T> item) where T : new()
        {
            return (item != null && item.FirstOrDefault() != null) ? item.FirstOrDefault() : new T();
        }

        /// <summary>
        /// Finds last item in IQueryable, else returns new() constructed item
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="item">Item to search</param>
        /// <returns>First item in IQueryable, else returns new() constructed item</returns>
        public static T LastOrDefaultSafe<T>(this IQueryable<T> item) where T : new()
        {
            return (item != null && item.LastOrDefault() != null) ? item.LastOrDefault() : new T();
        }
    }
}
