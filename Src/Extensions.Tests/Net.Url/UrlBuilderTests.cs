using GoodToCode.Extensions.Collections;
using GoodToCode.Extensions.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class UrlBuilderTests
    {
        public Uri LocalhostWithPortAsUri { get; set; } = Defaults.Uri;
        public string LocalhostWithPortAsString { get; set; } = "http://localhost:80";
        public List<string> ParameterList = new List<string>() { "param1", "param2", "param3" };
        public KeyValueListString QuerystringList = new KeyValueListString() { { "key1", "param1" }, { "key2", "param2" }, { "key3", "param3" } };

        [TestMethod()]
        public void Core_Net_UrlBuilder()
        {
            var testItem = new UrlBuilder(LocalhostWithPortAsString);
            Assert.IsTrue(testItem.ToString() == LocalhostWithPortAsString);
            Assert.IsTrue(testItem.ToString() == LocalhostWithPortAsUri.OriginalString);
        }

        [TestMethod()]
        public void Core_Net_UrlBuilder_ParameterBinding()
        {
            var parameterValues = new List<string>() { "param1", "param2", "param3" };
            var testItem = new UrlBuilder(LocalhostWithPortAsUri.ToString(), parameterValues);
            Assert.IsTrue(testItem.ToString().Contains("http://"));
            Assert.IsTrue(testItem.ToString().Contains("/param1/param2/param3"));
        }

        [TestMethod()]
        public void Core_Net_UrlBuilder_Querystring()
        {
            // Test manually building querystring uri
            string manualQuerystring = "http://localhost:80";
            manualQuerystring += String.Join("&", QuerystringList.ToString("QS"));
            manualQuerystring = manualQuerystring.RemoveFirst("&");
            manualQuerystring = manualQuerystring.AddFirst("?");
            Assert.IsTrue(manualQuerystring.Contains(LocalhostWithPortAsString));
            Assert.IsTrue(manualQuerystring.Contains("?key1=param1"));
            Assert.IsTrue(manualQuerystring.Contains("&key2=param2"));
            Assert.IsTrue(manualQuerystring.Contains("&key3=param3"));

            // Now test UrlBuilder
            var testItem = new UrlBuilder(LocalhostWithPortAsUri.ToString(), QuerystringList);
            Assert.IsTrue(testItem.ToString().Contains(LocalhostWithPortAsString));
            Assert.IsTrue(testItem.ToString().Contains("?key1=param1"));
            Assert.IsTrue(testItem.ToString().Contains("&key2=param2"));
            Assert.IsTrue(testItem.ToString().Contains("&key3=param3"));
        }

        [TestMethod()]
        public void Core_Net_UrlBuilder_ToString()
        {
            var testItem = new UrlBuilder(LocalhostWithPortAsUri.ToString());
            Assert.IsTrue(testItem.ToString() == LocalhostWithPortAsString);
        }

        [TestMethod()]
        public void Core_Net_UrlBuilder_Encode()
        {
            Assert.IsTrue(UrlBuilder.Encode("@Test") == "%40Test", "Encode failed");
        }

        [TestMethod()]
        public void Core_Net_UrlBuilder_Decode()
        {
            Assert.IsTrue(UrlBuilder.Decode("%40Test") == "@Test", "Encode failed");
        }
    }
}