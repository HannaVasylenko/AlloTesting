using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    public class GiftCertificatesPage : BasePage
    {
        public GiftCertificatesPage(Driver webDriver) : base(webDriver) {}

        public void InputDataInCertificatesNumberField(string number)
        {
            Element txtCertificatesNumber = driver.FindElementByXpath("//input[@name='certNumber']");
            Element btnSubmit = driver.FindElementByXpath("//button[@type='submit']/span[contains(text(), 'Перевірити')]");

            txtCertificatesNumber.Click();
            txtCertificatesNumber.SendText(number);
            btnSubmit.Click();
        }

        public string GetCertificateConfirmationErrorMessage() => driver.FindElementByXpath("//div[@class='v-modal__cmp-body']//p").GetText();
    }
}
