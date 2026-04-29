using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using Core.Configuration;

namespace Core.DriverFactory
{
    public abstract class ChromiumDriverFactory<TOptions> : BaseDriverFactory<TOptions>
        where TOptions : ChromiumOptions, new()
    {
        protected override void ConfigureDownloadOptions(TOptions options, string downloadDirectory)
        {
            options.AddUserProfilePreference("download.default_directory", downloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", ConfigManager.Instance.PromptForDownload);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", ConfigManager.Instance.AlwaysOpenPdfExternally);

            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
        }
    }
}