using GoodToCode.Extensions.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class DesEncryptorTests
    {
        [TestMethod()]
        public void Core_Security_Cryptography_DesEncryptor()
        {
            var rawData = "Hello, I am raw";
            var encryptor = new DesEncryptor(rawData);
            var encrypted = encryptor.Encrypt(rawData);
            Assert.IsTrue(rawData != encrypted);
            Assert.IsTrue(encryptor.Decrypt(encrypted) == rawData);
        }        
    }
}