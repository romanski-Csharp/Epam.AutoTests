using Business.Pages;
using FluentAssertions;
using Reqnroll;

namespace Tests.UI.StepDefinitions
{
    [Binding]
    public class FileDownloadSteps
    {
        private readonly MainPage _mainPage;
        private readonly string _downloadDirectory;

        public FileDownloadSteps(MainPage mainPage)
        {
            _mainPage = mainPage;

            _downloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");
        }

        [When(@"I download the Code of Conduct file")]
        public void WhenIDownloadTheCodeOfConductFile()
        {
            _mainPage.DownloadCodeOfConduct();
        }

        [Then(@"the file ""(.*)"" should be downloaded successfully")]
        public void ThenTheFileShouldBeDownloadedSuccessfully(string expectedFileName)
        {
            string filePath = Path.Combine(_downloadDirectory, expectedFileName);

            bool isFileDownloaded = false;

            for (int i = 0; i < 15; i++)
            {
                if (File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length > 0)
                    {
                        isFileDownloaded = true;
                        break;
                    }
                }

                Thread.Sleep(1000);
            }

            isFileDownloaded.Should().BeTrue($"because the file '{expectedFileName}' should be fully downloaded into {_downloadDirectory}");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}