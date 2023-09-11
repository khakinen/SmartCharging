using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.Exceptions;
using SmartCharging.Domain.Contract.Validations;

namespace SmartCharging.Domain.Implementation.Validations;

public class GroupTotalAmpsValidator : IGroupTotalAmpsValidator
{
    private readonly IConnectorRepository _connectorRepository;
    private readonly IChargeStationRepository _chargeStationRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupTotalAmpsValidator(IConnectorRepository connectorRepository, IChargeStationRepository chargeStationRepository, IGroupRepository groupRepository)
    {
        _connectorRepository = connectorRepository;
        _chargeStationRepository = chargeStationRepository;
        _groupRepository = groupRepository;
    }

    public async Task ValidateForCapacityInAmpsUpdate(Guid groupId, int newCapacityInAmps)
    {
        var currentTotalOfGroup = await _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(groupId);
       
        if (newCapacityInAmps < currentTotalOfGroup)
        {
            throw new ValidationException($"Updated group capacity value:{newCapacityInAmps} can not be lower than actual total value:{currentTotalOfGroup}");
        }
    }

    public async Task ValidateForChargeStationAddition(Guid groupId, Guid addingChargeStationId)
    {
        var group = await _groupRepository.Read(groupId);
        
        var currentTotalOfGroup = await _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(groupId);

        var currentTotalOfAddingChargeStation = await _connectorRepository.GetTotalMaxCurrentInAmpsOfChargeStation(addingChargeStationId);
        
        if (group.Capacity < (currentTotalOfGroup + currentTotalOfAddingChargeStation))
        {
            throw new ValidationException($"group capacity can not be exceeded : {group.Capacity} < {currentTotalOfGroup} + {currentTotalOfAddingChargeStation}");
        }
    }

    public async Task ValidateForConnectorAddition(int addingMaxCurrentInAmps, Guid chargeStationId)
    {
        var chargeStation = await _chargeStationRepository.Read(chargeStationId); 
        var group = await _groupRepository.Read(chargeStation.GroupId);
        
        var currentTotalOfGroup = await _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(chargeStation.GroupId);

        if (group.Capacity < (currentTotalOfGroup + addingMaxCurrentInAmps))
        {
            throw new ValidationException($"group capacity can not be exceeded : {group.Capacity} < {currentTotalOfGroup} + {addingMaxCurrentInAmps}");
        }
    }

    public async Task ValidateForConnectorUpdate(int newMaxCurrentInAmps, Guid chargeStationId, int connectorNumber)
    {
        var chargeStation = await _chargeStationRepository.Read(chargeStationId); 
        var group = await _groupRepository.Read(chargeStation.GroupId);
        var connector = await _connectorRepository.Read(chargeStationId, connectorNumber);
        
        var currentTotalOfGroup = await _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(chargeStation.GroupId);

        if (group.Capacity < (currentTotalOfGroup - connector.MaxCurrent + newMaxCurrentInAmps))
        {
            throw new ValidationException($"group capacity can not be exceeded : {group.Capacity} < {currentTotalOfGroup} - {connector.MaxCurrent} + {newMaxCurrentInAmps}");
        }
    }
}