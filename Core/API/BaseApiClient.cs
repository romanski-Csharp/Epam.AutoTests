using RestSharp;
using Core.Configuration;
using Core.Utils;
using System.Threading.Tasks;

namespace Core.API
{
    public class BaseApiClient
    {
        private readonly RestClient _client;

        public BaseApiClient()
        {
            var options = new RestClientOptions(ConfigManager.Instance.ApiBaseUrl);
            _client = new RestClient(options);
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            Logger.Info($"[API REQUEST] {request.Method} {ConfigManager.Instance.ApiBaseUrl}/{request.Resource}");

            var response = await _client.ExecuteAsync(request);

            Logger.Info($"[API RESPONSE] Status: {response.StatusCode}");
            if (!string.IsNullOrEmpty(response.Content))
            {
                Logger.Debug($"[API CONTENT] {response.Content}");
            }

            return response;
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
        {
            Logger.Info($"[API REQUEST] {request.Method} {ConfigManager.Instance.ApiBaseUrl}/{request.Resource}");

            var response = await _client.ExecuteAsync<T>(request);

            Logger.Info($"[API RESPONSE] Status: {response.StatusCode}");

            return response;
        }
    }
}