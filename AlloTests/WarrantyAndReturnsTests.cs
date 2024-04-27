using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace AlloTests
{
    public class WarrantyAndReturnsTests : BaseTest
    {
        [Test]
        public void VerifyManufacturersShopsSelectionAlphabetically()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string alphabeticalСategory = config["alphabeticalСategory"];

            Header header = new Header(driver);
            header.SelectForCustomersDropdown();
            header.SelectForCustomersDropdownVariant(config["customersDropdownVariant"]);
            WarrantyAndReturnsPage warrantyAndReturnsPage = new WarrantyAndReturnsPage(driver);
            warrantyAndReturnsPage.SelectWarrantyAndReturnsTab(config["warrantyAndReturnsTab"]);
            warrantyAndReturnsPage.SelectManufacturersWebsiteAlphabetically(config["alphabeticalСategory"]);

            List<string> manufacturersShops = warrantyAndReturnsPage.GetManufacturerWebsites();
            foreach (var shop in manufacturersShops)
            {
                StringAssert.StartsWith(alphabeticalСategory.ToLower(), shop.ToLower(), $"The name {shop} of the manufacturer's shop does not start with {alphabeticalСategory} a category");
            }
        }

        [Test]
        public void VerifyManufacturersShopSelectionByName()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string shopName = config["shopName"];

            Header header = new Header(driver);
            header.SelectForCustomersDropdown();
            header.SelectForCustomersDropdownVariant(config["customersDropdownVariant"]);
            WarrantyAndReturnsPage warrantyAndReturnsPage = new WarrantyAndReturnsPage(driver);
            warrantyAndReturnsPage.SelectWarrantyAndReturnsTab(config["warrantyAndReturnsTab"]);
            warrantyAndReturnsPage.SelectManufacturersWebsiteAlphabetically(config["alphabeticalСategory"]);
            warrantyAndReturnsPage.SelectManufacturersWebsite(config["shopName"]);
            driver.SwitchToTab(1);

            StringAssert.Contains(shopName.ToLower(), warrantyAndReturnsPage.GetManufacturerWebsiteName().ToLower(), "Another page of the manufacturer's store is displayed");
        }
    }
}
