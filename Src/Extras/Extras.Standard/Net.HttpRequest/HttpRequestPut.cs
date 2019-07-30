//-----------------------------------------------------------------------
// <copyright file="HttpRequestPut.cs" company="GoodToCode">
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
using System.Threading.Tasks;
using GoodToCode.Extensions;
using GoodToCode.Extras.Security.Cryptography;
using GoodToCode.Extras.Serialization;

namespace GoodToCode.Extras.Net
{
    /// <summary>
    /// Communicates via PUT, sending data via HttpContent
    /// </summary>
    /// <typeparam name="TDataIn">The type of data to be sent in request</typeparam>
    /// <typeparam name="TDataOut">The type of data which will be received in response</typeparam>
    
    public class HttpRequestPut<TDataIn, TDataOut> : HttpRequestPutString
    {
        /// <summary>
        /// Data to send, initialized with default()
        /// </summary>
        protected TDataIn dataToSendStrong = TypeExtension.InvokeConstructorOrDefault<TDataIn>();

        /// <summary>
        /// Data to send, initialized with default()
        /// </summary>
        protected TDataOut dataReceivedStrong = TypeExtension.InvokeConstructorOrDefault<TDataOut>();

        /// <summary>
        /// Serializes data going to the endpoint
        /// </summary>
        public ISerializer<TDataIn> Serializer { get; protected set; } = new JsonSerializer<TDataIn>();

        /// <summary>
        /// Serializes data going to the endpoint
        /// </summary>
        public ISerializer<TDataOut> Deserializer { get; protected set; } = new JsonSerializer<TDataOut>();

        /// <summary>
        /// KnownTypes assist the serializer with types that cannot be mapped by default
        /// </summary>
        public List<Type> KnownTypes { get; protected set; } = new List<Type>();

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url, TDataIn dataToSend) : base(url) { dataToSendStrong = dataToSend; base.DataToSend = this.Serializer.Serialize(dataToSend); }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url, TDataIn dataToSend, IEncryptor encryptor) : this(url, dataToSend) { Encryptor = encryptor; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url, TDataIn dataToSend, List<Type> knownTypes) : this(url, dataToSend) { KnownTypes = knownTypes; }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public new TDataOut Send()
        {
            base.DataReceivedDecrypted = base.Send();
            dataReceivedStrong = Deserializer.Deserialize(base.DataReceivedDecrypted);
            return dataReceivedStrong;
        }

        /// <summary>
        /// Instantiates and transmits all data to the middle tier web service that will execute the workflow
        /// </summary>
        /// <returns></returns>
        public new async Task<TDataOut> SendAsync()
        {
            base.DataReceivedDecrypted = await base.SendAsync();
            dataReceivedStrong = Deserializer.Deserialize(base.DataReceivedDecrypted);
            return dataReceivedStrong;
        }
    }

    /// <summary>
    /// Communicates via PUT, sending data via HttpContent
    /// </summary>
    /// <typeparam name="TDataInOut">The type of data to be sent in request and which will be received in response</typeparam>
    
    public class HttpRequestPut<TDataInOut> : HttpRequestPut<TDataInOut, TDataInOut>
    {
        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url) : base(url) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(string url) : base(new Uri(url, UriKind.RelativeOrAbsolute)) { }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url, TDataInOut dataToSend) : base(url) { dataToSendStrong = dataToSend; base.DataToSend = this.Serializer.Serialize(dataToSend); }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url, TDataInOut dataToSend, IEncryptor encryptor) : this(url, dataToSend) { Encryptor = encryptor; }

        /// <summary>
        /// Construct with data
        /// </summary>
        public HttpRequestPut(Uri url, TDataInOut dataToSend, List<Type> knownTypes) : this(url, dataToSend) { KnownTypes = knownTypes; }
    }
}
