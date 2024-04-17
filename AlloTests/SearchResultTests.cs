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
    }
}
