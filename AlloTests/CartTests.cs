using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace AlloTests
{
    public class CartTests : BaseTest
    {
        [Test]
        public void VerifyAddProductToCart()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName = config["productTitle"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.AddProductToCart(config["productTitle"]);
            Cart cart = new Cart(driver);

            Assert.That(cart.GetProductTitle(config["productTitle"]), Is.EqualTo(productName), "Added another product");
        }

        [Test]
        public void VerifyAddProductToCartByClickingComeBackBtn()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.AddProductToCart(config["productTitle"]);
            Cart cart = new Cart(driver);
            cart.ClickComebackBtn();
            searchResultPage.AddProductToCart(config["productTitle2"]);

            Assert.That(cart.GetSumProductsPrices(), Is.EqualTo(double.Parse(cart.GetOrderPrice())), "The cost of the order does not equal the sum of the cost of the products");
        }

        [Test]
        public void VerifyAddProductServices()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.AddProductToCart(config["productTitle"]);
            Cart cart = new Cart(driver);
            cart.ExpandProductServices(config["productTitle"]);
            cart.SelectProductService(config["productTitle"], config["productService1"]);
            cart.SelectProductService(config["productTitle"], config["productService2"]);
            double firstService = double.Parse(cart.GetProductServicePrice(config["productTitle"], config["productService1"]));
            double secondService = double.Parse(cart.GetProductServicePrice(config["productTitle"], config["productService2"]));
            double orderPricesSum = cart.GetSumProductsPrices() + firstService + secondService;

            Assert.That(orderPricesSum, Is.EqualTo(double.Parse(cart.GetOrderPrice())), "The cost of the order does not equal the sum of the cost of the products");
        }

        [Test]
        public void VerifyIncreaseQuantityOfProduct()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.AddProductToCart(config["productTitle"]);
            Cart cart = new Cart(driver);
            cart.IncreaseQuantityOfProduct(config["productTitle"]);
            cart.IncreaseQuantityOfProduct(config["productTitle"]);

            Assert.That(cart.GetSumProductsPrices(), Is.EqualTo(double.Parse(cart.GetOrderPrice())), "The cost of the order does not match the increase in the number of products");
        }

        [Test]
        public void VerifyDecreaseQuantityOfProduct()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.AddProductToCart(config["productTitle"]);
            Cart cart = new Cart(driver);
            cart.IncreaseQuantityOfProduct(config["productTitle"]);
            cart.IncreaseQuantityOfProduct(config["productTitle"]);
            cart.DecreaseQuantityOfProduct(config["productTitle"]);

            Assert.That(cart.GetSumProductsPrices(), Is.EqualTo(double.Parse(cart.GetOrderPrice())), "The cost of the order does not match the decrease in the number of products");
        }

        [Test]
        public void VerifyDeleteProductFromCart()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string emptyCartMessage = config["emptyCartMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.AddProductToCart(config["productTitle"]);
            Cart cart = new Cart(driver);
            cart.ClickComebackBtn();
            searchResultPage.AddProductToCart(config["productTitle2"]);
            cart.DeleteProductFromCart(config["productTitle"]);
            cart.DeleteProductFromCart(config["productTitle2"]);

            Assert.That(cart.GetEmptyCartMessage(), Is.EqualTo(emptyCartMessage), "The cart is not empty");
        }
    }
}
