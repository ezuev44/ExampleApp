namespace Interfaces
{
    public interface IDiagnosticService
    {
        Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
    }
}
