using MediatR;

namespace SmartCharging.Domain.Contract.Commands;

public class RemoveChargeStationCommand : IRequest
{
    public Guid Id { get; set; }
}