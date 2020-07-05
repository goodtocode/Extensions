using GoodToCode.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// ConfigurationManagerCore Tests
    /// </summary>
    [TestClass()]
    public partial class ConnectionStringSafeTests
    {
        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ConnectionStringSafe()
        {
            var itemToTest = new ConnectionStringSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);

            // ADO
            itemToTest = configuration.ConnectionString("Example-SQLServer");
            Assert.IsTrue(itemToTest.Value != string.Empty);
            itemToTest.EDMXFileName = "TestEDMXFile";
            Assert.IsTrue(itemToTest.ToString("EF") != string.Empty);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != string.Empty);
            // EF
            itemToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(itemToTest.Value != string.Empty);
            Assert.IsTrue(itemToTest.ToString("ADO") != string.Empty);
            Assert.IsTrue(itemToTest.ToADO() != string.Empty);
        }
    }
}