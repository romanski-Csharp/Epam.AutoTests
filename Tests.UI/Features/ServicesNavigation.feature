Feature: Services Navigation
  As a user of the EPAM website
  I want to navigate through the Services menu
  So that I can view specific service offerings

  Scenario Outline: Validate Navigation to Services Section
    Given I open the Epam main page
    When I hover over the "Services" link in the main navigation menu
    And I select the "<ServiceCategory>" service category from the dropdown
    Then the page title should contain "<ExpectedTitle>"
    And the 'Our Related Expertise' section should be displayed on the page

    Examples: 
      | ServiceCategory | ExpectedTitle  |
      | Generative AI   | Generative AI  |
      | Responsible AI  | Responsible AI |