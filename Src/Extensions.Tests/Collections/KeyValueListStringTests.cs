using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodToCode.Extensions.Collections;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// KeyValueListString Tests
    /// </summary>
    [TestClass()]
    public class KeyValueListStringTests
    {
        [TestMethod()]
        public void Core_Collections_KeyValueListString_Construct()
        {
            var kvList = new KeyValueListString();
            Assert.AreEqual(0, kvList.Count);
        }

        [TestMethod()]
        public void Core_Collections_KeyValueListString_Add()
        {
            var kvString = new KeyValueListString();
            kvString.Add("TestKey", "TestValue");
            Assert.AreEqual(1, kvString.Count);
            kvString.Add("TestKey", "TestValue2");
            Assert.AreNotEqual(2, kvString.Count);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_Remove()
        {
            var kvList = new KeyValueListString();
            kvList.Add("Key1", "Value1");
            kvList.Add("Key2", "Value2");
            kvList.Remove("Key1");
            Assert.IsTrue(kvList.Count == 1);
        }

        [TestMethod()]
        public void Core_Collections_ListSafe_GetValue()
        {
            var kvList = new KeyValueListString();
            kvList.Add("Key1", "Value1");
            kvList.Add("Key2", "Value2");
            Assert.IsTrue(kvList.GetValue("Key1") != string.Empty);
        }
    }
}   