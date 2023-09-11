using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Contract.Validations;

namespace SmartCharging.Domain.Implementation.Connectors;

public class ConnectorServiceValidationDecorator : IConnectorService
{
    private readonly IConnectorService _connectorService;
    private readonly IGroupTotalAmpsValidator _groupTotalAmpsValidator;


    public ConnectorServiceValidationDecorator(IConnectorService connectorService, IGroupTotalAmpsValidator groupTotalAmpsValidator)
    {
        _connectorService = connectorService;
        _groupTotalAmpsValidator = groupTotalAmpsValidator;
    }

    public async Task RemoveConnector(Guid chargeStationId, int connectorId)
    {
        await _connectorService.RemoveConnector(chargeStationId, connectorId);
    }

    public async Task<ICollection<Connector>> GetByChargeStationId(Guid chargeStationId)
    {
        return await _connectorService.GetByChargeStationId(chargeStationId);
    }

    public async Task<Connector> CreateConnector(CreateConnectorCommand command)
    {
        await _groupTotalAmpsValidator.ValidateForConnectorAddition(command.MaxCurrentInAmps, command.ChargeStationId);

        return await _connectorService.CreateConnector(command);
    }

    public async Task<Connector> UpdateConnector(UpdateConnectorCommand command)
    {
        await _groupTotalAmpsValidator.ValidateForConnectorUpdate(command.MaxCurrentInAmps, command.ChargeStationId, command.ConnectorNumber);

        return await _connectorService.UpdateConnector(command);
    }

    public async Task<int> GetTotalMaxCurrentInAmpsOfGroup(Guid groupId)
    {
        return await _connectorService.GetTotalMaxCurrentInAmpsOfGroup(groupId);
    }

    public async Task<int> GetTotalMaxCurrentInAmpsOfChargeStation(Guid chargeStationId)
    {
        return await _connectorService.GetTotalMaxCurrentInAmpsOfChargeStation(chargeStationId);
    }

    public async Task<Connector> GetConnector(Guid chargeStationId, int connectorNumber)
    {
        return await _connectorService.GetConnector(chargeStationId, connectorNumber);
    }
}