using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected string DownloadDirectory;

        [SetUp]
        public void Setup()
        {
            DownloadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
            if (!Directory.Exists(DownloadDirectory))
            {
                Directory.CreateDirectory(DownloadDirectory);
            }

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", DownloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Data.TestData.baseUrl);
        }
        protected bool WaitUntilFileIsDownloaded(string filePath, int timeoutInSeconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            try
            {
                return wait.Until(d =>
                {
                    var fileInfo = new FileInfo(filePath);
                    return fileInfo.Exists && fileInfo.Length > 0;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();

            if (Directory.Exists(DownloadDirectory))
            {
                Directory.Delete(DownloadDirectory, true);
            }
        }
    }
}
