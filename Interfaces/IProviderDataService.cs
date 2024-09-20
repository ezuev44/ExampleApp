using Entities.ProviderOne;

namespace Interfaces
{
    public interface IProviderDataService <TItem, TRequest>
    {
        Task<TItem> GetDataAsync(TRequest request, CancellationToken token);
    }
}
