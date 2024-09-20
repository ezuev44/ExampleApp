using ExampleApp.Services.Base;
using Interfaces;
using Entities.ProviderOne;
using Microsoft.Extensions.Caching.Memory;
using Extentions;

namespace ExampleApp.Services
{
    public class CallApiProviderOneService : CallApiProviderServiceBase, IProviderService<ProviderOneSearchResponse, ProviderOneSearchRequest>
    {
        protected readonly HttpClient _client;
        protected readonly IMemoryCache _cache;

        protected override string _apiUrl => "api/v1";

        public CallApiProviderOneService(HttpClient client, IMemoryCache cache) : base(client)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<ProviderOneSearchResponse> SearchAsync(ProviderOneSearchRequest request, bool? useChache, CancellationToken cancellationToken)
        {
            var hashCode = request.GetStableHashCode();
            if (_cache.TryGetValue(hashCode, out ProviderOneSearchResponse response) && useChache.Value)
            {
                return response;
            }

            response = await GetAsync<ProviderOneSearchResponse>(HttpMethod.Post, "search", cancellationToken, GetJsonBody(request));

            if (response != null)
            {
                _cache.Set(hashCode, response, _cacheOptions);
            }
            return response;
        }
    }
}
