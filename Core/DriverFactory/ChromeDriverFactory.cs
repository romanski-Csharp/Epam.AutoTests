using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.DriverFactory
{
    public class ChromeDriverFactory : ChromiumDriverFactory<ChromeOptions>
    {
        protected override IWebDriver CreateDriverInstance(ChromeOptions options)
        {
            return new ChromeDriver(options);
        }
    }
}