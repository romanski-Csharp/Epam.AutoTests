using Core.Pages;
using Data;

namespace Tests
{
    internal class ValidationTests : BaseTest
    {
        [Theory]
        [TestCaseSource(typeof(TestData), nameof(TestData.SearchCriteria))]
        public void CriteriaBasedSearch_ShouldGivePosition(string position, string country)
        {
            
            var mainPage = new MainPage(driver); 
            mainPage.AcceptCookies();
            mainPage.GoToCarriersPage();

            var careersPage = new CareersPage(driver);
            careersPage.AcceptCookies();
            careersPage.SearchPositions(position, country);

            bool isPositionRelevant = careersPage.IsPostionRelevant(position);
            Assert.That(isPositionRelevant, Is.True);
        }
    }
}
