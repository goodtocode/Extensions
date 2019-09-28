using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class StringExtensionTests
    {
        [TestMethod()]
        public void Core_String_SubstringRight()
        {
            var TestItem = Defaults.Uri;
            Assert.IsTrue(TestItem.ToString().SubstringRight(1) == TestItem.ToString().Substring(TestItem.ToString().Length - 1, 1));
        }

        [TestMethod()]
        public void Core_String_SubstringLeft()
        {
            var TestItem = Defaults.Uri;
            Assert.IsTrue(TestItem.ToString().SubstringLeft(1) == TestItem.ToString().Substring(0, 1));
        }

        [TestMethod()]
        public void Core_String_SubstringSafe()
        {
            var TestItem = Defaults.Uri;
            Assert.IsTrue(TestItem.ToString().SubstringSafe(0, 1).Length == 1);
        }

        [TestMethod()]
        public void Core_String_RemoveFirst()
        {
            var TestItem = Defaults.Uri;
            Assert.IsTrue(TestItem.ToString().RemoveFirst("h").Length == TestItem.ToString().Length - 1);
        }

        [TestMethod()]
        public void Core_String_RemoveLast()
        {
            var TestItem = String.Format("{0}/", Defaults.Uri);
            Assert.IsTrue(TestItem.RemoveLast("/").Length == TestItem.Length - 1);
        }

        [TestMethod()]
        public void Core_String_ToPascalCase()
        {
            var lower = "hello";
            Assert.IsTrue(lower.ToPascalCase().SubstringLeft(2) == "He");
        }

        [TestMethod()]
        public void Core_String_IsCaseUpper()
        {
            var mixed = "Hello";
            var upper = "HELLO";
            Assert.IsTrue(mixed.IsCaseUpper() == false);
            Assert.IsTrue(upper.IsCaseUpper());
        }

        [TestMethod()]
        public void Core_String_IsCaseLower()
        {
            var mixed = "Hello";
            var lower = "hello";
            Assert.IsTrue(mixed.IsCaseLower() == false);
            Assert.IsTrue(lower.IsCaseLower());
        }

        [TestMethod()]
        public void Core_String_IsCaseMixed()
        {
            var mixed = "Hello";
            var upper = "HELLO";            
            Assert.IsTrue(mixed.IsCaseMixed());
            Assert.IsTrue(upper.IsCaseMixed() == false);
        }

        [TestMethod()]
        public void Core_String_IsFirst()
        {
            var testData = "Hello";
            Assert.IsTrue(testData.IsFirst("H"));
        }

        [TestMethod()]
        public void Core_String_IsLast()
        {
            var testData = "Hello";
            Assert.IsTrue(testData.IsLast("o"));
        }

        [TestMethod()]
        public void Core_String_IsEmail()
        {
            var testDataGood = "testing@GoodToCode.com";
            var testDataBad = "testingATGoodToCode.com";
            Assert.IsTrue(testDataGood.IsEmail());
            Assert.IsTrue(testDataBad.IsEmail() == false);
        }

        [TestMethod()]
        public void Core_String_IsInteger()
        {
            var testDataGood = "1234";
            var testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.IsInteger());
            Assert.IsTrue(testDataBad.IsInteger() == false);
        }

        [TestMethod()]
        public void Core_String_TryParseBoolean()
        {
            var testDataTrue1 = "1";
            var testDataTrue2 = "11";
            var testDataFalse = "0";
            Assert.IsTrue(testDataTrue1.TryParseBoolean());
            Assert.IsTrue(testDataTrue2.TryParseBoolean());
            Assert.IsTrue(testDataFalse.TryParseBoolean() == false);
        }

        [TestMethod()]
        public void Core_String_TryParseInt32()
        {
            var testDataGood = "1234";
            var testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseInt32() == 1234);
            Assert.IsTrue(testDataBad.TryParseInt32() == Defaults.Integer);
        }

        [TestMethod()]
        public void Core_String_TryParseInt64()
        {
            var testDataGood = "1234";
            var testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseInt64() == 1234);
            Assert.IsTrue(testDataBad.TryParseInt64() == Defaults.Integer);
        }

        [TestMethod()]
        public void Core_String_TryParseGuid()
        {
            var testDataGood = "A8CA69CE-F8C6-4FCC-9FED-6AF9F94879D9";
            var testDataBad = "A869CE-F8C6-4FCC-9FED-6AF994879D9";
            Assert.IsTrue(testDataGood.TryParseGuid() != Defaults.Guid);
            Assert.IsTrue(testDataBad.TryParseGuid() == Defaults.Guid);
        }

        [TestMethod()]
        public void Core_String_TryParseEnum()
        {
            // 1
            string testData1 = EnumExtensionTests.EnumConsumer.MyEnumFlags.one.ToString();            
            var parsedData1 = EnumExtensionTests.EnumConsumer.MyEnumFlags.one;
            var test1 = testData1.TryParseEnum<EnumExtensionTests.EnumConsumer.MyEnumFlags>(EnumExtensionTests.EnumConsumer.MyEnumFlags.one);
            Assert.IsTrue(test1 == parsedData1);
            // 2
            string testData2 = EnumExtensionTests.EnumConsumer.MyEnumFlags.eight.ToString();
            var parsedData2 = EnumExtensionTests.EnumConsumer.MyEnumFlags.eight;
            var test2 = testData2.TryParseEnum<EnumExtensionTests.EnumConsumer.MyEnumFlags>(EnumExtensionTests.EnumConsumer.MyEnumFlags.eight);
            Assert.IsTrue(test2 == parsedData2);
            // 3
            string testType3 = EnumExtensionTests.EnumConsumer.MyEnumInts.one.ToString();
            var parsedType3 = EnumExtensionTests.EnumConsumer.MyEnumInts.one;
            var test3 = testType3.TryParseEnum<EnumExtensionTests.EnumConsumer.MyEnumInts>(EnumExtensionTests.EnumConsumer.MyEnumInts.one);
            Assert.IsTrue(test3 == parsedType3);
        }

        [TestMethod()]
        public void Core_String_TryParseDecimal()
        {
            var testDataGood = "12.00";
            var testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseDecimal() == 12.00M);
            Assert.IsTrue(testDataBad.TryParseDecimal() == Defaults.Decimal);
        }

        [TestMethod()]
        public void Core_String_TryParseDouble()
        {
            var testDataGood = "12.00";
            var testDataBad = "OneTwo12";
            Assert.IsTrue(testDataGood.TryParseDouble() == 12.00);
            Assert.IsTrue(testDataBad.TryParseDouble() == Defaults.Double);
        }

        [TestMethod()]
        public void Core_String_TryParseDateTime()
        {
            var testDataGood = "08/24/2011";
            var testDataBad = "badDate";
            Assert.IsTrue(testDataGood.TryParseDateTime().Month == 8);
            Assert.IsTrue(testDataBad.TryParseDateTime() == Defaults.Date);
        }

        [TestMethod()]
        public void Core_String_TryParseTime()
        {
            var testDataGood = "10:45 PM";
            var testDataBad = "badTime";
            Assert.IsTrue(testDataGood.TryParseTime().Minute == 45);
            Assert.IsTrue(testDataBad.TryParseTime().Minute == Defaults.Date.Minute );
        }
    }
}