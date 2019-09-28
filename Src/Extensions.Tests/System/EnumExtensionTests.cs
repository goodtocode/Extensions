using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions;
using System;

namespace GoodToCode.Extensions.Test
{
    [TestClass()] 
    public class EnumExtensionTests
    {
        [TestMethod()]
        public void Core_Enum_Contains()
        {
            var consumer = new EnumConsumer();
            Assert.IsTrue(consumer.enumFlag.Contains(0x01));
        }

        [TestMethod()]
        public void Core_Enum_ToDictionary()
        {
            var dict = EnumConsumer.MyEnumInts.one.ToDictionary();
            Assert.IsTrue(dict.Count > 0);
        }
        public class EnumConsumer
        {
            public enum MyEnumInts
            {
                one = 1,
                two = 2,
                three = 3
            }

            [Flags]
            public enum MyEnumFlags
            {
                one = 0x01,
                two = 0x02,
                four = 0x04,
                eight = 0x08
            }

            public MyEnumInts enumInt = MyEnumInts.one;
            public MyEnumFlags enumFlag = MyEnumFlags.one;
        }

    }
}