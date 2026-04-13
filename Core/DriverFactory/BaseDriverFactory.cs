using OpenQA.Selenium;

namespace Core.DriverFactory
{
    public abstract class BaseDriverFactory<TOptions> : Interfaces.IDriverFactory
        where TOptions : DriverOptions, new()
    {
        public IWebDriver CreateDriver(string downloadDirectory)
        {
            var options = new TOptions();

            ConfigureDownloadOptions(options, downloadDirectory);

            return CreateDriverInstance(options);
        }

        protected abstract void ConfigureDownloadOptions(TOptions options, string downloadDirectory);
        protected abstract IWebDriver CreateDriverInstance(TOptions options);
    }
}