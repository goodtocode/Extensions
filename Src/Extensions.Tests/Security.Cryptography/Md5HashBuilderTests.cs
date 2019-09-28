using GoodToCode.Extensions.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class MD5HashBuilderTests
    {
        [TestMethod()]
        public void Core_Security_Cryptography_MD5HashBuilder_Compare()
        {
            var rawData = "Hello, I am raw";
            var hasher = new Md5HashBuilder(rawData);
            var hashed = hasher.HashedString;
            Assert.IsTrue(rawData != hashed);
            Assert.IsTrue(hasher.Compare(rawData));
        }
    }
}