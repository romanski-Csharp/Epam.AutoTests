using Core.Configuration;
using Core.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(2)]
namespace Tests
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public abstract class BaseTest
    {
        protected IWebDriver driver;
        public string DownloadDirectory;

        [OneTimeSetUp]
        public static void GlobalSetup()
        {
            Core.Utils.Logger.InitLogger();
        }
        [SetUp]
        public void Setup()
        {
            Core.Utils.Logger.Info($"[START] Початок тесту: {TestContext.CurrentContext.Test.Name}");

            DownloadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", Guid.NewGuid().ToString());
            Directory.CreateDirectory(DownloadDirectory);

            string browser = ConfigManager.Instance.Browser;
            string envUrl = ConfigManager.Instance.EnvironmentUrl;

            driver = DriverFactory.CreateDriver(browser, DownloadDirectory);

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(envUrl);
            Core.Utils.Logger.Info($"Відкрито URL: {envUrl}");
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
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshotOnFailure();
            }

            driver?.Quit();
            driver?.Dispose();

            if (Directory.Exists(DownloadDirectory))
            {
                Directory.Delete(DownloadDirectory, true);
            }
        }
        private void TakeScreenshotOnFailure()
        {
            try
            {
                var screenshotDriver = driver as ITakesScreenshot;
                if (screenshotDriver == null) return;

                Screenshot screenshot = screenshotDriver.GetScreenshot();

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string rawTestName = TestContext.CurrentContext.Test.Name;

                string cleanTestName = string.Join("_", rawTestName.Split(Path.GetInvalidFileNameChars()));

                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Screenshots\\{cleanTestName}_{timestamp}.png");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                screenshot.SaveAsFile(filePath);
                Core.Utils.Logger.Error($"[FAILED] Тест впав! Скріншот збережено: {filePath}");

                TestContext.AddTestAttachment(filePath);
            }
            catch (Exception ex)
            {
                Core.Utils.Logger.Error($"Помилка при створенні скріншоту: {ex.Message}");
            }
        }
    }
}
