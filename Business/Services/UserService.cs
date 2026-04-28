using Business.Models;
using Core.API;
using RestSharp;

namespace Business.Services
{
    public class UserService
    {
        private readonly BaseApiClient _apiClient;

        public UserService(BaseApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<RestResponse<List<UserModel>>> GetUsersAsync()
        {
            var request = new ApiRequestBuilder()
                .WithEndpoint("/users")
                .WithMethod(Method.Get)
                .Build();

            return await _apiClient.ExecuteAsync<List<UserModel>>(request);
        }

        public async Task<RestResponse> GetUsersRawAsync()
        {
            var request = new ApiRequestBuilder()
                .WithEndpoint("/users")
                .WithMethod(Method.Get)
                .Build();

            return await _apiClient.ExecuteAsync(request);
        }

        public async Task<RestResponse<UserModel>> CreateUserAsync(UserModel user)
        {
            var request = new ApiRequestBuilder()
                .WithEndpoint("/users")
                .WithMethod(Method.Post)
                .WithJsonBody(user)
                .Build();

            return await _apiClient.ExecuteAsync<UserModel>(request);
        }

        public async Task<RestResponse> GetInvalidEndpointAsync()
        {
            var request = new ApiRequestBuilder()
                .WithEndpoint("/invalidendpoint")
                .WithMethod(Method.Get)
                .Build();

            return await _apiClient.ExecuteAsync(request);
        }
    }
}