//-----------------------------------------------------------------------
// <copyright file="KeyValueListTests.cs" company="GoodToCode">
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
using GoodToCode.Extras.Text;
using GoodToCode.Extensions;

namespace GoodToCode.Extras.Test
{
    /// <summary>
    /// KeyValueList Tests
    /// </summary>
    [TestClass()]
    public class KeyValueListTests
    {
        [TestMethod()]
        public void Core_Collections_KeyValueList_Construct()
        {
            var kvList = new KeyValueList<int, int>();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Core_Collections_KeyValueList_Add()
        {
            var kvList = new KeyValueList<int, int>();
            kvList.Add(new KeyValuePairSafe<int, int>(0,0));
            kvList.Add(new KeyValuePairSafe<int, int>(1,1));
            kvList.Add(new KeyValuePairSafe<int, int>(2,2));

            Assert.AreNotEqual(2, kvList.Count);
            Assert.AreEqual(3, kvList.Count);
        }

        [TestMethod()]
        public void Core_Collections_KeyValueList_Remove()
        {
            var kvList = new KeyValueList<int, double>();
            kvList.Add(1, 100.00);
            kvList.Add(2, 200.00);
            kvList.Remove(1);
            Assert.IsTrue(kvList.Count == 1);
        }        

        [TestMethod()]
        public void Core_Collections_KeyValueList_GetValue()
        {
            var kvList = new KeyValueList<int, double>();
            kvList.Add(1, 100.00);
            kvList.Add(2, 200.00);
            Assert.IsTrue(kvList.GetValue(2) == 200.00);
        }

        [TestMethod()]
        public void Core_Collections_KeyValueList_ToString()
        {
            var kvList = new KeyValueList<int, StringMutable>();
            var delimiterLength = 0;
            var lengthExpected = 0;
            kvList.Add(1, "Value1");
            kvList.Add(2, "Value2");
            kvList.Add(3, "Value3");
            foreach(var item in kvList)
            {
                lengthExpected += item.ToString().Length + delimiterLength;
                delimiterLength = 2;
            }
            Assert.IsTrue(kvList.ToString("G").Length == lengthExpected);
        }
    }
}