﻿//-----------------------------------------------------------------------
// <copyright file="FileSearcherTests.cs" company="GoodToCode">
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoodToCode.Extensions.IO;
using GoodToCode.Extensions;
using System;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class FileSearcherTests
    {
        /// <summary>
        /// Searcher worker
        /// </summary>
        [TestMethod()]
        public void Core_IO_FileSearcher()
        {
            var pathsToSearch = new List<string>() { Directory.GetCurrentDirectory() };
            var maskToSearch = @"*.*";
            var searcher = new FileSearcher(pathsToSearch, maskToSearch, 2);
            Assert.IsTrue(searcher.FoundFiles.Count() == 0);
            var files = searcher.Search();
            Assert.IsTrue(searcher.FoundFiles.Count() > 0);
            Assert.IsTrue(files.Count() > 0);
        }


        /// <summary>
        /// Read appsettings.{environment}.json
        /// </summary>
        [TestMethod()]
        public void Core_IO_FileSearcher_Configs()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var pathsToSearch = new List<string>() { Directory.GetCurrentDirectory() };
            var maskToSearch = $"appsettings.{env}.json";
            var searcher = new FileSearcher(pathsToSearch, maskToSearch, 3);
            Assert.IsTrue(searcher.FoundFiles.Count() == 0);
            var files = searcher.Search();
            Assert.IsTrue(searcher.FoundFiles.Count() > 0);
            Assert.IsTrue(files.Count() > 0);
        }
    }
}