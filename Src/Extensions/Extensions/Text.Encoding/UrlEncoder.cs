//-----------------------------------------------------------------------
// <copyright file="UrlEncoder.cs" company="GoodToCode">
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
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Encoding
{
    /// <summary>
    /// Encoders and decodes Base64 text
    /// </summary>
    
    public class UrlEncoder : IEncoder
    {
        private string dataIn = Defaults.String;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataToProcess">Data to encrypt or decrypt</param>
        public UrlEncoder(string dataToProcess) : base()
        {
            dataIn = dataToProcess;
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <returns>Url Encoded escaped string</returns>
        public string Encode()
        {            
            return Uri.EscapeDataString(dataIn).Replace("+", "%20");
        }

        /// <summary>
        /// Encodes to Base64
        /// </summary>
        /// <param name="stringToEncode"></param>
        /// <returns></returns>
        public static string Encode(string stringToEncode)
        {
            var encoder = new UrlEncoder(stringToEncode);
            return encoder.Encode();
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <returns></returns>
        public string Decode()
        {
            return Uri.UnescapeDataString(dataIn).Replace("%20", "+");
        }

        /// <summary>
        /// Decodes from Base64
        /// </summary>
        /// <param name="stringToDecode"></param>sd
        /// <returns></returns>
        public static string Decode(string stringToDecode)
        {
            var encoder = new UrlEncoder(stringToDecode);
            return encoder.Decode();
        }
    }
}
