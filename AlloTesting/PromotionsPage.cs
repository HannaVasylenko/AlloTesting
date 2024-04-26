using AlloTesting;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class PromotionsPage : BasePage
    {
        public PromotionsPage(Driver webDriver) : base(webDriver) {}

        public void SelectPromoCategory(string link)
        {
            List <string> promotionsList = GetPromotionList();
            driver.FindElementByXpath($"//div[@class='promo-list__categories']//span[text()='{link}']").Click();
            driver.WaitUntil(driver => !GetPromotionList().SequenceEqual(promotionsList));
        }

        public bool IsPromoCategoryActive(string link) => driver.FindElementByXpath($"//div[@class='promo-list__categories']//span[text()='{link}']/..").GetAttribute("class") == "active";

        public List<string> GetPromotionList()
        {
            return driver.FindElementsByXpath("//div[@class='promo-list__items']/a/img")
                    .ToList()
                    .ConvertAll(e => e.GetAttribute("alt"));
        }
    }
}
