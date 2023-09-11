using MediatR;
using SmartCharging.Domain.Contract.Groups;
using SmartCharging.Domain.Contract.Queries;

namespace SmartCharging.Domain.Implementation.QueryHandlers;

public class GroupsQueryHandler : IRequestHandler<GroupsQuery, ICollection<Group>>
{
    private readonly IGroupService _groupService;

    public GroupsQueryHandler(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public async Task<ICollection<Group>> Handle(GroupsQuery request, CancellationToken cancellationToken)
    {
        return await _groupService.GetGroups();
    }
}