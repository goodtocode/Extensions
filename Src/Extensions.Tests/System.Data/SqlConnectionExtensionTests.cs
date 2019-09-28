using GoodToCode.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// ByteExtension Tests
    /// </summary>
    [TestClass()]
    public class SqlConnectionExtensionTests
    {
        public void Core_SqlConnection_CanOpen()
        {
            var databaseAccess = false;
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            using (var connection = new SqlConnection(configuration.ConnectionStringValue("DefaultConnection")))
            {
                databaseAccess = connection.CanOpen();
            }
            Assert.IsTrue(databaseAccess);
        }
    }
}