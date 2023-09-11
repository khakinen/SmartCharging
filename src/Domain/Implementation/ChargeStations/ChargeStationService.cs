using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Domain.Implementation.ChargeStations;

public class ChargeStationService: IChargeStationService
{
    private readonly IChargeStationRepository _chargeStationRepository;
    private readonly IConnectorService _connectorService;

    public ChargeStationService(IConnectorService connectorService, IChargeStationRepository chargeStationRepository)
    {
        _connectorService = connectorService;
        _chargeStationRepository = chargeStationRepository;
    }
    public async Task<ChargeStation> CreateChargeStation(CreateChargeStationCommand command)
    {
        var chargeStation = new ChargeStation
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            GroupId = command.GroupId
        };
        
        await _chargeStationRepository.Create(chargeStation);

        return chargeStation;
    }

    public async Task RemoveChargeStation(Guid chargeStationId)
    {
        var connectors = await _connectorService.GetByChargeStationId(chargeStationId);

        foreach (var connector in connectors)
        {
            await _connectorService.RemoveConnector(chargeStationId, connector.ConnectorNumber);
        }

        await _chargeStationRepository.Delete(chargeStationId);
    }

    public async Task<ChargeStation> UpdateChargeStation(UpdateChargeStationCommand command)
    {
        return await _chargeStationRepository.Update(command.ChargeStationId, command);
    }

    public async Task<ICollection<ChargeStation>> GetByGroupId(Guid groupId)
    {
        return await _chargeStationRepository.GetByGroupId(groupId);
    }

    public async Task<ChargeStation> GetChargeStation(Guid id)
    {
        return await _chargeStationRepository.Read(id);
    }
}