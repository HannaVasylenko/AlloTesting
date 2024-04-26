using AlloTesting;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class Header : BasePage
    {
        public Header(Driver webDriver) : base(webDriver) {}

        public void SelectForCustomersDropdownVariant(string variant) => driver.FindElementByXpath($"//div[@class='mh-button__dropdown']/a[contains(text(), '{variant}')]").Click();

        public void SelectForCustomersDropdown() => driver.FindElementByXpath("//div[@class='mh-button__wrap']").Click();

        public void SelectHeaderLink(string link) => driver.FindElementByXpath($"//div[@class='mh-links']/a[contains(text(), '{link}')]").Click();

        public void SelectCompareProducts() => driver.FindElementByXpath("//header[@class='c-header']//div[@class='mh-actions']/a[@aria-label='Порівняти']").Click();
    }
}
