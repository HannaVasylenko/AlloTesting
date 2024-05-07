using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate Vacancies page where the user can find out the employer's vacancies
    /// </summary>
    public class VacanciesPage : BasePage
    {
        public VacanciesPage(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Selects a specified dropdown by a specified name
        /// </summary>
        /// <param name="ddlVariant">The ame of the dropdown to select</param>
        public void SelectDropdown(string ddlVariant) => driver.FindElementByXpath($"//span[contains(text(), '{ddlVariant}')]").Click();

        /// <summary>
        /// Selects a specific item from the dropdown list.
        /// </summary>
        /// <param name="ddlVariant">The ame of the dropdown to select</param>
        /// <param name="ddlListVariant">The name of the variant to select from the dropdown list.</param>
        public void SelectVariantFromDropdownList(string ddlVariant, string ddlListVariant) => driver.FindElementByXpath($"//div[@data-filter='{ddlVariant}_spec']//ul/li[text()='{ddlListVariant}']").Click();

        /// <summary>
        /// Selects a vacancy by its name.
        /// </summary>
        /// <param name="vacancyName">The name of the vacancy to select.</param>
        public void SelectVacancy(string vacancyName) => driver.FindElementByXpath($"//div[@class='awsm-job-listings awsm-row awsm-grid-col-3']/div//h2[contains(text(), '{vacancyName}')]");

        /// <summary>
        /// Gets the name of the selected vacancy.
        /// </summary>
        /// <returns>The name of the selected vacancy.</returns>
        public string GetSelectedVacancyName() => driver.FindElementByXpath("//h1").GetText();
    }
}
