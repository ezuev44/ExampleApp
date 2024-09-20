using Entities;
namespace Interfaces
{
    public interface IProviderService <TResponce, TRequest> : IDiagnosticService
    {
        Task<TResponce> SearchAsync(TRequest request, bool? useChache, CancellationToken cancellationToken);
    }
}
