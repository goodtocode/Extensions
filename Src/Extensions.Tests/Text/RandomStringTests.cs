using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Text;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class RandomStringTests
    {
        [TestMethod()]
        public void Core_Text_RandomString_Next()
        {
            Assert.IsTrue(RandomString.Next().Length == 10);
        }
    }
}