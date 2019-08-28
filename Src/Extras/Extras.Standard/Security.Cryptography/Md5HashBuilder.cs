﻿//-----------------------------------------------------------------------
// <copyright file="MD5HashBuilderFull.cs" company="GoodToCode">
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
using System.Security.Cryptography;
using System.Text;
using GoodToCode.Extensions;

namespace GoodToCode.Extras.Security.Cryptography
{
    /// <summary>
    /// Builds a hashed string from raw text
    /// </summary>
    
    public class Md5HashBuilder
    {
        private string salt = "0873F24C-FA01-4811-AB36-2F079D4CA0D9";
        /// <summary>
        /// HashedString
        /// </summary>
        public string HashedString { get; protected set; } = Defaults.String;

        /// <summary>
        /// Force immutability
        /// </summary>
        private Md5HashBuilder() : base() {}

        /// <summary>
        /// Force immutability
        /// </summary>
        private Md5HashBuilder(string salt) : this() { if (salt != Defaults.String) this.salt = salt; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stringToHash">string to hash</param>
        /// <param name="salt">Salted value to add to hashed string</param>
        public Md5HashBuilder(string stringToHash, string salt = "")
            : this(salt)
        {
            HashedString = this.HashCreate(stringToHash);
        }

        /// <summary>
        /// Hashes a String
        /// </summary>
        /// <param name="stringToHash">string to hash</param>
        /// <returns>Hashed string data</returns>
        private string HashCreate(string stringToHash)
        {
            var returnValue = Defaults.String;
            var hashValue = new StringBuilder();

            try
            {
                stringToHash += this.salt;
                using (MD5 MD55Hash = MD5.Create())
                {
                var Data = MD55Hash.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                    for (var Count = 0; Count <= Data.Length - 1; Count++)
                    {
                        hashValue.Append(Data[Count].ToString("x2"));
                    }
                }
                returnValue = hashValue.ToString();
            }
            catch
            {
                returnValue = Defaults.String;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Hashes and compares a string
        /// </summary>
        /// <param name="rawString">string to hash compare</param>
        /// <returns>True if string + salt hashed matches current hash string</returns>
        public bool Compare(string rawString)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var returnValue = Defaults.Boolean;
            var rawStringHash = Defaults.String;

            rawStringHash = this.HashCreate(rawString);
            if (0 == comparer.Compare(rawStringHash, this.HashedString))
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}