using AlloPageObjects;
using NUnit.Framework;
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
        public void CheckDataEntryInSearchField()
        {
            InitialPage initialPage = new InitialPage(driver);
            initialPage.ClickOnSearchField();
        }
    }
}
