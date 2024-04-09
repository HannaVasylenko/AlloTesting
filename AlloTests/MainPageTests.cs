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
    public class MainPageTests : BaseTest
    {
        [Test]
        public void VerifyInputDataInSearchField()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName = config["productName"];

            InitialPage initialPage = new InitialPage(driver);
            initialPage.InputDataInSearchField(config["productName"]);
            
            List<string> productNames = initialPage.GetProductNames();
            foreach (var productTitle in productNames)
            {
                StringAssert.Contains(productName.ToLower(), productTitle.ToLower(), $"Product name {productName} is missing from the product title");
            }
        }
    }
}
