//-----------------------------------------------------------------------
// <copyright file="DataAccessBehavior.cs" company="GoodToCode">
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
using GoodToCode.Extensions;

namespace GoodToCode.Extras.Data
{
    /// <summary>
    /// enumeration to allow the attribute to use strongly-typed Id
    /// </summary>
    
    public enum DataAccessBehaviors
    {
        /// <summary>
        /// All Select, Insert, Update and Delete functionality
        /// </summary>
        AllAccess = 0,

        /// <summary>
        /// Insert functionality
        /// </summary>
        InsertOnly = 1,

        /// <summary>
        /// Select functionality
        /// </summary>
        SelectOnly = 2,

        /// <summary>
        /// Select, Insert and Delete functionality
        /// </summary>
        NoUpdate = 3,

        /// <summary>
        /// Select, Insert and Update functionality
        /// </summary>
        NoDelete = 4
    }

    /// <summary>
    /// Connection string Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DataAccessBehavior : Attribute, IAttributeValue<DataAccessBehaviors>
    {
        /// <summary>
        /// Value of attribute
        /// </summary>
        public DataAccessBehaviors Value { get; set; } = DataAccessBehaviors.AllAccess;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to hydrate</param>
        public DataAccessBehavior(DataAccessBehaviors value)
        {
            Value = value;
        }
    }
}
