using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class IntegerExtensionTests
    {
        [TestMethod()]
        public void Core_Integer_ToDecimal()
        {
            var testItem = 10;
            Assert.IsTrue(testItem.ToDecimal() == 10.00M);
        }

        [TestMethod()]
        public void Core_Integer_ToGuid()
        {
            var itemGuid = new Guid("00003039-0000-0000-0000-000000000000");
            var itemInt = 12345;

            Assert.IsTrue(itemGuid.ToInteger() == itemInt);
            Assert.IsTrue(itemInt.ToGuid() == itemGuid);
        }

        [TestMethod()]
        public void Core_Integer_Negate()
        {
            var testItem = 10;
            Assert.IsTrue(testItem.Negate() == (testItem * -1));
        }
    }
}