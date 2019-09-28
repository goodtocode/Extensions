using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class IListExtensionTests
    {
        public IList<string> names1 = new List<string>() { "Burke", "Connor", "Frank", "Everett", "Albert", "George", "Harris", "David" };
        public IList<string> names2 = new List<string>() { "Joe", "James", "Jack" };

        public class ComplexObject
        {
            public string Name { get; set; }
        }
        public class ComplexList : List<ComplexObject>
        {
            public ComplexList() : base()
            {
                AddRange(new List<ComplexObject>() {
                    new ComplexObject() { Name = "Larry" }, new ComplexObject() { Name = "Curly" }, new ComplexObject() { Name = "Mo" }});
            }
        }

        [TestMethod()]
        public void Core_IList_AddRange()
        {
            var allNames = new List<string>();
            allNames.AddRange(names1);
            allNames.AddRange(names2);
            Assert.IsTrue(allNames.Count == (names1.Count + names2.Count));
        }

        [TestMethod()]
        public void Core_IList_FillRange()
        {
            var fullList = new ComplexList();
            var emptyList = new ComplexList();

            emptyList.Clear();
            Assert.IsTrue(emptyList.Count == 0);
            emptyList.FillRange(fullList);
            Assert.IsTrue(emptyList.Count == fullList.Count);

            emptyList.Clear();
            Assert.IsTrue(emptyList.Count == 0);
            emptyList.FillRange(fullList.Select(x => x));
            Assert.IsTrue(emptyList.Count == fullList.Count);
        }
    }
}