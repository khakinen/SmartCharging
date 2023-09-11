using MediatR;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Contract.Commands;

public class CreateGroupCommand : GroupCommand, IRequest<Group>
{
}