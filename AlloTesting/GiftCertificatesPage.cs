using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate page Gift certificates
    /// </summary>
    public class GiftCertificatesPage : BasePage
    {
        public GiftCertificatesPage(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Inputs the provided certificate number into the certificates number field and submits for verification.
        /// </summary>
        /// <param name="number">The certificate number to input.</param>
        public void InputDataInCertificatesNumberField(string number)
        {
            Element txtCertificatesNumber = driver.FindElementByXpath("//input[@name='certNumber']");
            Element btnSubmit = driver.FindElementByXpath("//button[@type='submit']/span[contains(text(), 'Перевірити')]");

            txtCertificatesNumber.Click();
            txtCertificatesNumber.SendText(number);
            btnSubmit.Click();
        }

        /// <summary>
        /// Retrieves the error message displayed for the certificate confirmation.
        /// </summary>
        /// <returns>The error message displayed for the certificate confirmation.</returns>
        public string GetCertificateConfirmationErrorMessage() => driver.FindElementByXpath("//div[@class='v-modal__cmp-body']//p").GetText();
    }
}
