//-----------------------------------------------------------------------
// <copyright file="HttpResponseExtension.cs" company="GoodToCode">
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
using Microsoft.AspNetCore.Http;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// HttpResponseExtension
    /// </summary>
    public static class HttpResponseExtension
    {
        /// <summary>
        /// Writes bytes to Html Binary Output Stream. 
        /// Mainly for sending Images/Blobs over http, typically from data access framework to a Html Page img tag.
        /// </summary>
        /// <param name="item">Item to write byte array</param>
        /// <param name="blobBytes">Byte array to write</param>
        public static void BinaryWriteSafe(this HttpResponse item, byte[] blobBytes)
        {
            if ((blobBytes == null == false) && (blobBytes.Length > 0))
            {
                item.Body.Write(blobBytes, 0, blobBytes.Length);
            }
        }
    }
}
