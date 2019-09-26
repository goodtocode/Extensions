//-----------------------------------------------------------------------
// <copyright file="HttpRequestGetString.cs" company="GoodToCode">
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
using System.Threading.Tasks;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Security.Cryptography;
using System.Net.Http;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Communicates via GET, all transmissions String
    /// </summary>
    
    public class HttpRequestGetString : HttpRequestClient
    {
        /// <summary>
        /// Immutable
        /// </summary>
        protected internal HttpRequestGetString() : base() { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGetString(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGetString(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestGetString(Uri url, IEncryptor encrptor) : base(url, encrptor) { }

        /// <summary>
        /// Synchronously sends a GET request, Receives string response
        /// Warning: Not thread safe, particularly in Web-based UIs. This is a stopgap to allow legacy code to operate with blocking/deadlock risk.
        /// </summary>
        /// <returns>Result</returns>
        public override string Send()
        {
            var client = Task.Run<HttpResponseMessage>(async () => await Client.GetAsync(this.Url, this.CompletionOption));
            var Response = client.Result;

            if (this.Response.IsSuccessStatusCode)
            {
                var responseString = Task.Run<string>(async () => await Response.Content.ReadAsStringAsync());
                DataReceivedRaw = responseString.Result;
                DataReceivedRaw = DataReceivedRaw;
                if (ThrowExceptionWithEmptyReponse == true && DataReceivedRaw == Defaults.String)
                { throw new System.DataMisalignedException("Response is empty. Expected data to be returned."); } else if (SendPlainText == false)
                { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } else { DataReceivedDecrypted = DataReceivedRaw; }
            }
            return DataReceivedDecrypted;
        }

        /// <summary>
        /// Asynchronously sends a GET request, Receives strongly typed response
        /// </summary>
        /// <returns>Response data</returns>
        public override async Task<string> SendAsync()
        {
            Response = await this.Client.GetAsync(this.Url, this.CompletionOption);
            if (this.Response.IsSuccessStatusCode)
            {
                DataReceivedRaw = await this.Response.Content.ReadAsStringAsync();
                if (ThrowExceptionWithEmptyReponse == true && DataReceivedRaw == Defaults.String)
                { throw new System.DataMisalignedException("Response is empty. Expected data to be returned."); } 
                else if (SendPlainText == false)
                    { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } 
                else { DataReceivedDecrypted = DataReceivedRaw; }
            }
            return DataReceivedDecrypted;
        }
    }
}
