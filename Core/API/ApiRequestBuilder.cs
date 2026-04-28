using RestSharp;

namespace Core.API
{
    public class ApiRequestBuilder
    {
        private RestRequest _request;

        public ApiRequestBuilder()
        {
            _request = new RestRequest();
        }

        public ApiRequestBuilder WithEndpoint(string endpoint)
        {
            _request.Resource = endpoint;
            return this;
        }

        public ApiRequestBuilder WithMethod(Method method)
        {
            _request.Method = method;
            return this;
        }

        public ApiRequestBuilder WithHeader(string name, string value)
        {
            _request.AddHeader(name, value);
            return this;
        }

        public ApiRequestBuilder WithJsonBody(object body)
        {
            _request.AddJsonBody(body);
            return this;
        }

        public RestRequest Build()
        {
            return _request;
        }
    }
}