using MediatR;
using SmartCharging.Domain.Contract.Groups;
using SmartCharging.Domain.Contract.Queries;

namespace SmartCharging.Domain.Implementation.QueryHandlers;

public class GroupQueryHandler : IRequestHandler<GroupQuery, Group>
{
    private readonly IGroupService _groupService;

    public GroupQueryHandler(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public async Task<Group> Handle(GroupQuery request, CancellationToken cancellationToken)
    {
        return await _groupService.GetGroup(request.Id);
    }
}