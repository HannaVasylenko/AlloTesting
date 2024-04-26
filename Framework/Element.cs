using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class Element
    {
        public IWebElement element { get; set; }

        public Element(IWebElement element) => this.element = element;

        public void Click() => element.Click();

        public void Clear() => element.Clear();

        public bool IsDisplayed() => element.Displayed;

        public bool IsSelected() => element.Selected;

        public bool IsEnabled() => element.Enabled;

        public void SendText(string text) => element.SendKeys(text);

        public Element FindElementByXpath(string xpath) => new(element.FindElement(By.XPath(xpath)));

        public string GetText() => element.Text;
        
        public string GetAttribute(string attribute) => element.GetAttribute(attribute);
    }
}
