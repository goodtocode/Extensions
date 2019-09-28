using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Media;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// RGBStandardInfo Tests
    /// </summary>
    [TestClass()]
    public class RGBStandardInfoTests
    {
        [TestMethod()]
        public void Core_Media_RGBStandardInfo()
        {
            var RGBStandardObject = new RGBStandardInfo();
            Assert.IsTrue(RGBStandardObject.Inverse().Red == 1);
        }
    }
}