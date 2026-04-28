using System.Text.RegularExpressions;
using Core.Utils;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Business.Pages
{
    public class CareersPage : BasePage
    {
        public CareersPage(IWebDriver driver) : base(driver)
        {
        }

        IWebElement CookiesBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("onetrust-accept-btn-handler")));
        IWebElement JobSearchField => driver.FindElement(By.CssSelector("[data-testid='search-input']"));
        IWebElement CountryField => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[aria-label='Choose your country']")));
        IWebElement RemoteCheckBox => driver.FindElement(By.XPath("//label[contains(@for, 'Remote')]"));
        IWebElement SearchBtn => driver.FindElement(By.CssSelector("[data-event-content='search']"));
        IList<IWebElement> JobCards => driver.FindElements(By.ClassName("JobCard_panel__gTD7e"));
        By PreloaderLocator => By.XPath("//div[contains(@class, 'Preloader_fullSize') or contains(@class, 'Preloader_transparent')]");

        public void AcceptCookies()
        {
            try
            {
                Logger.Info("Приймаємо Cookies");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CookiesBtn);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Info("Банер Cookies не з'явився.");
            }
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("onetrust-group-container")));
        }

        private void WaitForPreloaderToDisappear()
        {
            Logger.Debug("Чекаємо зникнення Preloader...");
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(PreloaderLocator));
        }

        public void SearchPositions(string position, string countryName)
        {
            Logger.Info($"Searching for position: '{position}' in country '{countryName}'");

            WaitForPreloaderToDisappear();
            CountryField.SendKeys(Keys.Backspace);
            WaitForPreloaderToDisappear();
            CountryField.SendKeys(countryName);
            CountryField.SendKeys(Keys.Tab);
            WaitForPreloaderToDisappear();

            JobSearchField.Clear();
            JobSearchField.SendKeys(position);

            WaitForPreloaderToDisappear();

            RemoteCheckBox.Click();
            WaitForPreloaderToDisappear();

            ClickSearchAndWaitForResults();
        }

        private void ClickSearchAndWaitForResults()
        {
            var oldResults = JobCards;
            IWebElement firstOldResult = oldResults.Count > 0 ? oldResults[0] : null;

            SearchBtn.Click();

            if (firstOldResult != null)
            {
                wait.Until(ExpectedConditions.StalenessOf(firstOldResult));
            }

            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("JobCard_panel__gTD7e")));
        }

        public bool IsPostionRelevant(string position)
        {
            var allCards = JobCards;

            if (!allCards.Any())
            {
                Logger.Info("No vacancies found for this request.");
                return false;
            }

            string actualCardText = string.Empty;

            wait.Until(d =>
            {
                try
                {
                    if (JobCards.Count == 0) return false;

                    actualCardText = (string)((IJavaScriptExecutor)d)
                        .ExecuteScript("return arguments[0].textContent;", JobCards.Last())!;

                    return !string.IsNullOrWhiteSpace(actualCardText);
                }
                catch (StaleElementReferenceException)
                {
                    Logger.Debug("Caught StaleElementReferenceException, waiting for DOM update...");
                    return false;
                }
            });

            string pattern = $@"\b{Regex.Escape(position)}\b";
            bool isMatch = Regex.IsMatch(actualCardText, pattern, RegexOptions.IgnoreCase);

            if (!isMatch)
            {
                Logger.Debug($"Searched for: '{position}'. Received text:\n{actualCardText}");
            }

            return isMatch;
        }
    }
}
