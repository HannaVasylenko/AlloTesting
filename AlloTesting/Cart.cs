using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays the Cart window. 
    /// </summary>
    public class Cart : BasePage
    {
        public Cart(Driver webDriver) : base(webDriver) { }

        /// <summary>
        /// Deletes a product from the shopping cart based on its name.
        /// </summary>
        /// <param name="productName">The name of the product to be deleted.</param>
        public void DeleteProductFromCart(string productName)
        {
            Element deleteButton = driver.FindElementByXpath($"//div[@class='product-item__wrap']//span[contains(text(), '{productName}')]/ancestor::div[@class='title']/a/following-sibling::*/child::*");
            driver.WaitUntil(e => {
                try
                {
                    deleteButton.Click();
                    return true;
                }
                catch (ElementClickInterceptedException ex)
                {
                    return false;
                }
            });
            driver.WaitUntil(v =>
            {
                try
                {
                    List<Element> productList = driver.FindElementsByXpath("//ul[@class='products__list']/li//span[@class='wrap']");
                    bool result = true;
                    foreach (var e in productList)
                    {
                        if (e.GetText().Equals(productName))
                        {
                            result = false;
                            break;
                        }
                    }
                    return result;
                } catch (StaleElementReferenceException ex)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Increases the quantity of a product in the shopping cart based on its name.
        /// </summary>
        /// <param name="productName">The name of the product whose quantity will be increased.</param>
        public void IncreaseQuantityOfProduct(string productName)
        {
            string priceBefore = GetProductPrice(productName);
            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='title']/following-sibling::*/div[@class='qty']/label/following-sibling::*").Click();
            driver.WaitUntil(e => GetProductPrice(productName) != priceBefore);
        }

        /// <summary>
        /// Decreases the quantity of a product in the shopping cart based on its name.
        /// </summary>
        /// <param name="productName">The name of the product whose quantity will be decreased.</param>
        public void DecreaseQuantityOfProduct(string productName)
        {
            string priceBefore = GetProductPrice(productName);
            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='title']/following-sibling::*/div[@class='qty']/label/preceding-sibling::*").Click();
            driver.WaitUntil(e => GetProductPrice(productName) != priceBefore);
        }

        /// <summary>
        /// Retrieves the title of a product based on its name.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <returns></returns>
        public string GetProductTitle(string productName) => driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='title']//span").GetText();

        /// <summary>
        /// Retrieves the price of a specific service associated with a product.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="productService">The name of the service associated with the product.</param>
        /// <returns>The price of the specified service.</returns>
        public string GetProductServicePrice(string productName, string productService) => driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product-item__wrap']//ul[@class='product-services__group-list']/li//a[contains(text(), '{productService}')]/../following-sibling::*//span[@class='service__price']").GetText().Replace(" ", "");

        /// <summary>
        /// Retrieves the total price of the order.
        /// </summary>
        /// <returns>The total price of the order.</returns>
        public string GetOrderPrice() => driver.FindElementByXpath("//span[@class='total-box__price']").GetText().Replace(" ", "").Replace("₴", ""); //.Replace("&nbsp;", "")

        /// <summary>
        /// Clicks the Button to return and waits for the cart window to close.
        /// </summary>
        public void ClickComebackBtn()
        {
            driver.FindElementByXpath("//button[@class='comeback']").Click();

            Element cartWindow = driver.FindElementByXpath("//div[@class='v-modal__modal-overlay']");
            driver.WaitUntil(e => !cartWindow.IsDisplayed());
        }

        /// <summary>
        /// Expands the list of services associated with a product.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        public void ExpandProductServices(string productName)
        {
            Element productList = driver.FindElementByXpath("//ul[@class='products__list']");
            driver.WaitUntil(e => productList.IsDisplayed());

            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product-item__wrap']//span[@class='product-services__expand-text']").Click();
        }

        /// <summary>
        /// Selects a specific service for a given product.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="productService">The name of the service to select.</param>
        public void SelectProductService(string productName, string productService)
        {
            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product-item__wrap']//ul[@class='product-services__group-list']/li//a[contains(text(), '{productService}')]/../preceding-sibling::*").Click();

            Element selectedProductService = driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::div[@class='product-item__wrap']//a[contains(text(), '{productService}')]/../preceding-sibling::*/input/following-sibling::*");
            driver.WaitUntil(driver =>
            {
                string e = selectedProductService.GetAttribute("class");
                return e.Contains("active");
            });
        }

        /// <summary>
        /// Retrieves the price of a given product.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <returns>The price of the product.</returns>
        public string GetProductPrice(string productName) => driver.FindElementByXpath($"//ul[@class='products__list']/li//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product__item']//div[@class='price-box__cur']").GetText().Replace(" ", "").Replace("₴", "");

        /// <summary>
        /// Calculates the sum of prices of all products in the cart.
        /// </summary>
        /// <returns>The total sum of prices of all products in the cart.</returns>
        public double GetSumProductsPrices()
        {
            double orderPrice = 0.0;
            driver.WaitUntil(e => driver.FindElementsByXpath("//ul[@class='products__list']/li").ToList().Count != 0);

            List<Element> productsPricesInCart = driver.FindElementsByXpath("//ul[@class='products__list']/li").ToList();

            foreach (var product in productsPricesInCart)
            {
                string price = product.FindElementByXpath(".//div[@class='price-box__cur']").GetText().Replace(" ", "").Replace("₴", "");
                orderPrice += double.Parse(price);
            }
            return orderPrice;
        }

        /// <summary>
        /// Retrieves the message displayed when the cart is empty.
        /// </summary>
        /// <returns>The message indicating that the cart is empty.</returns>
        public string GetEmptyCartMessage() => driver.FindElementByXpath("//div[@class='cart-popup_empty']/p/a/../preceding-sibling::*").GetText();
    }
}
