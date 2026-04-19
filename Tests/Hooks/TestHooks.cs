using Core.DriverFactory;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;

namespace Tests.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _container;

        public TestHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var factory = new ChromeDriverFactory();
            var downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

            var driver = factory.CreateDriver(downloadPath);
            driver.Manage().Window.Maximize();

            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            driver?.Quit();
            driver?.Dispose();
        }
    }
}