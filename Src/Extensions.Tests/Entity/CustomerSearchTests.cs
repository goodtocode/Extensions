using GoodToCode.Extensions;
using GoodToCode.Extensions.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class CustomerSearchTests
    {
        // From Framework.UniversalApp Send() method
        public const string CustomerSearchISO8601 = "{\"CreatedDate\":\"1900-01-01T00:00:00\",\"Id\":-1,\"Key\":\"00000000-0000-0000-0000-000000000000\",\"ModifiedDate\":\"1900-01-01T00:00:00\",\"State\":\"00000000-0000-0000-0000-000000000000\",\"BirthDate\":\"1900-01-01T00:00:00\",\"CustomerTypeKey\":\"bf3797ee-06a5-47f2-9016-369beb21d944\",\"FirstName\":\"s\",\"GenderId\":-1,\"LastName\":\"g\",\"MiddleName\":\"\"}";
        public const string CustomerSearchISO8601F = "{\"CreatedDate\":\"1900-01-01T00:00:00.000\",\"Id\":-1,\"Key\":\"00000000-0000-0000-0000-000000000000\",\"ModifiedDate\":\"1900-01-01T00:00:00.000\",\"State\":\"00000000-0000-0000-0000-000000000000\",\"BirthDate\":\"1900-01-01T00:00:00.000\",\"CustomerTypeKey\":\"bf3797ee-06a5-47f2-9016-369beb21d944\",\"FirstName\":\"s\",\"GenderId\":-1,\"LastName\":\"g\",\"MiddleName\":\"\"}";
        public const string CustomerSearchDefault = "{\"FirstName\":\"j\",\"MiddleName\":\"\",\"LastName\":\"g\",\"birthDate\":\"1900-01-01T00:00:00Z\",\"genderId\":-1,\"customerTypeKey\":\"bf3797ee-06a5-47f2-9016-369beb21d944\",\"id\":-1,\"key\":\"00000000-0000-0000-0000-000000000000\",\"createdDate\":\"1900-01-01T00:00:00Z\",\"modifiedDate\":\"1900-01-01T00:00:00Z\",\"state\":\"00000000-0000-0000-0000-000000000000\",\"isNew\":true,\"failedRules\":[],\"results\":[]\"\"}";

        [TestMethod()]
        public void Core_Entity_CustomerSearch_Class()
        {
            var searchChar = "i";
            var originalObject = new CustomerSearch() { FirstName = searchChar, LastName = searchChar };
            var resultObject = new CustomerSearch();
            var resultString = string.Empty;
            var serializer = new JsonSerializer<CustomerSearch>();

            resultString = serializer.Serialize(originalObject);
            Assert.IsTrue(resultString != string.Empty);
            resultObject = serializer.Deserialize(resultString);
            Assert.IsTrue(resultObject.FirstName == searchChar);
            Assert.IsTrue(resultObject.LastName == searchChar);
        }

        [TestMethod()]
        public void Core_Entity_CustomerSearch_Json()
        {
            var resultObject = new CustomerSearch();
            var resultString = string.Empty;
            var serializer = new JsonSerializer<CustomerSearch>();

            //
            // Default: ISO8601 for seconds, no milliseconds
            //
            serializer.DateTimeFormatString = new DateTimeFormat(DateTimeExtension.Formats.ISO8601) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };
            serializer.ThrowException = true;
            resultObject = serializer.Deserialize(CustomerSearchISO8601);
            Assert.IsTrue(resultObject.FirstName.Length > 0 && resultObject.LastName.Length > 0);
            serializer.ThrowException = false;
            resultObject = serializer.Deserialize(CustomerSearchISO8601F);
            Assert.IsTrue(resultObject.FirstName.Length > 0 && resultObject.LastName.Length > 0);

            //
            // ISO8601F for milliseconds
            //            
            serializer.DateTimeFormatString = new DateTimeFormat(DateTimeExtension.Formats.ISO8601F) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };
            serializer.ThrowException = true;
            resultObject = serializer.Deserialize(CustomerTests.Customer_HHMMSSf);
            Assert.IsTrue(resultObject.FirstName.Length > 0 && resultObject.LastName.Length > 0);
            serializer.ThrowException = false;
            CustomerSearch searchToSearch = serializer.Deserialize(CustomerSearchISO8601);
            Assert.IsTrue(resultObject.FirstName.Length > 0 && resultObject.LastName.Length > 0);

            //
            // Microsoft default DateTime format
            //
            serializer.DateTimeFormatString = new DateTimeFormat(DateTimeExtension.Formats.Default) { DateTimeStyles = System.Globalization.DateTimeStyles.RoundtripKind };
            serializer.ThrowException = true;
            resultObject = serializer.Deserialize(CustomerSearchDefault);
            Assert.IsTrue(resultObject.FirstName.Length > 0 && resultObject.LastName.Length > 0);
            serializer.ThrowException = false;
            resultObject = serializer.Deserialize(CustomerSearchISO8601F);
            Assert.IsTrue(resultObject.FirstName.Length > 0 && resultObject.LastName.Length > 0);
        }
    }
}