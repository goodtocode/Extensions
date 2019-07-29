//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensionTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
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
using System;

namespace Genesys.Extensions.Test
{   
    [TestClass()]
    public class DateTimeExtensionTests
    {
        [TestMethod()]
        public void Portable_DateTime_ISO8601_FormatStrings()
        {
            DateTime defaultDateValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            DateTime defaultShort = TypeExtension.DefaultDate;
            Assert.IsTrue(defaultDateValue.Ticks == defaultShort.Ticks);
            Assert.IsTrue(defaultDateValue.ToString() == defaultShort.ToString());

            string ISO8601 = defaultDateValue.ToString(DateTimeExtension.Formats.ISO8601);
            string ISO8601F = defaultDateValue.ToString(DateTimeExtension.Formats.ISO8601F);
            Assert.IsTrue(ISO8601.TryParseDateTime().Ticks == ISO8601F.TryParseDateTime().Ticks);
            Assert.IsTrue(ISO8601.TryParseDateTime().ToString() == ISO8601F.TryParseDateTime().ToString());
        }

        [TestMethod()]
        public void Portable_DateTime_Tomorrow()
        {
            var date = DateTime.Now;
            Assert.IsTrue(date.Tomorrow().Day == DateTime.Now.AddDays(1).Day);
        }

        [TestMethod()]
        public void Portable_DateTime_Yesterday()
        {
            var date = DateTime.Now;
            Assert.IsTrue(date.Yesterday().Day == DateTime.Now.AddDays(-1).Day);
        }

        [TestMethod()]
        public void Portable_DateTime_FirstDayOfMonth()
        {
            var date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.FirstDayOfMonth().Day == 1);
        }

        [TestMethod()]
        public void Portable_DateTime_LastDayOfMonth()
        {
            var date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.LastDayOfMonth().Day == 31);
        }

        [TestMethod()]
        public void Portable_DateTime_IsSavable()
        {
            var date = new DateTime(1700, 1, 1);
            Assert.IsTrue(date.IsSavable() == false);
        }
    }
}