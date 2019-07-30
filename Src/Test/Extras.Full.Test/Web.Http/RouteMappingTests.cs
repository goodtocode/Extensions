﻿//-----------------------------------------------------------------------
// <copyright file="RouteMappingTests.cs" company="GoodToCode">
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
using GoodToCode.Extras.Web.Http;

namespace GoodToCode.Extras.Test
{
    [TestClass()]
    public class RouteMappingTests
    {
        [TestMethod()]
        public void Full_Web_Http_RouteMapping()
        {
            var custom = new RouteMapping("CustomRoute" ,"/CustomPath", new { controller = "Custom", action = "ActionMethod" });
            Assert.IsTrue(custom.ToString() != string.Empty);
        }
    }
}