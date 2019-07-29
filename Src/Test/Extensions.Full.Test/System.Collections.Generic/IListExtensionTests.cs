﻿//-----------------------------------------------------------------------
// <copyright file="ListExtensionTests.cs" company="GoodToCode">
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
using System.Linq;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class IListExtensionTests
    {
        public IList<string> names1 = new List<string>() { "Burke", "Connor", "Frank", "Everett", "Albert", "George", "Harris", "David" };
        public IList<string> names2 = new List<string>() { "Joe", "James", "Jack" };

        public class ComplexObject
        {
            public string Name { get; set; }
        }
        public class ComplexList : List<ComplexObject>
        {
            public ComplexList() : base()
            {
                AddRange(new List<ComplexObject>() {
                    new ComplexObject() { Name = "Larry" }, new ComplexObject() { Name = "Curly" }, new ComplexObject() { Name = "Mo" }});
            }
        }

        [TestMethod()]
        public void Full_IList_AddRange()
        {
            var allNames = new List<string>();
            allNames.AddRange(names1);
            allNames.AddRange(names2);
            Assert.IsTrue(allNames.Count == (names1.Count + names2.Count));
        }

        [TestMethod()]
        public void Full_IList_FillRange()
        {
            var fullList = new ComplexList();
            var emptyList = new ComplexList();

            emptyList.Clear();
            Assert.IsTrue(emptyList.Count == 0);
            emptyList.FillRange(fullList);
            Assert.IsTrue(emptyList.Count == fullList.Count);

            emptyList.Clear();
            Assert.IsTrue(emptyList.Count == 0);
            emptyList.FillRange(fullList.Select(x => x));
            Assert.IsTrue(emptyList.Count == fullList.Count);
        }
    }
}