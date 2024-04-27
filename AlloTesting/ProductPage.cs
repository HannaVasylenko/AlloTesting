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
    public class ProductPage : BasePage
    {
        public ProductPage(Driver webDriver) : base(webDriver) {}

        public string GetSelectedProductColor() => driver.FindElementByXpath("//div[@class='p-attributes__content p-attributes__item is-color']//span[@class='title__label']").GetText();

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

        public string GetPageTitleAfterClickingOnProduct()
        {
            Element mapWindow = driver.FindElementByXpath("//div[@class='v-modal__cmp map_modal_container map_modal_container--with-stores']/div[@class='v-modal__close-btn']/child::*");
            if (driver.IsElementExists(By.XPath("//div[@class='v-modal__cmp map_modal_container map_modal_container--with-stores']/div[@class='v-modal__close-btn']/child::*")))
            {
                mapWindow.Click();
            }
            return driver.FindElementByXpath("//h1").GetText();
        }

        public void ClickOnTooltip() => driver.FindElementByXpath("//div[@class='v-tooltip']//i").Click();

        public string GetTooltipMessage() => driver.FindElementByXpath("//div[@class='v-tooltip__content']//p[@class='s-tooltip-content__text']").GetText();
    }
}
