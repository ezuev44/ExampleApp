using Entities.ProviderTwo;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProviderTwo.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProviderTwoSearchController : ControllerBase
    {

        protected readonly IProviderDataService<ProviderTwoSearchResponse, ProviderTwoSearchRequest> _providerData;

        public ProviderTwoSearchController(IProviderDataService<ProviderTwoSearchResponse, ProviderTwoSearchRequest> providerData)
        {
            _providerData = providerData;
        }

        [HttpPost("search")]
        public async Task<ProviderTwoSearchResponse> Search([FromBody] ProviderTwoSearchRequest request, CancellationToken token)
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
