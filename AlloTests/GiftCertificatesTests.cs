﻿using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace AlloTests
{
    public class GiftCertificatesTests : BaseTest
    {
        [Test]
        public void VerifyTheCertificateValidation()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string certificateConfirmationErrorMessage = config["certificateConfirmationErrorMessage"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.footer.SelectFooterLink(config["footerLink4"]);
            GiftCertificatesPage giftCertificatesPage = new GiftCertificatesPage(driver);
            giftCertificatesPage.InputDataInCertificatesNumberField(config["certificatesNumber"]);

            Assert.That(giftCertificatesPage.GetCertificateConfirmationErrorMessage(), Is.EqualTo(certificateConfirmationErrorMessage), $"The error message {certificateConfirmationErrorMessage} is not displayed");
        }
    }
}
