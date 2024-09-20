using Entities.ProviderOne;
using Entities.ProviderTwo;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using ProviderTwo.Context;

namespace ProviderTwo.Services
{
    public class ProviderTwoDataService : IProviderDataService<ProviderTwoSearchResponse, ProviderTwoSearchRequest>
    {
        protected readonly AppDbContext _dbContext;

        public ProviderTwoDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProviderTwoSearchResponse> GetDataAsync(ProviderTwoSearchRequest request, CancellationToken token)
        {
            var query = _dbContext.TwoRoutes.Include(x => x.TwoPonts)
                    .Where(x => x.TwoPonts.Any(s => s.PointType == Context.Entities.TwoPointType.Departure && s.Point == request.Departure && s.Date >= request.DepartureDate));

            query = query.Where(x => x.TwoPonts.Any(s => s.PointType == Context.Entities.TwoPointType.Arrival && s.Point == request.Arrival));

            if(request.MinTimeLimit != null)
            {
                query = query.Where(x => x.TimeLimit <= request.MinTimeLimit);
            }

            var dataResult = await query.AsNoTracking()
                .ToListAsync(token);

            return new ProviderTwoSearchResponse
            {
                Routes = dataResult.Select(x => new ProviderTwoRoute
                {
                    Arrival = new ProviderTwoPoint
                    {
                        Date = x.TwoPonts.FirstOrDefault(s => s.PointType == Context.Entities.TwoPointType.Arrival).Date,
                        Point = x.TwoPonts.FirstOrDefault(s => s.PointType == Context.Entities.TwoPointType.Arrival).Point,
                    },
                    Departure = new ProviderTwoPoint
                    {
                        Date = x.TwoPonts.FirstOrDefault(s => s.PointType == Context.Entities.TwoPointType.Departure).Date,
                        Point = x.TwoPonts.FirstOrDefault(s => s.PointType == Context.Entities.TwoPointType.Departure).Point,
                    },
                    Price = x.Price,
                    TimeLimit = x.TimeLimit
                }).ToArray()
            };
        }
    }
}
