using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Core.Configuration;

namespace Core.DriverFactory
{
    public class FirefoxDriverFactory : BaseDriverFactory<FirefoxOptions>
    {
        protected override void ConfigureDownloadOptions(FirefoxOptions options, string downloadDirectory)
        {
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.dir", downloadDirectory);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
            options.SetPreference("pdfjs.disabled", ConfigManager.Instance.AlwaysOpenPdfExternally);
        }

        protected override IWebDriver CreateDriverInstance(FirefoxOptions options)
        {
            return new FirefoxDriver(options);
        }
    }
}