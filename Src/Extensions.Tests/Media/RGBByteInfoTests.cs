using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Media;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// RGBByteInfo Tests
    /// </summary>
    [TestClass()]
    public class RGBByteInfoTests
    {
        [TestMethod()]
        public void Core_Media_RGBByteInfo()
        {
            var RGBByteObject = new RGBByteInfo();
            Assert.IsTrue(RGBByteObject.ToHex() == "#000000");
        }
    }
}