using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate Warranty / Returns page
    /// </summary>
    public class WarrantyAndReturnsPage : BasePage
    {
        public WarrantyAndReturnsPage(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Selects a tab in the "Warranty and Returns" section.
        /// </summary>
        /// <param name="tabName">The name of the tab to select.</param>
        public void SelectWarrantyAndReturnsTab(string tabName) => driver.FindElementByXpath($"//div[@class='sp-main-tab']/button[text()='{tabName}']").Click();

        /// <summary>
        /// Selects a manufacturer's website alphabetically from the list.
        /// </summary>
        /// <param name="alphabeticalCategory">The alphabetical category to select.</param>
        public void SelectManufacturersWebsiteAlphabetically(string alphabeticalCategory) => driver.FindElementByXpath($"//ul[@class='alphabet']/li/a[text()='{alphabeticalCategory}']").Click();

        /// <summary>
        /// Selects a manufacturer's website from the list.
        /// </summary>
        /// <param name="shopName">The name of the manufacturer's website to select.</param>
        public void SelectManufacturersWebsite(string shopName) => driver.FindElementByXpath($"//ul[@class='dictionary']/li/a[contains(text(), '{shopName}')]").Click();

        /// <summary>
        /// Gets the list of manufacturer websites.
        /// </summary>
        /// <returns>The list of manufacturer websites.</returns>
        public List<string> GetManufacturerWebsites() => driver.FindElementsByXpath("//div[@class='container_search']//ul[@class='dictionary']/li[@style='']/a")
                    .ToList()
                    .ConvertAll(e => e.GetText());

        /// <summary>
        /// Gets the name of the manufacturer website.
        /// </summary>
        /// <returns>The name of the manufacturer website.</returns>
        public string GetManufacturerWebsiteName()
        {
            driver.WaitUntil(e => driver.Title != null);
            return driver.Title;
        }
    }
}
