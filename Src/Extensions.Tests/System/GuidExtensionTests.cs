using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class GuidExtensionTests
    {
        [TestMethod()]
        public void Core_Guid_ToInteger()
        {
            var itemGuid = new Guid("00003039-0000-0000-0000-000000000000");
            var itemInt = 12345;

            Assert.IsTrue(itemGuid.ToInteger() == itemInt);
            Assert.IsTrue(itemInt.ToGuid() == itemGuid);
        }
    }
}