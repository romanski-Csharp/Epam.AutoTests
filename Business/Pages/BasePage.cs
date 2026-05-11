using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Core.Configuration;

namespace Business.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(ConfigManager.Instance.ExplicitWait));
        }
    }
}
