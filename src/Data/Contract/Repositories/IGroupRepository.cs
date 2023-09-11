using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Data.Contract.Repositories;

public interface IGroupRepository
{
    Task Create(Group group);
    Task<Group> Update(Guid id, UpdateGroupCommand group);
    Task Delete(Guid groupId);
    Task<Group> Read(Guid id);
}