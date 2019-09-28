using GoodToCode.Extensions;
using GoodToCode.Extensions.Configuration;
using GoodToCode.Extensions.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class HttpRequestGetTests
    {
        [TestMethod()]
        public async Task Core_Net_HttpRequestGetString_SendAsync()
        {
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var dataOut = Defaults.String;
            var request = new HttpRequestGetString(configuration.AppSettingValue("MyWebService") + "/HomeApi");
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
        public async Task Core_Net_HttpRequestGet_SendAsync()
        {
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            object dataOut = null;
            var request = new HttpRequestGet<object>(configuration.AppSettingValue("MyWebService") + "/HomeApi");
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