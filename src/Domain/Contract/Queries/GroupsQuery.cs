using MediatR;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Contract.Queries;

public class GroupsQuery : IRequest<ICollection<Group>>
{
}