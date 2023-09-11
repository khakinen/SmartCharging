using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Contract.Connectors;

public interface IConnectorService
{
    Task RemoveConnector(Guid chargeStationId, int connectorId);
    Task<ICollection<Connector>> GetByChargeStationId(Guid chargeStationId);
    Task<Connector> CreateConnector(CreateConnectorCommand command);
    Task<Connector> UpdateConnector(UpdateConnectorCommand command);
    Task<int> GetTotalMaxCurrentInAmpsOfGroup(Guid groupId);
    Task<int> GetTotalMaxCurrentInAmpsOfChargeStation(Guid chargeStationId);
    Task<Connector> GetConnector(Guid chargeStationId, int connectorNumber);
}