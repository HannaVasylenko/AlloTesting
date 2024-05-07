using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays the search result after certain actions.
    /// </summary>
    public class SearchResultPage : BasePage
    {
        public Cart cart => new Cart(driver);

        public SearchResultPage(Driver webDriver) : base(webDriver) { }

        private int clickCount = 0;

        /// <summary>
        /// Gets the names of all products displayed on the page.
        /// </summary>
        /// <returns>The list of product names.</returns>
        public List<string> GetProductNames() => driver.FindElementsByXpath("//div[@class='products-layout__container products-layout--grid']//div[@class='product-card__content']/a")
                    .ToList()
                    .ConvertAll(e => e.GetAttribute("title"));

        /// <summary>
        /// Selects a filter variant for the specified filter.
        /// </summary>
        /// <param name="filterName">The name of the filter.</param>
        /// <param name="filterItem">The filter variant to select.</param>
        public void SelectFilterVariant(string filterName, string filterItem) => driver.FindElementByXpath($"//h3[text()='{filterName}']/ancestor::section[@class='f-content']//a[contains(text(), '{filterItem}')]").Click();

        /// <summary>
        /// Submits the search result.
        /// </summary>
        public void SubmitSearchResult()
        {
            Element btnSubmit = driver.FindElementByXpath("//aside//button[@class='f-popup__btn']/span[contains(text(), 'Показати')]");
            driver.WaitUntil(e => btnSubmit.IsDisplayed());

            btnSubmit.Click();
        }

        /// <summary>
        /// Gets the titles and prices of all products displayed on the page.
        /// </summary>
        /// <returns>A dictionary containing product titles as keys and their corresponding prices as values.</returns>
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

        /// <summary>
        /// Inputs the minimum and maximum price for filtering.
        /// </summary>
        /// <param name="minPrice">The minimum price.</param>
        /// <param name="maxPrice">The maximum price.</param>
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

        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        /// <param name="productName">The name of the product to add.</param>
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

        /// <summary>
        /// Selects a product to compare.
        /// </summary>
        /// <param name="productName">The name of the product to compare.</param>
        public void SelectProductToCompare(string productName)
        {
            int selectedProductsToCompare = 0;
            if (driver.IsElementExists(By.XPath("//span[@class='c-counter__text']")))
            {
                selectedProductsToCompare = int.Parse(driver.FindElementByXpath("//span[@class='c-counter__text']").GetText());
            }

            driver.WaitUntil(e => {
                Element btnAddProductToCompare = driver.FindElementByXpath($"//div[@class='product-card__content']/a[@title='{productName}']/..//div[@class='actions__container']//button[contains(@class, 'compare')]");
                btnAddProductToCompare.Click();
                try {
                    driver.WaitUntil(a => btnAddProductToCompare.GetAttribute("class").Contains("loading"), 4);
                }
                catch(WebDriverTimeoutException ex)
                {
                    return false;
                }
                return true;
            });
            driver.WaitUntil(e => selectedProductsToCompare != int.Parse(driver.FindElementByXpath("//span[@class='c-counter__text']").GetText()));

            clickCount++;
        }

        /// <summary>
        /// Gets the names of products in the comparison list.
        /// </summary>
        /// <returns>The list of product names in the comparison list.</returns>
        public List<string> GetProductsNamesInCompareList() => driver.FindElementsByXpath("//div[@class='compare-list']/div[@class='compare-item']//p")
                    .ToList()
                    .ConvertAll(e => e.GetText());

        /// <summary>
        /// Gets the count of products in the comparison list.
        /// </summary>
        /// <returns>The count of products in the comparison list.</returns>
        public string GetCountProductsInCompareList() => driver.FindElementByXpath("//span[@class='c-counter__text']").GetText();

        /// <summary>
        /// Gets the number of clicks on the compare button.
        /// </summary>
        /// <returns>The number of clicks on the compare button.</returns>
        public int GetNumberOfClicksOnCompareBtn() => clickCount;

        /// <summary>
        /// Selects a product card.
        /// </summary>
        /// <param name="productName">The name of the product card to select.</param>
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

        /// <summary>
        /// Gets the list of products that were viewed.
        /// </summary>
        /// <returns>The list of products that were viewed.</returns>
        public List<string> GetListOfProductsThatWereViewed()
        {
            driver.ExecuteJsCommand("window.scrollTo(0, 3500)");
            Element recentlyProducts = driver.FindElementByXpath("//div[@class='recently-products__list v-ps']");
            driver.WaitUntil(e => recentlyProducts.IsDisplayed());

            return driver.FindElementsByXpath("//div[@class='recently-products recently-products--slider v-new-design']//div[@class='v-ps__item']//a[@class='product-card__title v-pc__title']")
                    .ToList()
                    .ConvertAll(e => e.GetText());
        }

        /// <summary>
        /// Clicks on a product to buy in the shop.
        /// </summary>
        /// <returns>The title of the product clicked on.</returns>
        public string ClickOnProductToBuyInShop()
        {
            string productTitle = "";

            driver.WaitUntil(e => { 
                try
                {
                    List<Element> productsToBuyInShop = driver.FindElementsByXpath("//button[@title='Забрати в магазині Алло']/ancestor::div[@class='product-card']");
                    if (productsToBuyInShop.Count > 0)
                    {
                        Element firstProduct = productsToBuyInShop[0];
                        productTitle = firstProduct.FindElementByXpath("./div[@class='product-card__content']/a").GetText();
                        firstProduct.FindElementByXpath(".//button[@title='Забрати в магазині Алло']").Click();
                    }
                    return true;
                } catch (StaleElementReferenceException ex)
                {
                    return false;
                }
            });
            return productTitle;
        }

        /// <summary>
        /// Gets the list layout status.
        /// </summary>
        /// <returns>True if the list layout is selected; otherwise, false.</returns>
        public bool GetListLayout() => driver.IsElementExists(By.XPath("//div[@class='v-catalog__products']//div[@class='products-layout__container products-layout--list']"));

        /// <summary>
        /// Clicks on the list layout button.
        /// </summary>
        public void ClickOnListLayout()
        {
            driver.FindElementByXpath("//button[@title='Список']/span/preceding-sibling::*").Click();

            List<Element> buyBoxList = driver.FindElementsByXpath("//div[@class='product-card__side-right']");
            driver.WaitUntil(driver => buyBoxList.All(product => product.IsDisplayed()));
        }
    }
}
