using Business.Pages;
using Core.Configuration;
using FluentAssertions;
using OpenQA.Selenium;
using Reqnroll;

namespace Tests.UI.StepDefinitions
{
    [Binding]
    public class ServicesNavigationSteps
    {
        private readonly IWebDriver _driver;
        private readonly MainPage _mainPage;
        private readonly ServiceDetailsPage _serviceDetailsPage;

        public ServicesNavigationSteps(IWebDriver driver)
        {
            _driver = driver;
            _mainPage = new MainPage(_driver);
            _serviceDetailsPage = new ServiceDetailsPage(_driver);
        }

        [Given("I open the Epam main page")]
        public void GivenIOpenTheEpamMainPage()
        {
            _driver.Navigate().GoToUrl(ConfigManager.Instance.EnvironmentUrl);
        }

        [When("I hover over the {string} link in the main navigation menu")]
        public void WhenIHoverOverTheLinkInTheMainNavigationMenu(string menuName)
        {
            if (menuName == "Services")
            {
                _mainPage.HoverServicesMenu();
            }
        }

        [When("I select the {string} service category from the dropdown")]
        public void WhenISelectTheServiceCategoryFromTheDropdown(string category)
        {
            _mainPage.ClickServiceCategory(category);
        }

        [Then("the page title should contain {string}")]
        public void ThenThePageTitleShouldContain(string expectedTitle)
        {
            _serviceDetailsPage.GetPageTitle().Should().Contain(expectedTitle);
        }

        [Then("the 'Our Related Expertise' section should be displayed on the page")]
        public void ThenTheSectionShouldBeDisplayedOnThePage()
        {
            _serviceDetailsPage.IsRelatedExpertiseSectionDisplayed()
                .Should().BeTrue("Because the 'Our Related Expertise' section is a mandatory part of the service details page.");
        }
    }
}