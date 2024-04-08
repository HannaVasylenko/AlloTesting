using AlloTesting;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class InitialPage : BasePage
    {
        public InitialPage(Driver driver) : base(driver)
        {

        }

        private Element txtSearchField => driver.FindElementByXpath("//input[@id='search-form__input']");

        public void ClickOnSearchField()
        {
            txtSearchField.Click();
        }

    }
}
