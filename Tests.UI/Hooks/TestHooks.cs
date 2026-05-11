using Core.Configuration;
using Core.DriverFactory;
using Core.Utils;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;

namespace Tests.UI.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Logger.Info($"[START] UI Test: '{_scenarioContext.ScenarioInfo.Title}'");
            string browserName = ConfigManager.Instance.Browser;
            var downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

            var driver = DriverFactory.CreateDriver(browserName, downloadPath);
            driver.Manage().Window.Maximize();

            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                var driver = _container.Resolve<IWebDriver>();

                if (_scenarioContext.TestError != null)
                {
                    MakeScreenshot(driver);
                }
                else
                {
                    Logger.Info($"[SUCCESS] UI Test: '{_scenarioContext.ScenarioInfo.Title}' completed successfully.");
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