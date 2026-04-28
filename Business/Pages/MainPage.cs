using Core.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Business.Pages
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
        IWebElement ServicesMenu => driver.FindElement(By.XPath("//a[contains(@class, 'top-navigation__item-link') and text()='Services']"));

        public void AcceptCookies()
        {
            try
            {
                Logger.Info("Accepting Cookies...");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CookiesBtn);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Info("Cookies banner did not appear.");
            }
        }

        public void GoToCarriersPage()
        {
            Logger.Info("Clicking on the 'Careers' link");
            CareersBtn.Click();

            Logger.Info("Clicking on the 'Start Your Search Here' button");
            StartSearhBtn.Click();
        }

        public SearchResultsPage PerformGlobalSearch(string keyword)
        {
            Logger.Info($"Starting global search for keyword: '{keyword}'");

            SearchIcon.Click();

            SearchInput.SendKeys(keyword);

            FindBtn.Click();

            return new SearchResultsPage(driver);
        }

        public void DownloadCodeOfConduct()
        {
            Logger.Info("Scrolling to the page footer");

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", CodeOfConductLink);

            wait.Until(ExpectedConditions.ElementToBeClickable(CodeOfConductLink));

            Logger.Info("Clicking on the 'Code of Ethical Conduct' link");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CodeOfConductLink);

        }

        public InsightsPage GoToInsightsPage()
        {
            InsightsBtn.Click();
            return new InsightsPage(driver);
        }

        public MainPage HoverServicesMenu()
        {
            new Actions(driver).MoveToElement(ServicesMenu).Perform();
            return this;
        }

        public void ClickServiceCategory(string categoryName)
        {
            var categoryLink = By.XPath($"//ul[@class='top-navigation__sub-list']//a[contains(text(), '{categoryName}')]");
            wait.Until(d => d.FindElement(categoryLink).Displayed);
            driver.FindElement(categoryLink).Click();
        }
    }
}
