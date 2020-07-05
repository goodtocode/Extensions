using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoodToCode.Extensions.Test
{   
    [TestClass()]
    public class DateTimeExtensionTests
    {
        [TestMethod()]
        public void Core_DateTime_ISO8601_FormatStrings()
        {
            DateTime defaultDateValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            DateTime defaultShort = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            Assert.IsTrue(defaultDateValue.Ticks == defaultShort.Ticks);
            Assert.IsTrue(defaultDateValue.ToString() == defaultShort.ToString());

            string ISO8601 = defaultDateValue.ToString(DateTimeExtension.Formats.ISO8601);
            string ISO8601F = defaultDateValue.ToString(DateTimeExtension.Formats.ISO8601F);
            Assert.IsTrue(ISO8601.TryParseDateTime().Ticks == ISO8601F.TryParseDateTime().Ticks);
            Assert.IsTrue(ISO8601.TryParseDateTime().ToString() == ISO8601F.TryParseDateTime().ToString());
        }

        [TestMethod()]
        public void Core_DateTime_Tomorrow()
        {
            var date = DateTime.Now;
            Assert.IsTrue(date.Tomorrow().Day == DateTime.Now.AddDays(1).Day);
        }

        [TestMethod()]
        public void Core_DateTime_Yesterday()
        {
            var date = DateTime.Now;
            Assert.IsTrue(date.Yesterday().Day == DateTime.Now.AddDays(-1).Day);
        }

        [TestMethod()]
        public void Core_DateTime_FirstDayOfMonth()
        {
            var date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.FirstDayOfMonth().Day == 1);
        }

        [TestMethod()]
        public void Core_DateTime_LastDayOfMonth()
        {
            var date = new DateTime(2016, 8, 15);
            Assert.IsTrue(date.LastDayOfMonth().Day == 31);
        }

        [TestMethod()]
        public void Core_DateTime_IsSavable()
        {
            var date = new DateTime(1700, 1, 1);
            Assert.IsTrue(date.IsSavable() == false);
        }
    }
}