using GoodToCode.Extensions.Text.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class UrlEncoderTests
    {
        [TestMethod()]
        public void Core_Text_Encoding_UrlEncoder()
        {
            var testObject = new UrlEncoder("& and < and /");
            var result = testObject.Encode();
            Assert.IsTrue(result.Length > 0, "Item did not work.");
            Assert.IsTrue(result.Contains("&") == false, "Item did not work.");
            Assert.IsTrue(result.Contains("<") == false, "Item did not work.");
            Assert.IsTrue(result.Contains("/") == false, "Item did not work.");
        }
    }
}