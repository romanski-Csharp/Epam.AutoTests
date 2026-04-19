using Business.Pages;
using Core.Configuration;
using FluentAssertions;
using OpenQA.Selenium;
using Reqnroll;

namespace Tests.StepDefinitions
{
    [Binding]
    public class CoreFunctionalitySteps
    {
        readonly IWebDriver _driver;
        readonly MainPage _mainPage;
        CareersPage _careersPage;
        SearchResultsPage _searchResultsPage;

        public CoreFunctionalitySteps(IWebDriver driver)
        {
            _driver = driver;
            _mainPage = new MainPage(_driver);
        }

        [Given("I navigate to the Epam Careers page")]
        public void GivenINavigateToTheEpamCareersPage()
        {
            _driver.Navigate().GoToUrl(ConfigManager.Instance.EnvironmentUrl);
            _mainPage.AcceptCookies();
            _mainPage.GoToCarriersPage();
            _careersPage = new CareersPage(_driver);
            _careersPage.AcceptCookies();
        }

        [When("I search for a {string} position in {string}")]
        public void WhenISearchForAPositionIn(string position, string country)
        {
            _careersPage.SearchPositions(position, country);
        }

        [Then("the search results should contain the {string} position")]
        public void ThenTheSearchResultsShouldContainThePosition(string position)
        {
            bool isPositionRelevant = _careersPage.IsPostionRelevant(position);
            isPositionRelevant.Should().BeTrue($"Because we searched for {position}");
        }

        [When("I perform a global search for {string}")]
        public void WhenIPerformAGlobalSearchFor(string keyword)
        {
            _mainPage.AcceptCookies();
            _searchResultsPage = _mainPage.PerformGlobalSearch(keyword);
        }

        [Then("all search results should be relevant to the keyword {string}")]
        public void ThenAllSearchResultsShouldBeRelevantToTheKeyword(string keyword)
        {
            bool areResultsRelevant = _searchResultsPage.AreAllResultsRelevant(keyword);
            areResultsRelevant.Should().BeTrue($"Because all results should contain the word '{keyword}'");
        }
    }
}