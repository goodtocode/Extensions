﻿//-----------------------------------------------------------------------
// <copyright file="Md5HashBuilderTests.cs" company="GoodToCode">
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
using GoodToCode.Extras.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extras.Test
{
    [TestClass()]
    public class MD5HashBuilderTests
    {
        [TestMethod()]
        public void Core_Security_Cryptography_MD5HashBuilder_Compare()
        {
            var rawData = "Hello, I am raw";
            var hasher = new Md5HashBuilder(rawData);
            var hashed = hasher.HashedString;
            Assert.IsTrue(rawData != hashed);
            Assert.IsTrue(hasher.Compare(rawData));
        }
    }
}