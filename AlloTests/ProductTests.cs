using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace AlloTests
{
    public class ProductTests : BaseTest
    {
        [Test]
        public void VerifySelectProductColor()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productColor = config["productColor"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.SelectProductCard(config["productTitle"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.SelectProductColor(config["productColor"]);

            Assert.That(productPage.GetSelectedProductColor(), Is.EqualTo(productColor), $"The color {productColor} does not match the selected one");
        }

        [Test]
        public void VerifySelectProductToBuyInShop()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            string productName = searchResultPage.ClickOnProductToBuyInShop();
            ProductPage productPage = new ProductPage(driver);

            Assert.That(productPage.GetPageTitleAfterClickingOnProduct(), Is.EqualTo(productName), $"The product {productName} does not match the selected one");
        }

        [Test]
        public void VerifyAppearanceOfTheTooltipText()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string tooltipMessage = config["tooltipMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.SelectProductCard(config["productTitle"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.ClickOnTooltip();

            StringAssert.Contains(tooltipMessage.ToLower(), productPage.GetTooltipMessage().ToLower(), $"The tooltip message {tooltipMessage} is not displayed");
        }
    }
}
