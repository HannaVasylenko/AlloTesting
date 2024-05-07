using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a page footer that is displayed on multiple pages.
    /// </summary>
    public class Footer : BasePage
    {
        public Footer(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Inputs the provided email into the subscription email field and submits.
        /// </summary>
        /// <param name="email">The email to input into the subscription field.</param>
        public void InputDataInSubscriptionEmailField(string email)
        {
            Element txtEmail = driver.FindElementByXpath("//input[@name='email']");
            txtEmail.Click();
            txtEmail.SendText(email);
            txtEmail.SendText(Keys.Enter);
        }

        /// <summary>
        /// Retrieves the error message displayed for the email input field.
        /// </summary>
        /// <returns>The error message displayed for the email input field.</returns>
        public string GetEmailErrorMessage() => driver.FindElementByXpath("//span[@class='a-input__message base-message is-error']").GetText();

        /// <summary>
        /// Navigates to the social media page specified by the platform.
        /// </summary>
        /// <param name="platform">The social media platform to navigate to.</param>
        public void GoToSocialMediaPages(string platform) => driver.FindElementByXpath($"//a[@aria-label='{platform}']").Click();

        /// <summary>
        /// Selects and clicks the footer link with the provided text.
        /// </summary>
        /// <param name="link">The name of the footer link to select.</param>
        public void SelectFooterLink(string link) => driver.FindElementByXpath($"//div[@class='footer__main footer__wrap']//a[contains(text(), '{link}')]").Click();
    }
}
