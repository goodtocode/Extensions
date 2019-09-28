using GoodToCode.Extensions.Configuration;
using GoodToCode.Extensions.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class HttpRequestPostTests
    {
        [TestMethod()]
        public async Task Core_Net_HttpRequestPostString_SendAsync()
        {
            var dataOut = Defaults.String;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var request = new HttpRequestPostString(configuration.AppSettingValue("MyWebService") + "/HomeApi");
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
        public async Task Core_Net_HttpRequestPost_SendAsync()
        {
            object dataOut = null;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var request = new HttpRequestPost<object>(configuration.AppSettingValue("MyWebService") + "/HomeApi");
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
        public async Task Core_Net_HttpRequestPost_CustomerSearch()
        {
            object dataOut = null;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var custSearch = new Customer() { FirstName = "j", LastName = "g" };
            var request = new HttpRequestPost<Customer, CustomerSearch>(new Uri(configuration.AppSettingValue("MyWebService") + "/CustomerSearch"), custSearch);
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