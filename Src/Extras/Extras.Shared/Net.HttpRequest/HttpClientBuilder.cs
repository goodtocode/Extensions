//-----------------------------------------------------------------------
// <copyright file="HttpClientBuilder.cs" company="GoodToCode">
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
using System.Collections.Generic;
using System.Net.Http;

namespace GoodToCode.Extras.Net
{
    /// <summary>
    /// Builds an HttpClient object
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    
    public class HttpClientBuilder : HttpClient
    {
        /// <summary>
        /// MaxResponseContentBufferSize
        /// </summary>
        public static long MaxResponseContentBufferSizeDefault { get; } = 2560000;

        /// <summary>
        /// Default user agent string
        /// </summary>
        public static KeyValuePair<string, string> UserAgentDefault { get; } = new KeyValuePair<string, string>("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        /// <summary>
        /// Constructor parameterless
        /// </summary>
        public HttpClientBuilder()
            : this(MaxResponseContentBufferSizeDefault, UserAgentDefault)
        {            
        }

        /// <summary>
        /// Builds a HttpClient with specific content buffer size and request header
        /// </summary>
        public HttpClientBuilder(Int64 maxResponseContentBufferSize, KeyValuePair<string, string> additionalHeader)
            : base()
        {
            base.MaxResponseContentBufferSize = maxResponseContentBufferSize;
            base.DefaultRequestHeaders.Add(additionalHeader.Key, additionalHeader.Value);
        }

        /// <summary>
        /// Builds a HttpClient with specific content buffer size and request header
        /// </summary>
        public HttpClientBuilder(Int64 maxResponseContentBufferSize, IEnumerable<KeyValuePair<string, string>> additionalHeaders)
            : base()
        {
            base.MaxResponseContentBufferSize = maxResponseContentBufferSize;
            foreach(var item in additionalHeaders)
            {
                base.DefaultRequestHeaders.Add(item.Key, item.Value);
            }            
        }
    }
}
