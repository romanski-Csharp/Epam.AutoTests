using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.DriverFactory
{
    public class EdgeDriverFactory : ChromiumDriverFactory<EdgeOptions>
    {
        protected override IWebDriver CreateDriverInstance(EdgeOptions options)
        {
            return new EdgeDriver(options);
        }
    }
}