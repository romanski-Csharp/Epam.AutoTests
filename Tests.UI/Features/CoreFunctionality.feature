Feature: Core Epam Functionality
  As a user
  I want to be able to search for jobs, use global search, and download files
  So that I can find relevant information on the EPAM website

  Scenario Outline: Criteria based search should give relevant position
    Given I navigate to the Epam Careers page
    When I search for a "<Position>" position in "<Country>"
    Then the search results should contain the "<Position>" position

    Examples:
      | Position | Country |
      | .NET     | Ukraine |
      | Java     | Brazil  |
      | Python   | Mexico  |

  Scenario Outline: Global search should give relevant results
    Given I open the Epam main page
    When I perform a global search for "<Keyword>"
    Then all search results should be relevant to the keyword "<Keyword>"

    Examples:
      | Keyword    |
      | Automation |
      | BLOCKCHAIN |
      | Cloud      |