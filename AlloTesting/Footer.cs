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
    public class Footer : BasePage
    {
        public Footer(Driver webDriver) : base(webDriver) {}

        public void InputDataInSubscriptionEmailField(string email)
        {
            Element txtEmail = driver.FindElementByXpath("//input[@name='email']");
            txtEmail.Click();
            txtEmail.SendText(email);
            txtEmail.SendText(Keys.Enter);
        }

        public string GetEmailErrorMessage() => driver.FindElementByXpath("//span[@class='a-input__message base-message is-error']").GetText();

        public void GoToSocialMediaPages(string platform) => driver.FindElementByXpath($"//a[@aria-label='{platform}']").Click();

        public void SelectFooterLink(string link) => driver.FindElementByXpath($"//div[@class='footer__main footer__wrap']//a[contains(text(), '{link}')]").Click();
    }
}
