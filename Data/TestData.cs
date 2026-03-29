using NUnit.Framework;
namespace Data
{
    public static class TestData
    {
        public const string BaseUrl = "https://www.epam.com/";
     
        public static IEnumerable<TestCaseData> SearchCriteria()
        {
            yield return new TestCaseData(".NET", "Colombia");
            yield return new TestCaseData("Java", "Brazil");
            yield return new TestCaseData("Python", "Mexico");
        }
    }
}
