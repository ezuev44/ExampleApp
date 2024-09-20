using Entities.ProviderTwo;
using ExampleApp.Services.Base;
using Extentions;
using Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ExampleApp.Services
{
    public class CallApiProviderTwoService : CallApiProviderServiceBase, IProviderService<ProviderTwoSearchResponse, ProviderTwoSearchRequest>
    {
        protected readonly HttpClient _client;
        protected readonly IMemoryCache _cache;

        protected override string _apiUrl => "api/v1";

        public CallApiProviderTwoService(HttpClient client, IMemoryCache cache) : base(client)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<ProviderTwoSearchResponse> SearchAsync(ProviderTwoSearchRequest request, bool? useChache, CancellationToken cancellationToken)
        {
            var hashCode = request.GetStableHashCode();
            if (_cache.TryGetValue(hashCode, out ProviderTwoSearchResponse response) && useChache.Value)
            {
                return response;
            }

            response = await GetAsync<ProviderTwoSearchResponse>(HttpMethod.Post, "search", cancellationToken, GetJsonBody(request));

            if (response != null)
            {
                _cache.Set(hashCode, response, _cacheOptions);
            }
            return response;
        }
    }
}
