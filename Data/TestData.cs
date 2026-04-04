using NUnit.Framework;
namespace Data
{
    public static class TestData
    {     
        public static IEnumerable<TestCaseData> SearchCriteria()
        {
            yield return new TestCaseData(".NET", "Ukraine");
            yield return new TestCaseData("Java", "Brazil");
            yield return new TestCaseData("Python", "Mexico");
        }

        public static IEnumerable<TestCaseData> GlobalSearchKeywords()
        {
            yield return new TestCaseData("BLOCKCHAIN");
            yield return new TestCaseData("Cloud");
            yield return new TestCaseData("Automation");
        }

        public static IEnumerable<TestCaseData> FileDownloadNames()
        {
            yield return new TestCaseData("Code-Of-Conduct_01_26.pdf");
        }
    }
}
