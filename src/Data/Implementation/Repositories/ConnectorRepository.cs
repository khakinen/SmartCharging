using Microsoft.EntityFrameworkCore;
using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Exceptions;
using SmartCharging.Domain.Contract.Groups;

using Connector = SmartCharging.Domain.Contract.Connectors.Connector;
using ConnectorRecord = SmartCharging.Data.Contract.Models.Connector;

namespace Data.Implementation.Repositories;

public class ConnectorRepository: IConnectorRepository
{
    private readonly SmartChargingDbContext _smartChargingDbContext;

    public ConnectorRepository(SmartChargingDbContext smartChargingDbContext)
    {
        _smartChargingDbContext = smartChargingDbContext;
    }
    
    public async Task Delete(Guid chargeStationId, int connectorId)
    {
        var record = await _smartChargingDbContext.Connectors
            .Where(e => e.ChargeStationId == chargeStationId && e.ConnectorNumber == connectorId)
            .FirstOrDefaultAsync();
        
        if (record == null)
        {
            throw new NotFoundException($"{nameof(Connector)}");
        }

        _smartChargingDbContext.Remove(record);
    }

    public async Task<ICollection<Connector>> ReadByChargeStationId(Guid chargeStationId)
    {
        var records = await _smartChargingDbContext.Connectors
            .Where(e => e.ChargeStationId == chargeStationId)
            .ToListAsync();

        return records.Select(Map).ToList();
    }

    public async Task<Connector> Read(Guid chargeStationId, int connectorNumber)
    {
        var record = await _smartChargingDbContext.Connectors
            .Where(e => e.ChargeStationId == chargeStationId && e.ConnectorNumber == connectorNumber)
            .FirstOrDefaultAsync();
        
        if (record == null)
        {
            throw new NotFoundException($"{nameof(Connector)}");
        }
        
        return Map(record);
    }

    public async Task<int> GetTotalMaxCurrentInAmpsOfChargeStation(Guid chargeStationId)
    {
        var sumMaxCurrentInAmps = await _smartChargingDbContext.Connectors
            .Where(connector => connector.ChargeStationId == chargeStationId)
            .SumAsync(connector => connector.MaxCurrentInAmps);

        return sumMaxCurrentInAmps;
    }

    public async Task CreateConnector(CreateConnectorCommand createConnectorCommand)
    {
        var record = Map(createConnectorCommand);

        await _smartChargingDbContext.AddAsync(record);
    }

    public async Task<Connector> UpdateConnector(Guid chargeStationId, int connectorNumber, UpdateConnectorCommand updateConnectorCommand)
    {
        var record = await _smartChargingDbContext.Connectors
            .Where(e => e.ChargeStationId==chargeStationId && e.ConnectorNumber == connectorNumber)
            .FirstOrDefaultAsync();

        if (record == null)
        {
            throw new NotFoundException($"{nameof(Connector)}");
        }

        record.MaxCurrentInAmps = updateConnectorCommand.MaxCurrentInAmps;

        return Map(record);
    }
    
    public async Task<int> GetTotalMaxCurrentInAmpsOfGroup(Guid groupId)
    {
        var sumMaxCurrentInAmps = await _smartChargingDbContext.Connectors
            .Include(e=>e.ChargeStation)
            .Where(connector => connector.ChargeStation.GroupId == groupId)
            .SumAsync(connector => connector.MaxCurrentInAmps);

        return sumMaxCurrentInAmps;
    }
    
    private ConnectorRecord Map(CreateConnectorCommand createConnectorCommand)
    {
        return new ConnectorRecord
        {
            Id = Guid.NewGuid(),
            ConnectorNumber = createConnectorCommand.ConnectorNumber,
            MaxCurrentInAmps = createConnectorCommand.MaxCurrentInAmps,
            ChargeStationId = createConnectorCommand.ChargeStationId
        };
    }

    private Connector Map(ConnectorRecord record)
    {
        return new Connector
        {
            ConnectorNumber = record.ConnectorNumber,
            MaxCurrentInAmps = record.MaxCurrentInAmps,
            ChargeStationId = record.ChargeStationId
        };
    }
}