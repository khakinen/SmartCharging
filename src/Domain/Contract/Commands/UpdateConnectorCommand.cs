using MediatR;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Domain.Contract.Commands;

public class UpdateConnectorCommand : ConnectorCommand, IRequest<Connector>
{
    public int  ConnectorNumber { get; set; }
    public Guid ChargeStationId { get; set; }
}