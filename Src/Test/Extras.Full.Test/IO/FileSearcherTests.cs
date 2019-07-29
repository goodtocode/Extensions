//-----------------------------------------------------------------------
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
using GoodToCode.Extras.IO;

namespace GoodToCode.Extras.Test
{
    [TestClass()]
    public class FileSearcherTests
    {
        [TestMethod()]
        public void Full_IO_FileSearcher()
        {
            var pathsToSearch = new List<string>() { Directory.GetCurrentDirectory() };
            var maskToSearch = @"app.config";
            var searcher = new FileSearcher(pathsToSearch, maskToSearch, 2);
            searcher.Search();
            Assert.IsTrue(searcher.FoundFiles.Count() > 0);
        }
    }
}