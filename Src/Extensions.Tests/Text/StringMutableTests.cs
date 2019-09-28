using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Text;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class StringMutableTests
    {
        [TestMethod()]
        public void Core_Text_StringMutable()
        {
            // Common
            var data = "test string";
            // Creation
            StringMutable item1 = data;
            Assert.IsTrue(item1 == data, "Failed. Should have same data.");
            // Identity
            StringMutable item2 = item1;
            StringMutable item3 = new StringMutable() { Value = data };
            Assert.IsTrue(item1 == item2, "Failed. Should be equivalent");
            Assert.IsTrue(item1 == item3, "Failed. Should be equivalent");
            // Equality
            item1 = data;
            item2 = data;
            item3 = new StringMutable() { Value = data };
            Assert.IsTrue(item1.Equals(item2), "Failed. Should be equivalent");
            Assert.IsTrue(item1.Equals(item3), "Failed. Should be equivalent");
        }
    }
}