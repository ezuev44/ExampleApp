using System.Text;
using System.Text.Json;
using Extentions;
using Microsoft.Extensions.Caching.Memory;

namespace ExampleApp.Services.Base
{
    public abstract class CallApiProviderServiceBase
    {
        protected readonly HttpClient _client;

        protected virtual string _apiUrl {  get; set; }

        protected readonly MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
        };

        protected CallApiProviderServiceBase(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await GetAsync<int>(HttpMethod.Get, string.Format(_apiUrl, $"ping"), cancellationToken);
                if (result == 200)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // SOME LOGS by ex

                return false;
            }
        }

        public async Task<T> GetAsync<T>(HttpMethod method, string route, CancellationToken token,
            string jsonBody = null)
        {
            var request = new HttpRequestMessage(method, $"{_apiUrl}/{route}");
            if (!string.IsNullOrWhiteSpace(jsonBody))
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                request.Content = content;
            }
            var response = await _client.SendAsync(request, token);
            return await response.ReadContentAsync<T>();
        }

        public string GetJsonBody<T>(T request)
        {
            return JsonSerializer.Serialize(request);
        }
    }
}
