using AlloTesting;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class VacanciesPage : BasePage
    {
        public VacanciesPage(Driver webDriver) : base(webDriver) {}

        public void SelectDropdown(string ddlVariant) => driver.FindElementByXpath($"//span[contains(text(), '{ddlVariant}')]").Click(); // місто, департамент, напрямок

        public void SelectVariantFromDropdownList(string ddlVariant, string ddlListVariant) => driver.FindElementByXpath($"//div[@data-filter='{ddlVariant}_spec']//ul/li[text()='{ddlListVariant}']").Click(); // city, unit, direction

        public void SelectVacancy(string vacancyName) => driver.FindElementByXpath($"//div[@class='awsm-job-listings awsm-row awsm-grid-col-3']/div//h2[contains(text(), '{vacancyName}')]");

        public string GetPageTitle() => driver.FindElementByXpath("//h1").GetText(); //*
    }
}
