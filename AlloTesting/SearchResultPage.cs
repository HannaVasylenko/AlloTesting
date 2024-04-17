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
    public class SearchResultPage : BasePage
    {
        public SearchResultPage(Driver webDriver) : base(webDriver) { }

        public void SelectFilterVariant(string filterName, string filterItem) => driver.FindElementByXpath($"//h3[text()='{filterName}']/ancestor::section[@class='f-content']//a[contains(text(), '{filterItem}')]").Click();

        public void SubmitSearchResult()
        {
            Element btnSubmit = driver.FindElementByXpath("//aside//button[@class='f-popup__btn']/span[contains(text(), 'Показати')]");
            driver.WaitUntil(e => btnSubmit.IsDisplayed());
            btnSubmit.Click();
        }

        public Dictionary<string, double> GetProductsTitlesAndPrices()
        {
            Dictionary<string, double> productsDetails = new Dictionary<string, double>();

            try
            {
                List<Element> productList = driver.FindElementsByXpath("//div[@class='product-card__content']").ToList();
                foreach (var product in productList)
                {
                    string title = product.FindElementByXpath("./a").GetText();
                    string price;

                    try
                    {
                        price = product.FindElementByXpath(".//div[@class='v-pb__cur discount']/span[@class='sum']").GetText().Replace(" ", "");
                    }
                    catch (NoSuchElementException)
                    {
                        try
                        {
                            price = product.FindElementByXpath(".//div[@class='v-pb__cur']/span[@class='sum']").GetText().Replace(" ", "");
                        }
                        catch (NoSuchElementException)
                        {
                            price = "0";
                        }
                    }

                    productsDetails.Add(title, Convert.ToDouble(price));
                }
            }
            catch (StaleElementReferenceException)
            {
                productsDetails.Clear();
            }
            return productsDetails;
        }

        public void InputFilterPrice(double minPrice, double maxPrice)
        {
            Element txtPriceFieldMin = driver.FindElementByXpath("//form[@data-range-filter='price']/input[1]");
            Element txtPriceFieldMax = driver.FindElementByXpath("//form[@data-range-filter='price']/input[2]");

            txtPriceFieldMin.Clear();
            txtPriceFieldMin.Click();
            txtPriceFieldMin.SendText(minPrice.ToString());

            txtPriceFieldMax.Clear();
            txtPriceFieldMax.Click();
            txtPriceFieldMax.SendText(maxPrice.ToString());
        }
    }
}
