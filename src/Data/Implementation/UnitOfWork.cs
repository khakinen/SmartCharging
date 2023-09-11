using SmartCharging.Data.Contract;

namespace Data.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly SmartChargingDbContext _smartChargingDbContext;

    public UnitOfWork(SmartChargingDbContext smartChargingDbContext)
    {
        _smartChargingDbContext = smartChargingDbContext;
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _smartChargingDbContext.SaveChangesAsync(ct);
    }
}