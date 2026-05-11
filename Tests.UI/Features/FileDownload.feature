Feature: File Download Functionality
  As a user
  I want to be able to download documents from the website
  So that I can read them offline

  @UI
  Scenario: Download the Code of Conduct file
    Given I open the Epam main page
    When I download the Code of Conduct file
    Then the file "Code-Of-Conduct_01_26.pdf" should be downloaded successfully