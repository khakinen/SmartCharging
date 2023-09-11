using MediatR;

namespace SmartCharging.Domain.Contract.Commands;

public class RemoveConnectorCommand : IRequest
{
    public int  ConnectorNumber { get; set; }
    public Guid ChargeStationId { get; set; }
}