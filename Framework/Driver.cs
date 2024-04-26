using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        public void SwitchToTab(int index)
        {
            var allWindowHandles = driver.WindowHandles;
            driver.SwitchTo().Window(allWindowHandles[index]);
        }

        public void SwitchToDefaultPage() => driver.SwitchTo().DefaultContent();

        public void GoBackToPreviousPage() => driver.Navigate().Back();

        public void MaximizeWindow() => driver.Manage().Window.Maximize();

        public Element FindElementByXpath(string xpath) => new(WaitUntilWebElementExists(By.XPath(xpath)));

        public List<Element> FindElementsByXpath(string xpath) => driver.FindElements(By.XPath(xpath)).Select(x => new Element(x)).ToList();

        public void CloseDriver() => driver.Quit();

        public void WaitUntil(Func<IWebDriver, bool> func, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(func);
        }
        
        public string Title => driver.Title;

        public bool IsElementExists(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public IWebElement WaitUntilWebElementExists(By by, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(e => driver.FindElement(by));
        }

        public void ExecuteJsCommand(string command)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(command);
        }
    }
}
