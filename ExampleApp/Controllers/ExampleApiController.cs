using Entities;
using Entities.ProviderOne;
using Entities.ProviderTwo;
using ExampleApp.Services.Mappers;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProviderOne.Services.Mappers;

namespace ExampleApp.Controllers
{
    [ApiController]
    [Route("api/example")]
    public class ExampleApiController : ControllerBase
    {

        protected readonly IProviderService<ProviderOneSearchResponse, ProviderOneSearchRequest> _providerOneService;
        protected readonly IProviderService<ProviderTwoSearchResponse, ProviderTwoSearchRequest> _providerTwoService;
        protected readonly IDictionary<Type, SearchResponseMapperService> _mappers;

        public ExampleApiController(IProviderService<ProviderOneSearchResponse, ProviderOneSearchRequest> providerOneService, IProviderService<ProviderTwoSearchResponse, ProviderTwoSearchRequest> providerTwoService) 
        {
            _providerOneService = providerOneService;
            _providerTwoService = providerTwoService;

            _mappers = new Dictionary<Type, SearchResponseMapperService>
            {
                { typeof(ProviderOneSearchResponse), new SearchResponseProviderOneMapperService() },
                { typeof(ProviderTwoSearchResponse), new SearchResponseProviderTwoMapperService() }
            };
            
        }

        [HttpPost("search")]
        public async Task<SearchResponse> Search([FromBody] SearchRequest request, CancellationToken token)
        {
            var searchOne = await _providerOneService.SearchAsync(new ProviderOneSearchRequest 
            {
                DateFrom = request.OriginDateTime,
                DateTo = request.Filters.DestinationDateTime,
                From = request.Origin,
                To = request.Destination,
                MaxPrice = request.Filters.MaxPrice
            }, request.Filters.OnlyCached, token);

            var mapOne = Map(searchOne);

            var searchTwo = await _providerTwoService.SearchAsync(new ProviderTwoSearchRequest
            {
                Departure = request.Origin,
                Arrival = request.Destination,
                DepartureDate = request.OriginDateTime,
                MinTimeLimit = request.Filters.MinTimeLimit,
            }, request.Filters.OnlyCached, token);

            var mapTwo = Map(searchTwo);

            var routes = mapOne.Routes.ToList();
            routes.AddRange(mapTwo.Routes);
            return new SearchResponse
            {
                Routes = routes.ToArray()
            };
        }


        [HttpGet("ping")]
        public async Task<IList<string>> Ping(CancellationToken token)
        {
            var prOneStatus = await _providerOneService.IsAvailableAsync(token);
            var prTwoStatus = await _providerTwoService.IsAvailableAsync(token);

            return new List<string>
            {
                prOneStatus ? "200 one provider is ready" : "500 one provider is down",
                prTwoStatus ? "200 two provider is ready" : "500 two provider is down",
            };
        }

        private SearchResponse Map<T>(T response)
        {
            var srv = _mappers[typeof(T)];
            var mapSrv = (ISearchResponseMapperService<T>) srv;
            return mapSrv.Map(response);
        }
    }
}
