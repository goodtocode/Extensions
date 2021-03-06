﻿using GoodToCode.Extensions.Configuration;
using GoodToCode.Extensions.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class HttpRequestPutTests
    {
        [TestMethod()]
        public async Task Core_Net_HttpRequestPutString_SendAsync()
        {
            var dataOut = string.Empty;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var request = new HttpRequestPutString(configuration.AppSettingValue("MyWebService") + "/HomeApi");
            try
            {
                dataOut = await request.SendAsync();
                Assert.IsTrue(request.Response.IsSuccessStatusCode);
                throw new HttpRequestException();
            }
            catch (HttpRequestException ex)
            {
                Assert.IsTrue(dataOut != null || ex.Message.Contains("No such host") || ex.Message.Contains("no data"));
            }
            finally
            {                
                request.Dispose();
            }
        }

        [TestMethod()]
        public async Task Core_Net_HttpRequestPut_SendAsync()
        {
            object dataOut = null;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var request = new HttpRequestPut<object>(new Uri(configuration.AppSettingValue("MyWebService") + "/HomeApi"), "This is a test");
            try
            {
                dataOut = await request.SendAsync();
                Assert.IsTrue(request.Response.IsSuccessStatusCode);
                throw new HttpRequestException();
            }
            catch (HttpRequestException ex)
            {
                Assert.IsTrue(dataOut != null || ex.Message.Contains("No such host") || ex.Message.Contains("no data"));
            }
            finally
            {
                request.Dispose();
            }
        }
    }
}