using Core.Utils;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Business.Pages
{
    public class SearchResultsPage : BasePage
    {
        public SearchResultsPage(IWebDriver driver) : base(driver) { }

        private readonly By _resultLinks = By.ClassName("search-results__item");

        public bool AreAllResultsRelevant(string keyword)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(_resultLinks));

            var links = driver.FindElements(_resultLinks);

            if (!links.Any())
            {
                Logger.Info($"Результатів для '{keyword}' не знайдено.");
                return false;
            }

            bool allMatch = links.All(link =>
                link.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (!allMatch)
            {
                var invalidLinks = links.Where(l => !l.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                foreach (var invalid in invalidLinks)
                {
                    Logger.Debug($"Знайдено нерелевантний лінк: {invalid.Text}");
                }
            }

            return allMatch;
        }
    }
}