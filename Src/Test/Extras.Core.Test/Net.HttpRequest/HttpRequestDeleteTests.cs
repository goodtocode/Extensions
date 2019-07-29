﻿//-----------------------------------------------------------------------
// <copyright file="HttpRequestDeleteTests.cs" company="GoodToCode">
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
using GoodToCode.Extensions;
using GoodToCode.Extras.Configuration;
using GoodToCode.Extras.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GoodToCode.Extras.Test
{
    [TestClass()]
    public class HttpRequestDeleteTests
    {
        [TestMethod()]
        public async Task Core_Net_HttpRequestDelete_SendAsync()
        {
            var dataOut = Defaults.String;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var request = new HttpRequestDelete(configuration.AppSettingValue(AppSettingList.MyWebServiceKeyName) + "/HomeApi");
            try
            {
                dataOut = await request.SendAsync();
                Assert.IsTrue(request.Response.IsSuccessStatusCode);
                throw new WebException();
            }
            catch (WebException)
            {
                Assert.IsTrue(dataOut != null);
            }
            finally
            {
                request.Dispose();
            }
        }        
    }
}