using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Business.Pages
{
    public class ServiceDetailsPage : BasePage
    {
        IWebElement RelatedExpertiseSection => wait.Until(d => d.FindElement(By.XPath("//*[contains(text(), 'Our Related Expertise')]//ancestor::section | //*[contains(@class, 'related-expertise')]")));

        public ServiceDetailsPage(IWebDriver driver) : base(driver) { }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public bool IsRelatedExpertiseSectionDisplayed()
        {
            try
            {
                return RelatedExpertiseSection.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}