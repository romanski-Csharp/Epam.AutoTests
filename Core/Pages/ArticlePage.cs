using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core.Pages
{
    public class ArticlePage : BasePage
    {
        public ArticlePage(IWebDriver driver) : base(driver)
        {
        }

        IList<IWebElement> Headers => wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("h1, .font-size-80-33, .font-size-80-44, .article-title")));

        public string GetArticleTitle()
        {
            foreach (var header in Headers)
            {
                string text = header.Text.Replace("\r", "").Replace("\n", " ").Trim();

                if (header.Displayed && text.Length > 5)
                {
                    return text;
                }
            }

            throw new NotFoundException("Не вдалося знайти видимий заголовок статті (всі знайдені елементи були закороткими).");
        }
    }
}