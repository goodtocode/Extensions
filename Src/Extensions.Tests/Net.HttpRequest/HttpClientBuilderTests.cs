using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Net;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class HttpClientBuilderTests
    {
        [TestMethod()]
        public void Core_Net_HttpRequest_HttpClientBuilder()
        {
            var builder = new HttpClientBuilder();
            Assert.IsTrue((builder.MaxResponseContentBufferSize > -1));
        }
    }
}