using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    public class Cart : BasePage
    {
        public Cart(Driver webDriver) : base(webDriver) { }

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

        public void IncreaseQuantityOfProduct(string productName)
        {
            string priceBefore = GetProductPrice(productName);
            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='title']/following-sibling::*/div[@class='qty']/label/following-sibling::*").Click();
            driver.WaitUntil(e => GetProductPrice(productName) != priceBefore);
        }

        public void DecreaseQuantityOfProduct(string productName)
        {
            string priceBefore = GetProductPrice(productName);
            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='title']/following-sibling::*/div[@class='qty']/label/preceding-sibling::*").Click();
            driver.WaitUntil(e => GetProductPrice(productName) != priceBefore);
        }

        public string GetProductTitle(string productName) => driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='title']//span").GetText();

        public string GetProductServicePrice(string productName, string productService) => driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product-item__wrap']//ul[@class='product-services__group-list']/li//a[contains(text(), '{productService}')]/../following-sibling::*//span[@class='service__price']").GetText().Replace(" ", "");

        public string GetOrderPrice() => driver.FindElementByXpath("//span[@class='total-box__price']").GetText().Replace(" ", "").Replace("₴", ""); //.Replace("&nbsp;", "")

        public void ClickComebackBtn()
        {
            driver.FindElementByXpath("//button[@class='comeback']").Click();

            Element cartWindow = driver.FindElementByXpath("//div[@class='v-modal__modal-overlay']");
            driver.WaitUntil(e => !cartWindow.IsDisplayed());
        }

        public void ExpandProductServices(string productName)
        {
            Element productList = driver.FindElementByXpath("//ul[@class='products__list']");
            driver.WaitUntil(e => productList.IsDisplayed());

            driver.FindElementByXpath($"//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product-item__wrap']//span[@class='product-services__expand-text']").Click();
        }

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

        public string GetProductPrice(string productName) => driver.FindElementByXpath($"//ul[@class='products__list']/li//span[contains(text(), '{productName}')]/ancestor::*/div[@class='product__item']//div[@class='price-box__cur']").GetText().Replace(" ", "").Replace("₴", "");

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

        public string GetEmptyCartMessage() => driver.FindElementByXpath("//div[@class='cart-popup_empty']/p/a/../preceding-sibling::*").GetText();
    }
}
