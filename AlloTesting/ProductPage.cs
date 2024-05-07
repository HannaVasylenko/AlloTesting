using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate page of the selected product.
    /// </summary>
    public class ProductPage : BasePage
    {
        public ProductPage(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Gets the selected product color.
        /// </summary>
        /// <returns>The selected product color.</returns>
        public string GetSelectedProductColor() => driver.FindElementByXpath("//div[@class='p-attributes__content p-attributes__item is-color']//span[@class='title__label']").GetText();

        /// <summary>
        /// Selects the specified product color.
        /// </summary>
        /// <param name="productColor">The product color to select.</param>
        public void SelectProductColor(string productColor)
        {
            string colorNameBeforeSelecting = driver.FindElementByXpath("//div[@class='p-attributes__content p-attributes__item is-color']//span[@class='title__label']").GetText();
            Element btnColor = driver.FindElementByXpath($"//div[@class='p-attributes p-dynamic__component']//li[@class='p-attributes-color__item']/a[contains(text(),'{productColor}')]");
            driver.WaitUntil(e =>
            {
                try
                {
                    btnColor.Click();
                    return false;
                }
                catch (Exception ex)
                {
                    return true;
                }
            });
            driver.WaitUntil(e => {
                string colorNameAfterSelecting = driver.FindElementByXpath("//div[@class='p-attributes__content p-attributes__item is-color']//span[@class='title__label']").GetText();
                return !colorNameBeforeSelecting.Equals(colorNameAfterSelecting) && colorNameAfterSelecting != "";
            });
        }

        /// <summary>
        /// Gets the page title after clicking on the product.
        /// </summary>
        /// <returns>The page title after clicking on the product.</returns>
        public string GetPageTitleAfterClickingOnProduct()
        {
            try
            {
                driver.WaitUntil(e => {
                    if (driver.IsElementExists(By.XPath("//div[@class='v-modal__cmp map_modal_container map_modal_container--with-stores']/div[@class='v-modal__close-btn']/child::*")))
                    {
                        driver.FindElementByXpath("//div[@class='v-modal__cmp map_modal_container map_modal_container--with-stores']/div[@class='v-modal__close-btn']/child::*").Click();
                        return true;
                    }
                    return false;
                });
            } catch (WebDriverTimeoutException ex)
            {
            }

            return driver.FindElementByXpath("//h1").GetText();
        }

        /// <summary>
        /// Clicks on the tooltip.
        /// </summary>
        public void ClickOnTooltip() => driver.FindElementByXpath("//div[@class='v-tooltip']//i").Click();

        /// <summary>
        /// Gets the tooltip message.
        /// </summary>
        /// <returns>The tooltip message.</returns>
        public string GetTooltipMessage() => driver.FindElementByXpath("//div[@class='v-tooltip__scroll']//p").GetText();
    }
}
