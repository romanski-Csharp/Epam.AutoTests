using Business.Pages;
using Core.Utils;
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

        [Theory]
        [TestCaseSource(typeof(TestData), nameof(TestData.GlobalSearchKeywords))]
        public void GlobalSearch_ShouldGiveRelevantResults(string keyword)
        {
            var mainPage = new MainPage(driver);
            mainPage.AcceptCookies();

            var searchResultsPage = mainPage.PerformGlobalSearch(keyword);

            bool areResultsRelevant = searchResultsPage.AreAllResultsRelevant(keyword);

            Assert.That(areResultsRelevant, Is.True,
                $"Not all search results contain the word '{keyword}'.");
        }

        [Theory]
        [TestCaseSource(typeof(TestData), nameof(TestData.FileDownloadNames))]
        public void FileDownload_ShouldDownloadCorrectFile(string expectedFileName)
        {
            var mainPage = new MainPage(driver);
            mainPage.AcceptCookies();

            mainPage.DownloadCodeOfConduct();

            string expectedFilePath = Path.Combine(DownloadDirectory, expectedFileName);
            bool isFileDownloaded = WaitUntilFileIsDownloaded(expectedFilePath);

            Assert.That(isFileDownloaded, Is.True,
                $"File '{expectedFileName}' was not downloaded to folder {DownloadDirectory} within the expected time.");
        }

        [Test]
        public void CarouselArticleTitle_ShouldMatchOpenedArticle()
        {
            var mainPage = new MainPage(driver);
            mainPage.AcceptCookies();
            var insightsPage = mainPage.GoToInsightsPage();

            int swipes = 2;
            string expectedTitleFromCarousel = insightsPage.SwipeCarouselAndGetTitle(swipes);
            Logger.Info($"Carousel: {expectedTitleFromCarousel}");

            var articlePage = insightsPage.ClickReadMoreOnActiveArticle();

            string actualTitleFromArticle = articlePage.GetArticleTitle();
            Logger.Info($"Article: {actualTitleFromArticle}");

            var significantWords = StringHelper.GetSignificantWords(expectedTitleFromCarousel);

            foreach (var word in significantWords)
            {
                Assert.That(actualTitleFromArticle, Does.Contain(word).IgnoreCase,
                    $"The article title does not contain the keyword '{word}' from the carousel.\n" +
                    $"Carousel: {expectedTitleFromCarousel}\nArticle: {actualTitleFromArticle}");
            }
        }
    }
}
