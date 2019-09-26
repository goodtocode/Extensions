//-----------------------------------------------------------------------
// <copyright file="TypeExtension.cs" company="GoodToCode">
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
using System.Linq;
using System.Reflection;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends System.Type
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Invokes the parameterless constructor. If no parameterless constructor, returns default()
        /// </summary>
        /// <typeparam name="T">Type to invoke</typeparam>
        public static T InvokeConstructorOrDefault<T>()
        {
            var returnValue = default(T);

            if (TypeExtension.HasParameterlessConstructor<T>())
            {
                returnValue = Activator.CreateInstance<T>();
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if type has a parameterless constructor
        /// </summary>
        /// <typeparam name="T">Type to interrogate for parameterless constructor</typeparam>
        /// <returns></returns>
        public static bool HasParameterlessConstructor<T>()
        {
            IEnumerable<ConstructorInfo> constructors = typeof(T).GetTypeInfo().DeclaredConstructors;
            return constructors.Where(x => x.GetParameters().Count() == 0).Any();
        }

        /// <summary>
        /// Determines if type has a parameterless constructor
        /// </summary>
        /// <param name="item">Type to interrogate for parameterless constructor</param>
        /// <returns></returns>
        public static bool HasParameterlessConstructor(this Type item)
        {
            IEnumerable<ConstructorInfo> constructors = item.GetTypeInfo().DeclaredConstructors;
            return constructors.Where(x => x.GetParameters().Count() == 0).Any();
        }
    }
}
