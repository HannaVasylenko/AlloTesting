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
            initialPage.InputDataInSearchField(config["productName"]);
            
            List<string> productNames = initialPage.GetProductNames();
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
            initialPage.InputDataInSubscriptionEmailField(config["email"]);
            
            StringAssert.AreEqualIgnoringCase(emailErrorMessage, initialPage.GetEmailErrorMessage(), $"The error message {emailErrorMessage} is not displayed");
        }

        [Test]
        public void VerifyTransitionToSocialMediaPages()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string websiteNameEng = config["websiteNameEng"];
            string websiteNameUkr = config["websiteNameUkr"];


            InitialPage initialPage = new InitialPage(driver);
            initialPage.GoToSocialMediaPages("facebook");
            driver.SwitchToTab(1);
            string facebook = initialPage.GetFacebookAccountName();
            driver.SwitchToTab(0);
            initialPage.GoToSocialMediaPages("instagram");
            driver.SwitchToTab(2);
            string instagram = initialPage.GetInstagramAccountName();
            
            StringAssert.AreEqualIgnoringCase(websiteNameUkr, facebook, "A different page is displayed");
            StringAssert.AreEqualIgnoringCase(websiteNameEng, instagram, "A different page is displayed");
        }

        [Test]
        public void VerifySelectCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string categoryName = config["categoryName"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectCategory(config["categoryName"]);
            SmartphonesAndPhonesPage smartphonesAndPhonesPage = new SmartphonesAndPhonesPage(driver);
            
            Assert.That(smartphonesAndPhonesPage.GetPageTitle(), Is.EqualTo(categoryName), "Another page is displayed");
        }

        [Test]
        public void VerifySelectLocation()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string cityName = config["city"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectLocation();
            initialPage.InputDataInSearchLocationField(config["city"]);
            initialPage.SelectLocationVariant(config["city"]);

            Assert.That(initialPage.GetLocationOfWebsite(), Is.EqualTo(cityName), "Another city is selected");
        }

        [Test]
        public void VerifyLoginByEnteringPhoneNumber()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string loginErrorMessage = config["loginErrorMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.ClickOnProfileBtn();
            initialPage.InputDataInLoginPhoneNumberField(config["phoneNumber"]);
            initialPage.ClickOnLoginBtn();

            Assert.That(initialPage.GetLoginErrorMessage(), Is.EqualTo(loginErrorMessage), $"The error message {loginErrorMessage} is not displayed");
        }

        [Test]
        public void VerifyLoginByEnteringEmailAndPassword()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string loginErrorMessage = config["emailErrorMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.ClickOnProfileBtn();
            initialPage.ClickOnLoginWithEmailAndPasswordBtn();
            initialPage.InputDataInLoginWithEmailAndPhoneFields(config["email"], config["password"]);
            initialPage.ClickOnLoginBtn();

            Assert.That(initialPage.GetLoginErrorMessage(), Is.EqualTo(loginErrorMessage), $"The error message {loginErrorMessage} is not displayed");
        }
    }
}
