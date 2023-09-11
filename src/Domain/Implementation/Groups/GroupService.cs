using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Implementation.Groups;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IChargeStationService _chargeStationService;

    public GroupService(IGroupRepository groupRepository, IChargeStationService chargeStationService)
    {
        _groupRepository = groupRepository;
        _chargeStationService = chargeStationService;
    }

    public async Task RemoveGroup(Guid groupId)
    {
        var chargeStations = await _chargeStationService.GetByGroupId(groupId);

        foreach (var chargeStation in chargeStations)
        {
            await _chargeStationService.RemoveChargeStation(chargeStation.Id);
        }

        await _groupRepository.Delete(groupId);
    }

    public async Task<Group> UpdateGroup(UpdateGroupCommand updateGroupCommand)
    {
        return await _groupRepository.Update(updateGroupCommand.GroupId, updateGroupCommand);
    }

    public async Task<Group> CreateGroup(CreateGroupCommand command)
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Capacity = command.Capacity
        };

        await _groupRepository.Create(group);

        return group;
    }

    public async Task<Group> GetGroup(Guid id)
    {
        return await _groupRepository.Read(id);
    }

    public async Task<ICollection<Group>> GetGroups()
    {
        return await _groupRepository.ReadAll();
    }
}