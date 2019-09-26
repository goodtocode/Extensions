//-----------------------------------------------------------------------
// <copyright file="HttpRequestPutString.cs" company="GoodToCode">
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
using System.Net.Http;
using System.Threading.Tasks;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Security.Cryptography;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Communicates via PUT, string in and string out
    /// </summary>
    
    public class HttpRequestPutString : HttpRequestClient
    {
        /// <summary>
        /// DataToSend
        /// </summary>
        public string DataToSend { get; set; } = Defaults.String;
        /// <summary>
        /// Mime content type of data to send
        /// </summary>
        public string ContentType { get; set; } = MimeTypes.Json;

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(Uri url, string dataToSend) : this(url) { DataToSend = dataToSend; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPutString(Uri url, string dataToSend, IEncryptor encrptor) : this(url, dataToSend) { Encryptor = Encryptor; }

        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.Send()
        /// Warning: Not thread safe, particularly in Web-based UIs. This is a stopgap to allow legacy code to operate with blocking/deadlock risk.
        /// </summary>
        /// <returns></returns>
        public override string Send()
        {
            var returnValue = Defaults.String;
            var client = new HttpClientBuilder();
            var data = new StringContent(DataToSend, System.Text.Encoding.UTF8, this.ContentType);

            var response = Task.Run<HttpResponseMessage>(async () => await client.PutAsync(this.Url, data));
            Response = response.Result;
            if (this.Response.IsSuccessStatusCode)
            {
                var responseString = Task.Run<string>(async () => await Response.Content.ReadAsStringAsync());
                DataReceivedRaw = responseString.Result;
                if (SendPlainText == false)
                { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } else { DataReceivedDecrypted = DataReceivedRaw; }
            }

            return DataReceivedDecrypted;
        }

        /// <summary>
        /// Sends a GET request, Receives string response
        ///     Overrides HttpRequestBase.SendAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task<string> SendAsync()
        {
            var returnValue = Defaults.String;
            var client = new HttpClientBuilder();
            var data = new StringContent(DataToSend, System.Text.Encoding.UTF8, this.ContentType);

            Response = await client.PutAsync(this.Url, data);
            if (this.Response.IsSuccessStatusCode)
            {
                DataReceivedRaw = await this.Response.Content.ReadAsStringAsync();
                if (SendPlainText == false)
                { DataReceivedDecrypted = this.Encryptor.Decrypt(DataReceivedRaw); } else { DataReceivedDecrypted = DataReceivedRaw; }
            }

            return DataReceivedDecrypted;
        }
    }
}
