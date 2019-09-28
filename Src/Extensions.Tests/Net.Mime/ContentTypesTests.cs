using GoodToCode.Extensions;
using GoodToCode.Extensions.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class ContentTypesTests
    {
        [TestMethod()]
        public void Core_Net_Mime_ContentTypes()
        {
            // Structure of Http content types
            Assert.IsTrue(MimeTypes.ApplicationUnknown == "application/unknown");

            var x = new ContentTypes();
            Assert.IsTrue(x.Count > 0);
        }
    }
}