using MediatR;

namespace SmartCharging.Domain.Contract.Commands;

public class RemoveGroupCommand : IRequest
{
    public Guid Id { get; set; }
}