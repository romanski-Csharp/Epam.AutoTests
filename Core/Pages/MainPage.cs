using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core.Pages
{
    public class MainPage : BasePage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        IWebElement CareersBtn => driver.FindElement(By.LinkText("Careers"));
        IWebElement CookiesBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("onetrust-accept-btn-handler")));
        IWebElement StartSearhBtn => driver.FindElement(By.XPath("//span[contains(text(), 'Start Your Search Here')]/ancestor::a"));
        IWebElement SearchIcon => driver.FindElement(By.CssSelector(".header-search__button"));
        IWebElement SearchInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("new_form_search")));
        IWebElement FindBtn => driver.FindElement(By.XPath("//span[contains(text(), 'Find')]/parent::button"));
        IWebElement CodeOfConductLink => driver.FindElement(By.XPath("//a[contains(@href, 'Code-Of-Conduct')]"));
        IWebElement InsightsBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(@class, 'top-navigation__item-link') and text()='Insights']")));

        public void AcceptCookies()
        {
            CookiesBtn.Click();
        }

        public void GoToCarriersPage()
        {
            CareersBtn.Click();
            StartSearhBtn.Click();
        }

        public SearchResultsPage PerformGlobalSearch(string keyword)
        {
            SearchIcon.Click();

            SearchInput.SendKeys(keyword);

            FindBtn.Click();

            return new SearchResultsPage(driver);
        }

        public void DownloadCodeOfConduct()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CodeOfConductLink);
        }

        public InsightsPage GoToInsightsPage()
        {
            InsightsBtn.Click();
            return new InsightsPage(driver);
        }
    }
}
