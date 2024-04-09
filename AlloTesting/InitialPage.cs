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
    public class InitialPage : BasePage
    {
        public InitialPage(Driver driver) : base(driver) {}

        private Element txtSearchField => driver.FindElementByXpath("//input[@id='search-form__input']");
        
        private List<Element> productList => driver.FindElementsByXpath("//div[@class='products-layout__container products-layout--grid']/div");

        public void InputDataInSearchField (string text)
        {
            txtSearchField.Click();
            txtSearchField.SendText(text);
            txtSearchField.SendText(Keys.Enter);
        }

        public List<string> GetProductNames() => driver.FindElementsByXpath("//div[@class='products-layout__container products-layout--grid']//div[@class='product-card__content']/a")
                    .ToList()
                    .ConvertAll(e => e.GetAttribute("title"));
    }
}
