using AlloTesting;
using Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class ShopsPage : BasePage
    {
        public ShopsPage(Driver webDriver) : base(webDriver) {}

        public void InputDataInSearchShopsField(string location)
        {
            Element shopsLocationTitle = driver.FindElementByXpath("//h2[@class='offline-store__city']");
            Element txtShopsLocation = driver.FindElementByXpath("//input[@id='city']");
            txtShopsLocation.Click();
            txtShopsLocation.SendText(location);
            string before = GetPageTitle();
            driver.WaitUntil(e => !before.Equals(GetPageTitle()));
        }

        public string GetPageTitle() => driver.FindElementByXpath("//h2[@class='offline-store__city']").GetText();
    }
}
