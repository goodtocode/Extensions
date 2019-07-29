﻿//-----------------------------------------------------------------------
// <copyright file="Customer.cs" company="GoodToCode">
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
using GoodToCode.Extensions;
using System;
using System.Collections.Generic;

namespace GoodToCode.Extras.Test
{
    /// <summary>
    /// Entity for direct table access
    /// </summary>    
    public partial class Customer
    {
        /// <summary>
        /// Id of record
        ///  Can set Id before saving, and will be preserved
        ///  only if using GoodToCode.Framework.Repository for CRUD
        /// </summary>
        public virtual int Id { get; set; } = Defaults.Integer;

        /// <summary>
        /// Key of record
        ///  Can set Key before saving, and will be preserved
        ///  only if using GoodToCode.Framework.Repository for CRUD
        /// </summary>
        public virtual Guid Key { get; set; } = Defaults.Guid;

        /// <summary>
        /// Activity history that created this record
        /// </summary>
        public virtual Guid ActivityContextKey { get; set; } = Defaults.Guid;

        /// <summary>
        /// Date record was created
        /// </summary>
        public virtual DateTime CreatedDate { get; set; } = Defaults.Date;

        /// <summary>
        /// Date record was modified
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; } = Defaults.Date;

        /// <summary>
        /// Status of this record
        /// </summary>
        public virtual Guid State { get; set; } = Defaults.Guid;

        /// <summary>
        /// FirstName of customers
        /// </summary>
        public string FirstName { get; set; } = Defaults.String;

        /// <summary>
        /// MiddleName of customer
        /// </summary>
        public string MiddleName { get; set; } = Defaults.String;

        /// <summary>
        /// LastName of customer
        /// </summary>
        public string LastName { get; set; } = Defaults.String;

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        public DateTime BirthDate { get; set; } = Defaults.Date;

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        public int GenderId { get; set; } = Genders.NotSet.Key;

        /// <summary>
        /// Type of customer
        /// </summary>
        public int CustomerTypeId { get; set; } = Defaults.Integer;
       
        /// <summary>
        /// ISO 5218 Standard for Gender values
        /// </summary>
        public struct Genders
        {
            /// <summary>
            /// Default. Not set
            /// </summary>
            public static KeyValuePair<int, string> NotSet { get; } = new KeyValuePair<int, string>(-1, "Not Set");

            /// <summary>
            /// Unknown gender
            /// </summary>
            public static KeyValuePair<int, string> NotKnown { get; } = new KeyValuePair<int, string>(0, "Not Known");

            /// <summary>
            /// Male gender
            /// </summary>
            public static KeyValuePair<int, string> Male { get; } = new KeyValuePair<int, string>(1, "Male");

            /// <summary>
            /// Femal Gender
            /// </summary>
            public static KeyValuePair<int, string> Female { get; } = new KeyValuePair<int, string>(2, "Female");

            /// <summary>
            /// Not applicable or do not want to specify
            /// </summary>
            public static KeyValuePair<int, string> NotApplicable { get; } = new KeyValuePair<int, string>(9, "Not Applicable");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Customer() : base()
        {
        }
    }
}
