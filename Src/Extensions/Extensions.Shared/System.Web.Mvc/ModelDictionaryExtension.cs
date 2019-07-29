//-----------------------------------------------------------------------
// <copyright file="ModelDictionaryExtension.cs" company="GoodToCode">
//      Copyright (c) 2017-2020 GoodToCode. All rights reserved.
// 
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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends the System.Web.Mvc.ModelStateDictionary class
    /// </summary>
    public static class ModelStateDictionaryExtension
    {
        /// <summary>
        /// Clears and add new list of model state dictionary errors
        /// </summary>
        /// <remarks></remarks>
        public static void AddModelError(this ModelStateDictionary item, IEnumerable<string> errors)
        {
            item.Clear();
            foreach (var Error in errors)
            {
                item.AddModelError("Error", Error);
            }
        }
    }
}
