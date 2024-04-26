using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloTests
{
    public class SearchResultTests : BaseTest
    {
        [Test]
        public void VerifySearchResultByBrand()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string brandName = config["subCategotyItem"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SelectFilterVariant(config["filterName2"], config["filterItem2"]);
            searchResultPage.SubmitSearchResult();

            var searchResultDetails = searchResultPage.GetProductsTitlesAndPrices();
            foreach (var item in searchResultDetails)
            {
                StringAssert.Contains(brandName.ToLower(), item.Key.ToLower(), $"Brand name {brandName} is missing from the product title");
            }
        }

        [Test]
        public void VerifySearchResultByPrice()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            double minPrice = double.Parse(config["minPrice"]);
            double maxPrice = double.Parse(config["maxPrice"]);


            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.InputFilterPrice(minPrice, maxPrice);
            searchResultPage.SubmitSearchResult();

            var searchResultDetails = searchResultPage.GetProductsTitlesAndPrices();
            foreach (var item in searchResultDetails)
            {
                Assert.That(item.Value, Is.LessThanOrEqualTo(maxPrice), $"Product {item.Key} price {item.Value} is more than the maximum price");
                Assert.That(item.Value, Is.GreaterThanOrEqualTo(minPrice), $"Product {item.Key} price {item.Value} is less than the minimum price");
            }
        }

        [Test]
        public void VerifyPresenceOfProductInCompareList() 
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName1 = config["productTitle"];
            string productName2 = config["productTitle2"];


            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.SelectProductToCompare(config["productTitle"]);
            searchResultPage.SelectProductToCompare(config["productTitle2"]);
            List <string> compareList = searchResultPage.GetProductsNamesInCompareList();
            
            CollectionAssert.Contains(compareList, productName1, $"The product {productName1} is not in the comparison list");
            CollectionAssert.Contains(compareList, productName2, $"The product {productName2} is not in the comparison list");
        }

        [Test]
        public void VerifyCountProductsInCompareList() 
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.SelectProductToCompare(config["productTitle"]);
            searchResultPage.SelectProductToCompare(config["productTitle2"]);

            Assert.That(int.Parse(searchResultPage.GetCountProductsInCompareList()), Is.EqualTo(searchResultPage.GetNumberOfClicksOnCompareBtn()), "Added another number of products to the comparison list");
        }

        [Test]
        public void VerifyProductsThatWereViewed()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName1 = config["productTitle"];
            string productName2 = config["productTitle2"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.SelectProductCard(config["productTitle"]);
            driver.GoBackToPreviousPage();
            searchResultPage.SelectProductCard(config["productTitle2"]);
            driver.GoBackToPreviousPage();
            List<string> listOfProductsThatWereViewed = searchResultPage.GetListOfProductsThatWereViewed();

            CollectionAssert.Contains(listOfProductsThatWereViewed, productName1, $"The product {productName1} is not in the list of products you viewed");
            CollectionAssert.Contains(listOfProductsThatWereViewed, productName2, $"The product {productName2} is not in the list of products you viewed");
        }

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
            searchResultPage.SelectProductColor(config["productColor"]);
            
            Assert.That(searchResultPage.GetSelectedProductColor(), Is.EqualTo(productColor), $"The color {productColor} does not match the selected one");
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

            Assert.That(searchResultPage.GetPageTitle(), Is.EqualTo(productName), $"The product {productName} does not match the selected one");
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
            searchResultPage.ClickOnTooltip();

            StringAssert.Contains(tooltipMessage.ToLower(), searchResultPage.GetTooltipMessage().ToLower(), $"The tooltip message {tooltipMessage} is not displayed");
        }


        [Test]
        public void VerifyChangeListLayout()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            smartphonesAndPhonesPage.SelectSubCategory(config["subCategory"], config["subCategotyItem"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.SelectFilterVariant(config["filterName1"], config["filterItem1"]);
            searchResultPage.SubmitSearchResult();
            searchResultPage.ClickOnListLayout();
            
            Assert.That(searchResultPage.GetListLayout(), Is.True, "Different product list layout is displayed");
        }
    }
}
