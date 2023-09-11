using Microsoft.EntityFrameworkCore;
using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Exceptions;
using ChargeStation = SmartCharging.Domain.Contract.ChargeStations.ChargeStation;
using ChargeStationRecord = SmartCharging.Data.Contract.Models.ChargeStation;

namespace Data.Implementation.Repositories;

public class ChargeStationRepository : IChargeStationRepository
{
    private readonly SmartChargingDbContext _smartChargingDbContext;

    public ChargeStationRepository(SmartChargingDbContext smartChargingDbContext)
    {
        _smartChargingDbContext = smartChargingDbContext;
    }

    public async Task Create(ChargeStation chargeStation)
    {
        var record = Map(chargeStation);

        await _smartChargingDbContext.AddAsync(record);
    }

    public async Task<ChargeStation> Update(Guid id, UpdateChargeStationCommand updateChargeStationCommand)
    {
        var record = await _smartChargingDbContext.ChargeStations
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (record == null)
        {
            throw new NotFoundException($"{nameof(ChargeStation)}");
        }
        
        record.Name = updateChargeStationCommand.Name;
        record.GroupId = updateChargeStationCommand.GroupId;

        return Map(record);
    }

    public async Task Delete(Guid id)
    {
        var record = await _smartChargingDbContext.ChargeStations
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
        
        if (record == null)
        {
            throw new NotFoundException($"{nameof(ChargeStation)}");
        }

        _smartChargingDbContext.Remove(record);
    }

    public async Task<ICollection<ChargeStation>> GetByGroupId(Guid groupId)
    {
        var records = await _smartChargingDbContext.ChargeStations
            .Where(e => e.GroupId == groupId)
            .ToListAsync();

        return records.Select(Map).ToList();
    }

    public async Task<ChargeStation> Read(Guid id)
    {
        var record = await _smartChargingDbContext.ChargeStations
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
        
        if (record == null)
        {
            throw new NotFoundException($"{nameof(ChargeStation)}");
        }  

        return Map(record);
    }

    private ChargeStation Map(ChargeStationRecord record)
    {
        return new ChargeStation
        {
            Id = record.Id,
            Name = record.Name,
            GroupId = record.GroupId
        };
    }
    private ChargeStationRecord Map(ChargeStation chargeStation)
    {
        return new ChargeStationRecord
        {
            Id = chargeStation.Id,
            Name = chargeStation.Name,
            GroupId = chargeStation.GroupId
        };
    }
}