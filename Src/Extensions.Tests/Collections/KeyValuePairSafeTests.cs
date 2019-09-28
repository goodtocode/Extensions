using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Collections;
using GoodToCode.Extensions.Text;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class KeyValuePairSafeTests
    {
        [TestMethod()]
        public void Core_Collections_KeyValuePairSafe()
        {
            var kvp = new KeyValuePairSafe<int, int>(1,1);
            kvp.Key = 1;
            kvp.Value = 1;           
            Assert.AreEqual(1, kvp.Key);
            var kvp1 = new KeyValuePairSafe<int, StringMutable>(1, "1");
            Assert.AreEqual(1, kvp1.Key);
        }
    }
}