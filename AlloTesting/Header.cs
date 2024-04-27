using AlloTesting;
using Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class Header : BasePage
    {
        public Header(Driver webDriver) : base(webDriver) {}

        public void InputDataInSearchField(string text)
        {
            Element txtSearchField = driver.FindElementByXpath("//input[@id='search-form__input']");
            txtSearchField.Click();
            txtSearchField.SendText(text);
            txtSearchField.SendText(Keys.Enter);
        }

        public void SelectForCustomersDropdownVariant(string variant) => driver.FindElementByXpath($"//div[@class='mh-button__dropdown']/a[contains(text(), '{variant}')]").Click();

        public void SelectForCustomersDropdown() => driver.FindElementByXpath("//div[@class='mh-button__wrap']").Click();

        public void SelectHeaderLink(string link) => driver.FindElementByXpath($"//div[@class='mh-links']/a[contains(text(), '{link}')]").Click();

        public void SelectCompareProducts() => driver.FindElementByXpath("//header[@class='c-header']//div[@class='mh-actions']/a[@aria-label='Порівняти']").Click();

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

        public string GetLoginErrorMessage() => driver.FindElementByXpath("//span[@class='a-input__message base-message is-error']").GetText();

        public void InputDataInLoginPhoneNumberField(string phoneNumber) => driver.FindElementByXpath("//input[@name='telephone']").SendText(phoneNumber);

        public void ClickOnLoginBtn() => driver.FindElementByXpath("//span[contains(text(), 'Увійти')]").Click();

        public void ClickOnProfileBtn() => driver.FindElementByXpath("//button[@aria-label='Профіль']").Click();

        public void ClickOnLoginWithEmailAndPasswordBtn() => driver.FindElementByXpath("//span[contains(text(), 'Логін та пароль')]").Click();

        public void InputDataInLoginWithEmailAndPhoneFields(string email, string password)
        {
            Element txtEmail = driver.FindElementByXpath("//input[@name='phoneEmail']");
            Element txtPassword = driver.FindElementByXpath("//input[@name='password']");

            txtEmail.SendText(email);
            txtPassword.Click();
            txtPassword.SendText(password);
        }
    }
}
