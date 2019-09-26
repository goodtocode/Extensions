﻿//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerCoreTests.cs" company="GoodToCode">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Configuration;
using System.Collections.Specialized;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// ConfigurationManagerCore Tests
    /// </summary>
    [TestClass()]
    public partial class ApplicationSettingSafeTests
    {
        /// <summary>
        /// Connection strings in safe version of configuration manager
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ApplicationSettingSafe()
        {
            var itemToTest = new AppSettingSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);

            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
        }
    }
}