using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Groups;
using SmartCharging.Domain.Contract.Validations;

namespace SmartCharging.Domain.Implementation.Groups;

public class GroupServiceValidationDecorator : IGroupService
{
    private readonly IGroupService _groupService;
    private readonly IGroupTotalAmpsValidator _groupTotalAmpsValidator;

    public GroupServiceValidationDecorator(IGroupService groupService, IGroupTotalAmpsValidator groupTotalAmpsValidator)
    {
        _groupService = groupService;
        _groupTotalAmpsValidator = groupTotalAmpsValidator;
    }

    public async Task RemoveGroup(Guid groupId)
    {
         await  _groupService.RemoveGroup(groupId);
    }

    public async Task<Group> UpdateGroup(UpdateGroupCommand updateGroupCommand)
    {
        await _groupTotalAmpsValidator.ValidateForCapacityInAmpsUpdate(updateGroupCommand.GroupId, updateGroupCommand.Capacity);

        return await _groupService.UpdateGroup(updateGroupCommand);
    }

    public async Task<Group> CreateGroup(CreateGroupCommand createGroupCommand)
    {
        return await _groupService.CreateGroup(createGroupCommand);
    }

    public async Task<Group> GetGroup(Guid id)
    {
        return await _groupService.GetGroup(id);
    }
}