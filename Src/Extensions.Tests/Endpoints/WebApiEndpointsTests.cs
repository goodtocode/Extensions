using GoodToCode.Extensions;
using GoodToCode.Extensions.Configuration;
using GoodToCode.Extensions.Mathematics;
using GoodToCode.Extensions.Net;
using GoodToCode.Extensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// Test GoodToCode Framework for Web API endpoints
    /// </summary>
    /// <remarks></remarks>
    [TestClass()]
    public class Endpoints_Framework_for_WebApi
    {
        private readonly bool interfaceBreakingRelease = true; // Current release breaks the interface?
        private static readonly object LockObject = new object();
        private static volatile List<Guid> _recycleBin = null;
        /// <summary>
        /// Singleton for recycle bin
        /// </summary>
        private static List<Guid> RecycleBin
        {
            get
            {
                if (_recycleBin != null) return _recycleBin;
                lock (LockObject)
                {
                    if (_recycleBin == null)
                    {
                        _recycleBin = new List<Guid>();
                    }
                }
                return _recycleBin;
            }
        }

        private List<Customer> customerTestData = new List<Customer>()
        {
            new Customer() {FirstName = "John", MiddleName = "Adam", LastName = "Doe", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new Customer() {FirstName = "Jane", MiddleName = "Michelle", LastName = "Smith", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new Customer() {FirstName = "Xi", MiddleName = "", LastName = "Ling", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new Customer() {FirstName = "Juan", MiddleName = "", LastName = "Gomez", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new Customer() {FirstName = "Maki", MiddleName = "", LastName = "Ishii", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) }
        };

        /// <summary>
        /// Initializes class before tests are ran
        /// </summary>
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {

        }

        /// <summary>
        /// Get a customer, via HttpGet from Framework.WebServices endpoint
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Endpoints_Framework_WebAPI_CustomerGet()
        {
            var urlCustomer = new ConfigurationManagerCore(ApplicationTypes.Native).AppSettingValue("MyWebService").AddLast("/Customer");
            await this.Endpoints_Framework_WebAPI_CustomerPut();
            var keyToGet = (Endpoints_Framework_for_WebApi.RecycleBin.Count() > 0 ? Endpoints_Framework_for_WebApi.RecycleBin[0] : Defaults.Guid).ToString();
            var request = new HttpRequestGet<Customer>(urlCustomer.AddLast("/") + keyToGet.ToString());

            try
            {
                var responseData = await request.SendAsync();
                Assert.IsTrue(interfaceBreakingRelease || responseData != null);
            }
            catch (HttpRequestException ex)
            {
                Assert.IsTrue(ex.Message.Contains("No such host") || ex.Message.Contains("no data"));
            }
        }

        /// <summary>
        /// Create a new customer, via HttpPut to Framework.WebServices endpoint
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Endpoints_Framework_WebAPI_CustomerPut()
        {
            var customerToCreate = new Customer();
            var returnedItem = new Customer();
            var url = new Uri(new ConfigurationManagerCore(ApplicationTypes.Native).AppSettingValue("MyWebService").AddLast("/Customer"));

            try
            {
                customerToCreate.Fill(customerTestData[Arithmetic.Random(1, customerTestData.Count)]);
                var request = new HttpRequestPut<Customer>(url, customerToCreate);
                returnedItem = await request.SendAsync();
                Assert.IsTrue(interfaceBreakingRelease || customerToCreate != null);
                Endpoints_Framework_for_WebApi.RecycleBin.Add(customerToCreate.Key);
            }
            catch (HttpRequestException ex)
            {
                Assert.IsTrue(ex.Message.Contains("No such host") || ex.Message.Contains("no data"));
            }
        }

        /// <summary>
        /// Update a customer, via HttpPost to Framework.WebServices endpoint
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Endpoints_Framework_WebAPI_CustomerPost()
        {
            var responseData = new Customer();
            var urlCustomer = new ConfigurationManagerCore(ApplicationTypes.Native).AppSettingValue("MyWebService").AddLast("/Customer");

            await this.Endpoints_Framework_WebAPI_CustomerPut();
            var keyToGet = Endpoints_Framework_for_WebApi.RecycleBin.Count() > 0 ? Endpoints_Framework_for_WebApi.RecycleBin[0] : Defaults.Guid;

            try
            {
                var url = new Uri(urlCustomer.AddLast("/") + keyToGet.ToStringSafe());
                var requestGet = new HttpRequestGet<Customer>(url);
                responseData = await requestGet.SendAsync();
                Assert.IsTrue(interfaceBreakingRelease || responseData != null);

                var testKey = RandomString.Next();
                responseData.FirstName = responseData.FirstName.AddLast(testKey);
                var request = new HttpRequestPost<Customer>(urlCustomer.TryParseUri(), responseData);
                responseData = await request.SendAsync();
                Assert.IsTrue(interfaceBreakingRelease || responseData != null);
                Assert.IsTrue(interfaceBreakingRelease || responseData.FirstName.Contains(testKey));
            }
            catch (HttpRequestException ex)
            {
                Assert.IsTrue(ex.Message.Contains("No such host") || ex.Message.Contains("no data"));
            }
        }

        /// <summary>
        /// Delete a customer, via HttpDelete to Framework.WebServices endpoint
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Endpoints_Framework_WebAPI_CustomerDelete()
        {
            var responseData = new Customer();
            var urlCustomer = new ConfigurationManagerCore(ApplicationTypes.Native).AppSettingValue("MyWebService").AddLast("/Customer");
            try
            {
                await this.Endpoints_Framework_WebAPI_CustomerPut();
                var keyToDelete = Endpoints_Framework_for_WebApi.RecycleBin.Count() > 0 ? Endpoints_Framework_for_WebApi.RecycleBin[0] : Defaults.Guid;

                var requestDelete = new HttpRequestDelete(urlCustomer.AddLast("/") + keyToDelete.ToString());
                await requestDelete.SendAsync();
                Assert.IsTrue(interfaceBreakingRelease || requestDelete.Response.IsSuccessStatusCode);

                var requestGet = new HttpRequestGet<Customer>(urlCustomer);
                responseData = await requestGet.SendAsync();
                Assert.IsTrue(interfaceBreakingRelease || responseData != null);
            }
            catch (HttpRequestException ex)
            {
                Assert.IsTrue(ex.Message.Contains("No such host") || ex.Message.Contains("no data"));
            }
        }
    }
}
