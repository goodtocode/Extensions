using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Text.Cleansing;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class TextCleaningTests
    {
        private string safeTag1 = "Hello World";
        private string unsafeTag1 = "<script>function () { var unsafeX = 1;}</script>";
        private string unsafeTag2 = "<script type=\"text\\javascript/\">function () { var unsafeX = 2;}</script>";
        private string unsafeHtml { get { return string.Format("{0}{1}{2}", unsafeTag1, safeTag1, unsafeTag2); } }

        [TestMethod()]
        public void Core_Text_Cleanser_HtmlUnsafe()
        {
            var safeHtml = Defaults.String;
            var cleanser = new HtmlUnsafeCleanser(unsafeHtml);
            safeHtml = cleanser.Cleanse();
            Assert.IsTrue(safeHtml.Contains(unsafeTag1.SubstringLeft(6)) == false, "Did not work.");
            Assert.IsTrue(safeHtml.Contains(safeTag1) == true, "Did not work.");
        }

        [TestMethod()]
        public void Core_Text_Cleanser_Attribute()
        {
            var testItem = new CleanserAttributeTester() { CleanseMe = unsafeHtml };

            Cleanser.CleanseAll(testItem);

            Assert.IsTrue(testItem.CleanseMe.Contains(unsafeTag1.SubstringLeft(6)) == false, "Did not work.");
            Assert.IsTrue(testItem.CleanseMe.Contains(safeTag1) == true, "Did not work.");
        }

        private class CleanserAttributeTester
        {
            [CleanseFor(CleanserIds.UnsafeHtml)]
            public string CleanseMe { get; set; }
        }
    }
}