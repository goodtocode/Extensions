using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoodToCode.Extensions.Net;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class UrlInfoTests
    {
        [TestMethod()]
        public void Core_Net_UrlInfo()
        {
            var TestItem = new UrlInfo("http://test");
            Assert.IsTrue(TestItem.ToString() == "http://test:80");
        }
        
        [TestMethod()]
        public void Core_Net_UrlInfo_ToString()
        {
            var MyRoot = "http://testURL";
            var MyController = "MyController";
            var MyAction = "MyAction";
            var TestItem = new UrlInfo(MyRoot, MyController, MyAction);

            // Check formatting
            Assert.IsTrue(TestItem.ToString().ToLowerInvariant() == String.Format("{0}:80/{1}/{2}", MyRoot, MyController, MyAction).ToLowerInvariant());
        }
    }
}