using Core.Utils;
using Reqnroll;

namespace Tests.API.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Logger.Info($"[START] API Test: '{_scenarioContext.ScenarioInfo.Title}'");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                Logger.Error($"[FAILED] API test failed: {_scenarioContext.TestError.Message}");
            }
            else
            {
                Logger.Info($"[SUCCESS] API Test: '{_scenarioContext.ScenarioInfo.Title}' completed successfully.");
            }
        }
    }
}