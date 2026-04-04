using OpenQA.Selenium;
using Core.Utils;
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

        public void AcceptCookies()
        {
            try
            {
                Logger.Info("Приймаємо Cookies...");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CookiesBtn);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Info("Банер Cookies не з'явився.");
            }
        }

        public void GoToCarriersPage()
        {
            Logger.Info("Натискаємо на посилання 'Careers'");
            CareersBtn.Click();

            Logger.Info("Натискаємо на кнопку 'Start Your Search Here'");
            StartSearhBtn.Click();
        }

        public SearchResultsPage PerformGlobalSearch(string keyword)
        {
            Logger.Info($"Починаємо глобальний пошук за ключовим словом: '{keyword}'");

            SearchIcon.Click();

            SearchInput.SendKeys(keyword);

            FindBtn.Click();

            return new SearchResultsPage(driver);
        }

        public void DownloadCodeOfConduct()
        {
            Logger.Info("Скролимо до футера сторінки");

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", CodeOfConductLink);

            wait.Until(ExpectedConditions.ElementToBeClickable(CodeOfConductLink));

            Logger.Info("Натискаємо на посилання 'Code of Ethical Conduct'");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CodeOfConductLink);

        }

        public InsightsPage GoToInsightsPage()
        {
            InsightsBtn.Click();
            return new InsightsPage(driver);
        }
    }
}
