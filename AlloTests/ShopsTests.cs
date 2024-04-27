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
    public class ShopsTests : BaseTest
    {
        [Test]
        public void VerifySelectShopsLocation()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string cityName = config["city"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.footer.SelectFooterLink(config["footerLink2"]);
            ShopsPage shopsPage = new ShopsPage(driver);
            shopsPage.InputDataInSearchShopsField(config["city"]);
            
            StringAssert.Contains(shopsPage.GetSelectedCityName().ToLower(), cityName.ToLower(), "Another city is selected");
        }
    }
}
