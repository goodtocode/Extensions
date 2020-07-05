using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Configuration;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public partial class ConfigurationManagerCoreTests
    {
        /// <summary>
        /// Connection strings in safe version of configuration manager
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ConfigurationManagerCore_AppSettings()
        {
            // Specific config entries
            var itemToTestString = string.Empty;
            var itemToTest = new AppSettingSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);
            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != string.Empty);
            itemToTestString = configuration.AppSettings.GetValue("TestAppSetting");
            Assert.IsTrue(itemToTestString != string.Empty);
        }

        /// <summary>
        /// Read connection information for embedded databases (on devices, primarily)
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ConfigurationManagerCore_ConnectionStrings()
        {
            var itemToTest = new ConnectionStringSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);

            // Validity
            itemToTest.Value = "Invalid Connection String!!!";
            Assert.IsTrue(itemToTest.IsValid == false);
            // ADO
            itemToTest = configuration.ConnectionString("Example-SQLServer");
            Assert.IsTrue(itemToTest.ToString("EF") != string.Empty);
            Assert.IsTrue(itemToTest.IsADO);
            Assert.IsTrue(itemToTest.IsEF == false);
            Assert.IsTrue(itemToTest.IsValid);
            Assert.IsTrue(itemToTest.ConnectionStringType == ConnectionStringSafe.ConnectionStringTypes.ADO);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != string.Empty);
            // EF
            itemToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(itemToTest.ToString("ADO") != string.Empty);
            Assert.IsTrue(itemToTest.IsEF);
            Assert.IsTrue(itemToTest.IsADO == false);
            Assert.IsTrue(itemToTest.IsValid);
            Assert.IsTrue(itemToTest.ConnectionStringType == ConnectionStringSafe.ConnectionStringTypes.EF);
            Assert.IsTrue(itemToTest.ToEF(this.GetType()) != string.Empty);
            // Specific config entries
            var connectionToTest = new ConnectionStringSafe();
            connectionToTest = configuration.ConnectionString("Example-EDMX");
            Assert.IsTrue(connectionToTest.Value != string.Empty);
            Assert.IsTrue(connectionToTest.IsEF);
            connectionToTest = configuration.ConnectionString("Example-SQLServer");
            Assert.IsTrue(connectionToTest.Value != string.Empty);
            Assert.IsTrue(connectionToTest.IsADO);
        }
    }
}