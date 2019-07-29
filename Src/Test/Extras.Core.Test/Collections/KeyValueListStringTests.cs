//-----------------------------------------------------------------------
// <copyright file="KeyValueListStringTests.cs" company="GoodToCode">
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
using GoodToCode.Extras.Collections;
using GoodToCode.Extensions;

namespace GoodToCode.Extras.Test
{
    /// <summary>
    /// KeyValueListString Tests
    /// </summary>
    [TestClass()]
    public class KeyValueListStringTests
    {
        [TestMethod()]
        public void Core_Collections_KeyValueListString_Construct()
        {
            var kvList = new KeyValueListString();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Core_Collections_KeyValueListString_Add()
        {
            var kvString = new KeyValueListString();
            kvString.Add("TestKey", "TestValue");
            Assert.AreEqual(1, kvString.Count);
            kvString.Add("TestKey", "TestValue2");
            Assert.AreNotEqual(2, kvString.Count);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_Remove()
        {
            var kvList = new KeyValueListString();
            kvList.Add("Key1", "Value1");
            kvList.Add("Key2", "Value2");
            kvList.Remove("Key1");
            Assert.IsTrue(kvList.Count == 1);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_GetValue()
        {
            var kvList = new KeyValueListString();
            kvList.Add("Key1", "Value1");
            kvList.Add("Key2", "Value2");
            Assert.IsTrue(kvList.GetValue("Key1") != Defaults.String);
        }
    }
}   