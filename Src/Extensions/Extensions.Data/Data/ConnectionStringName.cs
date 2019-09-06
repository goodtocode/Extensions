//-----------------------------------------------------------------------
// <copyright file="ConnectionString.cs" company="GoodToCode">
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

namespace GoodToCode.Extensions.Data
{
    /// <summary>
    /// Class attribute decoration that holds the ConnectionStringName 
    /// Name is the key used to lookup connection string from config file.
    /// </summary>    
    [AttributeUsage(AttributeTargets.Class)]
    public class ConnectionStringName : Attribute, IAttributeValue<string>
    {
        /// <summary>
        /// Default database connection string name
        /// </summary>
        public const string DefaultConnectionName = "DefaultConnection";

        /// <summary>
        /// Name supplied by attribute.
        /// Default is DefaultConnection
        /// </summary>
        public string Value { get; set; } = DefaultConnectionName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string name</param>
        public ConnectionStringName(string connectionString)
        {
            Value = connectionString;
        }
    }
}