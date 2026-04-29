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
            string isGitHub = Environment.GetEnvironmentVariable("GITHUB_ACTIONS");
            if (isGitHub == "true")
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
            }
        }
    }
}