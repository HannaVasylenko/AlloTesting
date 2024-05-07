using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate Shop network page where you can find out the address of the shops
    /// </summary>
    public class ShopsPage : BasePage
    {
        public ShopsPage(Driver webDriver) : base(webDriver) {}

        /// <summary>
        /// Inputs data into the search shops field.
        /// </summary>
        /// <param name="location">The location (city) to search for shops.</param>
        public void InputDataInSearchShopsField(string location)
        {
            string defaultCity = GetSelectedCityName();
            Element txtShopsLocation = driver.FindElementByXpath("//input[@id='city']");
            txtShopsLocation.Click();
            txtShopsLocation.SendText(location);
            driver.WaitUntil(e => !defaultCity.Equals(GetSelectedCityName()));
        }

        /// <summary>
        /// Gets the selected city name.
        /// </summary>
        /// <returns>The name of the selected city.</returns>
        public string GetSelectedCityName() => driver.FindElementByXpath("//h2[@class='offline-store__city']").GetText();
    }
}
