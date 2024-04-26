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
            string defaultCity = GetPageTitle();
            driver.WaitUntil(e => !defaultCity.Equals(GetPageTitle()));
        }

        public string GetPageTitle()
        {
            Element shopsMap = driver.FindElementByXpath("//div[@class='offline-stores-map-container']");
            driver.WaitUntil(e => shopsMap.IsDisplayed());
            return driver.FindElementByXpath("//h2[@class='offline-store__city']").GetText();
        }
    }
}
