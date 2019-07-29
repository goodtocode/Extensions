//-----------------------------------------------------------------------
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
using GoodToCode.Extensions;
using GoodToCode.Extras.Configuration;

namespace GoodToCode.Extras.Test
{
    [TestClass()]
    public partial class ConfigurationManagerCoreTests
    {
        /// <summary>
        /// Connection strings in safe version of configuration manager
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ConfigurationManagerCore_AppSettings()
        {
            // Specific config entries
            var itemToTestString = Defaults.String;
            var itemToTest = new AppSettingSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
            itemToTestString = configuration.AppSettings.GetValue("TestAppSetting");
            Assert.IsTrue(itemToTestString != Defaults.String);
        }

        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ConfigurationManagerCore_ConnectionStrings()
        {
            var itemToTest = new ConnectionStringSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);

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
            // Specific config entries
            var connectionToTest = new ConnectionStringSafe();
            connectionToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(connectionToTest.Value != Defaults.String);
            Assert.IsTrue(connectionToTest.IsEF);
            connectionToTest = configuration.ConnectionString("Example-SQLServer");
            Assert.IsTrue(connectionToTest.Value != Defaults.String);
            Assert.IsTrue(connectionToTest.IsADO);
        }
    }
}