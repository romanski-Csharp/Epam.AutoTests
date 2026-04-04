using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Pages
{
    public class InsightsPage : BasePage
    {
        public InsightsPage(IWebDriver driver) : base(driver) 
        {
        }

        IWebElement CaruselNextArrow => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".slider__right-arrow")));
        IWebElement CurrentSlide => driver.FindElement(By.ClassName("slider__pagination--current-page"));
        IWebElement ReadMoreBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".owl-item.active .slider-cta-link")));
        IWebElement ActiveTitleElement => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".owl-item.active .text-ui-23")));

        public string SwipeCarouselAndGetTitle(int swipesCount)
        {
            for (int i = 0; i < swipesCount; i++)
            {
                int currentSlideNumber = int.Parse(CurrentSlide.Text.Trim());
                new Actions(driver).MoveToElement(CaruselNextArrow).Click().Perform();
                string nextSlideText = (currentSlideNumber == 4 ? 1 : currentSlideNumber + 1).ToString("D2");
                wait.Until(ExpectedConditions.TextToBePresentInElement(CurrentSlide, nextSlideText));
            }

            return ActiveTitleElement.Text.Replace("\r", "").Replace("\n", " ").Trim();
        }

        public ArticlePage ClickReadMoreOnActiveArticle()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", ReadMoreBtn);

            return new ArticlePage(driver);
        }
    }
}