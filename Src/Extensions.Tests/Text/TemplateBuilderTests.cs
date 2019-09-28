using GoodToCode.Extensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class TemplateBuilderTests
    {
        [TestMethod()]
        public void Core_Text_TemplateBuilder_ToString()
        {
            var template = "1: {0}, 2: {1}, 3: {2}";
            var result = Defaults.String;
            var data = new List<string>() { "FirstItem", "SecondItem", "ThirdItem" };
            var builder = new TemplateBuilder(template, data);
            result = builder.ToString();
            foreach(var item in data)
            {
                Assert.IsTrue(result.Contains(item));
            }            
        }        
    }
}