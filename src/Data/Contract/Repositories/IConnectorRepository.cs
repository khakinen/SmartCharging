using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Data.Contract.Repositories;

public interface IConnectorRepository
{
    Task Delete(Guid chargeStationId, int connectorId);
    Task CreateConnector(CreateConnectorCommand createConnectorCommand);
    Task<Connector> UpdateConnector(Guid chargeStationId, int connectorNumber, UpdateConnectorCommand updateConnectorCommand);
    Task<int> GetTotalMaxCurrentInAmpsOfGroup(Guid groupId);
    Task<ICollection<Connector>> ReadByChargeStationId(Guid chargeStationId);
    Task<Connector> Read(Guid chargeStationId, int connectorNumber);
    Task<int> GetTotalMaxCurrentInAmpsOfChargeStation(Guid chargeStationId);
}