using Entities;
using Entities.ProviderTwo;
using ExampleApp.Services.Mappers;
using Interfaces;
using Route = Entities.Route;

namespace ProviderOne.Services.Mappers
{
    public class SearchResponseProviderTwoMapperService : SearchResponseMapperService, ISearchResponseMapperService<ProviderTwoSearchResponse>
    {
        public SearchResponse Map(ProviderTwoSearchResponse item)
        {
            return new SearchResponse
            {
                Routes = item.Routes.Select(x => new Route
                {
                    Id = Guid.NewGuid(),
                    Origin = x.Departure.Point,
                    OriginDateTime = x.Departure.Date,
                    Destination = x.Arrival.Point,
                    DestinationDateTime = x.Arrival.Date,
                    Price = x.Price,
                    TimeLimit = x.TimeLimit
                }).ToArray()
            };
        }
    }
}
