using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    public class InitialPage : BasePage
    {
        public InitialPage(Driver driver) : base(driver) {}

        public Header header => new Header(driver);

        public Footer footer => new Footer(driver);

        /// <summary>
        /// Clicks on the button to show more products.
        /// </summary>
        public void ClickOnBtnShowMoreProducts() => driver.FindElementByXpath("//div[@data-products-type='top']//button[@class='h-pl__more-button']").Click();

        /// <summary>
        /// Retrieves the Facebook account name.
        /// </summary>
        /// <returns>The Facebook account name.</returns>
        public string GetFacebookAccountName()
        {
            if (driver.IsElementExists(By.XPath("//div[@aria-label='Close']")))
            {
                driver.FindElementByXpath("//div[@aria-label='Close']").Click();
            }
            return driver.FindElementByXpath("//h1").GetText().Replace(" ", "");
        }

        /// <summary>
        /// Retrieves the Instagram account name.
        /// </summary>
        /// <returns>The Instagram account name.</returns>
        public string GetInstagramAccountName()
        {
            if (driver.IsElementExists(By.XPath("//div[text()='Reload page']")))
            {
                driver.FindElementByXpath("//div[text()='Reload page']").Click();
            }
            return driver.FindElementByXpath("//h2").GetText();
        }

        /// <summary>
        /// Selects the specified category.
        /// </summary>
        /// <param name="category">The category to select.</param>
        public void SelectCategory(string category) => driver.FindElementByXpath($"//li[@class='mm__item']/a[contains(normalize-space(), '{category}')]").Click();

        /// <summary>
        /// Gets the quantity of products.
        /// </summary>
        /// <returns>The quantity of products.</returns>
        public int GetQuantityOfProducts() => driver.FindElementsByXpath("//div[@data-products-type='top']/div[@class='h-products__list h-pl']/div[@class='h-pc']/div[@class='h-pc__content']/a").Count;
    }
}
