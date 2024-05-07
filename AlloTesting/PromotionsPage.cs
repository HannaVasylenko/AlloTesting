using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate page of the Promotions.
    /// </summary>
    public class PromotionsPage : BasePage
    {
        public PromotionsPage(Driver webDriver) : base(webDriver) { }

        /// <summary>
        /// Selects the specified promo category.
        /// </summary>
        /// <param name="link">The name of the promo category to select.</param>
        public void SelectPromoCategory(string link)
        {
            List<string> promotionsList = GetPromotionList();
            driver.FindElementByXpath($"//div[@class='promo-list__categories']//span[text()='{link}']").Click();
            driver.WaitUntil(driver => !GetPromotionList().SequenceEqual(promotionsList));
        }

        /// <summary>
        /// Checks if the specified promo category is active.
        /// </summary>
        /// <param name="link">The name of the promo category to check.</param>
        /// <returns>True if the promo category is active; otherwise, false.</returns>
        public bool IsPromoCategoryActive(string link) => driver.FindElementByXpath($"//div[@class='promo-list__categories']//span[text()='{link}']/..").GetAttribute("class") == "active";

        /// <summary>
        /// Gets the list of promotions.
        /// </summary>
        /// <returns>The list of promotion names.</returns>
        public List<string> GetPromotionList()
        {
            return driver.FindElementsByXpath("//div[@class='promo-list__items']/a/img")
                    .ToList()
                    .ConvertAll(e => e.GetAttribute("alt"));
        }
    }
}
