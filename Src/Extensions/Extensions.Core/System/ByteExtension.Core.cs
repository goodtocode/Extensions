//-----------------------------------------------------------------------
// <copyright file="ByteExtension.Core.cs" company="GoodToCode">
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
using Microsoft.AspNetCore.Mvc;
using System;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extensions to byte class
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// Converts a byte Array to FileContentResult.
        /// Typically to show an image returned from MVC controller in an Html Img tag.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="contentType"></param>
        /// <returns>Content (typically images) to display from MVC controller.</returns>
        public static FileContentResult ToFileContentResult(this Byte[] item, string contentType)
        {
            return new FileContentResult(item, contentType);
        }
    }
}
