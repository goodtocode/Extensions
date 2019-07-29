﻿//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafeTests.cs" company="GoodToCode">
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
using GoodToCode.Extras.Configuration;
using System.Collections.Specialized;

namespace GoodToCode.Extras.Test
{
    /// <summary>
    /// ConfigurationManagerSafe Tests
    /// </summary>
    [TestClass()]
    public partial class ConnectionStringSafeTests
    {
        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Full_Configuration_ConnectionStringSafe()
        {
            var itemToTest = new ConnectionStringSafe();
            var configuration = ConfigurationManagerSafeTests.ConfigurationManagerSafeConstruct();

            // ADO
            itemToTest = configuration.ConnectionString("Example-SQLServer");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
            itemToTest.EDMXFileName = "TestEDMXFile";
            Assert.IsTrue(itemToTest.ToString("EF") != Defaults.String);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != Defaults.String);
            // EF
            itemToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
            Assert.IsTrue(itemToTest.ToString("ADO") != Defaults.String);
            Assert.IsTrue(itemToTest.ToADO() != Defaults.String);
        }
        
        /// <summary>
        /// Universal cant access ConfigurationManager directly. 
        ///  This method uses the ConfigurationManager to get data, then returns as a cross-platform friendly array
        /// </summary>
        /// <returns></returns>
        public static string[,] ConnectionStringsGet()
        {
            var itemToConvert = ConfigurationManager.ConnectionStrings ?? new ConnectionStringSettingsCollection();
            string[,] returnValue = new string[itemToConvert.Count, 2];

            for (var count = 0; count < itemToConvert.Count; count++)
            {
                returnValue[count, 0] = itemToConvert[count].Name;
                returnValue[count, 1] = itemToConvert[count].ConnectionString;
            }

            return returnValue;
        }
    }
}