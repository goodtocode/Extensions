//-----------------------------------------------------------------------
// <copyright file="RecordIdentity.cs" company="GoodToCode">
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
    public enum Fields
    {
        /// <summary>
        /// Value object: Record has no single identity field
        /// </summary>
        None = -1,

        /// <summary>
        /// Either Id or Key drives record identity
        /// </summary>
        IdOrKey = 0,

        /// <summary>
        /// Id fields defines identity
        /// </summary>
        Id = 1,

        /// <summary>
        /// Key field defines identity
        /// </summary>
        Key = 2
    }

    /// <summary>
    /// Connection string Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RecordIdentity : Attribute, IAttributeValue<Fields>
    {
        /// <summary>
        /// Value of attribute
        /// </summary>
        public Fields Value { get; set; } = Fields.IdOrKey;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to hydrate</param>
        public RecordIdentity(Fields value)
        {
            Value = value;
        }
    }
}
