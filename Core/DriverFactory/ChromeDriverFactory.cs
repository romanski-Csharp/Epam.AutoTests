using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.DriverFactory
{
    public class ChromeDriverFactory : Interfaces.IDriverFactory
    {
        public IWebDriver CreateDriver(string DownloadDirectory)
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", DownloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            return new ChromeDriver(options);
        }
    }
}
