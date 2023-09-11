namespace SmartCharging.Data.Contract;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
}