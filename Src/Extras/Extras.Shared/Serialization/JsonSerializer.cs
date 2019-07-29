//-----------------------------------------------------------------------
// <copyright file="JsonSerializer.cs" company="GoodToCode">
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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using GoodToCode.Extensions;
using GoodToCode.Extras.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GoodToCode.Extras.Serialization
{
    /// <summary>
    /// JSON serialization and deserialization
    /// </summary>
    /// <remarks></remarks>
    
    public class JsonSerializer<T> : Serializer<T>
    {
        /// <summary>
        /// Gets or sets a DateTimeFormat that defines the culturally appropriate format of displaying dates and times.
        /// Default is ISO 8601 DateTime Format. Does not default to microsoft Date()
        /// </summary>
        public DateTimeFormat DateTimeFormatString { get; set; } = new DateTimeFormat(DateTimeExtension.Formats.ISO8601F) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };

        /// <summary>
        /// Gets or sets the data contract JSON serializer settings to emit type information.
        /// </summary>
        public EmitTypeInformation EmitTypeInformation { get; set; } = EmitTypeInformation.Never;

        /// <summary>
        /// Ignore types that are readonly?
        /// </summary>
        public bool SerializeReadOnly { get; set; } = Defaults.Boolean;

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer(T entityToSerialize) : base(entityToSerialize) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer(string entityToDeserialize) : base(entityToDeserialize) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonSerializer(IListSafe<Type> knownTypes) : base(knownTypes) { }

        /// <summary>
        /// Serializes and returns the JSON as a string
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public override string Serialize()
        {
            var returnValue = Defaults.String;
            DataContractJsonSerializer serializer;

            try
            {
                if (entityObject == null && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is null. Unable to serialize null objects."); }
                serializer = new DataContractJsonSerializer(entityObject.GetType(), new DataContractJsonSerializerSettings() { EmitTypeInformation = this.EmitTypeInformation, DateTimeFormat = DateTimeFormatString, KnownTypes = this.KnownTypes });
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, entityObject);
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        returnValue = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <returns>Concrete class</returns>
        public override T Deserialize()
        {
            T returnValue = TypeExtension.InvokeConstructorOrDefault<T>();
            object result = null;

            try
            {
                if (entityString == Defaults.String && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is empty. Unable to deserialize empty strings."); }
                foreach(var format in DateTimeExtension.FormatList)
                {
                    result = DeserializeWorker(format);
                    if (result != null)
                    {
                        returnValue = (T)result;
                        break;
                    }
                }
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <returns>NULL or Concrete class.</returns>
        private object DeserializeWorker(string format)
        {
            object returnValue = null;
            Byte[] bytes = null;
            DataContractJsonSerializer serializer;
            var formatStrong = new DateTimeFormat(format) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };
            var settings = new DataContractJsonSerializerSettings() { EmitTypeInformation = this.EmitTypeInformation, DateTimeFormat = formatStrong, KnownTypes = this.KnownTypes, SerializeReadOnlyTypes = this.SerializeReadOnly };            

            try
            {
                serializer = new DataContractJsonSerializer(typeof(T), settings);
                bytes = Encoding.Unicode.GetBytes(entityString);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    returnValue = serializer.ReadObject(stream);                    
                }
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }
    }
}
