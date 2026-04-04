using Core.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.DriverFactory
{
    public class EdgeDriverFactory : IDriverFactory
    {
        public IWebDriver CreateDriver(string DownloadDirectory)
        {
            var options = new EdgeOptions();
            options.AddUserProfilePreference("download.default_directory", DownloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            return new EdgeDriver(options);
        }
    }
}
