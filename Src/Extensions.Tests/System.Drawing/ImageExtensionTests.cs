using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class ImageExtensionTests
    {
        [TestMethod()]
        public void Core_Image_Resize()
        {
        }

        [TestMethod()]
        public void Core_Image_Crop()
        {
        }

        [TestMethod()]
        public void Core_Image_ToBytes()
        {
            var item = ImageExtension.ImageEmpty;
            var bytes = item.ToBytes();
            Assert.IsTrue(bytes.Length > 0);
        }
    }
}