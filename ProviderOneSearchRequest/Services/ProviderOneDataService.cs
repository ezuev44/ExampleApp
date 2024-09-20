using Entities.ProviderOne;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using ProviderOne.Context;

namespace ProviderOne.Services
{
    public class ProviderOneDataService : IProviderDataService<ProviderOneSearchResponse, ProviderOneSearchRequest>
    {
        protected readonly AppDbContext _dbContext;

        public ProviderOneDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProviderOneSearchResponse> GetDataAsync(ProviderOneSearchRequest request, CancellationToken token)
        {
            var query = _dbContext.OneRoutes.Where(x => x.From == request.From && request.To == request.To && x.DateFrom >= request.DateFrom);

            if(request.DateTo != null)
            {
                query = query.Where(x => x.DateTo <= request.DateTo);
            }

            if(request.MaxPrice != null)
            {
                query = query.Where(x => x.Price <= request.MaxPrice);
            }

            var dataResult = await query.AsNoTracking()
                .ToListAsync(token);

            return new ProviderOneSearchResponse
            {
                Routes = dataResult.Select(x => new ProviderOneRoute
                {
                    Price = x.Price,
                    DateFrom = x.DateFrom,
                    DateTo = x.DateTo,
                    From = x.From,
                    To = x.To,
                    TimeLimit = x.DateFrom
                }).ToArray()
            };
        }
    }
}
