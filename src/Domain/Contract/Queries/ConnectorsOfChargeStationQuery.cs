using MediatR;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Domain.Contract.Queries;

public class ConnectorsOfChargeStationQuery : IRequest<ICollection<Connector>>
{
    public Guid ChargeStationId { get; set; }
}