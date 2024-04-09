using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Framework
{
    public class Driver
    {
        public IWebDriver driver { get; set; }

        public Driver(IWebDriver driver) => this.driver = driver;

        public void GoToUrl(string url) => driver.Navigate().GoToUrl(url);

        public void MaximizeWindow() => driver.Manage().Window.Maximize();

        public Element FindElementByXpath(string xpath) => new(driver.FindElement(By.XPath(xpath)));

        public List<Element> FindElementsByXpath(string xpath) => driver.FindElements(By.XPath(xpath)).Select(x => new Element(x)).ToList();

        public void CloseDriver() => driver.Quit();
    }
}
