//-----------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="GoodToCode">
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
using GoodToCode.Extras.Collections;
using System;

namespace GoodToCode.Extras.Serialization
{
    /// <summary>
    /// Strongly typed serializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISerializer<T>
    {
        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        IListSafe<Type> KnownTypes { get; set; }

        /// <summary>
        /// Setting to throw exception
        /// </summary>
        bool ThrowException { get; set; }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        string Serialize(T objectToSerialize);

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="stringToDeserialize"></param>
        /// <returns></returns>
        T Deserialize(string stringToDeserialize);
    }
}
