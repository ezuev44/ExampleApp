using Entities;
using Entities.ProviderOne;
using Interfaces;
using Route = Entities.Route;

namespace ExampleApp.Services.Mappers
{
    public class SearchResponseProviderOneMapperService : SearchResponseMapperService, ISearchResponseMapperService<ProviderOneSearchResponse>
    {
        public SearchResponse Map(ProviderOneSearchResponse item)
        {
            return new SearchResponse
            {
                Routes = item.Routes.Select(x => new Route
                {
                    Id = Guid.NewGuid(),
                    Origin = x.From,
                    OriginDateTime = x.DateFrom,
                    Destination = x.To,
                    DestinationDateTime = x.DateTo,
                    Price = x.Price,
                    TimeLimit = x.TimeLimit
                }).ToArray()
            };
        }
    }
}
