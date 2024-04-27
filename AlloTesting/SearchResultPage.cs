using AlloTesting;
using Framework;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class SearchResultPage : BasePage
    {
        public Cart cart => new Cart(driver);

        public SearchResultPage(Driver webDriver) : base(webDriver) { }

        private int clickCount = 0;

        public List<string> GetProductNames() => driver.FindElementsByXpath("//div[@class='products-layout__container products-layout--grid']//div[@class='product-card__content']/a")
                    .ToList()
                    .ConvertAll(e => e.GetAttribute("title"));

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

        public void AddProductToCart(string productName)
        {
            driver.WaitUntil(e =>
            {
                try
                {
                    driver.FindElementByXpath($"//a[@title='{productName}']/..//button[@title='Купити']").Click();
                    return false;
                }
                catch (Exception ex)
                {
                    return true;
                }
            });
        }

        public void SelectProductToCompare(string productName)
        {
            int selectedProductsToCompare = 0;
            if (driver.IsElementExists(By.XPath("//span[@class='c-counter__text']")))
            {
                selectedProductsToCompare = int.Parse(driver.FindElementByXpath("//span[@class='c-counter__text']").GetText());
            }

            Element btnAddProductToCompare = driver.FindElementByXpath($"//div[@class='product-card__content']/a[@title='{productName}']/..//div[@class='actions__container']//button[contains(@class, 'compare')]");
            driver.WaitUntil(e => {
                if (btnAddProductToCompare.GetAttribute("class").Contains("active"))
                {
                    return true;
                }
                btnAddProductToCompare.Click();
                return false;
            });
            driver.WaitUntil(e => selectedProductsToCompare != int.Parse(driver.FindElementByXpath("//span[@class='c-counter__text']").GetText()));

            clickCount++;
        }

        public List<string> GetProductsNamesInCompareList() => driver.FindElementsByXpath("//div[@class='compare-list']/div[@class='compare-item']//p")
                    .ToList()
                    .ConvertAll(e => e.GetText());

        public bool IsProductAddToCompare()
        {
            List<Element> btnsAddProductsToCompareList = driver.FindElementsByXpath("//div[@class='actions__container']/child::button[contains(@aria-label, 'Порівняти')]");
            foreach (var e in btnsAddProductsToCompareList)
            {
                if (e.GetAttribute("class") == "compare active")
                {
                    return true;
                }
            }
            return false;
        }

        public string GetCountProductsInCompareList() => driver.FindElementByXpath("//span[@class='c-counter__text']").GetText();

        public int GetNumberOfClicksOnCompareBtn() => clickCount;

        public void SelectProductCard(string productName)
        {
            Element titleInProductCard = driver.FindElementByXpath($"//div[@class='products-layout__container products-layout--grid']//div[@class='product-card__content']/a[@title='{productName}']");
            driver.WaitUntil(e =>
            {
                try
                {
                    titleInProductCard.Click();
                    return false;
                }
                catch (Exception ex)
                {
                    return true;
                }
            });

            Element attributesBox = driver.FindElementByXpath("//div[@class='p-attributes p-dynamic__component']");
            driver.WaitUntil(e => attributesBox.IsDisplayed());
        }

        public List<string> GetListOfProductsThatWereViewed()
        {
            driver.ExecuteJsCommand("window.scrollTo(0, 3500)");
            Element recentlyProducts = driver.FindElementByXpath("//div[@class='recently-products__list v-ps']");
            driver.WaitUntil(e => recentlyProducts.IsDisplayed());

            return driver.FindElementsByXpath("//div[@class='recently-products recently-products--slider v-new-design']//div[@class='v-ps__item']//a[@class='product-card__title v-pc__title']")
                    .ToList()
                    .ConvertAll(e => e.GetText());
        }

        public void SelectBuyProductInShop(string productName) => driver.FindElementByXpath($"//button[@title='Забрати в магазині Алло']/ancestor::div[@class='product-card__content']/a[@title='{productName}']").Click();

        public string ClickOnProductToBuyInShop()
        {
            string productTitle = "";

            driver.WaitUntil(e => { 
                try
                {
                    List<Element> productsToBuyInShop = driver.FindElementsByXpath("//button[@title='Забрати в магазині Алло']/ancestor::div[@class='product-card']");
                    Element firstProduct = productsToBuyInShop.First();
                    productTitle = firstProduct.FindElementByXpath("./div[@class='product-card__content']/a").GetText();
                    firstProduct.FindElementByXpath(".//button[@title='Забрати в магазині Алло']").Click();
                    return true;
                } catch (StaleElementReferenceException ex)
                {
                    return false;
                }
            });
            return productTitle;
        }

        public void CloseModalWindow() => driver.FindElementByXpath("//div[@class='v-modal__close-btn']").Click();

        public bool GetListLayout() => driver.IsElementExists(By.XPath("//div[@class='v-catalog__products']//div[@class='products-layout__container products-layout--list']"));

        public void ClickOnListLayout()
        {
            driver.FindElementByXpath("//button[@title='Список']/span/preceding-sibling::*").Click();

            List<Element> buyBoxList = driver.FindElementsByXpath("//div[@class='product-card__side-right']");
            driver.WaitUntil(driver => buyBoxList.All(product => product.IsDisplayed()));
        }
    }
}
