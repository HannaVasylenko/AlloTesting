using AlloTesting;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class SmartphonesAndPhonesPage : BasePage
    {
        public SmartphonesAndPhonesPage(Driver webDriver) : base(webDriver) {}

        public string GetPageTitle() => driver.FindElementByXpath("//h1").GetText();

        public void SelectSubCategory(string subCategory, string subCategotyItem) => driver.FindElementByXpath($"//h3[contains(text(), '{subCategory}')]/ancestor::div[@class='portal__navigation']//li[@class='portal-category__item']/a[text()='{subCategotyItem}']").Click();
    }
}
