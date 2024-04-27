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

        public void ClickOnBtnShowMoreProducts() => driver.FindElementByXpath("//div[@data-products-type='top']//button[@class='h-pl__more-button']").Click();

        public string GetFacebookAccountName()
        {
            if (driver.IsElementExists(By.XPath("//div[@aria-label='Close']")))
            {
                driver.FindElementByXpath("//div[@aria-label='Close']").Click();
            }
            return driver.FindElementByXpath("//h1").GetText().Replace(" ", "");
        }

        public string GetInstagramAccountName()
        {
            if (driver.IsElementExists(By.XPath("//div[text()='Reload page']")))
            {
                driver.FindElementByXpath("//div[text()='Reload page']").Click();
            }
            return driver.FindElementByXpath("//h2").GetText();
        }

        public void SelectCategory(string category) => driver.FindElementByXpath($"//li[@class='mm__item']/a[contains(normalize-space(), '{category}')]").Click();

        public int GetQuantityOfProducts() => driver.FindElementsByXpath("//div[@data-products-type='top']/div[@class='h-products__list h-pl']/div[@class='h-pc']/div[@class='h-pc__content']/a").Count;
    }
}
