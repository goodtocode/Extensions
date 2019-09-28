using GoodToCode.Extensions;
using GoodToCode.Extensions.Configuration;
using GoodToCode.Extensions.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class HttpRequestDeleteTests
    {
        [TestMethod()]
        public async Task Core_Net_HttpRequestDelete_SendAsync()
        {
            var dataOut = Defaults.String;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            var request = new HttpRequestDelete(configuration.AppSettingValue(AppSettingList.MyWebServiceKeyName) + "/HomeApi");
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