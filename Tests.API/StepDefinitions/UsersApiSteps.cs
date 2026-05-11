using Business.Models;
using Business.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using Reqnroll;
using RestSharp;

namespace Tests.API.StepDefinitions
{
    [Binding]
    public class UsersApiSteps
    {
        private readonly UserService _userService;

        private RestResponse<List<UserModel>> _usersResponse;
        private RestResponse _rawResponse;
        private RestResponse<UserModel> _createdUserResponse;

        public UsersApiSteps(UserService userService)
        {
            _userService = userService;
        }

        [When("I request the list of users")]
        public async Task WhenIRequestTheListOfUsers()
        {
            _usersResponse = await _userService.GetUsersAsync();
        }

        [When("I request the raw list of users")]
        public async Task WhenIRequestTheRawListOfUsers()
        {
            _rawResponse = await _userService.GetUsersRawAsync();
        }

        [When("I request an invalid endpoint")]
        public async Task WhenIRequestAnInvalidEndpoint()
        {
            _rawResponse = await _userService.GetInvalidEndpointAsync();
        }

        [When("I send a POST request to create a user with Name {string} and Username {string}")]
        public async Task WhenISendAPOSTRequestToCreateAUserWithNameAndUsername(string name, string username)
        {
            var newUser = new UserModel
            {
                Name = name,
                Username = username
            };
            _createdUserResponse = await _userService.CreateUserAsync(newUser);
        }

        [Then("the response status code should be {int}")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            var actualCode = (int)(_usersResponse?.StatusCode ?? _createdUserResponse.StatusCode);
            actualCode.Should().Be(expectedStatusCode, "because the API should return correct success code");
        }

        [Then("the raw response status code should be {int}")]
        public void ThenTheRawResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            ((int)_rawResponse.StatusCode).Should().Be(expectedStatusCode);
        }

        [Then("the response should contain a list of users with all required information populated")]
        public void ThenTheResponseShouldContainAListOfUsersWithAllRequiredInformationPopulated()
        {
            var users = _usersResponse.Data;

            users.Should().NotBeNullOrEmpty();

            using (new AssertionScope())
            {
                foreach (var user in users)
                {
                    user.Id.Should().BeGreaterThan(0);
                    user.Name.Should().NotBeNullOrEmpty();
                    user.Username.Should().NotBeNullOrEmpty();
                    user.Email.Should().NotBeNullOrEmpty();
                    user.Phone.Should().NotBeNullOrEmpty();
                    user.Website.Should().NotBeNullOrEmpty();
                    user.Address.Should().NotBeNull();
                    user.Company.Should().NotBeNull();
                }
            }
        }

        [Then("the response should contain the {string} header with value {string}")]
        public void ThenTheResponseShouldContainTheHeaderWithValue(string headerName, string expectedValue)
        {
            var header = _rawResponse.Headers?.FirstOrDefault(h => h.Name.Equals(headerName, System.StringComparison.OrdinalIgnoreCase))
                      ?? _rawResponse.ContentHeaders?.FirstOrDefault(h => h.Name.Equals(headerName, System.StringComparison.OrdinalIgnoreCase));

            header.Should().NotBeNull($"because the '{headerName}' header must exist in the response");
            header.Value.ToString().Should().Be(expectedValue);
        }

        [Then("the response body should contain exactly {int} users")]
        public void ThenTheResponseBodyShouldContainExactlyUsers(int expectedCount)
        {
            _usersResponse.Data.Should().HaveCount(expectedCount);
        }

        [Then("each user should have a unique ID")]
        public void ThenEachUserShouldHaveAUniqueID()
        {
            var users = _usersResponse.Data;
            var uniqueIds = users.Select(u => u.Id).Distinct();

            uniqueIds.Should().HaveCount(users.Count, "because all 10 users must have different IDs");
        }

        [Then("each user should have a non-empty Name and Username")]
        public void ThenEachUserShouldHaveANonEmptyNameAndUsername()
        {
            var users = _usersResponse.Data;
            users.Should().OnlyContain(u => !string.IsNullOrWhiteSpace(u.Name) && !string.IsNullOrWhiteSpace(u.Username));
        }

        [Then("each user should contain a Company with a non-empty Name")]
        public void ThenEachUserShouldContainACompanyWithANonEmptyName()
        {
            var users = _usersResponse.Data;
            users.Should().OnlyContain(u => u.Company != null && !string.IsNullOrWhiteSpace(u.Company.Name));
        }

        [Then("the created user response should not be empty and contains a valid ID")]
        public void ThenTheCreatedUserResponseShouldNotBeEmptyAndContainsAValidID()
        {
            var createdUser = _createdUserResponse.Data;

            createdUser.Should().NotBeNull();
            createdUser.Id.Should().BeGreaterThan(0, "because the API should generate a valid ID for the new user");
        }
    }
}