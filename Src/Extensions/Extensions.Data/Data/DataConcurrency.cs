//-----------------------------------------------------------------------
// <copyright file="DataConcurrency.cs" company="GoodToCode">
//      Copyright (c) GoodToCode. All rights reserved.
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

namespace GoodToCode.Extensions.Data
{
    /// <summary>
    /// enumeration to allow the attribute to use strongly-typed Id
    /// </summary>    
    public enum DataConcurrencies
    {
        /// <summary>
        /// When Autonmatic, Optimistic will be used in Select operations, Pessimistic will be used for Insert/Update/Delete
        /// </summary>
        Automatic = -1,

        /// <summary>
        /// Forces clean read of committed data, will not read uncommitted data
        /// Will wait for commits to finish, can be blocked
        /// This entity will re-pull itself from the database after a save, insert then fully reselect to ensure data integrity
        /// </summary>
        Pessimistic = 0,

        /// <summary>
        /// allows dirty reads of uncommitted data
        /// Can not be blocked by other processes commits
        /// This entity will not re-pull itself from the database after a save call.
        /// After a save, any data changes in the data tier will not be reflected in the object
        /// </summary>
        Optimistic = 1,
    }

    /// <summary>
    /// Connection string Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DataConcurrency : System.Attribute
    {
        /// <summary>
        /// Value of attribute
        /// </summary>
        public DataConcurrencies Value { get; set; } = DataConcurrencies.Pessimistic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to hydrate</param>
        public DataConcurrency(DataConcurrencies value)
        {
            Value = value;
        }        
    }
}
