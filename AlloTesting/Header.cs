using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a page header that is displayed on multiple pages.
    /// </summary>
    public class Header : BasePage
    {
        public Header(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Inputs the provided text into the search field and submits the search.
        /// </summary>
        /// <param name="text">The text to input into the search field.</param>
        public void InputDataInSearchField(string text)
        {
            Element txtSearchField = driver.FindElementByXpath("//input[@id='search-form__input']");
            txtSearchField.Click();
            txtSearchField.SendText(text);
            txtSearchField.SendText(Keys.Enter);
        }

        /// <summary>
        /// Selects the specified variant from the "For Customers" dropdown menu.
        /// </summary>
        /// <param name="variant">The variant to select from the dropdown menu.</param>
        public void SelectForCustomersDropdownVariant(string variant) => driver.FindElementByXpath($"//div[@class='mh-button__dropdown']/a[contains(text(), '{variant}')]").Click();

        /// <summary>
        /// Opens the "For Customers" dropdown menu.
        /// </summary>
        public void SelectForCustomersDropdown() => driver.FindElementByXpath("//div[@class='mh-button__wrap']").Click();

        /// <summary>
        /// Selects the header link with the specified text.
        /// </summary>
        /// <param name="link">The name of the header link to select.</param>
        public void SelectHeaderLink(string link) => driver.FindElementByXpath($"//div[@class='mh-links']/a[contains(text(), '{link}')]").Click();

        /// <summary>
        /// Opens the location selection window.
        /// </summary>
        public void SelectLocation() => driver.FindElementByXpath("//div[@class='mh-loc']/button").Click();

        /// <summary>
        /// Inputs the provided location into the search location field.
        /// </summary>
        /// <param name="location">The location (city) to input into the search location field.</param>
        public void InputDataInSearchLocationField(string location)
        {
            Element txtLocation = driver.FindElementByXpath("//input[@id='city']");
            txtLocation.Click();
            txtLocation.SendText(location);
        }

        /// <summary>
        /// Selects the specified city variant from the dropdown list.
        /// </summary>
        /// <param name="city">The city variant to select.</param>
        public void SelectLocationVariant(string city) => driver.FindElementByXpath($"//li[@title='{city}']").Click();

        /// <summary>
        /// Retrieves the location (city) of the website.
        /// </summary>
        /// <returns>The location of the website (city) as a string.</returns>
        public string GetLocationOfWebsite()
        {
            Element txtActualLocation = driver.FindElementByXpath("//span[@class='mh-loc__label']");
            driver.WaitUntil(e => txtActualLocation.IsDisplayed());

            return txtActualLocation.GetText();
        }

        /// <summary>
        /// Retrieves the error message displayed for login.
        /// </summary>
        /// <returns>The login error message as a string.</returns>
        public string GetLoginErrorMessage() => driver.FindElementByXpath("//span[@class='a-input__message base-message is-error']").GetText();

        /// <summary>
        /// Inputs the provided phone number into the login phone number field.
        /// </summary>
        /// <param name="phoneNumber">The phone number to input.</param>
        public void InputDataInLoginPhoneNumberField(string phoneNumber) => driver.FindElementByXpath("//input[@name='telephone']").SendText(phoneNumber);

        /// <summary>
        /// Clicks on the login button.
        /// </summary>
        public void ClickOnLoginBtn() => driver.FindElementByXpath("//span[contains(text(), 'Увійти')]").Click();

        /// <summary>
        /// Clicks on the profile button.
        /// </summary>
        public void ClickOnProfileBtn() => driver.FindElementByXpath("//button[@aria-label='Профіль']").Click();

        /// <summary>
        /// Clicks on the login with email and password button.
        /// </summary>
        public void ClickOnLoginWithEmailAndPasswordBtn() => driver.FindElementByXpath("//span[contains(text(), 'Логін та пароль')]").Click();

        /// <summary>
        /// Inputs the provided email and password into the respective fields.
        /// </summary>
        /// <param name="email">The email to input.</param>
        /// <param name="password">The password to input.</param>
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
