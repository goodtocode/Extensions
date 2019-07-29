//-----------------------------------------------------------------------
// <copyright file="SaveBehavior.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
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
using Genesys.Extensions;

namespace Genesys.Extras.Data
{
    /// <summary>
    /// enumeration to allow the attribute to use strongly-typed Id
    /// </summary>    
    public enum SaveBehaviors
    {
        /// <summary>
        /// All Select, Insert, Update and Delete functionality
        /// </summary>
        IdDecides = 0,

        /// <summary>
        /// Insert functionality
        /// </summary>
        InsertOnly = 1,

        /// <summary>
        /// Select functionality
        /// </summary>
        UpdateOnly = 2,

        /// <summary>
        /// Select, Insert and Delete functionality
        /// </summary>
        DeleteOnly = 3
    }

    /// <summary>
    /// Connection string Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SaveBehavior : Attribute, IAttributeValue<SaveBehaviors>
    {
        /// <summary>
        /// Value of attribute
        /// </summary>
        public SaveBehaviors Value { get; set; } = SaveBehaviors.IdDecides;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to hydrate</param>
        public SaveBehavior(SaveBehaviors value)
        {
            Value = value;
        }
    }
}
