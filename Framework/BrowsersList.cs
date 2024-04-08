using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Framework
{
    public class BrowsersList
    {
        public Driver GetBrowserByName(BrowserEnum browser)
        {
            IWebDriver driver;

            switch (browser)
            {
                case BrowserEnum.Chrome:
                    driver = new ChromeDriver("E:\\VisualStudio\\RepVisualStudio\\AlloTesting\\AlloTesting\\bin\\Debug\\net8.0");
                    break;
                case BrowserEnum.FireFox:
                    string geckodriverPath = "E:/Git/geckodriver.exe";
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(geckodriverPath);
                    driver = new FirefoxDriver();
                    break;
                case BrowserEnum.Edge:
                    driver = new EdgeDriver();
                    break;
                default:
                    throw new Exception("You selected wrong browser");
            }
            return new Driver(driver);
        }
    }
}
