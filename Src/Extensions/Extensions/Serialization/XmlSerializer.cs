//-----------------------------------------------------------------------
// <copyright file="XmlSerializerFull.cs" company="GoodToCode">
//      Copyright (c) GoodToCode. All rights reserved.
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
using System.Xml;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Collections;

namespace GoodToCode.Extensions.Serialization
{
    /// <summary>
    /// XML serialization and deserialization
    /// </summary>    
    public class XmlSerializer<T> : Serializer<T> where T : new()
    {
        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        public new IListSafe<Type> KnownTypes { get; set; } = new ListSafe<Type>();

        /// <summary>
        /// Constructor
        /// </summary>
        public XmlSerializer()
            : base()
        {
        }

        /// <summary>
        /// Serializes and returns the JSON as a string
        /// </summary>
        /// <returns>string serialized with passed object</returns>
        public override string Serialize()
        {
            var returnValue = Defaults.String;
            var stream = new MemoryStream();

            try
            {
                if (entityObject == null && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is null. Unable to serialize null objects."); }
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(entityObject.GetType());
                var textWriter = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
                System.Text.UTF8Encoding Encoding = new System.Text.UTF8Encoding();
                xs.Serialize(stream, entityObject);
                stream = (MemoryStream)textWriter.BaseStream;
                returnValue = Encoding.GetString(stream.ToArray());
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

            try
            {
                if (entityString == Defaults.String && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is empty. Unable to deserialize empty strings."); }
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                var byteArray = (byte[])encoding.GetBytes(entityString);
                var memoryStream = new MemoryStream(byteArray);
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                returnValue = (T)xs.Deserialize(memoryStream);
            }
            catch (System.InvalidOperationException)
            {
                returnValue = (T)Activator.CreateInstance(typeof(T));
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }
    }
}
