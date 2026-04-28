@API
Feature: Users API Testing
  As an API consumer
  I want to interact with the Users endpoint
  So that I can validate data retrieval, structure, and creation

  # Task 1
  Scenario: Validate that the list of users can be received successfully
    When I request the list of users
    Then the response status code should be 200
    And the response should contain a list of users with all required information populated

  # Task 2
  Scenario: Validate response header for a list of users
    When I request the raw list of users
    Then the raw response status code should be 200
    And the response should contain the "Content-Type" header with value "application/json; charset=utf-8"

  # Task 3
  Scenario: Validate data integrity for a list of users
    When I request the list of users
    Then the response status code should be 200
    And the response body should contain exactly 10 users
    And each user should have a unique ID
    And each user should have a non-empty Name and Username
    And each user should contain a Company with a non-empty Name

  # Task 4
  Scenario: Validate that user can be created
    When I send a POST request to create a user with Name "Roman BDD" and Username "rchub_auto"
    Then the response status code should be 201
    And the created user response should not be empty and contains a valid ID

  # Task 5
  Scenario: Validate that user is notified if resource doesn't exist
    When I request an invalid endpoint
    Then the raw response status code should be 404