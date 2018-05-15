using DSoft.AgileSprinter.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSoft.AgileSprinter.Tests
{
    [TestClass]
    public class TestHomeController
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new HomeController();

            var result = controller.Index();
            Assert.IsNotNull(result);
        }
    }
}
