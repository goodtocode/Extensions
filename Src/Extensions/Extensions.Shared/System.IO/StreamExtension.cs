//-----------------------------------------------------------------------
// <copyright file="StreamExtension.cs" company="GoodToCode">
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
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extension methods to the System.IO.Stream class
    /// </summary>
    public static class StreamExtension
    {
        /// <summary>
        /// Converts a Stream to XDocument. I.e. Reading an XML file
        /// </summary>
        /// <param name="item">Stream array containing the XDocument data.</param>
        /// <returns>XDocument from a valid Stream, or empty XDocument</returns>
        public static XDocument ToXDocument(this Stream item)
        {
            var returnValue = new XDocument();

            try
            {
                if (item?.Length > 0)
                {
                    returnValue = XDocument.Load(item);
                }
            }
            catch (NullReferenceException)
            {
                returnValue = new XDocument();
            }

            return returnValue;
        }

        /// <summary>
        /// Validates a stream to be a image-like type and is less than a maximum size
        /// </summary>
        /// <param name="item">Stream array to check for image</param>
        /// <param name="maxSizeInKb">Default is 4 Mb</param>
        /// <returns>True if stream is image</returns>
        public static bool IsImage(this Stream item, int maxSizeInKb = 4096)
        {            
            return new Bitmap(item).ToBytes().IsImage(maxSizeInKb);
        }
    }
}
