using Framework;

namespace AlloTesting
{
    public class BasePage
    {
        protected static Driver driver;

        public BasePage(Driver webDriver) 
        {
            driver = webDriver;
        }
    }
}
