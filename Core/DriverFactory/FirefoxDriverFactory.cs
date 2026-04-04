using Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Core.DriverFactory
{
    public class FirefoxDriverFactory : IDriverFactory
    {
        public IWebDriver CreateDriver(string DownloadDirectory)
        {
            var options = new FirefoxOptions();
            options.SetPreference("browser.download.dir", DownloadDirectory);
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
            options.SetPreference("pdfjs.disabled", true);

            return new FirefoxDriver(options);
        }
    }
}
