using AlloTesting;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class SmartphonesAndPhonesPage : BasePage
    {
        public SmartphonesAndPhonesPage(Driver webDriver) : base(webDriver) {}

        public string GetPageTitle() => driver.FindElementByXpath("//h1").GetText();
    }
}
