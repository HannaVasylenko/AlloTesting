using Framework;
using NUnit.Framework;

namespace AlloTests
{
    public class BaseTest
    {
        protected Driver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new BrowsersList().GetBrowserByName(BrowserEnum.Chrome);
            driver.GoToUrl("https://allo.ua/");
            driver.MaximizeWindow();
        }

        [TearDown]
        public void TearDown()
        {
            driver.CloseDriver();
        }
    }
}
