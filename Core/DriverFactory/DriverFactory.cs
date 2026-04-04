using OpenQA.Selenium;

namespace Core.DriverFactory
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver(string browser, string DownloadDirectory)
        {
            Interfaces.IDriverFactory factory = browser switch
            {
                "Chrome" => new ChromeDriverFactory(),
                "Edge" => new EdgeDriverFactory(),
                "Firefox" => new FirefoxDriverFactory(),
                _ => throw new ArgumentException("Unsupported browser type")
            };
            return factory.CreateDriver(DownloadDirectory);
        }
    }
}
