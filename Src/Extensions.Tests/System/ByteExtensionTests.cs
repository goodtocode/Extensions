using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// ByteExtension Tests
    /// </summary>
    [TestClass()]
    public class ByteExtensionTests
    {
        [TestMethod()]
        public void Core_Byte_ToString()
        {
            byte[] bytes = { 0, 0, 0, 25 };
            Assert.IsTrue(bytes.ToString().Length > 0);
        }
    }
}