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
    public class VacanciesTests : BaseTest
    {
        [Test]
        public void VerifySelectVacancy()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string vacancyVariant = config["vacancyVariant"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.SelectFooterLink(config["footerLink"]);
            driver.SwitchToTab(1);
            VacanciesPage vacanciesPage = new VacanciesPage(driver);
            vacanciesPage.SelectDropdown(config["dropdownVariant"]);
            vacanciesPage.SelectVariantFromDropdownList(config["ddlVariantXpath"], config["ddlListVariant"]);
            vacanciesPage.SelectVacancy(config["vacancyVariant"]);
            
            StringAssert.Contains(vacanciesPage.GetPageTitle().ToLower(), vacancyVariant.ToLower(), "Unselected vacancy is displayed");
        }
    }
}
