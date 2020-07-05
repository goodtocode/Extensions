using GoodToCode.Extensions;
using GoodToCode.Extensions.Text.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class Base64EncoderTests
    {
        [TestMethod()]
        public void Core_Text_Encoding_Base64Encoder()
        {
            var rawValue = "Raw data value";
            var encodedValue = string.Empty;

            var encoder = new Base64Encoder(rawValue);
            encodedValue = encoder.Encode();
            encoder = new Base64Encoder(encodedValue);
            Assert.IsTrue(encoder.Decode() == rawValue, "Did not work.");
        }
    }
}