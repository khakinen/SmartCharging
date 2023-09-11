using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Contract.Groups;

public interface IGroupService
{
     Task  RemoveGroup(Guid groupId);
     Task<Group>  UpdateGroup(UpdateGroupCommand updateGroupCommand);
     Task<Group> CreateGroup(CreateGroupCommand createGroupCommand);
     Task<Group> GetGroup(Guid id);
}