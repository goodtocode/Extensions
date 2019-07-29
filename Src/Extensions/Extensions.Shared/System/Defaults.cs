//-----------------------------------------------------------------------
// <copyright file="TypeExtension.cs" company="GoodToCode">
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

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends System.Type
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// () for int
        /// </summary>
        public static readonly int Integer = -1;

        /// <summary>
        /// () for Int16
        /// </summary>
        public static readonly short Int16 = -1;

        /// <summary>
        /// () for int
        /// </summary>
        public static readonly int Int32 = -1;

        /// <summary>
        /// () for Int64
        /// </summary>
        public static readonly long Int64 = -1;

        /// <summary>
        /// () for Int16
        /// </summary>
        public static readonly short Short = -1;

        /// <summary>
        /// () for unsigned int
        /// </summary>
        public static readonly int UInteger = 0;

        /// <summary>
        /// () for Int64
        /// </summary>
        public static readonly long Long = -1;

        /// <summary>
        /// () for Guid
        /// </summary>
        public static readonly Guid Guid = Guid.Empty;

        /// <summary>
        /// () for Decimal
        /// </summary>
        public static readonly decimal Decimal = 0m;

        /// <summary>
        /// () for Double
        /// </summary>
        public static readonly double Double = 0.0;

        /// <summary>
        /// () for Single
        /// </summary>
        public static readonly float Single = 0.0f;

        /// <summary>
        /// () for String
        /// </summary>
        public static readonly string String = String.Empty;

        /// <summary>
        /// () for Boolean
        /// </summary>
        public static readonly bool Boolean = false;

        /// <summary>
        /// () for Char
        /// </summary>
        public static readonly char Char = Char.MinValue;

        /// <summary>
        /// () for DateTime
        /// </summary>
        public static readonly DateTime Date = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);

        /// <summary>
        /// Generate new date equal to DateTime.UtcNow 
        /// </summary>
        public static DateTime DateNew() { return DateTime.UtcNow; }

        /// <summary>
        /// () for string Uri
        /// </summary>        
        public static readonly Uri Uri = new Uri("http://localhost:80", UriKind.RelativeOrAbsolute);

        /// <summary>
        ///  byte array - 0x0
        /// </summary>
        public static readonly byte Byte = 0;

        /// <summary>
        ///  byte array - 0x0
        /// </summary>
        public static readonly byte[] Bytes = new byte[] { 0, 0, 0, 1 };

        /// <summary>
        ///  hex - 0x0
        /// </summary>
        public static readonly string Hex = "0x0";
    }
}
