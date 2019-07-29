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
    public partial class ConfigurationManagerSafeTests
    {
        /// <summary>
        /// Connection strings in safe version of configuration manager
        /// </summary>
        [TestMethod()]
        public void Full_Configuration_ConfigurationManagerSafe_AppSettings()
        {
            var itemToTest = new AppSettingSafe();
            var configuration = ConfigurationManagerSafeTests.ConfigurationManagerSafeConstruct();

            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
        }

        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Full_Configuration_ConfigurationManagerSafe_ConnectionStrings()
        {
            var itemToTest = new ConnectionStringSafe();
            var configuration = ConfigurationManagerSafeTests.ConfigurationManagerSafeConstruct();

            // Validity
            itemToTest.Value = "Invalid Connection String!!!";
            Assert.IsTrue(itemToTest.IsValid == false);
            // ADO
            itemToTest = configuration.ConnectionString("Example-SQLServer");
            Assert.IsTrue(itemToTest.ToString("EF") != Defaults.String);
            Assert.IsTrue(itemToTest.IsADO);
            Assert.IsTrue(itemToTest.IsEF == false);
            Assert.IsTrue(itemToTest.IsValid);
            Assert.IsTrue(itemToTest.ConnectionStringType == ConnectionStringSafe.ConnectionStringTypes.ADO);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != Defaults.String);
            // EF
            itemToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(itemToTest.ToString("ADO") != Defaults.String);
            Assert.IsTrue(itemToTest.IsEF);
            Assert.IsTrue(itemToTest.IsADO == false);
            Assert.IsTrue(itemToTest.IsValid);
            Assert.IsTrue(itemToTest.ConnectionStringType == ConnectionStringSafe.ConnectionStringTypes.EF);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != Defaults.String);
        }

        /// <summary>
        /// Universal cant access ConfigurationManager directly. 
        ///  This method uses the ConfigurationManager to get data, then returns as a cross-platform friendly array
        /// </summary>
        /// <returns></returns>
        public static string[,] AppSettingsGet()
        {
            var itemToConvert = ConfigurationManager.AppSettings ?? new NameValueCollection();
            string[,] returnValue = new string[itemToConvert.Count, 2];

            for (var count = 0; count < itemToConvert.Count; count++)
            {
                returnValue[count, 0] = itemToConvert.Keys[count];
                returnValue[count, 1] = itemToConvert[count];
            }

            return returnValue;
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

        /// <summary>
        /// Constructs a current instance of .config AppSettings and ConnectionStrings nodes
        /// Universal/Core does not support ConfigurationManager, so have to construct using Universal friendly means
        /// </summary>
        /// <returns></returns>
        public static ConfigurationManagerSafe ConfigurationManagerSafeConstruct()
        {
            return new ConfigurationManagerSafe(ConfigurationManagerSafeTests.AppSettingsGet(), ConfigurationManagerSafeTests.ConnectionStringsGet());
        }

    }
}