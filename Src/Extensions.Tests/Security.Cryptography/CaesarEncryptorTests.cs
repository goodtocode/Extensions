using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Security.Cryptography;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class CaesarEncryptorTests
    {
        [TestMethod()]
        public void Core_Security_Cryptography_CaesarEncryptor()
        {
            var TestItem = new CaesarEncryptor();
            var encryptedString = TestItem.Encrypt("Test");
            Assert.IsTrue(encryptedString == "bQB%2BAIwAjQA%3D");
            var decryptedString = TestItem.Decrypt(encryptedString);
            Assert.IsTrue(decryptedString == "Test");
        }                
    }
}