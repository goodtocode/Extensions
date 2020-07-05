
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Collections;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// ListSafe Tests
    /// </summary>
    [TestClass()]
    public class ListSafeTests
    {
        [TestMethod()]
        public void Core_Collections_ListSafe_Construct()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_Add()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            Assert.AreEqual(1, kvList.Count);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_Remove()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            kvList.Remove("TestKey1");
            Assert.IsTrue(kvList.Count == 1);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_FindIndex()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            Assert.IsTrue(kvList.FindIndex("TestKey2") == 1);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_GetValue()
        {
            ListSafe<string> kvList = new ListSafe<string>();
            kvList.Add("TestKey1");
            kvList.Add("TestKey2");
            Assert.IsTrue(kvList.GetValue("TestKey2") != string.Empty);
        }
    }
}