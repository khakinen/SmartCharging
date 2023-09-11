using MediatR;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Contract.Queries;

public class GroupQuery : IRequest<Group>
{
    public Guid Id { get; set; }
}