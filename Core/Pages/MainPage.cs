using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Core.Pages
{
    public class MainPage : BasePage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement CareersBtn => driver.FindElement(By.LinkText("Careers"));
        private IWebElement CookiesBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("onetrust-accept-btn-handler")));
        private IWebElement StartSearhBtn => driver.FindElement(By.XPath("//span[contains(text(), 'Start Your Search Here')]/ancestor::a"));

        public void AcceptCookies()
        {
            CookiesBtn.Click();
        }

        public void GoToCarriersPage()
        {
            CareersBtn.Click();
            StartSearhBtn.Click();
        }
    }
}
