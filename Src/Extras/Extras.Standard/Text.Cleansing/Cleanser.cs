//-----------------------------------------------------------------------
// <copyright file="Cleanser.cs" company="GoodToCode">
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
using System.Reflection;
using GoodToCode.Extensions;

namespace GoodToCode.Extras.Text.Cleansing
{
    /// <summary>
    /// Text Cleanser interface
    /// </summary>
    
    public abstract class Cleanser
    {
        /// <summary>
        /// Id of the target of this cleanser
        /// </summary>
        public abstract CleanserIds CleanserId { get; }

        /// <summary>
        /// Item to cleanse
        /// </summary>
        public string TextToCleanse { get; set; }

        /// <summary>
        /// Result after cleanse
        /// </summary>
        public string TextCleansed { get; protected set; }

        /// <summary>
        /// Worker that cleanses the text
        /// </summary>
        /// <returns></returns>
        public abstract string Cleanse();

        /// <summary>
        /// Cleanses all properties marked with CleanseFor attribute
        /// </summary>
        /// <param name="classToCleanse"></param>
        public static void CleanseAll(object classToCleanse)
        {
            // Get properties with CleanseFor() attribute
            IEnumerable<PropertyInfo> props = classToCleanse.GetPropertiesByAttribute(typeof(CleanseFor));
            foreach (var item in props)
            {
                var ValueToSet = item.GetValue(classToCleanse, null).ToStringSafe();
                Cleanser cleanserWorker = CleanserFactory.Construct(item.GetAttributeValue<CleanseFor, CleanserIds>(CleanserIds.Default), ValueToSet);
                ValueToSet = cleanserWorker.Cleanse();
                item.SetValue(classToCleanse, ValueToSet);
            }
        }
    }
}
