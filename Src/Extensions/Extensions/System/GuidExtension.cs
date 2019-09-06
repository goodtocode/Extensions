//-----------------------------------------------------------------------
// <copyright file="GuidExtension.cs" company="GoodToCode">
//      Copyright (c) 2017-2020 GoodToCode. All rights reserved.
// 
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
    /// DoubleExtension
    /// </summary>    
    public static class  GuidExtension
    {
        /// <summary>
        /// Parses without exceptions
        /// </summary>
        /// <param name="item">Integer to convert to guid.</param>
        /// <returns>Converted value, or default -1.</returns>
        public static int ToInteger(this Guid item)
        {
            var returnValue = Defaults.Integer;

            try
            {
            var b = item.ToByteArray();
                var bint = BitConverter.ToInt32(b, 0);
                returnValue = bint;
            }
            catch
            {
                returnValue = Defaults.Integer;
            }
            return returnValue;
        }

        /// <summary>
        /// Replaces Default.{Type} with String.Empty with a readable value
        /// Values to replace are: 01/01/1900, -1, 0.00, 00000000-0000-0000-0000-000000000000
        /// I.e. Replace -1 with "". Replace 0.00 with "Free"
        /// </summary>
        /// <param name="item">Original value to replace</param>
        /// <param name="replaceWith">Value to replace default values with</param>
        /// <returns></returns>
        public static string ToReadable(this Guid item, string replaceWith = "")
        {
            var returnValue = item.ToString();
            returnValue = item == Defaults.Guid ? replaceWith : returnValue;
            return returnValue;
        }
    }
}
