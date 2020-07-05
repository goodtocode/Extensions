using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class DecimalExtensionTests
    {
        [TestMethod()]
        public void Core_Decimal_ToDouble()
        {
            decimal original = 10.00M;
            double castedValue = 0.0;
            castedValue = original.ToDouble();
            Assert.IsTrue(castedValue == (double)original);
        }

        [TestMethod()]
        public void Core_Decimal_ToInt()
        {
            decimal original = 10.00M;
            var castedValue = -1;
            castedValue = original.ToInt();
            Assert.IsTrue(castedValue == (int)original);
        }

        [TestMethod()]
        public void Core_Decimal_ToShort()
        {
            decimal original = 10.00M;
            short castedValue = -1;
            castedValue = original.ToShort();
            Assert.IsTrue(castedValue == (short)original);
        }

        [TestMethod()]
        public void Core_Decimal_ToLong()
        {
            decimal original = 10.00M;
            long castedValue = -1;
            castedValue = original.ToLong();
            Assert.IsTrue(castedValue == (long)original);
        }
    }
}