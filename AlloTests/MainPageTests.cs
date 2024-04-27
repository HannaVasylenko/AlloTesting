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
    public class MainPageTests : BaseTest
    {
        [Test]
        public void VerifyInputDataInSearchField()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName = config["productName"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.header.InputDataInSearchField(config["productName"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            List<string> productNames = searchResultPage.GetProductNames();
            foreach (var productTitle in productNames)
            {
                StringAssert.Contains(productName.ToLower(), productTitle.ToLower(), $"Product name {productName} is missing from the product title");
            }
        }

        [Test]
        public void VerifyWebsiteSubscription()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string emailErrorMessage = config["emailErrorMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.footer.InputDataInSubscriptionEmailField(config["email"]);
            
            StringAssert.AreEqualIgnoringCase(emailErrorMessage, initialPage.footer.GetEmailErrorMessage(), $"The error message {emailErrorMessage} is not displayed");
        }

        [Test(Description = "The TEST FAILS when the Instagram page does not load when you click the Reload button")]
        public void VerifyTransitionToSocialMediaPages()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string websiteNameEng = config["websiteNameEng"];
            string websiteNameUkr = config["websiteNameUkr"];


            InitialPage initialPage = new InitialPage(driver);
            initialPage.footer.GoToSocialMediaPages("facebook");
            driver.SwitchToTab(1);
            string facebookName = initialPage.GetFacebookAccountName();
            driver.SwitchToTab(0);
            initialPage.footer.GoToSocialMediaPages("instagram");
            driver.SwitchToTab(2);
            string instagramName = initialPage.GetInstagramAccountName();
            
            StringAssert.AreEqualIgnoringCase(websiteNameUkr, facebookName, "A different page is displayed");
            StringAssert.AreEqualIgnoringCase(websiteNameEng, instagramName, "A different page is displayed");
        }

        [Test]
        public void VerifySelectLocation()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string cityName = config["city"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.header.SelectLocation();
            initialPage.header.InputDataInSearchLocationField(config["city"]);
            initialPage.header.SelectLocationVariant(config["city"]);

            Assert.That(initialPage.header.GetLocationOfWebsite(), Is.EqualTo(cityName), "Another city is selected");
        }

        [Test]
        public void VerifyLoginByEnteringPhoneNumber()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string loginErrorMessage = config["loginErrorMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.header.ClickOnProfileBtn();
            initialPage.header.InputDataInLoginPhoneNumberField(config["phoneNumber"]);
            initialPage.header.ClickOnLoginBtn();

            Assert.That(initialPage.header.GetLoginErrorMessage(), Is.EqualTo(loginErrorMessage), $"The error message {loginErrorMessage} is not displayed");
        }

        [Test]
        public void VerifyLoginByEnteringEmailAndPassword()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string loginErrorMessage = config["emailErrorMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.header.ClickOnProfileBtn();
            initialPage.header.ClickOnLoginWithEmailAndPasswordBtn();
            initialPage.header.InputDataInLoginWithEmailAndPhoneFields(config["email"], config["password"]);
            initialPage.header.ClickOnLoginBtn();

            Assert.That(initialPage.header.GetLoginErrorMessage(), Is.EqualTo(loginErrorMessage), $"The error message {loginErrorMessage} is not displayed");
        }

        [Test]
        public void CheckAbilityToViewMoreProductsInTopSalesLeadersSection()
        {
            InitialPage initialPage = new InitialPage(driver);
            int quantityOfProducts = initialPage.GetQuantityOfProducts();
            initialPage.ClickOnBtnShowMoreProducts();
            int updatedQuantityOfProducts = initialPage.GetQuantityOfProducts();
            int expectedQuantityOfProducts = quantityOfProducts + (updatedQuantityOfProducts - quantityOfProducts);
            
            Assert.That(updatedQuantityOfProducts, Is.EqualTo(expectedQuantityOfProducts), "The wrong number of products is displayed");
        }
    }
}
