using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloTests
{
    public class PromotionsTests : BaseTest
    {
        [Test]
        public void VerifySelectPromoCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectHeaderLink(config["headerLink"]);
            PromotionsPage promotionsPage = new PromotionsPage(driver);
            promotionsPage.SelectPromoCategory(config["promoCategory"]);
            
            Assert.That(promotionsPage.IsPromoCategoryActive(config["promoCategory"]), Is.True, "Another promotion category is selected");
        }
    }
}
