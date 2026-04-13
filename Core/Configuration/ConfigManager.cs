using NUnit.Framework;

namespace Core.Configuration
{
    public class ConfigManager
    {
        private static ConfigManager _instance;
        private static readonly object _lock = new object();

        private ConfigManager()
        {
        }

        public static ConfigManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigManager();
                    }
                    return _instance;
                }
            }
        }

        public string Browser => TestContext.Parameters.Get("Browser", "Chrome");
        public string EnvironmentUrl => TestContext.Parameters.Get("EnvironmentUrl", "https://www.epam.com/");
        public bool PromptForDownload => bool.Parse(TestContext.Parameters.Get("PromptForDownload", "false"));
        public bool AlwaysOpenPdfExternally => bool.Parse(TestContext.Parameters.Get("AlwaysOpenPdfExternally", "true"));
    }
}