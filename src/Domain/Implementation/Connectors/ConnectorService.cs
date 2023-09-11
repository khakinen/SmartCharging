using Microsoft.Extensions.Logging;
using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Contract.Exceptions;
using SmartCharging.Domain.Contract.Settings;

namespace SmartCharging.Domain.Implementation.Connectors;

public class ConnectorService : IConnectorService
{
    private readonly IConnectorRepository _connectorRepository;
    private readonly IAppSettings _appSettings;
    private readonly ILogger<ConnectorService> _logger;

    public ConnectorService(IConnectorRepository connectorRepository, IAppSettings appSettings, ILogger<ConnectorService> logger)
    {
        _connectorRepository = connectorRepository;
        _appSettings = appSettings;
        _logger = logger;
    }

    public async Task RemoveConnector(Guid chargeStationId, int connectorId)
    {
        await _connectorRepository.Delete(chargeStationId, connectorId);
    }

    public async Task<ICollection<Connector>> GetByChargeStationId(Guid chargeStationId)
    {
        return await _connectorRepository.ReadByChargeStationId(chargeStationId);
    }

    public async Task<Connector> CreateConnector(CreateConnectorCommand createConnectorCommand)
    {
        var currentConnectorsOfChargeStation = await GetByChargeStationId(createConnectorCommand.ChargeStationId);

        var currentNumbers = currentConnectorsOfChargeStation.Select(c => c.ConnectorNumber).ToList();

        createConnectorCommand.ConnectorNumber = GetAvailableConnectorNumber(currentNumbers, createConnectorCommand.ChargeStationId);

        await _connectorRepository.CreateConnector(createConnectorCommand);

        return new Connector
        {
            ConnectorNumber = createConnectorCommand.ConnectorNumber,
            MaxCurrent = createConnectorCommand.MaxCurrent,
            ChargeStationId = createConnectorCommand.ChargeStationId
        };
    }

    private int GetAvailableConnectorNumber(ICollection<int> exisitingConnectorNumbers, Guid chargeStationId)
    {
        try
        {
            return Enumerable.Range(1, _appSettings.MaxConnectorCountOfChargeStation)
                .Where(e => !exisitingConnectorNumbers.Contains(e))
                .Min();
        }
        catch (Exception)
        {
            var message = $"There is no space for new connector for charge station:{chargeStationId}";

            _logger.LogWarning(message);

            throw new ConflictException(message);
        }
    }

    public async Task<Connector> UpdateConnector(UpdateConnectorCommand updateConnectorCommand)
    {
        return await _connectorRepository.UpdateConnector(updateConnectorCommand.ChargeStationId, updateConnectorCommand.ConnectorNumber,
            updateConnectorCommand);
    }

    public async Task<int> GetTotalMaxCurrentInAmpsOfGroup(Guid groupId)
    {
        return await _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(groupId);
    }

    public async Task<int> GetTotalMaxCurrentInAmpsOfChargeStation(Guid chargeStationId)
    {
        return await _connectorRepository.GetTotalMaxCurrentInAmpsOfChargeStation(chargeStationId);
    }

    public async Task<Connector> GetConnector(Guid chargeStationId, int connectorNumber)
    {
        return await _connectorRepository.Read(chargeStationId, connectorNumber);
    }
}