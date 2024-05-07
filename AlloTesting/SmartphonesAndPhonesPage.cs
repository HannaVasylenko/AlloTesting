using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate page for the Smartphones and phones category
    /// </summary>
    public class SmartphonesAndPhonesPage : BasePage
    {
        public SmartphonesAndPhonesPage(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Gets the title of the selected category.
        /// </summary>
        /// <returns>The title of the selected category.</returns>
        public string GetSelectedCategoryTitle() => driver.FindElementByXpath("//h1").GetText();

        /// <summary>
        /// Selects a subcategory item within a specified subcategory.
        /// </summary>
        /// <param name="subCategory">The name of the subcategory.</param>
        /// <param name="subCategoryItem">The item to select within the subcategory.</param>
        public void SelectSubCategory(string subCategory, string subCategoryItem) => driver.FindElementByXpath($"//h3[contains(text(), '{subCategory}')]/ancestor::div[@class='portal__navigation']//li[@class='portal-category__item']/a[text()='{subCategoryItem}']").Click();
    }
}
