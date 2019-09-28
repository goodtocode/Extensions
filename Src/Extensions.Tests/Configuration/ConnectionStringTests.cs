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
            Assert.IsTrue(itemToTest.Value != Defaults.String);
            itemToTest.EDMXFileName = "TestEDMXFile";
            Assert.IsTrue(itemToTest.ToString("EF") != Defaults.String);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != Defaults.String);
            // EF
            itemToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
            Assert.IsTrue(itemToTest.ToString("ADO") != Defaults.String);
            Assert.IsTrue(itemToTest.ToADO() != Defaults.String);
        }
    }
}