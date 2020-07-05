using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Extensions.Test
{
    [TestClass()]
    public class DoubleExtensionTests
    {
        [TestMethod()]
        public void Core_Double_ToDecimal()
        {
            double original = 10.00;
            decimal castedValue = 0m;
            castedValue = original.ToDecimal();
            Assert.IsTrue(castedValue == (decimal)original);
        }
    }
}