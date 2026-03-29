using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
    }
}
