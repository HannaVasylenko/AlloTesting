using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    public class WarrantyAndReturnsPage : BasePage
    {
        public WarrantyAndReturnsPage(Driver webDriver) : base(webDriver) {}

        public void SelectWarrantyAndReturnsTab(string tabName) => driver.FindElementByXpath($"//div[@class='sp-main-tab']/button[text()='{tabName}']").Click();

        public void SelectManufacturersWebsiteAlphabetically(string alphabeticalCategory) => driver.FindElementByXpath($"//ul[@class='alphabet']/li/a[text()='{alphabeticalCategory}']").Click();

        public void SelectManufacturersWebsite(string shopName) => driver.FindElementByXpath($"//ul[@class='dictionary']/li/a[contains(text(), '{shopName}')]").Click();

        public List<string> GetManufacturerWebsites() => driver.FindElementsByXpath("//div[@class='container_search']//ul[@class='dictionary']/li[@style='']/a")
                    .ToList()
                    .ConvertAll(e => e.GetText());

        public string GetManufacturerWebsiteName()
        {
            driver.WaitUntil(e => driver.Title != null);
            return driver.Title;
        }
    }
}
