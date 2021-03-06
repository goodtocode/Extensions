﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Mathematics;
using System.Collections.Generic;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// Arithmetic Tests
    /// </summary>
    [TestClass()]
    public class ArithmeticTests
    {
        [TestMethod()]
        public void Core_Mathematics_Arithmetic_Addition()
        {
            Assert.IsTrue(Arithmetic.Addition(5, 6) == 11);
        }

        [TestMethod()]
        public void Core_Mathematics_Arithmetic_Subtraction()
        {
            Assert.IsTrue(Arithmetic.Subtraction(6, 6) == 0);
        }

        [TestMethod()]
        public void Core_Mathematics_Arithmetic_Multiply()
        {
            Assert.IsTrue(Arithmetic.Multiply(5, 6) == 30);
        }

        [TestMethod()]
        public void Core_Mathematics_Arithmetic_Divide()
        {
            Assert.IsTrue(Arithmetic.Divide(5, 5) == 1);
        }
        
        [TestMethod()]
        public void Core_Mathematics_Arithmetic_AverageDecimal()
        {
            List<decimal> data = new List<decimal>() { 1, 1, 3, 3 };
            Assert.IsTrue(Arithmetic.AverageDecimal(data) == 2);
        }

        [TestMethod()]
        public void Core_Mathematics_Arithmetic_ROI()
        {
            // ToDo: Assert.Fail();
        }

        /// <summary>
        /// Ensures Arithmentic.Random method behaves per spec
        /// </summary>
        [TestMethod()]
        public void Core_Mathematics_Arithmetic_Random()
        {
            // Should be semi unique
            var randoms = new List<int>();
            for(var count = 0; count < 30;  count++)
            {
                var random = Arithmetic.Random();
                randoms.Add(random);
            }
            var doubleCheck = Arithmetic.Random();
            Assert.IsTrue(randoms.Contains(doubleCheck) == false);

            // Should be able to be defined by length, for pin codes, etc.
            for (var count = 1; count < 11; count++)
            {
                var randomResult = (long)Arithmetic.Random(count);
                var length = randomResult.ToString().Length;
                Assert.IsTrue(length == count, "Did not work.");
            }            
        }        
    }
}