using MediatR;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Contract.Commands;

public class UpdateGroupCommand : GroupCommand, IRequest<Group>
{
    public Guid GroupId { get; set; }
}