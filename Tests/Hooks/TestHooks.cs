using Core.DriverFactory;
using Core.Utils;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;

namespace Tests.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public TestHooks(IObjectContainer container, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        private bool IsApiTest => _scenarioContext.ScenarioInfo.Tags.Contains("API") ||
                                  _featureContext.FeatureInfo.Tags.Contains("API");

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (IsApiTest)
            {
                Logger.Info($"[HOOKS] Starting API test '{_scenarioContext.ScenarioInfo.Title}'. WebDriver skipped.");
                return;
            }

            var factory = new ChromeDriverFactory();
            var downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

            var driver = factory.CreateDriver(downloadPath);
            driver.Manage().Window.Maximize();

            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (IsApiTest)
            {
                if (_scenarioContext.TestError != null)
                {
                    Logger.Error($"[FAILED] API test failed: {_scenarioContext.TestError.Message}");
                }
                return;
            }

            try
            {
                var driver = _container.Resolve<IWebDriver>();

                if (_scenarioContext.TestError != null)
                {
                    MakeScreenshot(driver);
                }

                driver.Quit();
                driver.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error($"[HOOKS] Error during WebDriver close: {ex.Message}");
            }
        }

        private void MakeScreenshot(IWebDriver driver)
        {
            try
            {
                string screenshotDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                string fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                fileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
                string filePath = Path.Combine(screenshotDirectory, fileName);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);

                Logger.Error($"[FAILED] Scenario failed. Screenshot saved: {filePath}");
                Logger.Error($"Failure reason: {_scenarioContext.TestError.Message}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to take screenshot: {ex.Message}");
            }
        }
    }
}