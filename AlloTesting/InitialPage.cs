﻿using AlloTesting;
using Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class InitialPage : BasePage
    {
        public InitialPage(Driver driver) : base(driver) {}

        private Element txtSearchField => driver.FindElementByXpath("//input[@id='search-form__input']");
        
        private List<Element> productList => driver.FindElementsByXpath("//div[@data-products-type='top']//div[@class='h-pc']");

        public void InputDataInSearchField (string text)
        {
            Element txtSearchField = driver.FindElementByXpath("//input[@id='search-form__input']");
            txtSearchField.Click();
            txtSearchField.SendText(text);
            txtSearchField.SendText(Keys.Enter);
        }

        public List<string> GetProductNames() => driver.FindElementsByXpath("//div[@class='products-layout__container products-layout--grid']//div[@class='product-card__content']/a")
                    .ToList()
                    .ConvertAll(e => e.GetAttribute("title"));

        public void ClickOnBtnAddMoreProducts() => driver.FindElementByXpath("//div[@data-products-type='top']//button[@class='h-pl__more-button']").Click();

        public void InputDataInSubscriptionEmailField(string email)
        {
            Element txtEmail = driver.FindElementByXpath("//input[@name='email']");
            txtEmail.Click();
            txtEmail.SendText(email);
            txtSearchField.SendText(Keys.Enter);
        }

        public string GetEmailErrorMessage() => driver.FindElementByXpath("//span[@class='a-input__message base-message is-error']").GetText();

        public void GoToSocialMediaPages(string platform) => driver.FindElementByXpath($"//a[@aria-label='{platform}']").Click();

        public string GetFacebookAccountName()
        {
            if (driver.IsElementExists(By.XPath("//div[@aria-label='Close']")))
            {
                driver.FindElementByXpath("//div[@aria-label='Close']").Click();
            }
            return driver.FindElementByXpath("//h1").GetText().Replace(" ", "");
        }

        public string GetInstagramAccountName()
        {
            if (driver.IsElementExists(By.XPath("//div[text()='Reload page']")))
            {
                driver.FindElementByXpath("//div[text()='Reload page']").Click();
            }
            return driver.FindElementByXpath("//h2").GetText();
        }

        public void SelectCategory(string category) => driver.FindElementByXpath($"//li[@class='mm__item']/a[contains(normalize-space(), '{category}')]").Click();

        public void SelectLocation() => driver.FindElementByXpath("//div[@class='mh-loc']/button").Click();

        public void InputDataInSearchLocationField(string location)
        {
            Element txtLocation = driver.FindElementByXpath("//input[@id='city']");
            txtLocation.Click();
            txtLocation.SendText(location);
        }

        public void SelectLocationVariant(string city) => driver.FindElementByXpath($"//li[@title='{city}']").Click();

        public string GetLocationOfWebsite()
        {
            Element txtActualLocation = driver.FindElementByXpath("//span[@class='mh-loc__label']");
            driver.WaitUntil(e => txtActualLocation.IsDisplayed());
            return txtActualLocation.GetText();
        }
    }
}
