using MediatR;
using SmartCharging.Domain.Contract.ChargeStations;

namespace SmartCharging.Domain.Contract.Commands;

public class UpdateChargeStationCommand : ChargeStationCommand, IRequest<ChargeStation>
{
    public Guid ChargeStationId { get; set; }
}