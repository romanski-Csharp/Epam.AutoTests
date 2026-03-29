using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core.Pages
{
    public class CareersPage : BasePage
    {
        public CareersPage(IWebDriver driver) : base(driver)
        {
        }

        IWebElement CookiesBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("onetrust-accept-btn-handler")));
        IWebElement JobSearchField => driver.FindElement(By.CssSelector("[data-testid='search-input']"));
        IWebElement CountryField => driver.FindElement(By.CssSelector("input[aria-label='Choose your country']"));
        IWebElement RemoteCheckBox => driver.FindElement(By.XPath("//label[contains(@for, 'Remote')]"));
        IWebElement SearchBtn => driver.FindElement(By.CssSelector("[data-event-content='search']"));
        IList<IWebElement> JobCards => driver.FindElements(By.ClassName("JobCard_panel__gTD7e"));

        public void AcceptCookies()
        {
            CookiesBtn.Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("onetrust-group-container")));
        }

        public void SearchPositions(string position, string countryName)
        {
            JobSearchField.Clear();
            JobSearchField.SendKeys(position);
            CountryField.SendKeys(Keys.Backspace);
            CountryField.SendKeys(countryName);
            CountryField.SendKeys(Keys.Tab);
            RemoteCheckBox.Click();
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
                Console.WriteLine("Вакансій за цим запитом не знайдено.");
                return false;
            }


            string actualCardText = string.Empty;

            wait.Until(d => {
                actualCardText = (string)((IJavaScriptExecutor)d)
                    .ExecuteScript("return arguments[0].textContent;", JobCards.Last())!;

                return !string.IsNullOrWhiteSpace(actualCardText);
            });

            string pattern = $@"\b{Regex.Escape(position)}\b";
            bool isMatch = Regex.IsMatch(actualCardText, pattern, RegexOptions.IgnoreCase);

            if (!isMatch)
            {
                Console.WriteLine($"[DEBUG] Шукали: '{position}'. Отримали текст:\n{actualCardText}");
            }

            return isMatch;
        }
    }
}
