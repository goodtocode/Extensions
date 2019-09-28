using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Configuration;
using System.Collections.Specialized;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// ConfigurationManagerCore Tests
    /// </summary>
    [TestClass()]
    public partial class ApplicationSettingSafeTests
    {
        /// <summary>
        /// Connection strings in safe version of configuration manager
        /// </summary>
        [TestMethod()]
        public void Core_Configuration_ApplicationSettingSafe()
        {
            var itemToTest = new AppSettingSafe();
            var configuration = new ConfigurationManagerCore(ApplicationTypes.Native);

            itemToTest = configuration.AppSetting("TestAppSetting");
            Assert.IsTrue(itemToTest.Value != Defaults.String);
        }
    }
}