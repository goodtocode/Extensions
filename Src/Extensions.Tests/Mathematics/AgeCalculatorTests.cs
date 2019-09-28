using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoodToCode.Extensions.Mathematics;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// Age Tests
    /// </summary>
    [TestClass()]
    public class AgeTests
    {
        [TestMethod()]
        public void Core_Mathematics_Age()
        {
            var now = DateTime.UtcNow;
            var birthDate = new DateTime(1988, 05, 18);
            var age = new Age(birthDate);
            var diff = now.Subtract(birthDate);
            Assert.IsTrue(age.Days == diff.Days);
        }
    }
}