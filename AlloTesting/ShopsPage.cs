using AlloTesting;
using Framework;

namespace AlloPageObjects
{
    public class ShopsPage : BasePage
    {
        public ShopsPage(Driver webDriver) : base(webDriver) {}

        public void InputDataInSearchShopsField(string location)
        {
            string defaultCity = GetSelectedCityName();
            Element txtShopsLocation = driver.FindElementByXpath("//input[@id='city']");
            txtShopsLocation.Click();
            txtShopsLocation.SendText(location);
            driver.WaitUntil(e => !defaultCity.Equals(GetSelectedCityName()));
        }

        public string GetSelectedCityName() => driver.FindElementByXpath("//h2[@class='offline-store__city']").GetText();
    }
}
