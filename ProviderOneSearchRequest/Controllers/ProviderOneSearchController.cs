using Entities.ProviderOne;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProviderOne.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProviderOneSearchController : ControllerBase
    {
        protected readonly IProviderDataService<ProviderOneSearchResponse, ProviderOneSearchRequest> _providerData;

        public ProviderOneSearchController(IProviderDataService<ProviderOneSearchResponse, ProviderOneSearchRequest> providerData)
        {
            _providerData = providerData;
        }

        [HttpPost("search")]
        public async Task<ProviderOneSearchResponse> Search([FromBody] ProviderOneSearchRequest request, CancellationToken token)
        {
            return await _providerData.GetDataAsync(request, token);
        }

        [HttpGet("ping")]
        public int Ping()
        {
            return 200;
        }
    }
}
