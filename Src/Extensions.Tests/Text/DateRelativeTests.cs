using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Text;
namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class DateRelativeTests
    {
        [TestMethod()]
        public void Core_Text_DateRelative_ToString()
        {
            var objDateRelative = new DateRelative(new System.DateTime(2000, 3, 6));
            Assert.IsTrue(objDateRelative.ToString() == "less than a minute ago");
        }
    }
}