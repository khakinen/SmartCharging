using MediatR;
using SmartCharging.Domain.Contract.ChargeStations;

namespace SmartCharging.Domain.Contract.Queries;

public class ChargeStationQuery : IRequest<ChargeStation>
{
    public Guid Id { get; set; }
}