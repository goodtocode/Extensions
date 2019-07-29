//-----------------------------------------------------------------------
// <copyright file="DecimalExtensionTests.cs" company="GoodToCode">
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

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class DecimalExtensionTests
    {
        [TestMethod()]
        public void Full_Decimal_ToDouble()
        {
            decimal original = 10.00M;
            double castedValue = Defaults.Double;
            castedValue = original.ToDouble();
            Assert.IsTrue(castedValue == (double)original);
        }

        [TestMethod()]
        public void Full_Decimal_ToInt()
        {
            decimal original = 10.00M;
            var castedValue = Defaults.Integer;
            castedValue = original.ToInt();
            Assert.IsTrue(castedValue == (int)original);
        }

        [TestMethod()]
        public void Full_Decimal_ToShort()
        {
            decimal original = 10.00M;
            short castedValue = Defaults.Short;
            castedValue = original.ToShort();
            Assert.IsTrue(castedValue == (short)original);
        }

        [TestMethod()]
        public void Full_Decimal_ToLong()
        {
            decimal original = 10.00M;
            long castedValue = Defaults.Long;
            castedValue = original.ToLong();
            Assert.IsTrue(castedValue == (long)original);
        }
    }
}